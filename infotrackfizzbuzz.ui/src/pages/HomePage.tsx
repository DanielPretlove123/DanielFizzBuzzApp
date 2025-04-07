import React from 'react';
import { Box, Typography, Button, Paper } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';
import { SportsEsports as GameIcon, Settings as AdminIcon } from '@mui/icons-material';
import Main from '../components/Layout/Main';

const HomePage: React.FC = () => {
  return (
    <Main>
      <Box sx={{ my: 4 }}>
        <Paper elevation={3} sx={{ p: 4, textAlign: 'center' }}>
          <Typography variant="h3" component="h1" gutterBottom>
            Welcome to FizzBuzz Game!
          </Typography>
          <Typography variant="h5" color="text.secondary" sx={{ mb: 4 }}>
            A fun twist on the classic FizzBuzz challenge
          </Typography>
         
          <Box 
            sx={{ 
              display: 'flex', 
              flexDirection: { xs: 'column', md: 'row' }, 
              gap: 4, 
              justifyContent: 'center', 
              mt: 2 
            }}
          >
            <Box sx={{ flex: 1, maxWidth: { xs: '100%', md: '45%' } }}>
              <Paper
                elevation={2}
                sx={{
                  p: 3,
                  height: '100%',
                  display: 'flex',
                  flexDirection: 'column',
                  alignItems: 'center',
                  transition: 'transform 0.3s',
                  '&:hover': {
                    transform: 'translateY(-5px)',
                  }
                }}
              >
                <GameIcon sx={{ fontSize: 60, color: 'primary.main', mb: 2 }} />
                <Typography variant="h5" gutterBottom>
                  Play Game
                </Typography>
                <Typography variant="body1" sx={{ mb: 3 }}>
                  Test your FizzBuzz skills with randomized challenges and customized rules
                </Typography>
                <Button
                  variant="contained"
                  color="primary"
                  component={RouterLink}
                  to="/game"
                  size="large"
                  sx={{ mt: 'auto' }}
                >
                  Start Playing
                </Button>
              </Paper>
            </Box>
           
            <Box sx={{ flex: 1, maxWidth: { xs: '100%', md: '45%' } }}>
              <Paper
                elevation={2}
                sx={{
                  p: 3,
                  height: '100%',
                  display: 'flex',
                  flexDirection: 'column',
                  alignItems: 'center',
                  transition: 'transform 0.3s',
                  '&:hover': {
                    transform: 'translateY(-5px)',
                  }
                }}
              >
                <AdminIcon sx={{ fontSize: 60, color: 'secondary.main', mb: 2 }} />
                <Typography variant="h5" gutterBottom>
                  Admin Portal
                </Typography>
                <Typography variant="body1" sx={{ mb: 3 }}>
                  Customize game rules, create new challenges, and manage the FizzBuzz experience
                </Typography>
                <Button
                  variant="contained"
                  color="secondary"
                  component={RouterLink}
                  to="/admin"
                  size="large"
                  sx={{ mt: 'auto' }}
                >
                  Admin Portal
                </Button>
              </Paper>
            </Box>
          </Box>
        </Paper>
      </Box>
    </Main>
  );
};

export default HomePage;