using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Ctybaohiem
{
    public int MaCty { get; set; }

    public string TenCty { get; set; }

    public string DiaChi { get; set; }

    public string Sdt { get; set; }

    public string Email { get; set; }

    public int TrangThai { get; set; }

    public virtual ICollection<Baohiemxe> Baohiemxes { get; } = new List<Baohiemxe>();

    public virtual Danhmuc TrangThaiNavigation { get; set; }
}
