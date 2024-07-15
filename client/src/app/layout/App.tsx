import React, { useState } from "react";
import Catalog from "../../features/catalog/Catalog";
import Header from "./Header";
import { Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { Route, Routes } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ProductDetails from "../../features/catalog/ProductDetails";
import AboutPage from "../../features/about/AboutPage";
import ContactPage from "../../features/contact/ContactPage";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {

const [darkMode, setDarkMode] = useState(false);
const paletteType = darkMode ? 'dark' : 'light';
const theme = createTheme({
  palette: {
    mode : paletteType,
  }
})

function handleThemeChange(){
  setDarkMode(!darkMode);
}
  return (
    <ThemeProvider theme={theme}>
      <ToastContainer position="bottom-right" hideProgressBar/>
    <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
        <Routes>
          <Route path="/" Component={HomePage} />
          <Route path="/catalog" Component={Catalog} />
          <Route path="/catalog/:id" Component={ProductDetails} />
          <Route path="/about" Component={AboutPage} />
          <Route path="/contact" Component={ContactPage} />
          <Route path="/server-error" Component={ServerError} />
          <Route Component={NotFound} />
        </Routes>
      </Container>
    </ThemeProvider>
  );
}

export default App;