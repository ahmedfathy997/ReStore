import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import React from "react";
import { Link } from 'react-router-dom';

export default function NotFound() {
    return (
        <Container component={Paper}>
            <Typography gutterBottom variant="h3">
                Oops - we could not find what you were looking for!
            </Typography>
            <Divider />
            <Button component={Link} to={'/catalog'} fullWidth>Go back to the store</Button>
            </Container>
    )
}