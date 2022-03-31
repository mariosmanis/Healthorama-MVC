using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Healthorama.Models;

namespace Healthorama.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
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
            return RedirectToAction("Patient", "Home");
        }


        [HttpGet]
        public ActionResult SearchAppointment()
        {
            DOCTOR doctor = new DOCTOR();
            return View(doctor);
        }

        [HttpGet]
        public ActionResult GetDoctorAMKA()
        {
            DOCTOR doctor = new DOCTOR();
            return View(doctor);
        }
        
        [HttpPost]
        public ActionResult GetDoctorAMKA(DOCTOR doctor) 
        {
            Session["doctorAMKA"] = doctor.doctorAMKA;
            return RedirectToAction("GetAppointmentDate", "Patient");
        }
        
        public ActionResult GetDoctorName()
        {
            List<SelectListItem> doctor_names = new List<SelectListItem>();
            using (HealthoramaEntities entities = new HealthoramaEntities())
            {
                string specialty = Session["doctorSpec"].ToString();
                var res = entities.DOCTOR.Where(x => x.doctorSpeciality == specialty);
                foreach(DOCTOR doc in res) 
                {
                    doctor_names.Add(new SelectListItem 
                    { 
                        Text = doc.doctorName + " " + doc.doctorSurname,
                        Value = doc.doctorAMKA
                    });
                }
                ViewBag.name = doctor_names;
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult fetchAppointments(DOCTOR doctor)
        {
            Session["doctorSpec"] = doctor.doctorSpeciality;
            return RedirectToAction("GetDoctorName", "Patient");
        }

        public JsonResult GetAppointments()
        {
            using (HealthoramaEntities entities = new HealthoramaEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                String patAMKA = Session["patientAMKA"].ToString();
                var appointments = entities.APPOINTMENT.Where(model => model.patientAppointmentAMKA == patAMKA).ToList();
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

        [HttpGet]
        public ActionResult GetAppointmentDate()
        {
            DOCTOR_AVAILABILITY doctor_availability = new DOCTOR_AVAILABILITY();
            return View(doctor_availability);
        }


        [HttpPost]
        public ActionResult GetAppointmentDate(DOCTOR_AVAILABILITY doctor_availability)
        {

            List<String> amkas = new List<String>();
            List<DateTime> start = new List<DateTime>();
            List<DateTime> end = new List<DateTime>();

            String spec = Session["doctorSpec"].ToString();
            DateTime date = doctor_availability.availabilityStart;
            String patient_AMKA = Session["patientAMKA"].ToString();
            String doctor_AMKA = Session["doctorAMKA"].ToString();

            using (HealthoramaEntities database = new HealthoramaEntities())
            {
                               
                    var results = database.DOCTOR_AVAILABILITY.Where(x => x.doctorAMKA == doctor_AMKA).ToList();

                    foreach (DOCTOR_AVAILABILITY d in results)
                    {
                        if (date.CompareTo(d.availabilityStart) == 1 && date.CompareTo(d.availabilityEnd) == -1)
                        {
                            var r = database.APPOINTMENT.Where(x => x.appointmentTime == date && x.doctorAppointmentAMKA == doctor_AMKA).ToList();

                            if (r.Count() == 0)
                            {
                                APPOINTMENT appointment = new APPOINTMENT();
                                appointment.appointmentTime = date;
                                appointment.doctorAppointmentAMKA = doctor_AMKA;
                                appointment.patientAppointmentAMKA = patient_AMKA;
                                database.APPOINTMENT.Add(appointment);
                                database.SaveChanges();
                            }
                        }
                    }
               
                return View("SearchAppointment");
            }




        }
    }
}