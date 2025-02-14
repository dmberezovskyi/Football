import clsx from 'clsx';
import React from 'react';
import PropTypes from 'prop-types';
import { useSnackbar } from 'notistack';
import fakeRequest from 'src/utils/fakeRequest';
import { useFormik, Form, FormikProvider } from 'formik';
import { makeStyles } from '@material-ui/core/styles';
import {
  Box,
  Card,
  Switch,
  FormGroup,
  Typography,
  FormControlLabel
} from '@material-ui/core';
import { LoadingButton } from '@material-ui/lab';

// ----------------------------------------------------------------------

const ACTIVITY_OPTIONS = [
  {
    value: 'activityComments',
    label: 'Email me when someone comments onmy article'
  },
  {
    value: 'activityAnswers',
    label: 'Email me when someone answers on my form'
  },
  { value: 'activityFollows', label: 'Email me hen someone follows me' }
];

const APPLICATION_OPTIONS = [
  { value: 'applicationNews', label: 'News and announcements' },
  { value: 'applicationProduct', label: 'Weekly product updates' },
  { value: 'applicationBlog', label: 'Weekly blog digest' }
];

const useStyles = makeStyles(theme => ({
  root: { padding: theme.spacing(3) }
}));

// ----------------------------------------------------------------------

Notifications.propTypes = {
  notifications: PropTypes.object,
  className: PropTypes.string
};

function Notifications({ notifications, className }) {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: {
      activityComments: notifications.activityComments,
      activityAnswers: notifications.activityAnswers,
      activityFollows: notifications.activityFollows,
      applicationNews: notifications.applicationNews,
      applicationProduct: notifications.applicationProduct,
      applicationBlog: notifications.applicationBlog
    },
    onSubmit: async (values, { setSubmitting }) => {
      await fakeRequest(500);
      setSubmitting(false);
      alert(JSON.stringify(values, null, 2));
      enqueueSnackbar('Save success', { variant: 'success' });
    }
  });

  const { values, isSubmitting, handleSubmit, getFieldProps } = formik;

  return (
    <Card className={clsx(classes.root, className)}>
      <FormikProvider value={formik}>
        <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
          <FormGroup>
            <Typography variant="overline" color="textSecondary" paragraph>
              Activity
            </Typography>
            {ACTIVITY_OPTIONS.map(activity => (
              <FormControlLabel
                key={activity.value}
                control={
                  <Switch
                    {...getFieldProps(activity.value)}
                    color="primary"
                    checked={values[activity.value]}
                  />
                }
                label={activity.label}
              />
            ))}
          </FormGroup>

          <Box sx={{ mt: 3, mb: 5 }}>
            <FormGroup>
              <Typography variant="overline" color="textSecondary" paragraph>
                Application
              </Typography>
              {APPLICATION_OPTIONS.map(item => (
                <FormControlLabel
                  key={item.value}
                  control={
                    <Switch
                      {...getFieldProps(item.value)}
                      color="primary"
                      checked={values[item.value]}
                    />
                  }
                  label={item.label}
                />
              ))}
            </FormGroup>
          </Box>

          <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
            <LoadingButton
              type="submit"
              variant="contained"
              pending={isSubmitting}
            >
              Save Changes
            </LoadingButton>
          </Box>
        </Form>
      </FormikProvider>
    </Card>
  );
}

export default Notifications;
