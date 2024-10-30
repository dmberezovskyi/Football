import clsx from 'clsx';
import React from 'react';
import * as Yup from 'yup';
import PropTypes from 'prop-types';
import { useSnackbar } from 'notistack';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import { Form, FormikProvider, useFormik } from 'formik';
import { UploadAvatar } from 'src/components/Upload';
import { makeStyles } from '@material-ui/core/styles';
import {
  Box,
  Grid,
  Card,
  TextField,
  CardContent,
  Typography
} from '@material-ui/core';
import { LoadingButton } from '@material-ui/lab';
import { useTranslation } from 'react-i18next'
import useValidation from 'src/hooks/useValidation'
import { useSelector, useDispatch } from 'react-redux'
import { updateUserProfile } from 'src/redux/slices/user'
import SvgIconStyle from 'src/components/SvgIconStyle';
import { Icon } from '@iconify/react';
import closeFill from '@iconify-icons/eva/close-fill';
import Divider from '@material-ui/core/Divider';
// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {}
}));

// ----------------------------------------------------------------------

General.propTypes = {
  className: PropTypes.string
};

function General({ className }) {
  const { userProfile, id, version } = useSelector(state => state.user);
  const { validationSchemas } = useValidation();
  const { t } = useTranslation();
  const classes = useStyles();
  const isMountedRef = useIsMountedRef();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const dispatch = useDispatch();

  const UpdateUserSchema = Yup.object().shape({
    firstName: validationSchemas.firstName,
    lastName: validationSchemas.lastName,
    email: validationSchemas.email,
    phone: validationSchemas.phone,
    about: validationSchemas.about,
  });

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: {
      firstName: userProfile?.firstName || '',
      lastName: userProfile?.lastName || '',
      email: userProfile?.email || '',
      photoURL: null,
      phone: userProfile?.phone || '',
      about: userProfile.about || '',
      middleName: userProfile?.middleName || '',
      birthDate: userProfile?.birthDate || null
    },
    validationSchema: UpdateUserSchema,
    onSubmit: async (values, { setErrors, setSubmitting }) => {
      try {
        await dispatch(updateUserProfile({
          id: id,
          version: version,

          profile: {
            firstName: values.firstName,
            lastName: values.lastName,
            middleName: values.middleName,
            birthDate: values.birthDate,
            phone: values.phone,
            about: values.about
          }
        }));

        enqueueSnackbar(t("labels.updateSuccess", "Update success"), {
          variant: 'success',
          action: key => (
            <SvgIconStyle size="small" onClick={() => closeSnackbar(key)}>
              <Icon icon={closeFill} />
            </SvgIconStyle>
          )
        });
        if (isMountedRef.current) {
          setSubmitting(false);
        }
      } catch (ex) {
        if (isMountedRef.current) {
          let errors = ex.getFormikErrors();

          var flatErrors = { ...errors?.profile }
          setErrors(flatErrors);
          setSubmitting(false);
        }
      }
    }
  });

  const {
    values,
    errors,
    touched,
    isSubmitting,
    handleSubmit,
    getFieldProps,
    setFieldValue
  } = formik;

  return (
    <div className={clsx(classes.root, className)}>
      <FormikProvider value={formik}>
        <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
          <Grid container spacing={3}>
            <Grid item xs={12} md={4}>
              <Card>
                <Box
                  sx={{
                    my: 10,
                    display: 'flex',
                    alignItems: 'center',
                    flexDirection: 'column'
                  }}
                >
                  <UploadAvatar
                    value={values.photoURL}
                    onChange={value => setFieldValue('photoURL', value)} />
                </Box>
              </Card>
            </Grid>

            <Grid item xs={12} md={8}>
              <Card>
                <CardContent>
                  <Grid container spacing={2}>
                  {/* <Grid item xs={12}>
                        <Typography
                          color="textSecondary"
                          display="block"
                          >
                          Personal
                        </Typography>
                      <Divider />
                    </Grid> */}

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.firstName")}
                        {...getFieldProps('firstName')}
                        error={Boolean(touched.firstName && errors.firstName)}
                        helperText={(touched.firstName && errors.firstName)}
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.lastName")}
                        {...getFieldProps('lastName')}
                        error={Boolean(touched.lastName && errors.lastName)}
                        helperText={(touched.lastName && errors.lastName)}
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.middleName")}
                        {...getFieldProps('middleName')}
                        error={Boolean(touched.middleName && errors.middleName)}
                        helperText={(touched.middleName && errors.middleName)}
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      {/* <LocalizedDatePicker
                        disableFuture
                        fullWidth
                        openTo="year"
                        views={["year", "month", "date"]}
                        label={t('placeholders.birthDate', "Birth date")}
                        format='dd/MM/yyyy'
                        onChange={value => setFieldValue("birthDate", value)}
                        value={values["birthDate"]}
                        error={Boolean(touched.birthDate && errors.birthDate)}
                        helperText={touched.birthDate && errors.birthDate}
                      /> */}
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        disabled
                        fullWidth
                        label={t("placeholders.email")}
                        {...getFieldProps('email')}
                        error={Boolean(touched.email && errors.email)}
                        helperText={(touched.email && errors.email)}
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.phone")}
                        {...getFieldProps('phone')}
                        error={Boolean(touched.phone && errors.phone)}
                        helperText={(touched.phone && errors.phone)}
                      />
                    </Grid>

                    <Grid item xs={12}>
                      <TextField
                        {...getFieldProps('about')}
                        fullWidth
                        multiline
                        minRows={3}
                        maxRows={3}
                        label={t("placeholders.about")}
                        error={Boolean(touched.about && errors.about)}
                        helperText={(touched.about && errors.about)}
                      />
                    </Grid>

                    <Grid item xs={12}>
                        {/* <Typography
                          color="textSecondary"
                          display="block"
                          >
                          Address
                        </Typography>
                      <Divider /> */}
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.country")}
                        {...getFieldProps('country')}
                        error={Boolean(touched.country && errors.country)}
                        helperText={(touched.country && errors.country)}
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.state")}
                        {...getFieldProps('state')}
                        error={Boolean(touched.state && errors.state)}
                        helperText={(touched.state && errors.state)}
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.city")}
                        {...getFieldProps('country')}
                        error={Boolean(touched.city && errors.city)}
                        helperText={(touched.city && errors.city)}
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <TextField
                        fullWidth
                        label={t("placeholders.zipCode")}
                        {...getFieldProps('zipCode')}
                        error={Boolean(touched.zipCode && errors.zipCode)}
                        helperText={(touched.zipCode && errors.zipCode)}
                      />
                    </Grid>

                    <Grid item xs={12}>
                      <TextField
                        fullWidth
                        label={t("placeholders.street")}
                        {...getFieldProps('streetAddress ')}
                        error={Boolean(touched.streetAddress && errors.streetAddress )}
                        helperText={(touched.streetAddress  && errors.streetAddress )}
                      />
                    </Grid>
                  </Grid>

                  <Box
                    sx={{ mt: 3, display: 'flex', justifyContent: 'flex-end' }}
                  >
                    <LoadingButton
                      type="submit"
                      variant="contained"
                      loading={isSubmitting}
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

export default General;
