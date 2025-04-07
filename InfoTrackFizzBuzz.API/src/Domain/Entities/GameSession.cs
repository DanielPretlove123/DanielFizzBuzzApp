using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrackFizzBuzz.Domain.Entities
{
    public class GameSession : BaseEntity<Guid>, IAggregateRoot
    {
        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? EndTime { get; set; }
        public int TotalRounds { get; set; } = 0;
        public int CorrectAnswers { get; set; } = 0;
        public virtual ICollection<GameRound> Rounds { get; set; } = new List<GameRound>();
    }
}
