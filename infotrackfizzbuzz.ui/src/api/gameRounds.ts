import axios from 'axios';
import { 
  GameChallenge, 
  SubmitAnswerRequest, 
  SubmitAnswerResponse, 
  EndGameRequest, 
  EndGameResponse 
} from './types';
import { getOrGenerateUuid } from '../utils/uuid';

const API_URL = 'https://localhost:5001/api/GameRounds';


export const getChallenge = async (sessionId?: string): Promise<GameChallenge> => {
  const validSessionId = getOrGenerateUuid(sessionId);
  const response = await axios.get<GameChallenge>(
    `${API_URL}/challenge`, 
    { 
      params: { sessionId: validSessionId }
    }
  );
  
  return response.data;
};

export const submitAnswer = async (request: SubmitAnswerRequest): Promise<SubmitAnswerResponse> => {
  const response = await axios.post<SubmitAnswerResponse>(`${API_URL}/submit`, request);
  return response.data;
};

export const endGame = async (request: EndGameRequest): Promise<EndGameResponse> => {
  const response = await axios.post<EndGameResponse>(`${API_URL}/end`, request);
  return response.data;
};