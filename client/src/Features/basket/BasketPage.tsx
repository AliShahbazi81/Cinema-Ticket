import { Typography } from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../App/api/agent";
import LoadingComponent from "../../App/Layout/LoadingComponent";
import { Basket } from "../../App/Models/basket";

export default function BasketPage() {
  const [loading, setLoading] = useState(true);
  const [basket, setBasket] = useState<Basket | null>(null);

  useEffect(() => {
    agent.Basket.get()
      .then((basket) => setBasket(basket))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <LoadingComponent message="Loading Basket..." />;
  if (!basket)
    return <Typography variant="h3">Your basket is empty !</Typography>;

  return <h1>Buyer ID: {basket.userId}</h1>;
}
