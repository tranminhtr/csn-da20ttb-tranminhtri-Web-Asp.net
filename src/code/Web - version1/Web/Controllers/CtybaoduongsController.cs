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
    public class CtybaoduongsController : Controller
    {
        private QlxeContext _context;

        public CtybaoduongsController(QlxeContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var ctybaoduongs = _context.Ctybaoduongs.Select(i => new {
                i.MaCtybd,
                i.TenCtybd,
                i.DiaChi,
                i.Sdt,
                i.Email,
                i.TrangThaiId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "MaCtybd" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(ctybaoduongs, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Ctybaoduong();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Ctybaoduongs.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.MaCtybd });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Ctybaoduongs.FirstOrDefaultAsync(item => item.MaCtybd == key);
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
            var model = await _context.Ctybaoduongs.FirstOrDefaultAsync(item => item.MaCtybd == key);

            _context.Ctybaoduongs.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Ctybaoduong model, IDictionary values) {
            string MA_CTYBD = nameof(Ctybaoduong.MaCtybd);
            string TEN_CTYBD = nameof(Ctybaoduong.TenCtybd);
            string DIA_CHI = nameof(Ctybaoduong.DiaChi);
            string SDT = nameof(Ctybaoduong.Sdt);
            string EMAIL = nameof(Ctybaoduong.Email);
            string TRANG_THAI_ID = nameof(Ctybaoduong.TrangThaiId);

            if(values.Contains(MA_CTYBD)) {
                model.MaCtybd = Convert.ToInt32(values[MA_CTYBD]);
            }

            if(values.Contains(TEN_CTYBD)) {
                model.TenCtybd = Convert.ToString(values[TEN_CTYBD]);
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

            if(values.Contains(TRANG_THAI_ID)) {
                model.TrangThaiId = values[TRANG_THAI_ID] != null ? Convert.ToInt32(values[TRANG_THAI_ID]) : (int?)null;
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