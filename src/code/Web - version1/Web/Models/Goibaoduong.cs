using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Goibaoduong
{
    public int MaGoiBd { get; set; }

    public string TenGoi { get; set; }

    public string HangMuc { get; set; }

    public double? ChiPhi { get; set; }

    public string SoKm { get; set; }

    public int? TrangThaiId { get; set; }
}
