using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TestGitHubActions.Controllers
{
    public class HospitalController : Controller
    {
        private readonly Models.HospitalDBContext db = new Models.HospitalDBContext();

        // GET: Hospital
        public ActionResult HospitalInfo(string id)
        {
            var hospitals = from h in db.Hospitals
                            select h;

            if (!string.IsNullOrEmpty(id))
            {
                hospitals = hospitals.Where(s => s.HospitalId.Equals(id));
            }

            return View(hospitals);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Hospital hospital = db.Hospitals.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }
    }
}