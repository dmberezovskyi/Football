import React from 'react'
import Page from 'src/components/Page';
import {
  Card,
  Table,
  Stack,
  Avatar,
  Button,
  Checkbox,
  TableRow,
  TableBody,
  TableCell,
  Container,
  Typography,
  TableContainer,
  TablePagination
} from '@material-ui/core';
import Label from 'src/components/Label';
import Scrollbar from 'src/components/Scrollbar';
import HeaderBreadcrumbs from 'src/components/HeaderBreadcrumbs';
import { Link as RouterLink } from 'react-router-dom';
import plusFill from '@iconify-icons/eva/plus-fill';
import { Icon } from '@iconify/react';

const TABLE_HEAD = [
  { id: 'name', label: 'Name', alignRight: false },
  { id: 'email', label: 'Email', alignRight: false },
  { id: 'role', label: 'Role', alignRight: false },
  { id: 'state', label: 'State', alignRight: false },
  { id: '' }
];

export default function UserList() {


  return (
    <Page title="User list">
      <Container>
        <HeaderBreadcrumbs
          heading="User List"
          links={[
            { name: 'Administration' },
            { name: 'User list' }
          ]}
          action={
            <Button
              variant="contained"
              component={RouterLink}
              to={"#"}
              startIcon={<Icon icon={plusFill} />}
            >
              New User
            </Button>
          }
        />



      </Container>
    </Page>
  )
}