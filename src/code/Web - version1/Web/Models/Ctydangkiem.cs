using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Ctydangkiem
{
    public int MaCtydk { get; set; }

    public string TenCty { get; set; }

    public string DiaChi { get; set; }

    public string Sdt { get; set; }

    public string Email { get; set; }

    public int TrangThai { get; set; }

    public virtual ICollection<Dangkiemxe> Dangkiemxes { get; } = new List<Dangkiemxe>();

    public virtual Danhmuc TrangThaiNavigation { get; set; }
}
