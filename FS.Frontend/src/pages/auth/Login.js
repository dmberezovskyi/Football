import React, { useEffect, useState } from 'react';
import * as Yup from 'yup';
import { useFormik, Form, FormikProvider } from 'formik';
import Page from 'src/components/Page';
import Logo from 'src/components/Logo';
import { AppRoutes } from '../../routes/paths';
import { Link as RouterLink } from 'react-router-dom';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import { makeStyles } from '@material-ui/core/styles';
import {
  Box,
  Link,
  Container,
  Typography,
  InputAdornment,
  IconButton,
  TextField,
  Checkbox,
  FormControlLabel,
  Card
} from '@material-ui/core';
import { Icon } from '@iconify/react';
import MHidden from 'src/components/@material-extend/MHidden'
import { useTranslation } from 'react-i18next'
import useValidation from 'src/hooks/useValidation'
import eyeOffFill from '@iconify-icons/eva/eye-off-fill';
import eyeFill from '@iconify-icons/eva/eye-fill';
import { LoadingButton } from '@material-ui/lab';
import useAuth from 'src/hooks/useAuth'
// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {
    [theme.breakpoints.up('md')]: {
      display: 'flex'
    },
    visibility: props => props.showLogin ? 'visible' : 'collapse'
  },
  header: {
    top: 0,
    zIndex: 9,
    lineHeight: 0,
    width: '100%',
    display: 'flex',
    alignItems: 'center',
    position: 'absolute',
    padding: theme.spacing(3),
    justifyContent: 'space-between',
    [theme.breakpoints.up('md')]: {
      alignItems: 'flex-start',
      padding: theme.spacing(7, 5, 0, 7)
    }
  },
  content: {
    maxWidth: 480,
    margin: 'auto',
    display: 'flex',
    minHeight: '100vh',
    flexDirection: 'column',
    justifyContent: 'center',
    padding: theme.spacing(12, 0)
  },
  divider: {
    margin: theme.spacing(3, 0)
  },
  section: {
    width: '100%',
    maxWidth: 464,
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    margin: theme.spacing(2, 0, 2, 2)
  }
}));


function Login() {
  const { login } = useAuth();
  const { validationSchemas } = useValidation();
  const { t } = useTranslation();
  const isMountedRef = useIsMountedRef();
  const [showLogin, setShowLogin] = useState(false);
  const classes = useStyles({ showLogin });

  const LoginSchema = Yup.object().shape({
    email: validationSchemas.email,
    password: validationSchemas.password  
  });

  const initSignIn = async () => {
    const returnUrl = getReturnParameter();

    if (!returnUrl) {
      setWindowLocation("/")
      return
    }

    setShowLogin(true);
  }

  useEffect(() => {
    initSignIn();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])


  const getReturnParameter = () => {
    let params = new URLSearchParams(window.location.search);
    return params.get("ReturnUrl");
  }

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
      remember: true
    },
    validationSchema: LoginSchema,
    onSubmit: async (values, { setErrors, setSubmitting }) => {
      try {
        const response = await login(values.email, values.password, getReturnParameter());
        setWindowLocation(response.redirectUrl);

        if (isMountedRef.current) {
          setSubmitting(false);
        }
      } catch (err) {
        if (isMountedRef.current) {
          setSubmitting(false);
          setErrors({ afterSubmit: err.code });
        }
      }
    }
  });

  const setWindowLocation = (location) => {
    window.location.assign(location);
  }

  const [showPassword, setShowPassword] = useState(false);
  const {
    errors,
    touched,
    values,
    isSubmitting,
    handleSubmit,
    getFieldProps
  } = formik;

  const handleShowPassword = () => {
    setShowPassword(prev => !prev);
  };


  return (
    <Page title="Login" className={classes.root}>
      <header className={classes.header}>
        <RouterLink to="/">
          <Logo />
        </RouterLink>
        <MHidden width='smDown'>
          <Box sx={{ mt: { md: -2 }, typography: 'body2' }}>
            {t("labels.dontHaveAccount", "Don’t have an account?")} &nbsp;
            <Link
              underline="none"
              variant="subtitle2"
              component={RouterLink}
              to={AppRoutes.auth.register}
            >
              {t("labels.getStarted", "Get started")}
            </Link>
          </Box>
        </MHidden>
      </header>

      <MHidden width='mdDown'>
        <Card className={classes.section}>
          <Box component="h3" sx={{ typography: 'h3', px: 5, mt: 10, mb: 10 }}>
            Hi, Welcome Back
          </Box>
          <img src="/static/illustrations/illustration_login.svg" alt="login" />
        </Card>
      </MHidden>

      <Container>
        <div className={classes.content}>
          <Box sx={{ mb: 5 }}>
            <Typography variant="h4" gutterBottom>
              {t("labels.signIn", "Sign In")}
            </Typography>
          </Box>

          <FormikProvider value={formik}>
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
              <TextField
                fullWidth
                type="email"
                label={t("placeholders.email", "Email address")}
                {...getFieldProps('email')}
                error={
                  Boolean(touched.email && errors.email)
                }
                helperText={
                  (touched.email && errors.email)
                }
              />
              <Box sx={{ mb: 3 }} />
              <TextField
                fullWidth
                autoComplete="current-password"
                type={showPassword ? 'text' : 'password'}
                label={t("placeholders.password", "Password")}
                {...getFieldProps('password')}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position='end'>
                      <IconButton onClick={handleShowPassword} edge="end">
                        <Icon icon={showPassword ? eyeFill : eyeOffFill} />
                      </IconButton>
                    </InputAdornment>
                  )
                }}
                error={Boolean(touched.password && errors.password)}
                helperText={(touched.password && errors.password)}
              />
              <Box
                sx={{
                  my: 2,
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'space-between'
                }}
              >
                <FormControlLabel
                  control={
                    <Checkbox
                      {...getFieldProps('remember')}
                      checked={values.remember}
                    />
                  }
                  label={t("labels.rememberMe", "Remember me")}
                />

                <Link
                  variant="subtitle2"
                  to={AppRoutes.auth.resetPassword}
                  component={RouterLink}
                >
                  {t("labels.forgotPassword", "Forgot password?")}
                </Link>
              </Box>

              <LoadingButton
                fullWidth
                size="large"
                type="submit"
                variant="contained"
                loading={isSubmitting}
              >
                {t("labels.signIn", "Sign In")}
              </LoadingButton>
            </Form>
          </FormikProvider>


          <MHidden width='smUp'>
            <Box sx={{ mt: 3, typography: 'body2', textAlign: 'center' }}>
              {t("labels.dontHaveAccount", "Don’t have an account?")}&nbsp;
              <Link
                variant="subtitle2"
                to={AppRoutes.auth.register}
                component={RouterLink}
              >
                {t("labels.getStarted", "Get started")}
              </Link>
            </Box>
          </MHidden>
        </div>
      </Container>
    </Page>
  );
}

export default Login;
