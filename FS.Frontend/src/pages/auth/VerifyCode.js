import React from 'react';
import * as Yup from 'yup';
import { useFormik, Form, FormikProvider } from 'formik';
import Page from 'src/components/Page';
import Logo from 'src/components/Logo';
import { Icon } from '@iconify/react';
import { useSnackbar } from 'notistack';
import { AppRoutes } from 'src/routes/paths';
import fakeRequest from 'src/utils/fakeRequest';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import arrowIosBackFill from '@iconify-icons/eva/arrow-ios-back-fill';
import { makeStyles } from '@material-ui/core/styles';
import { Box, Button, Link, Container, Typography, OutlinedInput, FormHelperText } from '@material-ui/core';
import { LoadingButton } from '@material-ui/lab';
import maxLengthCheck from 'src/utils/maxLengthCheck';

// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
    minHeight: '100%',
    alignItems: 'center',
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
  },
  input: {
    width: 36,
    height: 36,
    padding: 0,
    textAlign: 'center',
    [theme.breakpoints.up('sm')]: {
      width: 56,
      height: 56
    }
  }
}));

// ----------------------------------------------------------------------

function VerifyCode() {
  const classes = useStyles();
  const navigate = useNavigate();
  const { enqueueSnackbar } = useSnackbar();

  const VerifyCodeSchema = Yup.object().shape({
    code1: Yup.number().required('Code is required'),
    code2: Yup.number().required('Code is required'),
    code3: Yup.number().required('Code is required'),
    code4: Yup.number().required('Code is required'),
    code5: Yup.number().required('Code is required'),
    code6: Yup.number().required('Code is required')
  });

  const formik = useFormik({
    initialValues: {
      code1: '',
      code2: '',
      code3: '',
      code4: '',
      code5: '',
      code6: ''
    },
    validationSchema: VerifyCodeSchema,
    onSubmit: async values => {
      await fakeRequest(500);
      enqueueSnackbar('Verify success', { variant: 'success' });
      navigate();
    }
  });

  const {
    values,
    errors,
    isValid,
    touched,
    isSubmitting,
    handleSubmit,
    getFieldProps
  } = formik;

  return (
    <Page title="Verify" className={classes.root}>
      <header className={classes.header}>
        <RouterLink to="/">
          <Logo />
        </RouterLink>
      </header>

      <Container>
        <Box sx={{ maxWidth: 480, mx: 'auto' }}>
          <Button
            size="small"
            component={RouterLink}
            to={AppRoutes.auth.login}
            startIcon={<Icon icon={arrowIosBackFill} width={20} height={20} />}
            sx={{ mb: 3 }}
          >
            Back
          </Button>

          <Typography variant="h3" gutterBottom>
            Please check your email!
          </Typography>
          <Typography color="textSecondary">
            We have emailed a 6-digit confirmation code to acb@domain, please
            enter the code in below box to verify your email.
          </Typography>

          <Box sx={{ mt: 5, mb: 3 }}>
            <FormikProvider value={formik}>
              <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                  {Object.keys(values).map(item => (
                    <Box key={item} sx={{ mx: 1 }}>
                      <OutlinedInput
                        {...getFieldProps(item)}
                        type="number"
                        placeholder="-"
                        onInput={maxLengthCheck}
                        inputProps={{ maxLength: 1 }}
                        error={Boolean(touched[item] && errors[item])}
                        classes={{ input: classes.input }}
                      />
                    </Box>
                  ))}
                </Box>

                <FormHelperText error={!isValid} style={{ textAlign: 'right' }}>
                  {!isValid && 'Code is required'}
                </FormHelperText>

                <Box sx={{ mt: 3 }}>
                  <LoadingButton
                    fullWidth
                    size="large"
                    type="submit"
                    variant="contained"
                    pending={isSubmitting}
                  >
                    Verify
                  </LoadingButton>
                </Box>
              </Form>
            </FormikProvider>
          </Box>

          <Typography variant="body2" align="center">
            Don’t have a code? &nbsp;
            <Link variant="subtitle2" underline="none" onClick={() => { }}>
              Resend code
            </Link>
          </Typography>
        </Box>
      </Container>
    </Page>
  );
}

export default VerifyCode;
