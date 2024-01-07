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
    public class XesController : Controller
    {
        private QlxeContext _context;

        public XesController(QlxeContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var xes = _context.Xes.Select(i => new {
                i.BienSo,
                i.HangXe,
                i.Doi,
                i.NamSx,
                i.SoChoNgoi,
                i.KieuDang,
                i.TrangThai
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "BienSo" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(xes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Xe();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Xes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.BienSo });
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values) {
            var model = await _context.Xes.FirstOrDefaultAsync(item => item.BienSo == key);
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
        public async Task Delete(string key) {
            var model = await _context.Xes.FirstOrDefaultAsync(item => item.BienSo == key);

            _context.Xes.Remove(model);
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

        private void PopulateModel(Xe model, IDictionary values) {
            string BIEN_SO = nameof(Xe.BienSo);
            string HANG_XE = nameof(Xe.HangXe);
            string DOI = nameof(Xe.Doi);
            string NAM_SX = nameof(Xe.NamSx);
            string SO_CHO_NGOI = nameof(Xe.SoChoNgoi);
            string KIEU_DANG = nameof(Xe.KieuDang);
            string TRANG_THAI = nameof(Xe.TrangThai);

            if(values.Contains(BIEN_SO)) {
                model.BienSo = Convert.ToString(values[BIEN_SO]);
            }

            if(values.Contains(HANG_XE)) {
                model.HangXe = Convert.ToString(values[HANG_XE]);
            }

            if(values.Contains(DOI)) {
                model.Doi = Convert.ToString(values[DOI]);
            }

            if(values.Contains(NAM_SX)) {
                model.NamSx = Convert.ToInt32(values[NAM_SX]);
            }

            if(values.Contains(SO_CHO_NGOI)) {
                model.SoChoNgoi = Convert.ToInt32(values[SO_CHO_NGOI]);
            }

            if(values.Contains(KIEU_DANG)) {
                model.KieuDang = Convert.ToString(values[KIEU_DANG]);
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