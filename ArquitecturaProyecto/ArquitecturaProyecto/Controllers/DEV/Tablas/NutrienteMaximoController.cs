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
    public class NutrienteMaximoController : Controller
    {
        private NutricionModelDB db = new NutricionModelDB();

        // GET: NutrienteMaximo
        public ActionResult Index()
        {
            var nutrienteMaximo = db.NutrienteMaximo.Include(n => n.Nutriente);
            return View(nutrienteMaximo.ToList());
        }

        // GET: NutrienteMaximo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NutrienteMaximo nutrienteMaximo = db.NutrienteMaximo.Find(id);
            if (nutrienteMaximo == null)
            {
                return HttpNotFound();
            }
            return View(nutrienteMaximo);
        }

        // GET: NutrienteMaximo/Create
        public ActionResult Create()
        {
            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre");
            return View();
        }

        // POST: NutrienteMaximo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMaximo,IdNutriente,ValorMaximo")] NutrienteMaximo nutrienteMaximo)
        {
            if (ModelState.IsValid)
            {
                db.NutrienteMaximo.Add(nutrienteMaximo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre", nutrienteMaximo.IdNutriente);
            return View(nutrienteMaximo);
        }

        // GET: NutrienteMaximo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NutrienteMaximo nutrienteMaximo = db.NutrienteMaximo.Find(id);
            if (nutrienteMaximo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre", nutrienteMaximo.IdNutriente);
            return View(nutrienteMaximo);
        }

        // POST: NutrienteMaximo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMaximo,IdNutriente,ValorMaximo")] NutrienteMaximo nutrienteMaximo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutrienteMaximo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNutriente = new SelectList(db.Nutriente, "IdNutriente", "Nombre", nutrienteMaximo.IdNutriente);
            return View(nutrienteMaximo);
        }

        // GET: NutrienteMaximo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NutrienteMaximo nutrienteMaximo = db.NutrienteMaximo.Find(id);
            if (nutrienteMaximo == null)
            {
                return HttpNotFound();
            }
            return View(nutrienteMaximo);
        }

        // POST: NutrienteMaximo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NutrienteMaximo nutrienteMaximo = db.NutrienteMaximo.Find(id);
            db.NutrienteMaximo.Remove(nutrienteMaximo);
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
