using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Healthorama.Models;

namespace Healthorama.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterPatient()
        {
            PATIENT patient = new PATIENT();
            return View(patient);
        }

        [HttpPost]
        public ActionResult RegisterPatient(PATIENT patient)
        {
            using (HealthoramaEntities entities = new HealthoramaEntities())
            { 
                entities.PATIENT.Add(patient);
                entities.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration successful";
            return View("RegisterPatient", new PATIENT());
        }

        [HttpGet]
        public ActionResult RegisterDoctor()
        { 
            DOCTOR doctor = new DOCTOR();
            return View(doctor);
        }

        [HttpPost]
        public ActionResult RegisterDoctor(DOCTOR doctor)
        {
            using (HealthoramaEntities entities = new HealthoramaEntities())
            {
                entities.DOCTOR.Add(doctor);
                entities.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration successful";
            return View("RegisterDoctor", new DOCTOR());
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Admin", "Home");
        }
    }
}