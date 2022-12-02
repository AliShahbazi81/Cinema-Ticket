import { useEffect, useState } from "react";
import { Product } from "../../App/Models/product";
import ProductList from "./ProductList";

export default function Catalog() {
  const [products, setProduct] = useState<Product[]>([]);

  useEffect(() => {
    // Used for sending API Request to our back-end
    fetch("https://localhost:7159/Product/GetAllProducts", {
      headers: {
        method: "GET",
        Accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => setProduct(data));
  }, []);

  return (
    <>
      <ProductList products={products} />
    </>
  );
}
