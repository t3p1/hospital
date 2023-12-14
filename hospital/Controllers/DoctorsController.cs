using Microsoft.AspNetCore.Mvc;

namespace hospital.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly dental_hospitalContext _context;

        public DoctorsController(dental_hospitalContext context)
        {
            _context = context;
        }
        public IActionResult Doctors()
        {
            var doctor = _context.Doctors
                                  .Select(d => new Doctor 
                                  {
                                      Id = d.Id,
                                      Name = d.Name,
                                      Phone = d.Phone
                                  })
                                  .ToList();

            return View("Views/Home/Doctors.cshtml", doctor);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Add(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor); 
        }
        [HttpPost]
        public IActionResult Edit(Doctor editedDoctor)
        {
            if (ModelState.IsValid)
            {
                var doctorToUpdate = _context.Doctors.Find(editedDoctor.Id);

                if (doctorToUpdate == null)
                {
                    return NotFound();
                }
                doctorToUpdate.Name = editedDoctor.Name;
                doctorToUpdate.Phone = editedDoctor.Phone;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(editedDoctor);
        }
    }
}
