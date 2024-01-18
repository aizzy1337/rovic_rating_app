import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { colors } from '@mui/material';
import { useState } from 'react';
import { Navigate } from 'react-router';
import { jwtDecode } from "jwt-decode";

const defaultTheme = createTheme({
  palette: {
      background: {
        default: colors.grey[300]
      },
      text: {
        primary: colors.grey[900]
      },
      primary: {
          main: colors.grey[900]
      },
      secondary: {
          main: colors.grey[900]
      }
  }
});

interface JwtPayload {
  Id: string,
  name: string,
  role: string
}

export default function SignIn() {
  const [redirect, setRedirect] = useState(false);

  if(redirect) {
    return <Navigate to="/" />
  }

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);

    await fetch('https://localhost:44426/api/auth/login', {
      method: 'POST',
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        email: data.get('email'),
        password: data.get('password')
      })
    })
    .then((response) => {

        if(response.status == 200) {
            alert("Succesfully logged in");
            setRedirect(true);
        }

        return response.json()
    })
    .then((data) => {
       localStorage.setItem('token', data.token);
       const decoded = jwtDecode(data.token) as JwtPayload;
       localStorage.setItem('id', decoded.Id);
       localStorage.setItem('name', decoded.name);
       localStorage.setItem('role', decoded.role);
       console.log(decoded);
    })
    .catch((err) => {
      alert(err.message);
       console.log(err.message);
    });
  };

  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
            <TextField className='TextField'
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item>
                <Link href="/signup" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}
