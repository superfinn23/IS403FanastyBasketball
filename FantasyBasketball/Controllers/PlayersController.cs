using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FantasyBasketball.DAL;
using FantasyBasketball.Models;

namespace FantasyBasketball.Controllers
{
    public class PlayersController : Controller
    {
        private NBAContext db = new NBAContext();

        // GET: Players
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Position).Include(p => p.Team);
            return View(players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.positionCode = new SelectList(db.Positions, "positionCode", "positionDescription");
            ViewBag.teamID = new SelectList(db.Teams, "teamID", "teamName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "playerID,playerLastName,playerFirstName,positionCode,teamID")] Player player)
        {
            if (ModelState.IsValid)
            {
                player.playerID = db.Players.Max(p => p.playerID) + 1;
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.positionCode = new SelectList(db.Positions, "positionCode", "positionDescription", player.positionCode);
            ViewBag.teamID = new SelectList(db.Teams, "teamID", "teamName", player.teamID);
            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.positionCode = new SelectList(db.Positions, "positionCode", "positionDescription", player.positionCode);
            ViewBag.teamID = new SelectList(db.Teams, "teamID", "teamName", player.teamID);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "playerID,playerLastName,playerFirstName,positionCode,teamID")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.positionCode = new SelectList(db.Positions, "positionCode", "positionDescription", player.positionCode);
            ViewBag.teamID = new SelectList(db.Teams, "teamID", "teamName", player.teamID);
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
