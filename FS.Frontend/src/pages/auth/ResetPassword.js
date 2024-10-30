import React, { useState } from 'react';
import * as Yup from 'yup';
import { useFormik, Form, FormikProvider } from 'formik';
import Logo from 'src/components/Logo';
import Page from 'src/components/Page';
import { AppRoutes } from '../../routes/paths';
import { Link as RouterLink } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import { Box, Button, Container, Typography, TextField } from '@material-ui/core';
import { LoadingButton } from '@material-ui/lab';

// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
    minHeight: '100%',
    alignItems: 'center',
    justifyContent: 'center',
    padding: theme.spacing(12, 0)
  },
  header: {
    top: 0,
    left: 0,
    width: '100%',
    position: 'absolute',
    padding: theme.spacing(3),
    [theme.breakpoints.up('sm')]: {
      padding: theme.spacing(5)
    }
  }
}));

// ----------------------------------------------------------------------

function ResetPassword() {
  const classes = useStyles();
  const [sent, setSent] = useState(false);

  const ResetPasswordSchema = Yup.object().shape({
    email: Yup.string()
      .email('Email must be a valid email address')
      .required('Email is required')
  });

  const formik = useFormik({
    initialValues: {
      email: 'demo@minimals.cc'
    },
    validationSchema: ResetPasswordSchema,
    onSubmit: async (values, { setErrors, setSubmitting }) => {
      // try {

      // } catch (err) {
      //   if (isMountedRef.current) {
      //     setErrors({ afterSubmit: err.code });
      //     setSubmitting(false);
      //   }
      // }
    }
  });

  const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

  return (
    <Page title="Reset Password" className={classes.root}>
      <header className={classes.header}>
        <RouterLink to="/">
          <Logo />
        </RouterLink>
      </header>

      <Container>
        <Box sx={{ maxWidth: 480, mx: 'auto' }}>
          {!sent ? (
            <>
              <Typography variant="h3" gutterBottom>
                Forgot your password?
              </Typography>
              <Typography color="textSecondary">
                Please enter the email address associated with your account and
                We will email you a link to reset your password.
              </Typography>
              <Box sx={{ mt: 5, mb: 1 }}>
                <FormikProvider value={formik}>
                  <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                    <TextField
                      fullWidth
                      {...getFieldProps('email')}
                      type="email"
                      label="Email address"
                      error={
                        Boolean(touched.email && errors.email)
                      }
                      helperText={
                        touched.email && errors.email
                      }
                    />
                    <Box sx={{ mt: 3 }}>
                      <LoadingButton
                        fullWidth
                        size="large"
                        type="submit"
                        variant="contained"
                        pending={isSubmitting}
                      >
                        Reset Password
                      </LoadingButton>
                    </Box>
                  </Form>
                </FormikProvider>
              </Box>
              <Button
                fullWidth
                size="large"
                component={RouterLink}
                to={AppRoutes.auth.login}
              >
                Back
              </Button>
            </>
          ) : (
            <Box sx={{ textAlign: 'center' }}>
              <Box sx={{ mb: 5 }}>
                <Box
                  component="img"
                  alt="sent email"
                  src="/static/icons/ic_email_sent.svg"
                  sx={{ mb: 5, mx: 'auto' }}
                />
                <Typography variant="h3" gutterBottom>
                  Request sent successfully
                </Typography>
                <Typography>
                  We have sent a confirmation email to &nbsp;
                  <strong>{formik.values.email}</strong>
                  <br />
                  Please check your email.
                </Typography>
              </Box>
              <Button
                size="large"
                variant="contained"
                component={RouterLink}
                to={AppRoutes.auth.login}
              >
                Back
              </Button>
            </Box>
          )}
        </Box>
      </Container>
    </Page>
  );
}

export default ResetPassword;
