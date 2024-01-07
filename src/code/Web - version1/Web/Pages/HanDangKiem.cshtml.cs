using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages
{
    public class HanDangKiemModel : PageModel
    {
        private readonly QlxeContext _dbContext;

        public HanDangKiemModel(QlxeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            var dangkiemxes = _dbContext.Dangkiemxes.ToList();

            foreach (var dangkiem in dangkiemxes)
            {
                // Tính số ngày còn lại
                dangkiem.DemNgay = (int)(dangkiem.NgayKetThuc - DateTime.Now).TotalDays + 1;

                // Cập nhật trạng thái
                if (dangkiem.DemNgay > 30)
                {
                    dangkiem.TrangThai = 10;
                    dangkiem.DemNgay = null; // DemNgay = null khi Ngay > 30
                }
                else if (dangkiem.DemNgay > 0)
                {
                    dangkiem.TrangThai = 10;
                }
                else
                {
                    dangkiem.TrangThai = 11;
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
