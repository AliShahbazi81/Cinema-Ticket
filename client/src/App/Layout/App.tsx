import {
  Container,
  createTheme,
  CssBaseline,
  ThemeProvider,
} from "@mui/material";
import { useCallback, useEffect, useState } from "react";
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
import { useAppDispatch } from "../store/configureStore";
import { fetchBasketAsync } from "../../Features/basket/basketSlice";
import Login from "../../Features/account/Login";
import Register from "../../Features/account/Register";
import { fetchCurrentUser } from "../../Features/account/accountSlice";
import { PrivateRoute } from "./PrivateRoute";
import Orders from "../../Features/orders/Orders";
import CheckOutWrapper from "../../Features/checkout/CheckoutWrapper";

function App() {
  // Getting the basket from the context
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  // useCallback is used to prevent the function from being recreated on every render
  const initApp = useCallback(async () => {
    try {
      await dispatch(fetchCurrentUser());
      await dispatch(fetchBasketAsync());
    } catch (error) {
      console.log(error);
    }
  }, [dispatch]);

  // Check if the buyerId cookie is set in the browser
  useEffect(() => {
    initApp().then(() => setLoading(false));
  }, [initApp]);

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
      <Route exact path={"/"} component={HomePage} />
      <Route
        path={"/(.+)"}
        render={() => (
          <Container sx={{ mt: 4 }}>
            <Switch>
              <Route exact path={"/catalog/:id"} component={ProductDetails} />
              <Route exact path={"/about"} component={AboutPage} />
              <Route exact path={"/contact"} component={ContactPage} />
              <Route exact path={"/catalog"} component={Catalog} />
              <Route exact path={"/basket"} component={BasketPage} />
              <PrivateRoute
                exact
                path={"/checkout"}
                component={CheckOutWrapper}
              />
              <PrivateRoute exact path={"/orders"} component={Orders} />
              <Route exact path={"/login"} component={Login} />
              <Route exact path={"/register"} component={Register} />
              <Route component={NotFound} />
            </Switch>
          </Container>
        )}
      />
    </ThemeProvider>
  );
}
export default App;
