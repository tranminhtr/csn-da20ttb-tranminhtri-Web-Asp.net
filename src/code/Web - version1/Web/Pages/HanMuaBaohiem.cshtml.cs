using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages
{
    public class HanMuaBaohiemModel : PageModel
    {
        private readonly QlxeContext _dbContext;

        public HanMuaBaohiemModel(QlxeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            var baohiemxes = _dbContext.Baohiemxes.ToList();

            foreach (var baohiemxe in baohiemxes)
            {
                // Tính số ngày còn lại
                baohiemxe.DemNgay = (int)(baohiemxe.NgayKetThuc - DateTime.Now).TotalDays + 1;

                // Cập nhật trạng thái
                if (baohiemxe.DemNgay > 30)
                {
                    baohiemxe.TrangThai = 10;
                    baohiemxe.DemNgay = null; // DemNgay = null khi Ngay > 30
                }
                else if (baohiemxe.DemNgay > 0)
                {
                    baohiemxe.TrangThai = 10;
                }
                else
                {
                    baohiemxe.TrangThai = 11;
                }
            }
            SaveChangesWithMessage();

            return Page();
        }

        private void SaveChangesWithMessage()
        {
            // Lưu thay đổi vào cơ sở dữ liệu
            _dbContext.SaveChanges();

            // Kiểm tra xem có thay đổi nào hay không và hiển thị thông báo tương ứng
            TempData["SuccessMessage"] = _dbContext.ChangeTracker.HasChanges() ? "Dữ liệu đã được lưu thành công!" : "Không có bản ghi nào được cập nhật.";
        }

    }
}
