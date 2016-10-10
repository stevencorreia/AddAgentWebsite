using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AddCampaignB2b.Models;
using PagedList;

namespace AddCampaignB2b.Controllers
{
    public class AddAgentController : Controller
    {
        private pd_cmcEntities db = new pd_cmcEntities();

        //
        // GET: /AddAgent/

        public ActionResult Index()
        {
            ViewBag.ServiceAddress = WebConfigurationManager.AppSettings["AddCampginService"];
            return View();
        }

        //
        // GET: /AddAgent/Details/5

        public ActionResult Details(int id = 0)
        {
            AGENT_CAMP agent_camp = db.AGENT_CAMP.Find(id);
            if (agent_camp == null)
            {
                return HttpNotFound();
            }
            return View(agent_camp);
        }

        //
        // GET: /AddAgent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AddAgent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AGENT_CAMP agent_camp)
        {
            if (ModelState.IsValid)
            {
                agent_camp.CAMP_ABBR = "CMCB0001";
                agent_camp.ACTIVE_SW = true;
                agent_camp.ALWAYS_FILL_BOOK_OF_BUSINESS_SW = true;
                agent_camp.REGION = "Northeast";
                agent_camp.REGION_ID = 1;
                agent_camp.TEST_AGENT = false;

                db.AGENT_CAMP.Add(agent_camp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agent_camp);
        }

        //
        // GET: /AddAgent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AGENT_CAMP agent_camp = db.AGENT_CAMP.Find(id);
            if (agent_camp == null)
            {
                return HttpNotFound();
            }
            return View(agent_camp);
        }

        //
        // POST: /AddAgent/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AGENT_CAMP agent_camp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent_camp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agent_camp);
        }

        //
        // GET: /AddAgent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AGENT_CAMP agent_camp = db.AGENT_CAMP.Find(id);
            if (agent_camp == null)
            {
                return HttpNotFound();
            }
            return View(agent_camp);
        }

        //
        // POST: /AddAgent/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AGENT_CAMP agent_camp = db.AGENT_CAMP.Find(id);
            db.AGENT_CAMP.Remove(agent_camp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}