import { Button } from "@mui/material";
import { Product } from "../../App/Models/product";
import ProductList from "./ProductList";

// We are simulating the information that we being sent from App.tsx
// In this case, we had an array which was our product
// and a function which was responsible for adding the information to our product array
interface Props {
  products: Product[];
  addProduct: () => void;
}
//The created simulation for products and addProduct will be used instead of props.
// In this case, we are no longer need to put "props." at the beginning of every objects in the
// function.
export default function Catalog({ products, addProduct }: Props) {
  return (
    <>
      <ProductList products={products} />
      <Button variant="outlined" onClick={addProduct}>
        Add Product
      </Button>
    </>
  );
}
