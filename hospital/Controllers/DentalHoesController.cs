using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hospital;

namespace hospital.Controllers
{
    public class DentalHoesController : Controller
    {
        private readonly dental_hospitalContext _context;

        public DentalHoesController(dental_hospitalContext context)
        {
            _context = context;
        }
        public IActionResult Details(int visitId, int doctorId, int patientId)
        {

            var visit = _context.Visits
                .Include(v => v.PlanNavigation)
                    .ThenInclude(p => p.DiagnosisNavigation)
                .FirstOrDefault(v => v.Id == visitId);
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == doctorId);
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            ViewBag.Visit = visit;
            ViewBag.Doctor = doctor;
            ViewBag.Patient = patient;


            return View("Views/Home/Appointments/Details.cshtml");
        }

        public IActionResult Index(string? timeFilter, int? priceFilter, string? search, DateTime? fromDateFilter, DateTime? toDateFilter)
        {
            var query = _context.DentalHos
                .Include(d => d.DoctorNavigation)
                .Include(d => d.PatientNavigation)
                .Include(d => d.VisitNavigation)
                    .ThenInclude(v => v.PlanNavigation)
                        .ThenInclude(p => p.DiagnosisNavigation)
                .AsQueryable();


            if (priceFilter.HasValue)
            {
                query = priceFilter.Value == 1
                    ? query.OrderBy(d => d.VisitNavigation.PlanNavigation.DiagnosisNavigation.Price)
                    : query.OrderByDescending(d => d.VisitNavigation.PlanNavigation.DiagnosisNavigation.Price);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.DoctorNavigation.Name.Contains(search) ||
                    d.PatientNavigation.Name.Contains(search)
                );
            }

            var dentalHoData = query.ToList();
            return View("Views/Home/Index.cshtml", dentalHoData);
        }

        public IActionResult AddVisitData(int patientId, int doctorId, DateTime visitDate, int diagnosisId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == doctorId);
            var diagnosis = _context.Diagnoses.FirstOrDefault(diag => diag.Id == diagnosisId);

            if (patient != null && doctor != null && diagnosis != null)
            {
                DateTime currentTimeUtc = DateTime.UtcNow;
                DateTime currentTimeUtcNow= currentTimeUtc.AddHours(5);
                var newVisit = new Visit
                {
                    Stamp = currentTimeUtcNow,
                    Plan = diagnosis.Id
                };
                _context.Visits.Add(newVisit);
                _context.SaveChanges();
                var newDentalHo = new DentalHo
                {
                    Visit = newVisit.Id,
                    Patient = patient.Id,
                    Doctor = doctor.Id
                };
                _context.DentalHos.Add(newDentalHo);
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Index");
        }

    }
}
