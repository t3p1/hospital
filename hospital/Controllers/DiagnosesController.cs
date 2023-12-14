using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital.Controllers
{
    public class DiagnosesController : Controller
    {
        private readonly dental_hospitalContext _context;
        public DiagnosesController(dental_hospitalContext context)
        {
            _context = context;
        }
        public IActionResult Diagnoses()
        {
            var diagnosis = _context.Diagnoses
                                  .Select(d => new Diagnosis
                                  {
                                      Id = d.Id,
                                      Diagnosis1 = d.Diagnosis1,
                                      Description = d.Description,
                                      Price = d.Price
                                  })
                                  .ToList();

            return View("Views/Home/Diagnoses.cshtml", diagnosis);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                _context.Diagnoses.Add(diagnosis);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnosis);
        }
        public IActionResult DetailsDiagnosis(int id)
        {
            var diagnosis = _context.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            return View("Views/Home/Appointments/Details_diagnoses.cshtml", diagnosis);
        }
    }
}
