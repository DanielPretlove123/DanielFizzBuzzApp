import React from 'react';
import { Box, Typography, Paper, Container, Divider } from '@mui/material';
import GameRuleList from './GameRuleList';
import AddRuleForm from './AddRuleForm';
import { useGameRules } from '../../hooks/useGameRules';

const AdminPanel: React.FC = () => {
  const { 
    rules, 
    isLoading, 
    createRule, 
    updateRule, 
    deleteRule 
  } = useGameRules();

  return (
    <Container maxWidth="md">
      <Paper elevation={3} sx={{ p: 3, mt: 4 }}>
        <Typography variant="h4" gutterBottom>
          FizzBuzz Game Admin Panel
        </Typography>
        <Typography variant="body1" color="text.secondary" paragraph>
          Manage game rules for the FizzBuzz game. Add, edit, or delete rules as needed.
        </Typography>
        
        <Box sx={{ my: 3 }}>
          <Typography variant="h5" gutterBottom>
            Add New Rule
          </Typography>
          <AddRuleForm onSubmit={createRule} />
        </Box>
        
        <Divider sx={{ my: 3 }} />
        
        <Box sx={{ my: 3 }}>
          <Typography variant="h5" gutterBottom>
            Game Rules
          </Typography>
          <GameRuleList 
            rules={rules} 
            isLoading={isLoading} 
            onUpdate={updateRule}
            onDelete={deleteRule}
          />
        </Box>
      </Paper>
    </Container>
  );
};

export default AdminPanel;