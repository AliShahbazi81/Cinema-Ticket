import { Add, DeleteSharp, Remove } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import {
  Box,
  Button,
  Grid,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom";
import agent from "../../App/api/agent";
import { useStoreContext } from "../../App/context/StoreContext";
import { currencyFormat } from "../../App/util/util";
import CheckoutPage from "../checkout/CheckoutPage";
import BasketSummary from "./BasketSummary";

export default function BasketPage() {
  // initial state of the loading variable is false
  const [status, setStatus] = useState({
    loading: false,
    name: "",
  });
  // Since we do have a basket in the context, we can destructure it
  // Remember that we are using the useStoreContext() hook and we can use just a parameter from the context
  const { basket, setBasket, removeItem } = useStoreContext();

  function handleAddItem(productId: number, name: string) {
    setStatus({ loading: true, name });
    agent.Basket.addItem(productId)
      .then((basket) => setBasket(basket))
      .catch((error) => console.log(error))
      .finally(() => setStatus({ loading: false, name: "" }));
  }

  // This function will handle the update of the quantity of the product in the basket
  function handleUpdateItem(productId: number, quantity = 1, name: string) {
    setStatus({ loading: true, name });
    agent.Basket.removeItem(productId, quantity)
      .then(() => removeItem(productId, quantity))
      .catch((error) => console.log(error))
      .finally(() => setStatus({ loading: false, name: "" }));
  }

  if (!basket)
    return <Typography variant="h3">Your basket is empty !</Typography>;

  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }}>
          <TableHead>
            <TableRow>
              <TableCell>Product</TableCell>
              <TableCell align="right">Price</TableCell>
              <TableCell align="center">Quantity</TableCell>
              <TableCell align="right">Subtotal</TableCell>
              <TableCell align="right"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {basket.items.map((item) => (
              <TableRow
                key={item.productId}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                {/* Name and Picture  */}
                <TableCell component="th" scope="row">
                  <Box display="flex" alignItems="center">
                    <img
                      src={item.pictureUrl}
                      alt={item.name}
                      style={{ height: 50, marginRight: 20 }}
                    />
                    <Link
                      to={`/catalog/${item.productId}`}
                      style={{
                        display: "inline-block",
                        color: "white",
                        textDecoration: "none",
                      }}
                    >
                      <span>{item.name}</span>
                    </Link>
                  </Box>
                </TableCell>

                {/* Price  */}
                <TableCell align="right">
                  {currencyFormat(item.price)}
                </TableCell>

                {/* Quantity  */}
                <TableCell align="center">
                  <LoadingButton
                    loading={
                      status.loading && status.name === "rem" + item.name
                    }
                    onClick={() =>
                      handleUpdateItem(item.productId, 1, "rem" + item.name)
                    }
                  >
                    <Remove color="error" />
                  </LoadingButton>
                  {item.quantity}
                  <LoadingButton
                    loading={
                      status.loading && status.name === "add" + item.name
                    }
                    onClick={() =>
                      handleAddItem(item.productId, "add" + item.name)
                    }
                  >
                    <Add color="primary" />
                  </LoadingButton>
                </TableCell>

                {/* Subtotal */}
                <TableCell align="right">
                  {currencyFormat(item.price * item.quantity)}
                </TableCell>
                <TableCell align="right">
                  {/* Remove Icon  */}
                  <LoadingButton
                    loading={
                      status.loading && status.name === "del" + item.name
                    }
                    onClick={() =>
                      handleUpdateItem(
                        item.productId,
                        item.quantity,
                        "del" + item.name
                      )
                    }
                    color="error"
                  >
                    <DeleteSharp />
                  </LoadingButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
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
