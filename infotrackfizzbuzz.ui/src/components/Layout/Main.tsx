
import React from 'react';
import { Container, Box } from '@mui/material';

interface MainProps {
  children: React.ReactNode;
}

const Main: React.FC<MainProps> = ({ children }) => {
  return (
    <Box component="main" sx={{ minHeight: 'calc(100vh - 128px)', py: 4 }}>
      <Container maxWidth="lg">
        {children}
      </Container>
    </Box>
  );
};

export default Main;