using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using InfoTrackFizzBuzz.Domain.Common;

namespace InfoTrackFizzBuzz.Application.Interfaces
{
    public interface IRepository<T> : IReadRepository<T>, IRepositoryBase<T> where T : class, IAggregateRoot
    {

    }
}
