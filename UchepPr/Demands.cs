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
    
    public partial class Demands
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Demands()
        {
            this.Deals = new HashSet<Deals>();
        }
    
        public int ID { get; set; }
        public string Address_City { get; set; }
        public string Address_Street { get; set; }
        public Nullable<int> Address_House { get; set; }
        public Nullable<int> Address_Number { get; set; }
        public Nullable<long> MinPrice { get; set; }
        public Nullable<long> MaxPrice { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> AgentId { get; set; }
        public Nullable<int> HouseDemandsId { get; set; }
        public Nullable<int> ApartmentDemandsId { get; set; }
        public Nullable<int> LandDemandsId { get; set; }
        public Nullable<int> ObjectTypeId { get; set; }
    
        public virtual Agents Agents { get; set; }
        public virtual ApartmentDemands ApartmentDemands { get; set; }
        public virtual Clients Clients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deals> Deals { get; set; }
        public virtual HouseDemands HouseDemands { get; set; }
        public virtual LandDemands LandDemands { get; set; }
        public virtual ObjectType ObjectType { get; set; }
    }
}