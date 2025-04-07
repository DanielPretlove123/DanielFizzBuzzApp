import React from 'react';
import { Typography, Box } from '@mui/material';
import AdminPanel from '../components/AdminPanel/AdminPanel';
import Main from '../components/Layout/Main';

const AdminPage: React.FC = () => {
  return (
    <Main>
      <Box sx={{ mt: 2, mb: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          Game Master Portal
        </Typography>
        <Typography variant="body1" color="text.secondary" paragraph>
          Configure game rules, add new challenges, and customize the FizzBuzz experience.
        </Typography>
      </Box>
      <AdminPanel />
    </Main>
  );
};

export default AdminPage;