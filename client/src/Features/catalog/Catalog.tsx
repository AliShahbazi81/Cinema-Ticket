import { useEffect } from "react";
import LoadingComponent from "../../App/Layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../App/store/configureStore";
import { fetchProductsAsync, productSelectors } from "./catalogSlice";
import ProductList from "./ProductList";

export default function Catalog() {
  const { productsLoaded, status } = useAppSelector((state) => state.catalog);
  const dispatch = useAppDispatch();
  const products = useAppSelector(productSelectors.selectAll);

  useEffect(() => {
    if (!productsLoaded) dispatch(fetchProductsAsync());
  }, [productsLoaded, dispatch]);

  if (status.includes("pending"))
    return <LoadingComponent message="Loading Products..." />;

  return (
    <>
      <ProductList products={products} />
    </>
  );
}
