using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos
{
    public class SubmitGameChallengeAnswerResponseDto
    {
        public bool IsCorrect { get; set; }
        public string ExpectedAnswer { get; set; } = string.Empty;
        public string? PlayerAnswer { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GameRound, SubmitGameChallengeAnswerResponseDto>();
            }
        }
    }
}
