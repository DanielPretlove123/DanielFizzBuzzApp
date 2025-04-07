using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrackFizzBuzz.Domain.Entities
{
    public class GameRule : BaseEntity<int>, IAggregateRoot
    {
        public int Divisor { get; set; }
        public string OutputText { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
