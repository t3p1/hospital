using System;
using System.Collections.Generic;

namespace hospital
{
    public partial class Diagnosis
    {
        public Diagnosis()
        {
            Plans = new HashSet<Plan>();
        }

        public int Id { get; set; }
        public string Diagnosis1 { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
