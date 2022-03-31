using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Healthorama.Models;
using Newtonsoft.Json;

namespace Healthorama.Controllers
{
    public class DoctorController : Controller
    {
        private HealthoramaEntities database = new HealthoramaEntities();

        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAppointments()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session["doctorAMKA"] = null;
            return RedirectToAction("Doctor", "Home");
        }

        [HttpGet]
        public ActionResult RegisterAvailability()
        {
            DOCTOR_AVAILABILITY availability = new DOCTOR_AVAILABILITY();
            return View(availability);
        }

        [HttpPost]
        public ActionResult RegisterAvailability(DOCTOR_AVAILABILITY availability)
        {
            using (HealthoramaEntities enitities = new HealthoramaEntities())
            {
                enitities.DOCTOR_AVAILABILITY.Add(availability);
                enitities.SaveChanges();
            }
            ModelState.Clear();
            return View("RegisterAvailability", new DOCTOR_AVAILABILITY());
        }

        public JsonResult GetAppointments()
        {
            using (HealthoramaEntities entities = new HealthoramaEntities())
            {               
                entities.Configuration.LazyLoadingEnabled = false;
                String docAMKA = Session["doctorAMKA"].ToString();
                var appointments = entities.APPOINTMENT.Where(model => model.doctorAppointmentAMKA == docAMKA).ToList();                
                return new JsonResult { Data = appointments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult Remove(string id)
        {    
            var status = false;           
            Int16 c = Convert.ToInt16(id);
            using (HealthoramaEntities entities = new HealthoramaEntities())
            {     
                var v = entities.APPOINTMENT.Where(model => model.appointmentID == c).FirstOrDefault();               
                if (v != null)
                { 
                    entities.APPOINTMENT.Remove(v);
                    entities.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}