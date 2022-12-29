import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import { useEffect, useState } from "react";
import agent from "../../App/api/agent";
import LoadingComponent from "../../App/Layout/LoadingComponent";
import { useAppDispatch } from "../../App/store/configureStore";
import { setBasket } from "../basket/basketSlice";
import CheckoutPage from "./CheckoutPage";

const stripePromise = loadStripe(
  "pk_test_51MK3oFG5rTuyaYsKlAJJn002vu1Xf0CoYdUE1WcGP8knfl1tdCgeuSB4m4L0GALqZZXaOdpVNmbCyP8cuFEZJjek00FCROR6cu"
);

export default function CheckOutWrapper() {
  const [loading, setLoading] = useState(true);
  const dispatch = useAppDispatch();

  useEffect(() => {
    agent.Payments.createPaymentIntent()
      .then((basket) => setBasket(basket))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }, [dispatch]);

  if (loading) return <LoadingComponent message="Loading checkout..." />;

  return (
    <Elements stripe={stripePromise}>
      <CheckoutPage />
    </Elements>
  );
}
