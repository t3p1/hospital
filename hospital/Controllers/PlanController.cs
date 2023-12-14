using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital.Controllers
{
    public class PlanController : Controller
    {
        private readonly dental_hospitalContext _context;

        public PlanController(dental_hospitalContext context)
        {
            _context = context;
        }
        public IActionResult Plan()
        {
            var planDiagnosisData = _context.Plans
            .Join(
               _context.Diagnoses,
               plan => plan.Diagnosis,
               diagnosis => diagnosis.Id,
               (plan, diagnosis) => new Tuple<int, string, string>(plan.Id, diagnosis.Diagnosis1, plan.Treatment)
            )
            .ToList();

            return View("Views/Home/Plan.cshtml", planDiagnosisData);
        }
        public IActionResult DetailsPlan(int id)
        {
            var planDetails = (from plan in _context.Plans
                               join diagnosis in _context.Diagnoses on plan.Diagnosis equals diagnosis.Id
                               where plan.Id == id
                               select new
                               {
                                   PlanId = plan.Id,
                                   DiagnosName = diagnosis.Diagnosis1,
                                   TreatmentPlan = plan.Treatment
                               })
                     .FirstOrDefault();

            if (planDetails == null)
            {
                return NotFound();
            }

            ViewBag.PlanDetails = planDetails;

            return View("Views/Home/Appointments/Details_plan.cshtml");
        }
    }
}

