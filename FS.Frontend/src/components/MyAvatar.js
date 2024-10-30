import React from 'react';
import PropTypes from 'prop-types';
import { useSelector } from 'react-redux';
import createAvatar from 'src/utils/createAvatar';
import { MAvatar } from 'src/components/@material-extend';
import useAuth from 'src/hooks/useAuth'

MyAvatar.propTypes = {
  className: PropTypes.string
};

function MyAvatar({ className, ...other }) {
  const { userInfo } = useAuth();

  return (
    <MAvatar
      src={userInfo?.photoURL}
      alt={userInfo?.firstName}
      color={userInfo?.photoURL ? 'default' : createAvatar(userInfo?.firstName, userInfo?.lastName).color}
      className={className}
      {...other}
    >
      {createAvatar(userInfo?.firstName, userInfo?.lastName).name}
    </MAvatar>
  );
}

export default MyAvatar;
