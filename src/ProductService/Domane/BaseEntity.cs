using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Update { get; set; }

        public BaseEntity()
        {
            Created = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

            Update = Created;
        }
    }
}
