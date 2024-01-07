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
    public class CtybaohiemsController : Controller
    {
        private QlxeContext _context;

        public CtybaohiemsController(QlxeContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var ctybaohiems = _context.Ctybaohiems.Select(i => new {
                i.MaCty,
                i.TenCty,
                i.DiaChi,
                i.Sdt,
                i.Email,
                i.TrangThai
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "MaCty" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(ctybaohiems, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Ctybaohiem();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Ctybaohiems.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.MaCty });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Ctybaohiems.FirstOrDefaultAsync(item => item.MaCty == key);
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
            var model = await _context.Ctybaohiems.FirstOrDefaultAsync(item => item.MaCty == key);

            _context.Ctybaohiems.Remove(model);
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

        private void PopulateModel(Ctybaohiem model, IDictionary values) {
            string MA_CTY = nameof(Ctybaohiem.MaCty);
            string TEN_CTY = nameof(Ctybaohiem.TenCty);
            string DIA_CHI = nameof(Ctybaohiem.DiaChi);
            string SDT = nameof(Ctybaohiem.Sdt);
            string EMAIL = nameof(Ctybaohiem.Email);
            string TRANG_THAI = nameof(Ctybaohiem.TrangThai);

            if(values.Contains(MA_CTY)) {
                model.MaCty = Convert.ToInt32(values[MA_CTY]);
            }

            if(values.Contains(TEN_CTY)) {
                model.TenCty = Convert.ToString(values[TEN_CTY]);
            }

            if(values.Contains(DIA_CHI)) {
                model.DiaChi = Convert.ToString(values[DIA_CHI]);
            }

            if(values.Contains(SDT)) {
                model.Sdt = Convert.ToString(values[SDT]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(TRANG_THAI)) {
                model.TrangThai = Convert.ToInt32(values[TRANG_THAI]);
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