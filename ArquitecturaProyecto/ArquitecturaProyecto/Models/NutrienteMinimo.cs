//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArquitecturaProyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NutrienteMinimo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NutrienteMinimo()
        {
            this.DeficitNutricional = new HashSet<DeficitNutricional>();
        }
    
        public int IdMinimo { get; set; }
        public int IdNutriente { get; set; }
        public decimal ValorMinimo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeficitNutricional> DeficitNutricional { get; set; }
        public virtual Nutriente Nutriente { get; set; }
    }
}
