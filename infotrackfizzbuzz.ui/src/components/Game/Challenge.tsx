import React from 'react';
import { Box, Typography, Paper } from '@mui/material';

interface ChallengeProps {
  number: number;
}

const Challenge: React.FC<ChallengeProps> = ({ number }) => {
  return (
    <Paper 
      elevation={3} 
      sx={{ 
        p: 4, 
        mb: 3, 
        textAlign: 'center',
        background: 'linear-gradient(45deg, #2196F3 30%, #21CBF3 90%)',
        color: 'white'
      }}
    >
      <Typography variant="h6" gutterBottom>
        What is the correct response for:
      </Typography>
      <Typography variant="h2" sx={{ fontWeight: 'bold' }}>
        {number}
      </Typography>
    </Paper>
  );
};

export default Challenge;