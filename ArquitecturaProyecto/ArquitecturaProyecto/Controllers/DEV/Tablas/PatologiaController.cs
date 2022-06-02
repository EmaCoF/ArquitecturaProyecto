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
    public class PatologiaController : Controller
    {
        private NutricionModelDB db = new NutricionModelDB();

        // GET: Patologia
        public ActionResult Index()
        {
            return View(db.Patologia.ToList());
        }

        // GET: Patologia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patologia patologia = db.Patologia.Find(id);
            if (patologia == null)
            {
                return HttpNotFound();
            }
            return View(patologia);
        }

        // GET: Patologia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patologia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPatologia,Nombre,Descripcion")] Patologia patologia)
        {
            if (ModelState.IsValid)
            {
                db.Patologia.Add(patologia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patologia);
        }

        // GET: Patologia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patologia patologia = db.Patologia.Find(id);
            if (patologia == null)
            {
                return HttpNotFound();
            }
            return View(patologia);
        }

        // POST: Patologia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPatologia,Nombre,Descripcion")] Patologia patologia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patologia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patologia);
        }

        // GET: Patologia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patologia patologia = db.Patologia.Find(id);
            if (patologia == null)
            {
                return HttpNotFound();
            }
            return View(patologia);
        }

        // POST: Patologia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patologia patologia = db.Patologia.Find(id);
            db.Patologia.Remove(patologia);
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
