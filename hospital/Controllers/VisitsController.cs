using Microsoft.AspNetCore.Mvc;

namespace hospital.Controllers
{
    public class VisitsController : Controller
    {
        private readonly dental_hospitalContext _context;

        public VisitsController(dental_hospitalContext context)
        {
            _context = context;
        }
        public IActionResult Visits()
        {
            var visit = _context.Visits
                                  .Select(d => new Visit
                                  {
                                      Id = d.Id,
                                      Stamp = d.Stamp,
                                      Plan = d.Plan,
                                  })
                                  .ToList();

            return View("Views/Home/Visits.cshtml", visit);
        }
    }
}
