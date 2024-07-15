import React from "react";
import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import { useLocation } from "react-router-dom";
import { Link } from "react-router-dom";

export default function ServerError() {
    const { state } = useLocation();

    const errorTitle = state?.error?.title || "Server Error";
    const errorDetail = state?.error?.detail || "Internal server error";

    return (
        <Container component={Paper}>
            <Typography variant="h3" color="error" gutterBottom>{errorTitle}</Typography>
            <Divider />
            <Typography>{errorDetail}</Typography>
            <Button component={Link} to={'/catalog'} fullWidth>Go back to the store</Button>
        </Container>
    );
}