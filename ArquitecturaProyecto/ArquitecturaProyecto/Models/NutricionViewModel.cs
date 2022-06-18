using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquitecturaProyecto.Models
{
    public class NutricionViewModel
    {
        private NutricionModelDB db = new NutricionModelDB();
        private TablaNutricional tabla; 
        public IEnumerable<ValNutrientes> lista;
        public IEnumerable<Patologia> patologias;
        public List<ProductoCantidad> ListaProductos = new List<ProductoCantidad>();

        public void Obtenerdatos(Carrito carrito)
        {
            var IDS = carrito.lista.Select(p => p.ID).ToList();
            var tablas = db.TablaNutricional.Where(p => IDS.Contains(p.IdProducto));
            TablaNutricional tabladatos = new TablaNutricional();
            foreach (var item in tablas)
            {
                int multi =carrito.lista.Find(x => x.ID == item.IdProducto).Cantidad;
                tabladatos.Calorias = tabladatos.Calorias + (item.Calorias * multi);
                tabladatos.Azucares = tabladatos.Azucares + (item.Azucares * multi);
                tabladatos.CarbohidratosTotales = tabladatos.CarbohidratosTotales + (item.CarbohidratosTotales * multi);
                tabladatos.Colesterol = tabladatos.Colesterol + (item.Colesterol * multi);
                tabladatos.FibraDietetica = tabladatos.FibraDietetica + (item.FibraDietetica * multi);
                tabladatos.GrasasSaturadas = tabladatos.GrasasSaturadas + (item.GrasasSaturadas * multi);
                tabladatos.GrasasTrans = tabladatos.GrasasTrans + (item.GrasasTrans * multi);
                tabladatos.GrasaTotal = tabladatos.GrasaTotal + (item.GrasaTotal * multi);
                tabladatos.Proteina = tabladatos.Proteina + (item.Proteina * multi);
                tabladatos.Sodio = tabladatos.Sodio + (item.Sodio * multi);
            }
            tabla = tabladatos;

            foreach (var item in carrito.lista)
            {
                ProductoCantidad productoCantidad = new ProductoCantidad();
                productoCantidad.Cantidad = item.Cantidad;
                productoCantidad.Nombre = db.Producto.Find(item.ID).Nombre;
                productoCantidad.Id = item.ID;
                ListaProductos.Add(productoCantidad);
            }

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

            var Deficit = from p in db.Patologia
                         join d in db.DeficitNutricional on p.IdPatologia equals d.IdPatologia
                         join Mn in db.NutrienteMinimo on d.IdMinimo equals Mn.IdMinimo
                         join n in db.Nutriente on Mn.IdNutriente equals n.IdNutriente
                         where (tabla.Calorias<Mn.ValorMinimo && n.Nombre=="Calorias")
                         || (tabla.Azucares < Mn.ValorMinimo && n.Nombre == "Azucares")
                         || (tabla.CarbohidratosTotales < Mn.ValorMinimo && n.Nombre == "CarbohidratosTotales")
                         || (tabla.Colesterol < Mn.ValorMinimo && n.Nombre == "Colesterol")
                         || (tabla.FibraDietetica < Mn.ValorMinimo && n.Nombre == "FibraDietetica")
                         || (tabla.GrasasSaturadas < Mn.ValorMinimo && n.Nombre == "GrasasSaturadas")
                         || (tabla.GrasasTrans < Mn.ValorMinimo && n.Nombre == "GrasasTrans")
                         || (tabla.GrasaTotal < Mn.ValorMinimo && n.Nombre == "GrasaTotal")
                         || (tabla.Proteina < Mn.ValorMinimo && n.Nombre == "Proteina")
                         || (tabla.Sodio < Mn.ValorMinimo && n.Nombre == "Sodio")
                          select p;

            var Exceso = from p in db.Patologia
                         join e in db.ExcesoNutricional on p.IdPatologia equals e.IdPatologia
                         join Mx in db.NutrienteMaximo on e.IdMaximo equals Mx.IdMaximo
                         join n in db.Nutriente on Mx.IdNutriente equals n.IdNutriente
                         where (tabla.Calorias > Mx.ValorMaximo && n.Nombre == "Calorias")
                         || (tabla.Azucares > Mx.ValorMaximo && n.Nombre == "Azucares")
                         || (tabla.CarbohidratosTotales > Mx.ValorMaximo && n.Nombre == "CarbohidratosTotales")
                         || (tabla.Colesterol > Mx.ValorMaximo && n.Nombre == "Colesterol")
                         || (tabla.FibraDietetica > Mx.ValorMaximo && n.Nombre == "FibraDietetica")
                         || (tabla.GrasasSaturadas > Mx.ValorMaximo && n.Nombre == "GrasasSaturadas")
                         || (tabla.GrasasTrans > Mx.ValorMaximo && n.Nombre == "GrasasTrans")
                         || (tabla.GrasaTotal > Mx.ValorMaximo && n.Nombre == "GrasaTotal")
                         || (tabla.Proteina > Mx.ValorMaximo && n.Nombre == "Proteina")
                         || (tabla.Sodio > Mx.ValorMaximo && n.Nombre == "Sodio")
                         select p;


            patologias = Deficit.Union(Exceso).Distinct().ToList();

            foreach (var item in lista)
            {
                item.valor = Convert.ToDecimal(typeof(TablaNutricional).GetProperty(item.Nombre).GetValue(tabla));

                item.porcentaje1 = Convert.ToInt32((item.valor * 80) / item.ValorMaximo);

                if (item.porcentaje1 >= 100)
                {
                    item.porcentaje1 = 100;
                    item.porcentaje2 = Convert.ToInt32((item.ValorMinimo * 100) / item.valor);
                    item.porcentaje3 = Convert.ToInt32((item.ValorMaximo * 100) / item.valor)-item.porcentaje2;
                    
                    item.porcentaje4 = 100-(item.porcentaje3+item.porcentaje2);
                }
                else {
                    item.porcentaje4 = 20;
                    item.porcentaje2 = Convert.ToInt32((item.ValorMinimo * 80) / item.ValorMaximo);
                    item.porcentaje3 = 80 - item.porcentaje2;
                }
                
            }
        }
    }
    public class ProductoCantidad 
    {
        public int Id;
        public string Nombre;
        public int Cantidad;

    }
}