﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class praktikaEntities : DbContext
    {
        public praktikaEntities()
            : base("name=praktikaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<ApartmentDemands> ApartmentDemands { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Deals> Deals { get; set; }
        public virtual DbSet<Demands> Demands { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<HouseDemands> HouseDemands { get; set; }
        public virtual DbSet<LandDemands> LandDemands { get; set; }
        public virtual DbSet<ObjectType> ObjectType { get; set; }
        public virtual DbSet<RealtyObject> RealtyObject { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
