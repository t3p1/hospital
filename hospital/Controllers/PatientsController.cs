using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital.Controllers
{
    public class PatientsController : Controller
    {
        private readonly dental_hospitalContext _context;

        public PatientsController(dental_hospitalContext context)
        {
            _context = context;
        }
        public IActionResult Patients()
        {
            var patient = _context.Patients
                                  .Select(d => new Patient
                                  {
                                      Id = d.Id,
                                      Name = d.Name,
                                      Address = d.Address,
                                      Phone = d.Phone
                                  })
                                  .ToList();

            return View("Views/Home/Patients.cshtml", patient);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        public IActionResult Delete(int id)
        {
            var patient = _context.Patients
                                .Include(p => p.DentalHos)
                                .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }
            foreach (var dentalHo in patient.DentalHos.ToList())
            {
                _context.DentalHos.Remove(dentalHo);
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Edit(Patient editedPatient)
        {
            if (ModelState.IsValid)
            {
                var patientToUpdate = _context.Patients.Find(editedPatient.Id);

                if (patientToUpdate == null)
                {
                    return NotFound();
                }
                patientToUpdate.Name = editedPatient.Name;
                patientToUpdate.Address = editedPatient.Address;
                patientToUpdate.Phone = editedPatient.Phone;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(editedPatient);
        }
    }
}
