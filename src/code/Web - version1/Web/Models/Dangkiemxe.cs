using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Dangkiemxe
{
    public int Id { get; set; }

    public string BienSo { get; set; }

    public int MaCtydk { get; set; }

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public double Gia { get; set; }

    public int? TrangThai { get; set; }

    public int? DemNgay { get; set; }

    public virtual Xe BienSoNavigation { get; set; }

    public virtual Ctydangkiem MaCtydkNavigation { get; set; }

    public virtual Danhmuc TrangThaiNavigation { get; set; }
}
