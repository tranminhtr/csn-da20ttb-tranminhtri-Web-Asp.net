using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Models;

public partial class QlxeContext : DbContext
{
    public QlxeContext()
    {
    }

    public QlxeContext(DbContextOptions<QlxeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baoduongxe> Baoduongxes { get; set; }

    public virtual DbSet<Baohiemxe> Baohiemxes { get; set; }

    public virtual DbSet<Ctybaoduong> Ctybaoduongs { get; set; }

    public virtual DbSet<Ctybaohiem> Ctybaohiems { get; set; }

    public virtual DbSet<Ctydangkiem> Ctydangkiems { get; set; }

    public virtual DbSet<Dangkiemxe> Dangkiemxes { get; set; }

    public virtual DbSet<Danhmuc> Danhmucs { get; set; }

    public virtual DbSet<Goibaoduong> Goibaoduongs { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-G5K9LK8V\\SQLEXPRESS; Database=qlxe;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baoduongxe>(entity =>
        {
            entity.ToTable("baoduongxe");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BienSxId)
                .HasMaxLength(15)
                .HasColumnName("BienSX_ID");
            entity.Property(e => e.MaCtybdId).HasColumnName("MaCTYBD_ID");
            entity.Property(e => e.MaGoiBdId).HasColumnName("MaGoiBD_ID");
            entity.Property(e => e.NgayBd)
                .HasColumnType("date")
                .HasColumnName("NgayBD");
            entity.Property(e => e.NgayKt)
                .HasColumnType("date")
                .HasColumnName("NgayKT");
            entity.Property(e => e.TrangThaiId).HasColumnName("TrangThai_ID");
        });

        modelBuilder.Entity<Baohiemxe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__baohiemx__3214EC27AC835615");

            entity.ToTable("baohiemxe");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BienSo)
                .IsRequired()
                .HasMaxLength(15);
            entity.Property(e => e.MaCty).HasColumnName("MaCTY");
            entity.Property(e => e.NgayBatDau).HasColumnType("date");
            entity.Property(e => e.NgayKetThuc).HasColumnType("date");

            entity.HasOne(d => d.BienSoNavigation).WithMany(p => p.Baohiemxes)
                .HasForeignKey(d => d.BienSo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_baohiembienso");

            entity.HasOne(d => d.MaCtyNavigation).WithMany(p => p.Baohiemxes)
                .HasForeignKey(d => d.MaCty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_baohiemxecty");

            entity.HasOne(d => d.TrangThaiNavigation).WithMany(p => p.Baohiemxes)
                .HasForeignKey(d => d.TrangThai)
                .HasConstraintName("fk_TrangThaiBaoHiem");
        });

        modelBuilder.Entity<Ctybaoduong>(entity =>
        {
            entity.HasKey(e => e.MaCtybd);

            entity.ToTable("ctybaoduong");

            entity.Property(e => e.MaCtybd).HasColumnName("MaCTYBD");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenCtybd).HasColumnName("TenCTYBD");
            entity.Property(e => e.TrangThaiId).HasColumnName("TrangThai_ID");
        });

        modelBuilder.Entity<Ctybaohiem>(entity =>
        {
            entity.HasKey(e => e.MaCty).HasName("PK__ctybaohi__3DCB54E2E21EAC5C");

            entity.ToTable("ctybaohiem");

            entity.Property(e => e.MaCty)
                .ValueGeneratedNever()
                .HasColumnName("MaCTY");
            entity.Property(e => e.DiaChi)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenCty)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("TenCTY");

            entity.HasOne(d => d.TrangThaiNavigation).WithMany(p => p.Ctybaohiems)
                .HasForeignKey(d => d.TrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ctybaohiem_trangthai");
        });

        modelBuilder.Entity<Ctydangkiem>(entity =>
        {
            entity.HasKey(e => e.MaCtydk).HasName("PK__ctydangk__4E364A44D55A00C1");

            entity.ToTable("ctydangkiem");

            entity.Property(e => e.MaCtydk)
                .ValueGeneratedNever()
                .HasColumnName("MaCTYDK");
            entity.Property(e => e.DiaChi)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenCty)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("TenCTY");

            entity.HasOne(d => d.TrangThaiNavigation).WithMany(p => p.Ctydangkiems)
                .HasForeignKey(d => d.TrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ctydangkiem_trangthai");
        });

        modelBuilder.Entity<Dangkiemxe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__dangkiem__3214EC27DD26CAD4");

            entity.ToTable("dangkiemxe");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BienSo)
                .IsRequired()
                .HasMaxLength(15);
            entity.Property(e => e.MaCtydk).HasColumnName("MaCTYDK");
            entity.Property(e => e.NgayBatDau).HasColumnType("date");
            entity.Property(e => e.NgayKetThuc).HasColumnType("date");

            entity.HasOne(d => d.BienSoNavigation).WithMany(p => p.Dangkiemxes)
                .HasForeignKey(d => d.BienSo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dangkiembienso");

            entity.HasOne(d => d.MaCtydkNavigation).WithMany(p => p.Dangkiemxes)
                .HasForeignKey(d => d.MaCtydk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dangkiemxecty");

            entity.HasOne(d => d.TrangThaiNavigation).WithMany(p => p.Dangkiemxes)
                .HasForeignKey(d => d.TrangThai)
                .HasConstraintName("fk_TrangThaiDangKiem");
        });

        modelBuilder.Entity<Danhmuc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__danhmuc__3214EC27FC38F6A9");

            entity.ToTable("danhmuc");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TenDm)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("TenDM");
        });

        modelBuilder.Entity<Goibaoduong>(entity =>
        {
            entity.HasKey(e => e.MaGoiBd);

            entity.ToTable("goibaoduong");

            entity.Property(e => e.MaGoiBd)
                .ValueGeneratedNever()
                .HasColumnName("MaGoiBD");
            entity.Property(e => e.SoKm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SoKM");
            entity.Property(e => e.TrangThaiId).HasColumnName("TrangThai_ID");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.BienSo).HasName("PK__xe__F7052EB7EB120C88");

            entity.ToTable("xe");

            entity.HasIndex(e => e.Doi, "idx_xe_doi");

            entity.HasIndex(e => e.HangXe, "idx_xe_hangxe");

            entity.HasIndex(e => e.NamSx, "idx_xe_namsx");

            entity.HasIndex(e => e.TrangThai, "idx_xe_trangthai");

            entity.Property(e => e.BienSo).HasMaxLength(15);
            entity.Property(e => e.Doi)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.HangXe)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.KieuDang)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.NamSx).HasColumnName("NamSX");

            entity.HasOne(d => d.TrangThaiNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.TrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_xe_trangthai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
