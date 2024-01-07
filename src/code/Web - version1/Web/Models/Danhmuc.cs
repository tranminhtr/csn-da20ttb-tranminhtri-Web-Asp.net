using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Danhmuc
{
    public int Id { get; set; }

    public int MaLoai { get; set; }

    public string TenDm { get; set; }

    public virtual ICollection<Baohiemxe> Baohiemxes { get; } = new List<Baohiemxe>();

    public virtual ICollection<Ctybaohiem> Ctybaohiems { get; } = new List<Ctybaohiem>();

    public virtual ICollection<Ctydangkiem> Ctydangkiems { get; } = new List<Ctydangkiem>();

    public virtual ICollection<Dangkiemxe> Dangkiemxes { get; } = new List<Dangkiemxe>();

    public virtual ICollection<Xe> Xes { get; } = new List<Xe>();
}
