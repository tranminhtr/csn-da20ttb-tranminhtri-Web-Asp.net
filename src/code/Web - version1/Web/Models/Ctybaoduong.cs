using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Ctybaoduong
{
    public int MaCtybd { get; set; }

    public string TenCtybd { get; set; }

    public string DiaChi { get; set; }

    public string Sdt { get; set; }

    public string Email { get; set; }

    public int? TrangThaiId { get; set; }
}
