using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquitecturaProyecto.Models
{
    public class Carrito {
        public List<CarritoModel> lista = new List<CarritoModel>();
    }
    public class CarritoModel
    {
        public int ID;
        public int Cantidad;
    }
}