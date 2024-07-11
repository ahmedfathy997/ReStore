import React, { useState } from "react";
import Catalog from "../../features/catalog/Catalog";
import Header from "./Header";
import { Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";

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
    <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
        <Catalog />
      </Container>
    </ThemeProvider>
  );
}

export default App;