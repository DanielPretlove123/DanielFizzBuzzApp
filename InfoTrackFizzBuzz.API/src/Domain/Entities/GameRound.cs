using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrackFizzBuzz.Domain.Entities
{
    public class GameRound : BaseEntity<int>, IAggregateRoot
    {
        public Guid GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; } = null!;
        public int ChallengeNumber { get; set; }
        public string ExpectedAnswer { get; set; } = string.Empty;
        public string? PlayerAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    }
}
