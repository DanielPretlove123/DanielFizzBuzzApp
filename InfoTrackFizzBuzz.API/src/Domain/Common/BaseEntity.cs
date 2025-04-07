using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTrackFizzBuzz.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        private TKey _id = default!;

        public TKey Id
        {
            get => _id;
            set
            {
                if (typeof(TKey) == typeof(Guid) && value is Guid guidValue && guidValue == Guid.Empty)
                {
                    _id = (TKey)(object)Guid.NewGuid();
                }
                else
                {
                    _id = value;
                }
            }
        }

        protected BaseEntity()
        {
            if (typeof(TKey) == typeof(Guid))
            {
                Id = (TKey)(object)Guid.NewGuid();
            }
        }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
