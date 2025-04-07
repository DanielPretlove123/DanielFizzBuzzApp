using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos
{
    public class EndGameResponseDto
    {
        public Guid SessionId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int TotalRounds { get; set; }
        public int CorrectAnswers { get; set; }
        public double Accuracy { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GameSession, EndGameResponseDto>()
                    .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Accuracy, opt => opt.Ignore());
            }
        }
    }
}
