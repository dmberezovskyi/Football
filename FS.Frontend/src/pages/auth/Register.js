import React, { useState } from 'react';
import * as Yup from 'yup';
import clsx from 'clsx';
import { useFormik } from 'formik';
import Page from 'src/components/Page';
import Logo from 'src/components/Logo';
import closeFill from '@iconify-icons/eva/close-fill';
import { Icon } from '@iconify/react';
import { useSnackbar } from 'notistack';
import { Link as RouterLink } from 'react-router-dom';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import { makeStyles } from '@material-ui/core/styles';
import {
  Box,
  Link,
  Container,
  Typography,
  Grid,
  TextField,
  IconButton,
  InputAdornment,
  MenuItem,
  Card
} from '@material-ui/core';
import MHidden from 'src/components/@material-extend/MHidden'
import { MIconButton } from 'src/components/@material-extend';
import { useTranslation } from 'react-i18next'
import userManager from 'src/services/auth/userManager'
import useAuth from 'src/hooks/useAuth'
import useValidation from 'src/hooks/useValidation'
import { UserRole } from 'src/types'
import { LoadingButton } from '@material-ui/lab';
import { Form, FormikProvider } from 'formik';
import eyeFill from '@iconify-icons/eva/eye-fill';
import eyeOffFill from '@iconify-icons/eva/eye-off-fill';
import DatePicker from 'src/components/DatePicker'
// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {
    [theme.breakpoints.up('md')]: {
      display: 'flex'
    }
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
    justifyContent: 'center',
    flexDirection: 'column',
    margin: theme.spacing(2, 0, 2, 2)
  }
}));


function RegisterView() {
  const { t } = useTranslation();
  const { validationSchemas } = useValidation();
  const classes = useStyles();
  const isMountedRef = useIsMountedRef();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [showPassword, setShowPassword] = useState(false);
  const { register } = useAuth();
  const userRoles = [
    { value: UserRole.Trainer, name: t('labels.roles.trainer') },
    { value: UserRole.Player, name: t('labels.roles.player') }
  ];


  const RegisterSchema = Yup.object().shape({
    firstName: validationSchemas.firstName,
    lastName: validationSchemas.lastName,
    email: validationSchemas.email,
    password: validationSchemas.password,
    role: validationSchemas.role,
    birthDate: validationSchemas.birthDate,
    phone: validationSchemas.phone
  });

  const formik = useFormik({
    initialValues: {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      phone: '',
      role: '',
      birthDate: null
    },
    validationSchema: RegisterSchema,
    onSubmit: async (values, { setErrors, setSubmitting }) => {
      try {
        await register(values)

        enqueueSnackbar('You are successfully registered!', {
          variant: 'success',
          action: key => (
            <MIconButton size="small" onClick={() => closeSnackbar(key)}>
              <Icon icon={closeFill} />
            </MIconButton>
          )
        });


        if (isMountedRef.current) {
          setSubmitting(false);
        }
      } catch (err) {
        if (isMountedRef.current) {
          setErrors({ afterSubmit: err.code });
          setSubmitting(false);
        }
      }
    }
  });

  const { errors, touched, handleSubmit, isSubmitting, getFieldProps, setFieldValue, values } = formik;

  return (
    <Page title={t("titles.registration", "Registration")} className={classes.root}>
      <header className={classes.header}>
        <RouterLink to="/">
          <Logo />
        </RouterLink>
        <MHidden width="smDown">
          <Box sx={{ mt: { md: -2 }, typography: 'body2' }}>
            {t("labels.alreadyHaveAnAccount", "Already have an account?")} &nbsp;
            <Link
              to="#"
              underline="none"
              variant="subtitle2"
              component={RouterLink}
              onClick={async () => await userManager.signinRedirect()}
            >
              {t("labels.signIn", "Sign In")}
            </Link>
          </Box>
        </MHidden>
      </header>

      {/*------------ Section ------------*/}
      <MHidden width='mdDown'>
        <Card className={clsx(classes.section)}>
          <Box component="h3" sx={{ typography: 'h3', px: 5, mt: 10, mb: 10 }}>
            Manage your skills with us
          </Box>
          <img
            alt="register"
            src="/static/illustrations/illustration_register.svg"
          />
        </Card>
      </MHidden>

      <Container>
        <div className={classes.content}>
          <Typography variant="h4" gutterBottom>
            Get started absolutely free.
          </Typography>
          <Box sx={{ mb: 5 }} />

          <FormikProvider value={formik}>
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
              <Grid container spacing={2}>
                <Grid item xs={6}>
                  <TextField
                    fullWidth
                    label={t('placeholders.firstName', "First name")}
                    {...getFieldProps('firstName')}
                    error={Boolean(touched.firstName && errors.firstName)}
                    helperText={touched.firstName && errors.firstName}
                  />
                </Grid>
                <Grid item xs={6}>
                  <TextField
                    fullWidth
                    label={t('placeholders.lastName', "Last name")}
                    {...getFieldProps('lastName')}
                    error={Boolean(touched.lastName && errors.lastName)}
                    helperText={touched.lastName && errors.lastName}
                  />
                </Grid>
              </Grid>

              <Box sx={{ mb: 3 }} />
              <TextField
                fullWidth
                name="email"
                type="email"
                label={t('placeholders.email', "Email address")}
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
                select
                fullWidth
                error={Boolean(touched.role && errors.role)}
                helperText={touched.role && errors.role}
                label={t('placeholders.role', "Role")}
                {...getFieldProps('role')}>
                {userRoles.map(role => (
                  <MenuItem key={role.value} value={role.value}>
                    {role.name}
                  </MenuItem>
                ))}
              </TextField>

              <Box sx={{ mb: 3 }} />
              <Grid container spacing={2}>
                <Grid item xs={6} >
                  <DatePicker
                    disableFuture
                    openTo="year"
                    label={t('placeholders.birthDate', "Birth date")}
                    value={values["birthDate"]}
                    minDate={new Date('1900-01-01')}
                    maxDate={Date.now()}
                    onChange={value => setFieldValue("birthDate", value)}
                    error={Boolean(touched.birthDate && errors.birthDate)}
                    helperText={touched.birthDate && errors.birthDate}
                    renderInput={(params) => 
                      <TextField  fullWidth {...params} helperText={null} />}
                  /> 
                </Grid>
                <Grid item xs={6}>
                  <TextField
                    fullWidth
                    label={t('placeholders.phone', "Phone")}
                    {...getFieldProps('phone')}
                    error={Boolean(touched.phone && errors.phone)}
                    helperText={touched.phone && errors.phone}
                  />
                </Grid>
              </Grid>

              <Box sx={{ mb: 3 }} />
              <TextField
                fullWidth
                type={showPassword ? 'text' : 'password'}
                label={t('placeholders.password', "Password")}
                {...getFieldProps('password')}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="start">
                      <IconButton
                        edge="end"
                        onClick={() => setShowPassword(prev => !prev)}
                      >
                        <Icon icon={showPassword ? eyeFill : eyeOffFill} />
                      </IconButton>
                    </InputAdornment>
                  )
                }}
                error={
                  Boolean(touched.password && errors.password)
                }
                helperText={
                  (touched.password && errors.password)
                }
              />
              <Box sx={{ mt: 3 }}>
                <LoadingButton
                  fullWidth
                  size="large"
                  type="submit"
                  variant="contained"
                  loading={isSubmitting}
                >
                  {t("labels.signUp", "Sign Up")}
                </LoadingButton>
              </Box>
            </Form>
          </FormikProvider>

          <Box sx={{ mt: 3 }}>
            <Typography variant="body2" align="center" color="textSecondary">
              {t("labels.registerAccountAgreement", "By register, I agree to")}&nbsp;
              <Link to="#" color="textPrimary" underline="always">
                {t("labels.termsOfService", "Terms of Service")}
              </Link>
              &nbsp;{t("labels.and", "and")}&nbsp;
              <Link to="#" color="textPrimary" underline="always">
                {t("labels.privacyPolicy", "Privacy Policy")}
              </Link>
            </Typography>
          </Box>

          <MHidden width='smUp'>
            <Box sx={{ mt: 3, textAlign: 'center' }}>
              Already have an account?&nbsp;
              <Link
                to="#"
                onClick={async () => await userManager.signinRedirect()}
                variant="subtitle2"
                component={RouterLink}
              >
                Login
              </Link>
            </Box>
          </MHidden>
        </div>
      </Container>
    </Page>
  );
}

export default RegisterView;