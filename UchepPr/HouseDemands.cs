//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UchepPr
{
    using System;
    using System.Collections.Generic;
    
    public partial class HouseDemands
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HouseDemands()
        {
            this.Demands = new HashSet<Demands>();
        }
    
        public int Id { get; set; }
        public Nullable<int> MinFloors { get; set; }
        public Nullable<int> MaxFloors { get; set; }
        public Nullable<double> MinArea { get; set; }
        public Nullable<double> MaxArea { get; set; }
        public Nullable<int> MinRooms { get; set; }
        public Nullable<int> MaxRooms { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Demands> Demands { get; set; }
    }
}
