using System;
using System.Collections.Generic;

namespace hospital
{
    public partial class Patient
    {
        public Patient()
        {
            DentalHos = new HashSet<DentalHo>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<DentalHo> DentalHos { get; set; }
    }
}
