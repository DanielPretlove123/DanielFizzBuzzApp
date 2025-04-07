import React, { useState } from "react";
import {
  Box,
  TextField,
  Button,
  Switch,
  FormControlLabel,
  Grid,
  Alert,
} from "@mui/material";
import { CreateGameRuleRequest } from "../../api/types";

interface AddRuleFormProps {
  onSubmit: (rule: CreateGameRuleRequest) => void;
}

const AddRuleForm: React.FC<AddRuleFormProps> = ({ onSubmit }) => {
  const [divisor, setDivisor] = useState<number | "">("");
  const [outputText, setOutputText] = useState("");
  const [isActive, setIsActive] = useState(true);
  const [error, setError] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (!divisor) {
      setError("Please enter a divisor.");
      return;
    }

    if (divisor <= 0) {
      setError("Divisor must be greater than 0.");
      return;
    }

    if (!outputText) {
      setError("Please enter output text.");
      return;
    }

    const newRule: CreateGameRuleRequest = {
      divisor: Number(divisor),
      outputText,
      isActive,
    };

    onSubmit(newRule);
    resetForm();
  };

  const resetForm = () => {
    setDivisor("");
    setOutputText("");
    setIsActive(true);
    setError("");
  };

  return (
    <Box component="form" onSubmit={handleSubmit} noValidate>
      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {error}
        </Alert>
      )}

      <Box sx={{ display: "flex", flexWrap: "wrap", gap: 2 }}>
        <Box sx={{ flexBasis: { xs: "100%", sm: "30%" } }}>
          <TextField
            fullWidth
            required
            label="Divisor"
            type="number"
            value={divisor}
            onChange={(e) =>
              setDivisor(e.target.value ? Number(e.target.value) : "")
            }
            inputProps={{ min: 1 }}
          />
        </Box>

        <Box sx={{ flexBasis: { xs: "100%", sm: "30%" } }}>
          <TextField
            fullWidth
            required
            label="Output Text"
            value={outputText}
            onChange={(e) => setOutputText(e.target.value)}
          />
        </Box>

        <Box
          sx={{
            flexBasis: { xs: "100%", sm: "30%" },
            display: "flex",
            alignItems: "center",
          }}
        >
          <FormControlLabel
            control={
              <Switch
                checked={isActive}
                onChange={(e) => setIsActive(e.target.checked)}
              />
            }
            label="Active"
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            sx={{ ml: 2 }}
          >
            Add Rule
          </Button>
        </Box>
      </Box>
    </Box>
  );
};

export default AddRuleForm;