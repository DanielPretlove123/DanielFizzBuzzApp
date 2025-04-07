import React from 'react';
import { 
  Box, 
  List, 
  CircularProgress,
  Alert,
  Typography
} from '@mui/material';
import GameRuleItem from './GameRuleItem';
import { GameRule, UpdateGameRuleRequest } from '../../api/types';

interface GameRuleListProps {
  rules: GameRule[];
  isLoading: boolean;
  onUpdate: (rule: UpdateGameRuleRequest) => void;
  onDelete: (id: number) => void;
}

const GameRuleList: React.FC<GameRuleListProps> = ({ 
  rules, 
  isLoading, 
  onUpdate, 
  onDelete 
}) => {
  if (isLoading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', p: 3 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (rules.length === 0) {
    return (
      <Alert severity="info">
        No rules found. Add your first rule to get started.
      </Alert>
    );
  }

  return (
    <Box>
      <List>
        {rules.map((rule) => (
          <GameRuleItem 
            key={rule.id} 
            rule={rule} 
            onUpdate={onUpdate} 
            onDelete={onDelete} 
          />
        ))}
      </List>
      <Box sx={{ mt: 2 }}>
        <Typography variant="body2" color="text.secondary">
          Note: The game requires at least one active rule.
        </Typography>
      </Box>
    </Box>
  );
};

export default GameRuleList;