import { Grid, List } from "@mui/material";
import { Product } from "../../App/Models/product";
import { useAppSelector } from "../../App/store/configureStore";
import ProductCard from "./ProductCard";
import ProductCardSkeleton from "./ProductCardSkeleton";

interface Props {
  products: Product[];
}

export default function ProductList({ products }: Props) {
  const {productsLoaded} = useAppSelector(state => state.catalog);
  return (
    <Grid container spacing={4}>
      {products.map((product) => (
        <Grid key={product.id} item xs={4}>
          {!productsLoaded ? (
            <ProductCardSkeleton />
          ): <ProductCard product={product} />
          }
          
        </Grid>
      ))}
    </Grid>
  );
}
