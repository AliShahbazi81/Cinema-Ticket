import { Box, Button, Grid, Typography } from "@mui/material";
import { BasketItem } from "../../App/Models/basket";
import { Order } from "../../App/Models/order";
import BasketSummary from "../basket/BasketSummary";
import BasketTable from "../basket/BasketTable";

interface Props {
  order: Order;
  setSelectedOrder: (id: number) => void;
}

export default function OrderDetailed({ order, setSelectedOrder }: Props) {
  const subtotal = order.orderItems.reduce(
    (sum, item) => sum + item.price * item.quantity,
    0 ?? 0
  );
  return (
    <>
      <Box>
        <Typography sx={{ p: 2 }} gutterBottom variant="h4">
          Order# {order.id} - {order.orderStatus}
        </Typography>
        <Button
          onClick={() => setSelectedOrder(0)}
          sx={{ m: 2 }}
          size="large"
          variant="contained"
        >
          Back to Orders
        </Button>
      </Box>
      <BasketTable items={order.orderItems as BasketItem[]} isBasket={false} />
      <Grid container>
        <Grid item xs={6} />
        <Grid item xs={6}>
          <BasketSummary subtotal={subtotal} />
        </Grid>
      </Grid>
    </>
  );
}
