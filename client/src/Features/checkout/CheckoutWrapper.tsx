import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import CheckoutPage from "./CheckoutPage";

export default function CheckOutWrapper() {
  const stripePromise = loadStripe(
    "pk_test_51MK3oFG5rTuyaYsKlAJJn002vu1Xf0CoYdUE1WcGP8knfl1tdCgeuSB4m4L0GALqZZXaOdpVNmbCyP8cuFEZJjek00FCROR6cu"
  );
  return (
    <Elements stripe={stripePromise}>
      <CheckoutPage />
    </Elements>
  );
}
