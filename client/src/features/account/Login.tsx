import Avatar from '@mui/material/Avatar';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Paper } from '@mui/material';
import { Link, useLocation } from 'react-router-dom';
import { FieldValues, useForm } from 'react-hook-form';
import { LoadingButton } from '@mui/lab';
import { signInUser } from './accountSlice';
import { useAppDispatch } from '../../app/store/configureStore';
import { useNavigate } from 'react-router-dom';

export default function Login() {
  const dispatch = useAppDispatch();
  const location = useLocation();
  const navigate = useNavigate();

  const {register, handleSubmit, formState: {isSubmitting, errors, isValid}} = useForm({
    mode: "all"
  })

  async function submitForm(data: FieldValues){
    try {
      await dispatch((signInUser(data)));
      navigate(location.state?.from?.pathname || "/catalog")
    } catch (error) {
      console.log(error);
    }
  }

  return (
      <Container component={Paper} maxWidth="sm" sx={{display: "flex", flexDirection: "column", alignItems: "center", p: 4}}>
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              fullWidth
              label="Username"
              autoComplete="email"
              autoFocus
              {...register("username", {required: true})}
              error={!!errors.username}
              helperText={typeof errors.username?.message === 'string' ? errors.username.message : ''}
              />
            <TextField
              margin="normal"
              fullWidth
              label="Password"
              type="password"
              {...register("password", {required: true})}
              error={!!errors.password}
              helperText={typeof errors.password?.message === 'string' ? errors.password.message : ''}
              />
            <LoadingButton
              loading={isSubmitting}
              disabled={!isValid}
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign In
            </LoadingButton>
            <Grid container>
              <Grid item>
                <Link to="/register" >
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
      </Container>
  );
}