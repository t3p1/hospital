using System;
using System.Collections.Generic;

namespace hospital
{
    public partial class DentalHo
    {
        public int Visit { get; set; }
        public int Doctor { get; set; }
        public int Patient { get; set; }

        public virtual Doctor DoctorNavigation { get; set; }
        public virtual Patient PatientNavigation { get; set; }
        public virtual Visit VisitNavigation { get; set; }
    }
}
