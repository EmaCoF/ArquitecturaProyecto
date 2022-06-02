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
    public class NutrienteController : Controller
    {
        private NutricionModelDB db = new NutricionModelDB();

        // GET: Nutriente
        public ActionResult Index()
        {
            return View(db.Nutriente.ToList());
        }

        // GET: Nutriente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutriente nutriente = db.Nutriente.Find(id);
            if (nutriente == null)
            {
                return HttpNotFound();
            }
            return View(nutriente);
        }

        // GET: Nutriente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nutriente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNutriente,Nombre")] Nutriente nutriente)
        {
            if (ModelState.IsValid)
            {
                db.Nutriente.Add(nutriente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nutriente);
        }

        // GET: Nutriente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutriente nutriente = db.Nutriente.Find(id);
            if (nutriente == null)
            {
                return HttpNotFound();
            }
            return View(nutriente);
        }

        // POST: Nutriente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNutriente,Nombre")] Nutriente nutriente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutriente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nutriente);
        }

        // GET: Nutriente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutriente nutriente = db.Nutriente.Find(id);
            if (nutriente == null)
            {
                return HttpNotFound();
            }
            return View(nutriente);
        }

        // POST: Nutriente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nutriente nutriente = db.Nutriente.Find(id);
            db.Nutriente.Remove(nutriente);
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
