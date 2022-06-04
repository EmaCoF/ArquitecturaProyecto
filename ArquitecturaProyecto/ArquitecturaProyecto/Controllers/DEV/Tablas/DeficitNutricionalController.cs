using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquitecturaProyecto.Models;

namespace ArquitecturaProyecto.Controllers.DEV.Tablas
{
    public class DeficitNutricionalController : Controller
    {
        private NutricionModelDB db = new NutricionModelDB();

        // GET: DeficitNutricional
        public ActionResult Index()
        {
            var deficitNutricional = db.DeficitNutricional.Include(d => d.NutrienteMinimo).Include(d => d.Patologia);
            return View(deficitNutricional.ToList());
        }

        // GET: DeficitNutricional/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            if (deficitNutricional == null)
            {
                return HttpNotFound();
            }
            return View(deficitNutricional);
        }

        // GET: DeficitNutricional/Create
        public ActionResult Create()
        {
            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "Nutriente.Nombre");
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre");
            return View();
        }

        // POST: DeficitNutricional/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDeficit,IdMinimo,IdPatologia")] DeficitNutricional deficitNutricional)
        {
            if (ModelState.IsValid)
            {
                db.DeficitNutricional.Add(deficitNutricional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "Nutriente.Nombre", deficitNutricional.IdMinimo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", deficitNutricional.IdPatologia);
            return View(deficitNutricional);
        }

        // GET: DeficitNutricional/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            if (deficitNutricional == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "Nutriente.Nombre", deficitNutricional.IdMinimo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", deficitNutricional.IdPatologia);
            return View(deficitNutricional);
        }

        // POST: DeficitNutricional/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDeficit,IdMinimo,IdPatologia")] DeficitNutricional deficitNutricional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deficitNutricional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "Nutriente.Nombre", deficitNutricional.IdMinimo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", deficitNutricional.IdPatologia);
            return View(deficitNutricional);
        }

        // GET: DeficitNutricional/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            if (deficitNutricional == null)
            {
                return HttpNotFound();
            }
            return View(deficitNutricional);
        }

        // POST: DeficitNutricional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            db.DeficitNutricional.Remove(deficitNutricional);
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
