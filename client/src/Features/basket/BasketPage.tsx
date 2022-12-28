import { Button, Grid, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { useAppSelector } from "../../App/store/configureStore";
import BasketSummary from "./BasketSummary";
import BasketTable from "./BasketTable";

export default function BasketPage() {
  // And when we want to get the state, we need to use the useSelector function
  const { basket } = useAppSelector((state) => state.basket);

  if (!basket)
    return <Typography variant="h3">Your basket is empty !</Typography>;

  return (
    <>
      <BasketTable items={basket.items} />
      <Grid container display="flex" flexDirection="row-reverse" mt={3}>
        <Grid item xs={6}>
          <BasketSummary />
          <Button
            size="medium"
            fullWidth
            variant="contained"
            color="primary"
            component={Link}
            to="/checkout"
          >
            Checkout
          </Button>
        </Grid>
      </Grid>
    </>
  );
}
