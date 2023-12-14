using System;
using System.Collections.Generic;

namespace hospital
{
    public partial class Visit
    {
        public Visit()
        {
            DentalHos = new HashSet<DentalHo>();
        }

        public int Id { get; set; }
        public DateTime Stamp { get; set; }
        public int Plan { get; set; }

        public virtual Plan PlanNavigation { get; set; } = null!;
        public virtual ICollection<DentalHo> DentalHos { get; set; }
    }
}
