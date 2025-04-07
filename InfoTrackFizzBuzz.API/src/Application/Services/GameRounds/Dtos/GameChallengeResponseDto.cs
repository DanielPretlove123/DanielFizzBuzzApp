using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos
{
    public class GameChallengeResponseDto
    {
        public int RoundId { get; set; }
        public int ChallengeNumber { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GameRound, GameChallengeResponseDto>();
            }
        }
    }
}
