using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquitecturaProyecto.Models;


namespace ArquitecturaProyecto.Controllers.Main
{
    public class AplicacionController : Controller
    {
        private NutricionModelDB db = new NutricionModelDB();
        // GET: Aplicacion
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.TipoProducto);
            var categorias = db.TipoProducto;
            CatalogoViewModel catalogo = new CatalogoViewModel();
            catalogo.productos = producto.ToList();
            catalogo.categorias = categorias.ToList();
            catalogo.Filtro = false;
            return View(catalogo);
        }
        
        [HttpPost]
        public ActionResult Index(int[] Categorias)
        {

            var producto = db.Producto.Include(p => p.TipoProducto).Where(r=>Categorias.Contains(r.IdTipo));
            var categorias = db.TipoProducto;
            CatalogoViewModel catalogo = new CatalogoViewModel();
            catalogo.productos = producto.ToList();
            catalogo.categorias = categorias.ToList();
            catalogo.Filtro = true;
            return View(catalogo);
        }

        public ActionResult Detalle(int id) {
            DetalleViewModel detalle = new DetalleViewModel();
            Producto producto = new Producto();
            producto=db.Producto.Find(id);
            if (producto.Nombre==null)
            {
                return View("Index");
            }
            detalle.producto = producto;


            //var datos = db.Nutriente.Include(p => p.NutrienteMaximo)
            //    .Include(p => p.NutrienteMinimo)
            //    .Select(c => new { c.Nombre, c.NutrienteMaximo, c.NutrienteMinimo }).ToList();
            
            
            detalle.Obtenerdatos();
            //foreach (var item in datos)
            //{
            //    ValNutrientes nutrientes = new ValNutrientes();
            //    nutrientes.Nombre = item.Nombre;
            //    nutrientes.ValorMaximo = Convert.ToInt32( item.NutrienteMaximo.ToString());
            //    nutrientes.ValorMinimo = Convert.ToInt32(item.NutrienteMinimo.ToString());
            //    nutrientes.valor = Convert.ToInt32(typeof(Producto).GetProperty(nutrientes.Nombre.ToString()).GetValue(producto));
            //    detalle.lista.Append(nutrientes);
            //}

            return View(detalle);
        }
    }
}