import React from 'react';
import { Box, Typography, List, ListItem, ListItemIcon, ListItemText, Paper } from '@mui/material';
import { Rule as RuleIcon } from '@mui/icons-material';
import { GameRule } from '../../api/types';

interface GameRulesProps {
  rules: GameRule[];
}

const GameRules: React.FC<GameRulesProps> = ({ rules }) => {
  return (
    <Box sx={{ my: 3 }}>
      <Typography variant="h5" gutterBottom>
        Game Rules
      </Typography>
      
      <Paper variant="outlined" sx={{ p: 2, bgcolor: 'background.default' }}>
        <Typography variant="body1" paragraph>
          Welcome to FizzBuzz! You'll be given a random number. Your task is to apply the following rules:
        </Typography>
        
        <List>
          {rules.map((rule) => (
            <ListItem key={rule.id} disableGutters>
              <ListItemIcon>
                <RuleIcon />
              </ListItemIcon>
              <ListItemText 
                primary={
                  <Typography>
                    If the number is divisible by <strong>{rule.divisor}</strong>, 
                    type <strong>"{rule.outputText}"</strong>
                  </Typography>
                } 
              />
            </ListItem>
          ))}
          <ListItem disableGutters>
            <ListItemIcon>
              <RuleIcon />
            </ListItemIcon>
            <ListItemText 
              primary={
                <Typography>
                  If multiple rules apply, combine the outputs (e.g., "FizzBuzz")
                </Typography>
              } 
            />
          </ListItem>
          <ListItem disableGutters>
            <ListItemIcon>
              <RuleIcon />
            </ListItemIcon>
            <ListItemText 
              primary={
                <Typography>
                  If no rules apply, type the number as is
                </Typography>
              } 
            />
          </ListItem>
        </List>
      </Paper>
    </Box>
  );
};

export default GameRules;