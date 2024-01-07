using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Xe
{
    public string BienSo { get; set; }

    public string HangXe { get; set; }

    public string Doi { get; set; }

    public int NamSx { get; set; }

    public int SoChoNgoi { get; set; }

    public string KieuDang { get; set; }

    public int TrangThai { get; set; }

    public virtual ICollection<Baohiemxe> Baohiemxes { get; } = new List<Baohiemxe>();

    public virtual ICollection<Dangkiemxe> Dangkiemxes { get; } = new List<Dangkiemxe>();

    public virtual Danhmuc TrangThaiNavigation { get; set; }
}
