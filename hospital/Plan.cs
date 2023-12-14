using System;
using System.Collections.Generic;

namespace hospital
{
    public partial class Plan
    {
        public Plan()
        {
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public int Diagnosis { get; set; }
        public string Treatment { get; set; } = null!;

        public virtual Diagnosis DiagnosisNavigation { get; set; } = null!;
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
