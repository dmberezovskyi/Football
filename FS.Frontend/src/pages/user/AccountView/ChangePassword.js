import clsx from 'clsx';
import React from 'react';
import * as Yup from 'yup';
import PropTypes from 'prop-types';
import { useSnackbar } from 'notistack';
import { useFormik, Form, FormikProvider } from 'formik';
import { makeStyles } from '@material-ui/core/styles';
import { Box, Card, TextField, Grid, CardContent } from '@material-ui/core';
import { LoadingButton } from '@material-ui/lab';
import { useTranslation } from 'react-i18next'

// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {},
  margin: {
    marginBottom: theme.spacing(3)
  }
}));

// ----------------------------------------------------------------------

ChangePassword.propTypes = {
  className: PropTypes.string
};

function ChangePassword({ className }) {
  const { t } = useTranslation();
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();

  const ChangePassWordSchema = Yup.object().shape({
    oldPassword: Yup.string().required('Old Password is required'),
    newPassword: Yup.string()
      .min(6, 'Password must be at least 6 characters')
      .required('New Password is required'),
    confirmNewPassword: Yup.string().oneOf(
      [Yup.ref('newPassword'), null],
      'Passwords must match'
    )
  });

  const formik = useFormik({
    initialValues: {
      oldPassword: '',
      newPassword: '',
      confirmNewPassword: ''
    },
    validationSchema: ChangePassWordSchema,
    onSubmit: async (values, { setSubmitting }) => {

      setSubmitting(false);
      enqueueSnackbar('Save success', { variant: 'success' });
    }
  });

  const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

  return (
    <div className={clsx(classes.root, className)}>
      <FormikProvider value={formik}>
        <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
          <Grid container spacing={3}>
            <Grid item xs={12} md={6}>
              <Card>
                <CardContent>
                  <TextField
                    {...getFieldProps('oldPassword')}
                    fullWidth
                    type="password"
                    label={t("labels.oldPassword")}
                    error={Boolean(touched.oldPassword && errors.oldPassword)}
                    helperText={touched.oldPassword && errors.oldPassword}
                    className={classes.margin}
                  />

                  <TextField
                    {...getFieldProps('newPassword')}
                    fullWidth
                    type="password"
                    label={t("labels.newPassword")}
                    error={Boolean(touched.newPassword && errors.newPassword)}
                    helperText={(touched.newPassword && errors.newPassword)}
                    className={classes.margin}
                  />
                  <TextField
                    {...getFieldProps('confirmNewPassword')}
                    fullWidth
                    type="password"
                    label={t("labels.confirmNewPassword")}
                    error={Boolean(
                      touched.confirmNewPassword && errors.confirmNewPassword
                    )}
                    helperText={touched.confirmNewPassword && errors.confirmNewPassword}
                    className={classes.margin}
                  />

                  <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                    <LoadingButton
                      type="submit"
                      variant="contained"
                      pending={isSubmitting}
                    >
                      {t("labels.saveChanges")}
            </LoadingButton>
                  </Box>
                </CardContent>
              </Card>
            </Grid>
          </Grid>
        </Form>
      </FormikProvider>
    </div>
  );
}

export default ChangePassword;
