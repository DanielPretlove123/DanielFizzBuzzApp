import React, { useState } from 'react';
import {
  ListItem,
  ListItemText,
  IconButton,
  TextField,
  Switch,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Paper,
  Typography
} from '@mui/material';
import { Edit as EditIcon, Delete as DeleteIcon, Save as SaveIcon, Cancel as CancelIcon } from '@mui/icons-material';
import { GameRule, UpdateGameRuleRequest } from '../../api/types';

interface GameRuleItemProps {
  rule: GameRule;
  onUpdate: (rule: UpdateGameRuleRequest) => void;
  onDelete: (id: number) => void;
}

const GameRuleItem: React.FC<GameRuleItemProps> = ({ rule, onUpdate, onDelete }) => {
  const [editing, setEditing] = useState(false);
  const [divisor, setDivisor] = useState(rule.divisor);
  const [outputText, setOutputText] = useState(rule.outputText);
  const [isActive, setIsActive] = useState(rule.isActive);
  const [confirmDelete, setConfirmDelete] = useState(false);

  const handleSave = () => {
    onUpdate({
      id: rule.id,
      divisor,
      outputText,
      isActive
    });
    setEditing(false);
  };

  const handleCancel = () => {
    setDivisor(rule.divisor);
    setOutputText(rule.outputText);
    setIsActive(rule.isActive);
    setEditing(false);
  };

  const handleConfirmDelete = () => {
    onDelete(rule.id);
    setConfirmDelete(false);
  };

  return (
    <>
      <Paper variant="outlined" sx={{ mb: 2, p: 1 }}>
        {editing ? (
          <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 2, alignItems: 'center' }}>
            <Box sx={{ flexBasis: { xs: '100%', sm: '22%' } }}>
              <TextField
                label="Divisor"
                type="number"
                fullWidth
                value={divisor}
                onChange={(e) => setDivisor(Number(e.target.value))}
                inputProps={{ min: 1 }}
              />
            </Box>
            <Box sx={{ flexBasis: { xs: '100%', sm: '38%' } }}>
              <TextField
                label="Output Text"
                fullWidth
                value={outputText}
                onChange={(e) => setOutputText(e.target.value)}
              />
            </Box>
            <Box sx={{ flexBasis: { xs: '50%', sm: '15%' }, display: 'flex', alignItems: 'center' }}>
              <Typography variant="body2" sx={{ mr: 1 }}>Active:</Typography>
              <Switch
                checked={isActive}
                onChange={(e) => setIsActive(e.target.checked)}
              />
            </Box>
            <Box sx={{ flexBasis: { xs: '50%', sm: '15%' }, display: 'flex', justifyContent: 'flex-end' }}>
              <IconButton color="primary" onClick={handleSave}>
                <SaveIcon />
              </IconButton>
              <IconButton color="secondary" onClick={handleCancel}>
                <CancelIcon />
              </IconButton>
            </Box>
          </Box>
        ) : (
          <ListItem
            disableGutters
            secondaryAction={
              <Box>
                <IconButton edge="end" onClick={() => setEditing(true)}>
                  <EditIcon />
                </IconButton>
                <IconButton edge="end" onClick={() => setConfirmDelete(true)}>
                  <DeleteIcon />
                </IconButton>
              </Box>
            }
          >
            <ListItemText
              primary={
                <Typography>
                  If divisible by <strong>{rule.divisor}</strong>, display <strong>"{rule.outputText}"</strong>
                </Typography>
              }
              secondary={
                <Typography variant="body2" color={rule.isActive ? 'success.main' : 'error.main'}>
                  {rule.isActive ? 'Active' : 'Inactive'}
                </Typography>
              }
            />
          </ListItem>
        )}
      </Paper>

      <Dialog open={confirmDelete} onClose={() => setConfirmDelete(false)}>
        <DialogTitle>Delete Rule</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are you sure you want to delete this rule? This action cannot be undone.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setConfirmDelete(false)}>Cancel</Button>
          <Button onClick={handleConfirmDelete} color="error">Delete</Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default GameRuleItem;