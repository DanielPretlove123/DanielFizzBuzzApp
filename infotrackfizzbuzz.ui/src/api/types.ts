export interface GameRule {
  id: number;
  divisor: number;
  outputText: string;
  isActive: boolean;
}

export interface CreateGameRuleRequest {
  divisor: number;
  outputText: string;
  isActive: boolean;
}

export interface UpdateGameRuleRequest {
  id: number;
  divisor: number;
  outputText: string;
  isActive: boolean;
}

export interface GameSession {
  sessionId: string;
  startTime: string;
}

export interface GameSessionDetails {
  sessionId: string;
  startTime: string;
  endTime?: string;
  totalRounds: number;
  correctAnswers: number;
  accuracy: number;
  isActive: boolean;
  rounds: GameRound[];
}

export interface GameRound {
  id: number;
  gameSessionId: string;
  challengeNumber: number;
  expectedAnswer: string;
  playerAnswer?: string;
  isCorrect: boolean;
  timestamp: string;
}

export interface GameChallenge {
  roundId: number;
  challengeNumber: number;
}

export interface SubmitAnswerRequest {
  sessionId: string;
  roundId: number;
  answer: string;
}

export interface SubmitAnswerResponse {
  isCorrect: boolean;
  expectedAnswer: string;
  playerAnswer?: string;
}

export interface EndGameRequest {
  sessionId: string;
}

export interface EndGameResponse {
  sessionId: string;
  startTime: string;
  endTime: string;
  totalRounds: number;
  correctAnswers: number;
  accuracy: number;
}