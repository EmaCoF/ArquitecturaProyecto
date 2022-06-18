using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquitecturaProyecto.Models
{
    public class DetalleViewModel
    {
        private NutricionModelDB db = new NutricionModelDB();
        public bool EnCarrito;
        public Producto producto;
        public IEnumerable<ValNutrientes>lista;

        public void Obtenerdatos() {

            var datos = from n in db.Nutriente
                        join Mx in db.NutrienteMaximo on n.IdNutriente equals Mx.IdNutriente
                        join Mn in db.NutrienteMinimo on n.IdNutriente equals Mn.IdNutriente
                        select new ValNutrientes
                        {
                            Nombre = n.Nombre,
                            ValorMaximo = Mx.ValorMaximo,
                            ValorMinimo = Mn.ValorMinimo

                        };
            lista = datos.ToList();


            TablaNutricional tabla = db.TablaNutricional.Where(t => t.IdProducto == producto.IdProducto).FirstOrDefault<TablaNutricional>();
            foreach (var item in lista)
            {
                item.valor = Convert.ToDecimal(typeof(TablaNutricional).GetProperty(item.Nombre).GetValue(tabla));
                item.porcentaje4 = 20;
                item.porcentaje2 = Convert.ToInt32((item.ValorMinimo * 80)/item.ValorMaximo);
                item.porcentaje3 = 80 - item.porcentaje2;
                item.porcentaje1= Convert.ToInt32((item.valor * 80) / item.ValorMaximo);
            }
        }
    }
    public class ValNutrientes
    {
        public string Nombre;
        public decimal ValorMaximo;
        public decimal ValorMinimo;
        public decimal valor;

        public int porcentaje1;
        public int porcentaje2;
        public int porcentaje3;
        public int porcentaje4;

    }
}