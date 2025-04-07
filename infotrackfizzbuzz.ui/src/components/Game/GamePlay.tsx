// src/components/Game/GamePlay.tsx
import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  TextField,
  Button,
  Paper,
  Alert,
  Fade,
  CircularProgress,
  Divider
} from '@mui/material';
import { useGameRound } from '../../hooks/useGameRound';
import Challenge from './Challenge';
import { GameRule } from '../../api/types';

interface GamePlayProps {
  sessionId: string;
  rules: GameRule[];
}

const GamePlay: React.FC<GamePlayProps> = ({ sessionId, rules }) => {
  const [answer, setAnswer] = useState('');
  const [feedback, setFeedback] = useState<{ correct: boolean; message: string } | null>(null);
  const [error, setError] = useState<string | null>(null);
  
  useEffect(() => {
    if (!sessionId) {
      setError('Invalid session ID. Cannot start game without a valid session ID.');
    } else {
      setError(null);
    }
  }, [sessionId]);
 
  const {
    challenge,
    challengeLoading,
    submitAnswer,
    submitAnswerResult,
    endGame,
    isSubmitting,
    isEnding,
    submitError,
    endError
  } = useGameRound(sessionId);
  
  useEffect(() => {
    if (submitError) {
      setError(submitError.toString());
    } else if (endError) {
      setError(endError.toString());
    } else {
      setError(null);
    }
  }, [submitError, endError]);

  useEffect(() => {
    if (challenge) {
      setFeedback(null);
      setAnswer('');
    }
  }, [challenge]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
   
    if (!sessionId || !challenge || !answer.trim()) {
      return;
    }
    
    submitAnswer({
      sessionId,
      roundId: challenge.roundId,
      answer: answer.trim()
    });
  };

  const handleEndGame = () => {
    if (!sessionId) {
      return;
    }
    
    endGame({ sessionId });
  };

  const handleFeedback = (isCorrect: boolean, expected: string, submitted: string) => {
    if (isCorrect) {
      setFeedback({
        correct: true,
        message: 'Correct! Well done.'
      });
    } else {
      setFeedback({
        correct: false,
        message: `Incorrect. The answer was "${expected}", but you submitted "${submitted || '(empty)'}"` 
      });
    }
  };

  useEffect(() => {
    if (submitAnswerResult) {
      const { isCorrect, expectedAnswer, playerAnswer } = submitAnswerResult;
      handleFeedback(isCorrect, expectedAnswer, playerAnswer || '');
    }
  }, [submitAnswerResult]);
  if (error) {
    return (
      <Box sx={{ mt: 3 }}>
        <Alert severity="error" sx={{ mb: 2 }}>
          {error}
        </Alert>
        <Button 
          variant="contained" 
          color="primary" 
          onClick={() => window.location.reload()}
        >
          Reload Game
        </Button>
      </Box>
    );
  }

  if (challengeLoading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', p: 5 }}>
        <CircularProgress />
        <Typography sx={{ ml: 2 }}>Loading challenge...</Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ mt: 3 }}>
      <Paper variant="outlined" sx={{ p: 3, mb: 3 }}>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <Box>
            <Typography variant="h5" gutterBottom>
              Game in Progress (Session: {sessionId})
            </Typography>
            <Divider sx={{ mb: 2 }} />
          </Box>
         
          {challenge ? (
            <Box>
              <Challenge number={challenge.challengeNumber} />
            </Box>
          ) : (
            <Alert severity="warning">
              No challenge available. Please wait or try restarting the game.
            </Alert>
          )}
         
          <Box component="form" onSubmit={handleSubmit} sx={{ display: 'flex', alignItems: 'center' }}>
            <TextField
              fullWidth
              label="Your Answer"
              value={answer}
              onChange={(e) => setAnswer(e.target.value)}
              disabled={isSubmitting || isEnding || !challenge}
              sx={{ mr: 2 }}
            />
            <Button
              type="submit"
              variant="contained"
              color="primary"
              disabled={isSubmitting || isEnding || !answer.trim() || !challenge}
            >
              {isSubmitting ? <CircularProgress size={24} /> : 'Submit'}
            </Button>
          </Box>
         
          {feedback && (
            <Box>
              <Fade in={!!feedback}>
                <Alert severity={feedback.correct ? 'success' : 'error'}>
                  {feedback.message}
                </Alert>
              </Fade>
            </Box>
          )}
         
          <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
            <Button
              variant="outlined"
              color="secondary"
              onClick={handleEndGame}
              disabled={isEnding}
              sx={{ mt: 2 }}
            >
              {isEnding ? <CircularProgress size={24} /> : 'End Game'}
            </Button>
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default GamePlay;