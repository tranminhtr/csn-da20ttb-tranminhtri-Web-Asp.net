using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BaohiemxesController : Controller
    {
        private QlxeContext _context;

        public BaohiemxesController(QlxeContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var baohiemxes = _context.Baohiemxes.Select(i => new {
                i.Id,
                i.BienSo,
                i.MaCty,
                i.NgayBatDau,
                i.NgayKetThuc,
                i.Gia,
                i.TrangThai,
                i.DemNgay
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(baohiemxes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Baohiemxe();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Baohiemxes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Baohiemxes.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Baohiemxes.FirstOrDefaultAsync(item => item.Id == key);

            _context.Baohiemxes.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> DanhmucsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Danhmucs
                         orderby i.TenDm
                         select new {
                             Value = i.Id,
                             Text = i.TenDm
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Baohiemxe model, IDictionary values) {
            string ID = nameof(Baohiemxe.Id);
            string BIEN_SO = nameof(Baohiemxe.BienSo);
            string MA_CTY = nameof(Baohiemxe.MaCty);
            string NGAY_BAT_DAU = nameof(Baohiemxe.NgayBatDau);
            string NGAY_KET_THUC = nameof(Baohiemxe.NgayKetThuc);
            string GIA = nameof(Baohiemxe.Gia);
            string TRANG_THAI = nameof(Baohiemxe.TrangThai);
            string DEM_NGAY = nameof(Baohiemxe.DemNgay);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(BIEN_SO)) {
                model.BienSo = Convert.ToString(values[BIEN_SO]);
            }

            if(values.Contains(MA_CTY)) {
                model.MaCty = Convert.ToInt32(values[MA_CTY]);
            }

            if(values.Contains(NGAY_BAT_DAU)) {
                model.NgayBatDau = Convert.ToDateTime(values[NGAY_BAT_DAU]);
            }

            if(values.Contains(NGAY_KET_THUC)) {
                model.NgayKetThuc = Convert.ToDateTime(values[NGAY_KET_THUC]);
            }

            if(values.Contains(GIA)) {
                model.Gia = Convert.ToDouble(values[GIA], CultureInfo.InvariantCulture);
            }

            if(values.Contains(TRANG_THAI)) {
                model.TrangThai = values[TRANG_THAI] != null ? Convert.ToInt32(values[TRANG_THAI]) : (int?)null;
            }

            if(values.Contains(DEM_NGAY)) {
                model.DemNgay = values[DEM_NGAY] != null ? Convert.ToInt32(values[DEM_NGAY]) : (int?)null;
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}