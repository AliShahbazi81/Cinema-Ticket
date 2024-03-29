import {
  Box,
  Button,
  Paper,
  Step,
  StepLabel,
  Stepper,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { FieldValues, FormProvider, useForm } from "react-hook-form";
import AddressForm from "./AddressForm";
import PaymentForm from "./PaymentForm";
import Review from "./Review";
import { yupResolver } from "@hookform/resolvers/yup";
import { validationSchema } from "./checkoutValidation";
import agent from "../../App/api/agent";
import { useAppDispatch, useAppSelector } from "../../App/store/configureStore";
import { clearBasket } from "../basket/basketSlice";
import { LoadingButton } from "@mui/lab";
import { StripeElementType } from "@stripe/stripe-js";
import {
  CardNumberElement,
  useElements,
  useStripe,
} from "@stripe/react-stripe-js";

const steps = ["Shipping address", "Review your order", "Payment details"];

export default function CheckoutPage() {
  const [activeStep, setActiveStep] = useState(0);
  const [orderNumber, setOrderNumber] = useState(0);
  const [loading, setLoading] = useState(false);
  const dispatch = useAppDispatch();
  const [paymentMessage, setPaymentMessage] = useState("");
  const [paymentSucceeded, setPaymentSucceeded] = useState(false);
  // It stores our client secret and we need to send it to stripe to confirm the payment
  const { basket } = useAppSelector((state) => state.basket);
  // It allows us to create actual payment
  const stripe = useStripe();
  // Giving the card details
  const elements = useElements();

  // These two properties will be used for validating the card details in Stripe
  const [cardState, setCardState] = useState<{
    elementError: { [key in StripeElementType]?: string };
  }>({ elementError: {} });
  const [cardComplete, setCardComplete] = useState<any>({
    cardNumber: false,
    cardExpiry: false,
    cardCvc: false,
  });

  function onCardInputChange(event: any) {
    // This will update the card state with any errors and if the card is complete
    setCardState({
      ...cardState,
      elementError: {
        // This will spread the existing errors and add the new error
        ...cardState.elementError,
        // Stripe error
        [event.elementType]: event.error?.message,
      },
    });
    // If there was no error, then the card is complete
    setCardComplete({ ...cardComplete, [event.elementType]: event.complete });
  }

  function getStepContent(step: number) {
    switch (step) {
      case 0:
        return <AddressForm />;
      case 1:
        return <Review />;
      case 2:
        return (
          <PaymentForm
            cardState={cardState}
            onCardInputChange={onCardInputChange}
          />
        );
      default:
        throw new Error("Unknown step");
    }
  }

  const validationIndex = validationSchema[activeStep];
  // methods => for using in FormProvider
  const methods = useForm({
    mode: "all",
    resolver: yupResolver(validationIndex),
  });

  useEffect(() => {
    agent.Account.fetchAddress().then((response) => {
      if (response) {
        methods.reset({
          ...methods.getValues(),
          ...response,
          saveAddress: false,
        });
      }
    });
  }, [methods]);

  async function submitOrder(data: FieldValues) {
    setLoading(true);
    const { nameOnCard, saveAddress, ...shippingAddress } = data;
    if (!elements || !stripe) return; // => if there is no stripe or elements, then return because stripe is not ready yet
    try {
      const cardElement = elements.getElement(CardNumberElement);
      const paymentResult = await stripe.confirmCardPayment(
        basket?.clientSecret!,
        {
          payment_method: {
            card: cardElement!,
            billing_details: {
              name: nameOnCard,
            },
          },
        }
      );
      console.log(paymentResult);
      if (paymentResult.paymentIntent?.status === "succeeded") {
        // Create order using nameOnCard and shippingAddress
        const orderNumber = await agent.Orders.create({
          nameOnCard,
          shippingAddress,
        });
        // Save orderNumber in the state
        setOrderNumber(orderNumber);
        setPaymentSucceeded(true);
        setPaymentMessage("We have received your payment!");
        setActiveStep(activeStep + 1);
        // Delete the basket after creating the order
        //! Remember that we will also delete the basket after creating the basket in the backend
        dispatch(clearBasket());
        setLoading(false);
      } else {
        setPaymentMessage(paymentResult.error?.message!);
        setPaymentSucceeded(false);
        setLoading(false);
        setActiveStep(activeStep + 1);
      }
    } catch (error) {
      console.log(error);
      setLoading(false);
    }
  }

  const handleNext = async (data: FieldValues) => {
    if (activeStep == steps.length - 1) await submitOrder(data);
    else setActiveStep(activeStep + 1);
  };

  const handleBack = () => {
    setActiveStep(activeStep - 1);
  };

  function submitDisabled(): boolean {
    if (activeStep === steps.length - 1)
      return (
        !cardComplete.cardNumber ||
        !cardComplete.cardExpiry ||
        !cardComplete.cardCvc ||
        !methods.formState.isValid
      );
    else return !methods.formState.isValid;
  }

  //! FormProvider => prevent the losing data after refresh the page, go to the next step and go to the previous step
  return (
    <FormProvider {...methods}>
      <Paper
        variant="outlined"
        sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}
      >
        <Typography component="h1" variant="h4" align="center">
          Checkout
        </Typography>
        <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>
        <Paper>
          {activeStep === steps.length ? (
            <>
              <Typography variant="h5" gutterBottom>
                {paymentMessage}
              </Typography>
              {paymentSucceeded ? (
                <Typography variant="subtitle1">
                  Your order number is #{orderNumber}. We have emailed your
                  order confirmation, and will send you an update when your
                  order has shipped.
                </Typography>
              ) : (
                <Button variant="contained" onClick={handleBack}>
                  Back
                </Button>
              )}
            </>
          ) : (
            <form onSubmit={methods.handleSubmit(handleNext)}>
              {getStepContent(activeStep)}
              <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
                {activeStep !== 0 && (
                  <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
                    Back
                  </Button>
                )}
                <LoadingButton
                  loading={loading}
                  variant="contained"
                  type="submit"
                  sx={{ mt: 3, ml: 1 }}
                  disabled={submitDisabled()}
                >
                  {activeStep === steps.length - 1 ? "Place order" : "Next"}
                </LoadingButton>
              </Box>
            </form>
          )}
        </Paper>
      </Paper>
    </FormProvider>
  );
}
