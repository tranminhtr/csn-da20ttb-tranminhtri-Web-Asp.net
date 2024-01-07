using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Web.Models;

public partial class Baoduongxe
{
    [Key]
    public int Id { get; set; }

    public string BienSxId { get; set; }

    public int? MaCtybdId { get; set; }

    public DateTime? NgayBd { get; set; }

    public DateTime? NgayKt { get; set; }

    public int? MaGoiBdId { get; set; }

    public int? DemNgay { get; set; }

    public int? TrangThaiId { get; set; }
}
