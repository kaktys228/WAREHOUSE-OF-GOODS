﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace курсовая_работа.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KURSEntities1 : DbContext
    {
        public KURSEntities1()
            : base("name=KURSEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CONSUMABLE> CONSUMABLES { get; set; }
        public virtual DbSet<DELIVERY> DELIVERies { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEES { get; set; }
        public virtual DbSet<GOODS_ARIVAL> GOODS_ARIVAL { get; set; }
        public virtual DbSet<GOODS_CARE> GOODS_CARE { get; set; }
        public virtual DbSet<INVOICE> INVOICEs { get; set; }
        public virtual DbSet<PROVIDER> PROVIDERS { get; set; }
        public virtual DbSet<ROL> ROLS { get; set; }
        public virtual DbSet<STOCK> STOCKs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
