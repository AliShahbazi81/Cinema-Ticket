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
import { Link } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../App/store/configureStore";
import { currencyFormat } from "../../App/util/util";
import { addBasketItemAsync, removeItemBasketAsync } from "./basketSlice";
import BasketSummary from "./BasketSummary";

export default function BasketPage() {
  // When we want to do actions, we need to use the dispatch function
  const dispatch = useAppDispatch();
  // And when we want to get the state, we need to use the useSelector function
  const { basket, status } = useAppSelector((state) => state.basket);

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
                  {/* ?// -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ DECREASE BUTTON -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ */}
                  <LoadingButton
                    loading={
                      status === "pendingRemoveItem" + item.productId + "del"
                    }
                    onClick={() =>
                      dispatch(
                        removeItemBasketAsync({
                          productId: item.productId!,
                          quantity: 1,
                          name: "del",
                        })
                      )
                    }
                  >
                    <Remove color="error" />
                  </LoadingButton>
                  {item.quantity}

                  {/* ?// -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ ADD BUTTON -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ */}
                  <LoadingButton
                    loading={status === "pendingAddItem" + item.productId}
                    onClick={() =>
                      dispatch(
                        addBasketItemAsync({
                          productId: item.productId,
                        })
                      )
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
                  {/* ?// -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ SPLICE BUTTON -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ */}
                  <LoadingButton
                    loading={
                      status === "pendingRemoveItem" + item.productId + "rem"
                    }
                    onClick={() =>
                      dispatch(
                        removeItemBasketAsync({
                          productId: item.productId,
                          quantity: item.quantity,
                          name: "rem",
                        })
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
