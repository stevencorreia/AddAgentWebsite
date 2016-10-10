using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AddCampaignB2b.CommonClasses;
using AddCampaignB2b.Models;

namespace AddCampaignB2b.Controllers
{
    public class AddAgentCriteriaController : Controller
    {
        private readonly pd_cmcEntities db = new pd_cmcEntities();

        //
        // GET: /AddAgentCriteria/

        public ActionResult Index(int id)
        {
            var agentCampCriteria = db.AGENT_CAMP_CRITERIA.Include(a => a.AGENT_CAMP).Where(x => x.AGENT_CAMP_ID == id);
            ViewBag.AgentCampID = id;
            return View(agentCampCriteria.ToList());
        }

        //
        // GET: /AddAgentCriteria/Details/5

        public ActionResult Details(int id = 0)
        {
            AGENT_CAMP_CRITERIA agent_camp_criteria = db.AGENT_CAMP_CRITERIA.Find(id);
            if (agent_camp_criteria == null)
            {
                return HttpNotFound();
            }
            return View(agent_camp_criteria);
        }

        //
        // GET: /AddAgentCriteria/Create

        public ActionResult Create(int id)
        {
            try
            {
                ViewBag.AGENT_CAMP_ID = new SelectList(db.AGENT_CAMP, "AGENT_CAMP_ID", "COMMUNICATOR");
                ViewBag.AgentCampID = id;
                var agentCampCriteria = new AGENT_CAMP_CRITERIA {AGENT_CAMP_ID = id};
                FillCriteria(agentCampCriteria);
                return View("Create", agentCampCriteria);
            }
            catch (Exception exception)
            {
                CommonCode.HandleException(exception);
                return View("Error"); ;
            }
        }

        private void FillCriteria(AGENT_CAMP_CRITERIA agentCampCriteria)
        {
            agentCampCriteria.LIST = db.LIST_META_DATA.Where(x => x.ActiveOrInactive == "a").Select(x => new SelectListItem()
            {
                Value = x.LIST_ID,
                Text = x.LIST_ID
            }).ToList();
            agentCampCriteria.SEGMENT_CODE = db.LIST_META_DATA.Where(x => x.ActiveOrInactive == "a").Select(x => new SelectListItem()
            {
                Value = x.LIST_SEGMENT,
                Text = x.LIST_SEGMENT
            }).Distinct().ToList();
            agentCampCriteria.CRITERIA1 = db.AGENT_CAMP_CRITERIA_MASTER.Select(x => new SelectListItem
            {
                Value = x.CriteriaName,
                Text = x.CriteriaName
            }).ToList();
            agentCampCriteria.CRITERIA2 = db.AGENT_CAMP_CRITERIA_MASTER.Select(x => new SelectListItem
            {
                Value = x.CriteriaName,
                Text = x.CriteriaName
            }).ToList();
            agentCampCriteria.CRITERIA3 = db.AGENT_CAMP_CRITERIA_MASTER.Select(x => new SelectListItem
            {
                Value = x.CriteriaName,
                Text = x.CriteriaName
            }).ToList();
        }

        //
        // POST: /AddAgentCriteria/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AGENT_CAMP_CRITERIA agent_camp_criteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BuildQuery(agent_camp_criteria);
                    db.AGENT_CAMP_CRITERIA.Add(agent_camp_criteria);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = agent_camp_criteria.AGENT_CAMP_ID });
                }
                ViewBag.AGENT_CAMP_ID = new SelectList(db.AGENT_CAMP, "AGENT_CAMP_ID", "COMMUNICATOR", agent_camp_criteria.AGENT_CAMP_ID);
                return View(agent_camp_criteria);
            }
            catch (Exception exception)
            {
                CommonCode.HandleException(exception);
                return View("Error");
            }
        }

        //
        // GET: /AddAgentCriteria/Edit/5
        public JsonResult GetCurrentPercentage(AGENT_CAMP_CRITERIA a)
        {
            try
            {
                //var existingPercentage = 0;
                //var existingCriteria = db.AGENT_CAMP_CRITERIA.FirstOrDefault(x => x.AGENT_CAMP_CRITERIA_ID == a.AGENT_CAMP_CRITERIA_ID);
                //if (existingCriteria != null)
                //{
                //    existingPercentage = existingCriteria.PERCENT_APPLIED;
                //}
                //var result = db.AGENT_CAMP_CRITERIA.Where(x => x.AGENT_CAMP_ID == a.AGENT_CAMP_ID).Select(x => x.PERCENT_APPLIED).DefaultIfEmpty(0).Sum() + a.PERCENT_APPLIED - existingPercentage > 100;
                var result =
                    db.AGENT_CAMP_CRITERIA.Where(
                        x => x.AGENT_CAMP_ID == a.AGENT_CAMP_ID && x.AGENT_CAMP_CRITERIA_ID != a.AGENT_CAMP_CRITERIA_ID)
                        .Select(x => x.PERCENT_APPLIED)
                        .DefaultIfEmpty(0)
                        .Sum() + a.PERCENT_APPLIED > 100;
                return Json(!result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id = 0)
        {
            try
            {
                AGENT_CAMP_CRITERIA agentCampCriteria = db.AGENT_CAMP_CRITERIA.Find(id);
                if (agentCampCriteria == null)
                {
                    return HttpNotFound();
                }
                FillCriteria(agentCampCriteria);
                //agentCampCriteria.AGENT_CAMP_ID = id;
                ViewBag.AGENT_CAMP_ID = new SelectList(db.AGENT_CAMP, "AGENT_CAMP_ID", "COMMUNICATOR", agentCampCriteria.AGENT_CAMP_ID);
                return View(agentCampCriteria);
            }
            catch (Exception exception)
            {
                CommonCode.HandleException(exception);
                return View("Error");
            }
        }

        //
        // POST: /AddAgentCriteria/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AGENT_CAMP_CRITERIA agent_camp_criteria)
        {
            if (ModelState.IsValid)
            {
                BuildQuery(agent_camp_criteria);
                db.Entry(agent_camp_criteria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = agent_camp_criteria.AGENT_CAMP_ID });
            }
            ViewBag.AGENT_CAMP_ID = new SelectList(db.AGENT_CAMP, "AGENT_CAMP_ID", "COMMUNICATOR", agent_camp_criteria.AGENT_CAMP_ID);
            return View(agent_camp_criteria);
        }

        //
        // GET: /AddAgentCriteria/Delete/5
        private static void BuildQuery(AGENT_CAMP_CRITERIA agentCampCriteria)
        {
            if (agentCampCriteria.selectedListID != null) agentCampCriteria.QUERY = "LIST_ID in ('" + agentCampCriteria.selectedListID + "')";
            if (agentCampCriteria.selectedSegmentCode != null) agentCampCriteria.QUERY += " and SEGMENT_CODE in('" + agentCampCriteria.selectedSegmentCode + "')";
            if (agentCampCriteria.SelectedCRITERIA1 != null) agentCampCriteria.QUERY += " and " + agentCampCriteria.SelectedCRITERIA1 + " in('" + agentCampCriteria.SelectedCRITERIAValue1 + "')";
            if (agentCampCriteria.SelectedCRITERIA2 != null) agentCampCriteria.QUERY += " and " + agentCampCriteria.SelectedCRITERIA2 + " in('" + agentCampCriteria.SelectedCRITERIAValue2 + "')";
            if (agentCampCriteria.SelectedCRITERIA3 != null) agentCampCriteria.QUERY += " and " + agentCampCriteria.SelectedCRITERIA3 + " in('" + agentCampCriteria.SelectedCRITERIAValue3 + "')";
        }

        public ActionResult Delete(int id = 0)
        {
            AGENT_CAMP_CRITERIA agent_camp_criteria = db.AGENT_CAMP_CRITERIA.Find(id);
            ViewBag.AgentCampID = id;
            if (agent_camp_criteria == null)
            {
                return HttpNotFound();
            }
            return View(agent_camp_criteria);
        }

        //
        // POST: /AddAgentCriteria/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AGENT_CAMP_CRITERIA agent_camp_criteria = db.AGENT_CAMP_CRITERIA.Find(id);
            db.AGENT_CAMP_CRITERIA.Remove(agent_camp_criteria);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = agent_camp_criteria.AGENT_CAMP_ID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}