using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages
{
    public class HanBaoDuongXeModel : PageModel
    {
        private readonly QlxeContext _dbContext;

        public HanBaoDuongXeModel(QlxeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            var baoduongxes = _dbContext.Baoduongxes.ToList();

            foreach (var baoduong in baoduongxes)
            {
                    // Tính số ngày còn lại
                    baoduong.DemNgay = (int)(baoduong.NgayKt.Value - DateTime.Now).TotalDays + 1;

                    // Cập nhật trạng thái
                    if (baoduong.DemNgay > 30)
                    {
                        baoduong.TrangThaiId = 10;
                        baoduong.DemNgay = null; // DemNgay = null khi Ngay > 30
                    }
                    else if (baoduong.DemNgay > 0)
                    {
                        baoduong.TrangThaiId = 10;
                    }
                    else
                    {
                        baoduong.TrangThaiId = 11;
                    }
            }
            // Lưu thay đổi vào cơ sở dữ liệu
            _dbContext.SaveChanges();

            // Kiểm tra xem có thay đổi nào hay không và hiển thị thông báo tương ứng
            if (_dbContext.ChangeTracker.HasChanges())
            {
                TempData["SuccessMessage"] = "Dữ liệu đã được lưu thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không có bản ghi nào được cập nhật.";
            }

            return Page();
        }

    }
}
