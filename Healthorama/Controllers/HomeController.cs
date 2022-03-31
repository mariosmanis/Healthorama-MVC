using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Healthorama.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace Healthorama.Controllers
{
    public class HomeController : Controller
    {  
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Doctor()
        {
            return View();
        }

        public ActionResult Patient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthorizeAdmin(ADMIN admin)
        {          
            using (HealthoramaEntities database = new HealthoramaEntities())
            {
                var adminDetails = database.ADMIN.Where(x => x.adminUsername == admin.adminUsername && x.adminPassword == admin.adminPassword).FirstOrDefault();
                if (adminDetails == null)
                {
                   
                    return View("Admin", admin);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
        }

        [HttpPost]
        public ActionResult AuthorizeDoctor(DOCTOR doctor)
        {
            using (HealthoramaEntities database = new HealthoramaEntities())
            { 
                var doctorDetails = database.DOCTOR.Where(x => x.doctorAMKA == doctor.doctorAMKA && x.doctorPassword == doctor.doctorPassword).FirstOrDefault();
                if (doctorDetails == null)
                {
                    return View("Doctor", doctor);
                }
                else
                {
                    Session["doctorAMKA"] = doctorDetails.doctorAMKA;
                    return RedirectToAction("Index", "Doctor");
                }
            }
        }

        [HttpPost]
        public ActionResult AuthorizePatient(PATIENT patient)
        {

            using (HealthoramaEntities database = new HealthoramaEntities())
            {
                var patientDetails = database.PATIENT.Where(x => x.patientAMKA == patient.patientAMKA && x.patientPassword == patient.patientPassword).FirstOrDefault();
                if (patientDetails == null)
                {                   
                    return View("Patient", patient);
                }
                else
                {
                    Session["patientAMKA"] = patientDetails.patientAMKA;
                    return RedirectToAction("Index", "Patient");
                }
            }
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


    }
}