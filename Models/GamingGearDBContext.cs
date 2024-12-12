using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using DoAnWebGamingGear.Identity;

namespace DoAnWebGamingGear.Models
{
    public class GamingGearDBContext : IdentityDbContext<AppUser>
    {
        public GamingGearDBContext() : base("MyCS") { }

        public DbSet<Brands> Brands { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<PhieuDat> PhieuDats { get; set; }
        public DbSet<ChiTietPhieuDat> ChiTietPhieuDats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhieuDat>()
                .HasRequired(p => p.NhaCungCap)
                .WithMany(ncc => ncc.PhieuDats)
                .HasForeignKey(p => p.MaNhaCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.AppUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiTietPhieuDat>()
                .HasRequired(ctpd => ctpd.PhieuDat)
                .WithMany(pd => pd.ChiTietPhieuDats)
                .HasForeignKey(ctpd => ctpd.MaPhieuDat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .HasRequired(p => p.PhieuNhap)
                .WithMany(pn => pn.ChiTietPhieuNhaps)
                .HasForeignKey(p => p.MaPhieuNhap);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .HasRequired(c => c.ChiTietPhieuDat) // Nếu là khóa ngoại bắt buộc
                .WithMany(d => d.ChiTietPhieuNhaps)
                .HasForeignKey(c => new { c.MaPhieuDat, c.ProductID });

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .HasKey(c => new { c.MaPhieuDat, c.ProductID, c.MaPhieuNhap });

            modelBuilder.Entity<ChiTietPhieuDat>()
                .HasKey(c => new { c.MaPhieuDat, c.ProductID });
        }
    }
}