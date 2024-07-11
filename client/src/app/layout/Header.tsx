import { AppBar, FormControlLabel, FormGroup, Switch, Toolbar, Typography } from "@mui/material";

interface Props {
    darkMode: boolean;
    handleThemeChange: () => void;
}

export default function Header({darkMode, handleThemeChange} : Props){
    return(
        <AppBar position="static" sx={{mb: 4}}>
            <Toolbar>
                <Typography variant="h5">
                    Re-Store
                </Typography>
                <Switch checked={darkMode} onChange={handleThemeChange} color="warning"/>
            </Toolbar>
        </AppBar>
    )
}