using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystemManagement.Common;
using SystemManagement.Entity;

namespace SystemManagement.Repository.Contract
{
    public class SystemManageDbContext : DbContext
    {
        private readonly UserContext _userContext;

        public SystemManageDbContext([NotNullAttribute] DbContextOptions options, UserContext userContext) 
            : base(options)
        {
            _userContext = userContext;
        }

        public override int SaveChanges()
        {
            this.SetAuditFields();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.SetAuditFields();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.SetAuditFields();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetAuditFields()
        {
            var auditEntities = ChangeTracker.Entries<IAudit>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var entry in auditEntities)
            {
                var entity = entry.Entity as IAudit;
                if (entry.State == EntityState.Added)
                {
                    entity.CreateBy = _userContext.ID;
                    entity.CreateTime = DateTime.Now;
                }
                else
                {
                    entity.ModifyBy = _userContext.ID;
                    entity.ModifyTime = DateTime.Now;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysCfg>();
            modelBuilder.Entity<SysDept>();
            modelBuilder.Entity<SysDict>();
            modelBuilder.Entity<SysFileInfo>();
            modelBuilder.Entity<SysLoginLog>();
            modelBuilder.Entity<SysMenu>();
            modelBuilder.Entity<SysNotice>();
            modelBuilder.Entity<SysOperationLog>();
            modelBuilder.Entity<SysRelation>();
            modelBuilder.Entity<SysRole>();
            modelBuilder.Entity<SysTask>();
            modelBuilder.Entity<SysTaskLog>();
            modelBuilder.Entity<SysUser>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
