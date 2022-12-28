import Typography from "@mui/material/Typography";
import BasketTable from "../basket/BasketTable";
import { useAppSelector } from "../../App/store/configureStore";
import { Grid } from "@mui/material";
import BasketSummary from "../basket/BasketSummary";

export default function Review() {
  const { basket } = useAppSelector((state) => state.basket);
  return (
    <>
      <Typography variant="h6" gutterBottom>
        Order summary
      </Typography>
      {basket && <BasketTable items={basket.items} isBasket={false} />}
      <Grid container display="flex" flexDirection="row-reverse" mt={3}>
        <Grid item xs={6}>
          <BasketSummary />
        </Grid>
      </Grid>
    </>
  );
}
