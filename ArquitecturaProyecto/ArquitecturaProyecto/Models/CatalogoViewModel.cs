using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquitecturaProyecto.Models
{
    public class CatalogoViewModel
    {
        public IEnumerable<Producto> productos;
        public IEnumerable<TipoProducto> categorias;
        public bool Filtro;
    }
}