//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPayRoll_.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PayrollEmployeesEntities : DbContext
    {
        public PayrollEmployeesEntities()
            : base("name=PayrollEmployeesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employees_Tab> Employees_Tab { get; set; }
        public virtual DbSet<PayRoll_Tab> PayRoll_Tab { get; set; }
        public virtual DbSet<PayRollInfo_Tab> PayRollInfo_Tab { get; set; }
        public virtual DbSet<Role_Cat> Role_Cat { get; set; }
        public virtual DbSet<TokenAuth> TokenAuth { get; set; }
        public virtual DbSet<TokenStatus> TokenStatus { get; set; }
    }
}
