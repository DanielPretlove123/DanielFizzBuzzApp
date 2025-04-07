import axios from 'axios';
import { GameSession, GameSessionDetails } from './types';

const API_URL = 'https://localhost:5001/api/GameSessions';

export const startGameSession = async (): Promise<GameSession> => {
  const response = await axios.post<GameSession>(`${API_URL}/start`);
  return response.data;
};

export const getGameSessionDetails = async (sessionId: string): Promise<GameSessionDetails> => {
  const response = await axios.get<GameSessionDetails>(`${API_URL}/${sessionId}`);
  return response.data;
};