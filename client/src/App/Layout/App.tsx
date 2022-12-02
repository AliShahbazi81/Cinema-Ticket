import {
  Container,
  createTheme,
  CssBaseline,
  ThemeProvider,
} from "@mui/material";
import { palette } from "@mui/system";
import { useState } from "react";
import { Route } from "react-router-dom";
import AboutPage from "../../Features/about/AboutPage";
import Catalog from "../../Features/catalog/Catalog";
import ProductDetails from "../../Features/catalog/ProductDetail";
import ProductDetailPage from "../../Features/catalog/ProductDetail";
import ContactPage from "../../Features/contact/ContactPage";
import HomePage from "../../Features/home/HomePage";
import Header from "./Header";

function App() {
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

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
      <Container>
        <Route exact path={"/"} component={HomePage} />
        <Route exact path={"/about"} component={AboutPage} />
        <Route exact path={"/contact"} component={ContactPage} />
        <Route exact path={"/catalog"} component={Catalog} />
        <Route exact path={"/catalog/:id"} component={ProductDetails} />
      </Container>
    </ThemeProvider>
  );
}
export default App;
