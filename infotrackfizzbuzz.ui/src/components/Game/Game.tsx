import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import { Container, Paper, Typography, Box, Button, CircularProgress, Alert } from '@mui/material';
import GameRules from './GameRules';
import GamePlay from './GamePlay';
import GameSummary from './GameSummary';
import { useGameRules } from '../../hooks/useGameRules';
import { useGameSession } from '../../hooks/useGameSession';
import { RootState } from '../../store';
import { GameRule } from '../../api/types';

const Game: React.FC = () => {
  const [gameStarted, setGameStarted] = useState(false);
  const [gameOver, setGameOver] = useState(false);
  const [error, setError] = useState<string | null>(null);
  
  const { rules, isLoading: rulesLoading } = useGameRules();
  const { startSession, isStarting, sessionData } = useGameSession();
  
  const currentSession = useSelector((state: RootState) => state.gameSession.currentSession);
  const sessionDetails = useSelector((state: RootState) => state.gameSession.sessionDetails);
  const gameResults = useSelector((state: RootState) => state.gameRound.gameResults);
  const gameError = useSelector((state: RootState) => state.gameSession.error);
  
  useEffect(() => {
    if (gameError) {
      setError(gameError);
    } else {
      setError(null);
    }
  }, [gameError]);

  const handleStartGame = () => {
    setError(null);
    setGameStarted(false); 
    startSession();
  };
  
  useEffect(() => {
    if (currentSession && currentSession.sessionId) {
      setGameStarted(true);
      setGameOver(false);
    } 
  }, [currentSession]);
  
  useEffect(() => {
    if (gameResults) {
      setGameOver(true);
    }
  }, [gameResults]);
  
  const handlePlayAgain = () => {
    setGameOver(false);
    setGameStarted(false);
    setError(null);
  };
  
  if (rulesLoading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', p: 5 }}>
        <CircularProgress />
      </Box>
    );
  }
  
  const activeRules = rules.filter((rule: GameRule) => rule.isActive);
  const hasActiveRules = activeRules.length > 0;
  
  return (
    <Container maxWidth="md">
      <Paper elevation={3} sx={{ p: 3, mt: 4 }}>
        <Typography variant="h4" gutterBottom>
          FizzBuzz Game
        </Typography>
        
        {error && (
          <Alert severity="error" sx={{ mb: 3 }}>
            {error}
          </Alert>
        )}
        
        {!gameStarted && (
          <>
            <GameRules rules={activeRules} />
            <Box sx={{ mt: 4, textAlign: 'center' }}>
              <Button
                variant="contained"
                color="primary"
                size="large"
                onClick={handleStartGame}
                disabled={isStarting || !hasActiveRules}
              >
                {isStarting ? <CircularProgress size={24} /> : 'Start Game'}
              </Button>
              
              {!hasActiveRules && (
                <Typography variant="body2" color="error" sx={{ mt: 2 }}>
                  At least one active game rule is required to play
                </Typography>
              )}
            </Box>
          </>
        )}
        
        {gameStarted && !gameOver && currentSession && (
          <>
            <Typography variant="body2" sx={{ mb: 2 }}>
              Session ID: {currentSession.sessionId}
            </Typography>
            <GamePlay
              sessionId={currentSession.sessionId}
              rules={activeRules}
            />
          </>
        )}
        
        {isStarting && (
          <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', my: 4 }}>
            <CircularProgress />
            <Typography sx={{ mt: 2 }}>Creating game session...</Typography>
          </Box>
        )}
        
        {gameOver && gameResults && (
          <GameSummary
            results={gameResults}
            onPlayAgain={handlePlayAgain}
          />
        )}
      </Paper>
    </Container>
  );
};

export default Game;