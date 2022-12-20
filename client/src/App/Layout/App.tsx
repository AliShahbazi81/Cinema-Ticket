import {
  Container,
  createTheme,
  CssBaseline,
  ThemeProvider,
} from "@mui/material";
import { useEffect, useState } from "react";
import { Route, Switch } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import AboutPage from "../../Features/about/AboutPage";
import Catalog from "../../Features/catalog/Catalog";
import ProductDetails from "../../Features/catalog/ProductDetail";
import ContactPage from "../../Features/contact/ContactPage";
import HomePage from "../../Features/home/HomePage";
import Header from "./Header";
import "react-toastify/dist/ReactToastify.css";
import NotFound from "../errors/NotFound";
import BasketPage from "../../Features/basket/BasketPage";
import LoadingComponent from "./LoadingComponent";
import agent from "../api/agent";
import { getCookie } from "../util/util";
import CheckoutPage from "../../Features/checkout/CheckoutPage";
import { useAppDispatch } from "../store/configureStore";
import { setBasket } from "../../Features/basket/basketSlice";

function App() {
  // Getting the basket from the context
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);
  // Check if the buyerId cookie is set in the browser
  useEffect(() => {
    const buyerId = getCookie("buyerId");
    if (buyerId) {
      agent.Basket.get()
        .then((basket) => {
          dispatch(setBasket(basket));
          setLoading(false);
        })
        .catch((error) => console.log(error));
    } else setLoading(false);
  }, [dispatch]);

  const [darkMode, setDarkMode] = useState(true);
  const paletteType = darkMode ? "dark" : "light";
  const theme = createTheme({
    palette: {
      mode: paletteType,
      background: {
        default: paletteType === "light" ? "#eaeaea" : "#121212",
      },
    },
  });

  function handleThemeChange() {
    setDarkMode(!darkMode);
  }

  if (loading) <LoadingComponent message="Loading App..." />;

  return (
    <ThemeProvider theme={theme}>
      <ToastContainer theme="colored" hideProgressBar position="bottom-right" />
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
      <Container>
        <Switch>
          <Route exact path={"/"} component={HomePage} />
          <Route exact path={"/catalog/:id"} component={ProductDetails} />
          <Route exact path={"/about"} component={AboutPage} />
          <Route exact path={"/contact"} component={ContactPage} />
          <Route exact path={"/catalog"} component={Catalog} />
          <Route exact path={"/basket"} component={BasketPage} />
          <Route exact path={"/checkout"} component={CheckoutPage} />
          <Route component={NotFound} />
        </Switch>
      </Container>
    </ThemeProvider>
  );
}
export default App;
