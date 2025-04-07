using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameSessions.Dtos
{
    public class GameSessionResponseDto
    {
        public Guid SessionId { get; set; }
        public DateTimeOffset StartTime { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GameSession, GameSessionResponseDto>()
                    .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.Id));
            }
        }
    }
}
