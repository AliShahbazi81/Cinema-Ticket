import { useEffect, useState } from "react";
import agent from "../../App/api/agent";
import LoadingComponent from "../../App/Layout/LoadingComponent";
import { Product } from "../../App/Models/product";
import ProductList from "./ProductList";

export default function Catalog() {
  const [products, setProduct] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Used for sending API Request to our back-end
    agent.Catalog.list()
      .then((products) => setProduct(products))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <LoadingComponent message="Loading Products..." />;

  return (
    <>
      <ProductList products={products} />
    </>
  );
}
