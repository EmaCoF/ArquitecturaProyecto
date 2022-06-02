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
    public class NutrienteMinimoController : Controller
    {
        private NutricionModelDB db = new NutricionModelDB();

        // GET: NutrienteMinimo
        public ActionResult Index()
        {
            var nutrienteMinimo = db.NutrienteMinimo.Include(n => n.Nutriente);
            return View(nutrienteMinimo.ToList());
        }

        // GET: NutrienteMinimo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NutrienteMinimo nutrienteMinimo = db.NutrienteMinimo.Find(id);
            if (nutrienteMinimo == null)
            {
                return HttpNotFound();
            }
            return View(nutrienteMinimo);
        }

        // GET: NutrienteMinimo/Create
        public ActionResult Create()
        {
            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre");
            return View();
        }

        // POST: NutrienteMinimo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMinimo,IdNutriente,ValorMinimo")] NutrienteMinimo nutrienteMinimo)
        {
            if (ModelState.IsValid)
            {
                db.NutrienteMinimo.Add(nutrienteMinimo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre", nutrienteMinimo.IdNutriente);
            return View(nutrienteMinimo);
        }

        // GET: NutrienteMinimo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NutrienteMinimo nutrienteMinimo = db.NutrienteMinimo.Find(id);
            if (nutrienteMinimo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre", nutrienteMinimo.IdNutriente);
            return View(nutrienteMinimo);
        }

        // POST: NutrienteMinimo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMinimo,IdNutriente,ValorMinimo")] NutrienteMinimo nutrienteMinimo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutrienteMinimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre", nutrienteMinimo.IdNutriente);
            return View(nutrienteMinimo);
        }

        // GET: NutrienteMinimo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NutrienteMinimo nutrienteMinimo = db.NutrienteMinimo.Find(id);
            if (nutrienteMinimo == null)
            {
                return HttpNotFound();
            }
            return View(nutrienteMinimo);
        }

        // POST: NutrienteMinimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NutrienteMinimo nutrienteMinimo = db.NutrienteMinimo.Find(id);
            db.NutrienteMinimo.Remove(nutrienteMinimo);
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
