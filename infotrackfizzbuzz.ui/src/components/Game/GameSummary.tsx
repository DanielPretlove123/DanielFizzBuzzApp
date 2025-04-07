import React from 'react';
import { Box, Typography, Paper, Button, Divider } from '@mui/material';
import { EndGameResponse } from '../../api/types';

interface GameSummaryProps {
  results: EndGameResponse;
  onPlayAgain: () => void;
}

const GameSummary: React.FC<GameSummaryProps> = ({ results, onPlayAgain }) => {
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleString();
  };

  const getDuration = (start: string, end: string) => {
    const startDate = new Date(start);
    const endDate = new Date(end);
    const diff = (endDate.getTime() - startDate.getTime()) / 1000;
   
    const minutes = Math.floor(diff / 60);
    const seconds = Math.floor(diff % 60);
   
    return `${minutes}m ${seconds}s`;
  };

  return (
    <Box sx={{ mt: 3 }}>
      <Paper variant="outlined" sx={{ p: 3 }}>
        <Typography variant="h5" gutterBottom sx={{ mb: 3 }}>
          Game Summary
        </Typography>
       
        <Divider sx={{ mb: 3 }} />
       
        <Box sx={{ 
          display: 'flex', 
          flexDirection: { xs: 'column', md: 'row' }, 
          gap: 3
        }}>
          <Box sx={{ 
            flex: 1,
            width: '100%'
          }}>
            <Paper elevation={2} sx={{ p: 2, height: '100%' }}>
              <Typography variant="h6" gutterBottom>
                Game Statistics
              </Typography>
              <Box sx={{ mt: 2 }}>
                <Typography variant="body1">
                  <strong>Total Rounds:</strong> {results.totalRounds}
                </Typography>
                <Typography variant="body1">
                  <strong>Correct Answers:</strong> {results.correctAnswers}
                </Typography>
                <Typography variant="body1">
                  <strong>Accuracy:</strong> {(results.accuracy * 100).toFixed(1)}%
                </Typography>
              </Box>
            </Paper>
          </Box>
         
          <Box sx={{ 
            flex: 1,
            width: '100%'
          }}>
            <Paper elevation={2} sx={{ p: 2, height: '100%' }}>
              <Typography variant="h6" gutterBottom>
                Session Details
              </Typography>
              <Box sx={{ mt: 2 }}>
                <Typography variant="body1">
                  <strong>Started:</strong> {formatDate(results.startTime)}
                </Typography>
                <Typography variant="body1">
                  <strong>Ended:</strong> {formatDate(results.endTime)}
                </Typography>
                <Typography variant="body1">
                  <strong>Duration:</strong> {getDuration(results.startTime, results.endTime)}
                </Typography>
              </Box>
            </Paper>
          </Box>
        </Box>
       
        <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
          <Button
            variant="contained"
            color="primary"
            size="large"
            onClick={onPlayAgain}
            sx={{ minWidth: 200 }}
          >
            Play Again
          </Button>
        </Box>
      </Paper>
    </Box>
  );
};

export default GameSummary;