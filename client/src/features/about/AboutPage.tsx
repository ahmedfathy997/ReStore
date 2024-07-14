import { Button, ButtonGroup, Container, Typography } from "@mui/material";
import agent from "../../app/api/agent";

export default function AboutPage() {
    return(
       <Container>
            <Typography gutterBottom variant="h2">
                Errors for testing purposes.!
            </Typography>
            <ButtonGroup fullWidth>
                <Button variant="contained" onClick={() => agent.TestErrors.get404Error()}>404-Not found</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get401Error()}>401-Unauthorized</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get400Error()}>400-Bad Request</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get500Error()}>500-Server Error</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.getValidationError()}>Validation Error</Button>

            </ButtonGroup>
       </Container>
    )
}