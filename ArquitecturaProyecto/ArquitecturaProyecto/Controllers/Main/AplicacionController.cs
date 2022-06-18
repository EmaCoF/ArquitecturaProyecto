using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
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

            var producto = db.Producto.Include(p => p.TipoProducto).Where(r => Categorias.Contains(r.IdTipo));
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
            producto = db.Producto.Find(id);
            if (Session["Carrito"] == null)
            {
                Carrito carro = new Carrito();
                Session["Carrito"] = carro;

                detalle.EnCarrito = false;
            }
            else
            {
                var carrito = Session["Carrito"] as Carrito;
                List<CarritoModel> match = carrito.lista.Where(p => p.ID == id).ToList();
                if (match == null || match.Count == 0)
                {
                    detalle.EnCarrito = false;
                }
                else
                {
                    detalle.EnCarrito = true;
                }

            }
            if (producto.Nombre == null)
            {
                return RedirectToAction("Index");
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

        public ActionResult AgregarCarrito(int ID) {
            var carrito = (Carrito)Session["Carrito"];
            CarritoModel nuevo = new CarritoModel();
            nuevo.Cantidad = 1;
            nuevo.ID = ID;
            carrito.lista.Add(nuevo);
            return RedirectToAction("Index"); ;
        }

        public void EliminarProductoCarrito (int ID){
            var carrito = (Carrito)Session["Carrito"];
            carrito.lista.RemoveAll(p => p.ID == ID);
        }
        public ActionResult EliminarCarrito(int ID)
        {
            EliminarProductoCarrito(ID);
            return RedirectToAction("Index"); ;
        }

        public ActionResult Nutricion() {
            if (Session["Carrito"] == null)
            {
                Carrito carro = new Carrito();
                Session["Carrito"] = carro;
                return View("NutricionFalta");
            }
            var carrito = (Carrito)Session["Carrito"];
            if (carrito.lista.Count==0)
            {
                return View("NutricionFalta");
            }

            NutricionViewModel modelo = new NutricionViewModel();
            modelo.Obtenerdatos(carrito);

            return View(modelo);
        }

        public ActionResult DisminuirCantidad(int ID) {
            var carrito = (Carrito)Session["Carrito"];
            CarritoModel modelo= carrito.lista.Find(p => p.ID == ID);
            if (modelo.Cantidad>1)
            {
                modelo.Cantidad = modelo.Cantidad - 1;
            }
            else
            {
                EliminarProductoCarrito(ID);
            }
            return RedirectToAction("Nutricion");
        }
        public ActionResult AumentarCantidad(int ID)
        {
            var carrito = (Carrito)Session["Carrito"];
            CarritoModel modelo = carrito.lista.Find(p => p.ID == ID);
            
             modelo.Cantidad = modelo.Cantidad + 1;
            
            return RedirectToAction("Nutricion");
        }
        public ActionResult EliminarProducto(int ID)
        {
            EliminarProductoCarrito(ID);
            return RedirectToAction("Nutricion");
        }
    }
}