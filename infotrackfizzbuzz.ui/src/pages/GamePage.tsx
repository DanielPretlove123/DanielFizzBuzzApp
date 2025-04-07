import React from 'react';
import { Typography, Box } from '@mui/material';
import Game from '../components/Game/Game';
import Main from '../components/Layout/Main';

const GamePage: React.FC = () => {
  return (
    <Main>
      <Box sx={{ mt: 2, mb: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          FizzBuzz Challenge
        </Typography>
        <Typography variant="body1" color="text.secondary" paragraph>
          Apply the rules to each number that appears. Type your answer and submit to earn points!
        </Typography>
      </Box>
      <Game />
    </Main>
  );
};

export default GamePage;