import { Container, CssBaseline, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import Catalog from "../../Features/catalog/Catalog";
import { Product } from "../Models/product";
import Header from "./Header";
function App() {
  //It says that our useState is storing an array which is Product and also Product is array as well.
  //Ensure that bost useState and Product (Interface) are declared as array.
  const [products, setProduct] = useState<Product[]>([]);

  useEffect(() => {
    // Used for sending API Request to our back-end
    fetch("https://localhost:7159/Product/GetAllProducts", {
      // we are adding some more information and sending with our request to the back-end
      headers: {
        method: "GET", // Specifically saying that the request is using GET method
        Accept: "application/json", // Accepting only Json
      },
    }) // URL of our back-end
      .then((response) => response.json()) // Change the response of our request which is as an object to json
      .then((data) => setProduct(data)); // Set returned values from back-end to our const products through setProducts function
  }, []); // We should make sure to put [] at the end, otherwise a request will be sent to our back-end in every ms

  function addProduct() {
    // Declare a function named addProdcts in order to add element to const products
    setProduct((prevState) => [
      // Explanation of setProducts in which we declared it in const products.
      ...prevState, // Inform the platform to keep all of the elements that were added before
      {
        id: prevState.length + 1, // Indicate the id for the elements
        name: "product " + (prevState.length + 1), // Indicate the name for the elements
        price: prevState.length * 100 + 100, // Indicate the price for the elements
        description: "some description",
        pictureURL: "https://picsum.photos/200/300",
      },
    ]);
  }

  return (
    <>
      <CssBaseline />
      <Header />
      {/* Container is being used because when we use CssBaseLine, it actually removes margin and padding
      Adding Container tag will allow the platform to consider some space for the list items */}
      <Container>
        <Catalog products={products} addProduct={addProduct} />
      </Container>
    </>
  );
}

export default App;
