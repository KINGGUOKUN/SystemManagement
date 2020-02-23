using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using SystemManagement.Entity;

namespace SystemManagement.Repository.Contract
{
    public class SystemManageDbContext : DbContext
    {
        public SystemManageDbContext([NotNullAttribute] DbContextOptions options) 
            : base(options)
        {
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
