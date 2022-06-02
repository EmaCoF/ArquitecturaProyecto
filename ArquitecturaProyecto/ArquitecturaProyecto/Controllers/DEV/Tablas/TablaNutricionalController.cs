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
    public class TablaNutricionalController : Controller
    {
        private NutricionModelDB db = new NutricionModelDB();

        // GET: TablaNutricional
        public ActionResult Index()
        {
            var tablaNutricional = db.TablaNutricional.Include(t => t.Producto);
            return View(tablaNutricional.ToList());
        }

        // GET: TablaNutricional/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TablaNutricional tablaNutricional = db.TablaNutricional.Find(id);
            if (tablaNutricional == null)
            {
                return HttpNotFound();
            }
            return View(tablaNutricional);
        }

        // GET: TablaNutricional/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre");
            return View();
        }

        // POST: TablaNutricional/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTabla,IdProducto,Calorias,GrasaTotal,GrasasSaturadas,GrasasTrans,Colesterol,Sodio,CarbohidratosTotales,FibraDietetica,Azucares,Proteina")] TablaNutricional tablaNutricional)
        {
            if (ModelState.IsValid)
            {
                db.TablaNutricional.Add(tablaNutricional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre", tablaNutricional.IdProducto);
            return View(tablaNutricional);
        }

        // GET: TablaNutricional/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TablaNutricional tablaNutricional = db.TablaNutricional.Find(id);
            if (tablaNutricional == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre", tablaNutricional.IdProducto);
            return View(tablaNutricional);
        }

        // POST: TablaNutricional/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTabla,IdProducto,Calorias,GrasaTotal,GrasasSaturadas,GrasasTrans,Colesterol,Sodio,CarbohidratosTotales,FibraDietetica,Azucares,Proteina")] TablaNutricional tablaNutricional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tablaNutricional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "Nombre", tablaNutricional.IdProducto);
            return View(tablaNutricional);
        }

        // GET: TablaNutricional/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TablaNutricional tablaNutricional = db.TablaNutricional.Find(id);
            if (tablaNutricional == null)
            {
                return HttpNotFound();
            }
            return View(tablaNutricional);
        }

        // POST: TablaNutricional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TablaNutricional tablaNutricional = db.TablaNutricional.Find(id);
            db.TablaNutricional.Remove(tablaNutricional);
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
