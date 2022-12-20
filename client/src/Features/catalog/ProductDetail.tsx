import { LoadingButton } from "@mui/lab";
import {
  Divider,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import agent from "../../App/api/agent";
import NotFound from "../../App/errors/NotFound";
import LoadingComponent from "../../App/Layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../App/store/configureStore";
import {
  addBasketItemAsync,
  removeItemBasketAsync,
} from "../basket/basketSlice";
import { fetchProductAsync, productSelectors } from "./catalogSlice";

export default function ProductDetails() {
  const { id } = useParams<{ id: string }>();
  // Get the basket from the store context
  const { basket, status } = useAppSelector((state) => state.basket);
  const dispatch = useAppDispatch();
  const product = useAppSelector((state) =>
    productSelectors.selectById(state, parseInt(id))
  );
  const { status: productStatus } = useAppSelector((state) => state.catalog);
  // Quantity of the product to either add or update from the basket
  const [quantity, setQuantity] = useState(0);

  // Find the product in the basket if it exists
  const item = basket?.items.find((x) => x.productId === product?.id);
  useEffect(() => {
    // If the product exists in the basket, set the quantity to the quantity in the basket
    if (item) setQuantity(item.quantity);
    if (!product) dispatch(fetchProductAsync(parseInt(id)));
  }, [id, item, dispatch, product]);

  // Handle the input change for the quantity
  function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
    // Ensure the value is a number
    const value = parseInt(event.target.value);
    // Ensure the value is greater than 1 because we don't want to add 0 or less to the basket
    if (value >= 1) setQuantity(value);
  }

  function handleRemoveItem() {
    // Remove the item from the basket
    agent.Basket.removeItem(product!.id, item!.quantity)
      .then(() =>
        dispatch(
          removeItemBasketAsync({
            productId: product?.id!,
            quantity: item!.quantity,
          })
        )
      )
      .catch((error) => toast.error(error.data));
  }

  function handleUpdateCart() {
    // If the item does not exist in the basket, add it || If the quantity is greater than the quantity in the basket, add the difference
    if (!item || quantity > item.quantity) {
      const updatedQuantity = item ? quantity - item.quantity : quantity;
      dispatch(
        addBasketItemAsync({
          productId: product?.id!,
          quantity: updatedQuantity,
        })
      );
    } else {
      // If the item exists in the basket, update it
      const updatedQuantity = item.quantity - quantity;
      dispatch(
        removeItemBasketAsync({
          productId: product?.id!,
          quantity: updatedQuantity,
        })
      );
    }
  }

  if (productStatus.includes("pending"))
    return <LoadingComponent message="Loading Product..." />;
  if (!product) return <NotFound />;
  return (
    <Grid container spacing={6}>
      <Grid item xs={6}>
        <img
          src={product.pictureURL}
          alt={product.name}
          style={{ width: "100%" }}
        />
      </Grid>
      <Grid item xs={6}>
        <Typography variant="h4">{product.name}</Typography>
        <Divider sx={{ mb: 2 }} />
        <Typography variant="h5">
          ${(product.price / 100).toFixed(2)}
        </Typography>
        <TableContainer>
          <Table>
            <TableBody>
              <TableRow>
                <TableCell>Name</TableCell>
                <TableCell>{product.name}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Description</TableCell>
                <TableCell>{product.description}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Type</TableCell>
                <TableCell>{product.type}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Quantity in Stock</TableCell>
                <TableCell>{product.quantityInStock}</TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
        <Grid container spacing={2} marginTop={5}>
          <Grid item xs={6}>
            <TextField
              variant="outlined"
              label="Quantity"
              type="number"
              value={quantity}
              fullWidth
              onChange={handleInputChange}
            />
          </Grid>
          <Grid item xs={6} display="flex" alignItems="center">
            <LoadingButton
              disabled={quantity === item?.quantity || quantity === 0}
              loading={status.includes("pending" + product.id)}
              sx={{ height: "40px" }}
              color="primary"
              size="medium"
              variant="contained"
              onClick={handleUpdateCart}
            >
              {item ? "Update" : "Add"}
            </LoadingButton>

            {/* Remove Icon  */}
            <LoadingButton
              onClick={handleRemoveItem}
              color="error"
              sx={{ height: "40px", ml: 2 }}
              size="medium"
              variant="contained"
              loading={status.includes("pending" + product.id + "remove")}
            >
              Remove
            </LoadingButton>
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
