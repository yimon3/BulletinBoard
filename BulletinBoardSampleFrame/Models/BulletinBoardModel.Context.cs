﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BulletinBoardSampleFrame.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BulletinBoardEntity : DbContext
    {
        public BulletinBoardEntity()
            : base("name=BulletinBoardEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<password_resets> password_resets { get; set; }
        public IEnumerable<object> user { get; internal set; }
    }
}
