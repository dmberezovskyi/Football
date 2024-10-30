import clsx from 'clsx';
import axios from 'axios';
import PropTypes from 'prop-types';
import { Icon } from '@iconify/react';
import { useDropzone } from 'react-dropzone';
import { fData } from 'src/utils/formatNumber';
import React, { useCallback, useState } from 'react';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import roundAddAPhoto from '@iconify-icons/ic/round-add-a-photo';
import { alpha, makeStyles } from '@material-ui/core/styles';
import {
  Box,
  Typography,
  FormHelperText,
  CircularProgress
} from '@material-ui/core';
import { useTranslation } from 'react-i18next'
// ----------------------------------------------------------------------

const PHOTO_SIZE = 3145728; // bytes
const FILE_FORMATS = ['image/jpg', 'image/jpeg', 'image/gif', 'image/png'];

const useStyles = makeStyles(theme => ({
  root: {
    width: 144,
    height: 144,
    margin: 'auto',
    borderRadius: '50%',
    padding: theme.spacing(1),
    border: `1px dashed ${theme.palette.grey[500_32]}`
  },
  dropZone: {
    zIndex: 0,
    width: '100%',
    height: '100%',
    outline: 'none',
    display: 'flex',
    overflow: 'hidden',
    borderRadius: '50%',
    position: 'relative',
    alignItems: 'center',
    justifyContent: 'center',
    '& > *': { width: '100%', height: '100%' },
    '&:hover': { cursor: 'pointer', '& $placeholder': { zIndex: 9 } }
  },
  loading: {
    zIndex: 99,
    display: 'flex',
    alignItems: 'center',
    position: 'absolute',
    justifyContent: 'center',
    backgroundColor: alpha(theme.palette.grey[900], 0.72)
  },
  placeholder: {
    display: 'flex',
    position: 'absolute',
    alignItems: 'center',
    flexDirection: 'column',
    justifyContent: 'center',
    color: theme.palette.text.secondary,
    backgroundColor: theme.palette.background.neutral,
    transition: theme.transitions.create('opacity', {
      easing: theme.transitions.easing.easeInOut,
      duration: theme.transitions.duration.shorter
    }),
    '&:hover': { opacity: 0.72 }
  },
  hover: {
    opacity: 0,
    color: theme.palette.common.white,
    backgroundColor: theme.palette.grey[900],
    '&:hover': { opacity: 0.8 }
  },
  isDragActive: { opacity: 0.72 },
  isDragReject: {
    color: theme.palette.error.main,
    borderColor: theme.palette.error.light,
    backgroundColor: theme.palette.error.lighter
  },
  isDragAccept: {}
}));

// ----------------------------------------------------------------------

UploadAvatar.propTypes = {
  disabled: PropTypes.bool,
  caption: PropTypes.string,
  error: PropTypes.bool,
  file: PropTypes.object,
  setFile: PropTypes.func,
  className: PropTypes.string
};

function UploadAvatar({
  disabled,
  caption,
  error = false,
  value: file,
  onChange: setFile,
  className,
  ...other
}) {

  if(!file)
    file = '/static/images/avatar.png'
  const {t} = useTranslation();
  const classes = useStyles();
  const [isLoading, setIsLoading] = useState(false);
  const [isError, setIsError] = useState(null);
  const isMountedRef = useIsMountedRef();

  const handleDrop = useCallback(
    async acceptedFiles => {
      let file = acceptedFiles[0];

      const checkSize = file.size < PHOTO_SIZE;
      const checkType = FILE_FORMATS.includes(file.type);

      if (!checkSize) {
        setIsError('size-invalid');
      }

      if (!checkType) {
        setIsError('type-invalid');
      }
    },
    []
  );

  const {
    getRootProps,
    getInputProps,
    isDragActive,
    isDragReject,
    isDragAccept
  } = useDropzone({
    onDrop: handleDrop,
    multiple: false,
    disabled: disabled
  });

  return (
    <>
      <div className={clsx(classes.root, className)} {...other}>
        <div
          className={clsx(classes.dropZone, {
            [classes.isDragActive]: isDragActive,
            [classes.isDragAccept]: isDragAccept,
            [classes.isDragReject]: isDragReject || error
          })}
          {...getRootProps()}
        >
          <input {...getInputProps()} />

          {isLoading && (
            <Box className={classes.loading}>
              <CircularProgress size={32} thickness={2.4} />
            </Box>
          )}

          {file && (
            <Box
              component="img"
              alt=""
              src={file}
              sx={{ zIndex: 8, objectFit: 'cover' }}
            />
          )}

          <div className={clsx(classes.placeholder, { [classes.hover]: file })}>
            <Box
              component={Icon}
              icon={roundAddAPhoto}
              sx={{ width: 24, height: 24, mb: 0.5 }}
            />
            <Typography variant="caption" display="block">
              {file ? t("labels.updatePhoto") : t("labels.uploadPhoto")}
            </Typography>
          </div>
        </div>
      </div>

      <Box sx={{ display: 'flex', justifyContent: 'center' }}>
        {isError === 'size-invalid' && (
          <FormHelperText error>{`${t("validation.fileIsLargerThan")} ${fData(
            PHOTO_SIZE
          )}`}</FormHelperText>
        )}

        {isError === 'type-invalid' && (
          <FormHelperText error>
            {t("validation.fileTypeMustBe")} *.jpeg, *.jpg, *.png, *.gif
          </FormHelperText>
        )}
      </Box>

      <Box
        sx={{
          mt: 2,
          mb: 5,
          textAlign: 'center',
          typography: 'caption',
          color: 'text.secondary'
        }}
      >
        {caption ? (
          <span>caption</span>
        ) : (
          <span>
            {t("labels.allowed")} *.jpeg, *.jpg, *.png,
            <br /> {t("labels.maxFileSizeOf")} {fData(PHOTO_SIZE)}
          </span>
        )}
      </Box>
    </>
  );
}

export default UploadAvatar;
