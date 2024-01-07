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
    public class GoibaoduongsController : Controller
    {
        private QlxeContext _context;

        public GoibaoduongsController(QlxeContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var goibaoduongs = _context.Goibaoduongs.Select(i => new {
                i.MaGoiBd,
                i.TenGoi,
                i.HangMuc,
                i.ChiPhi,
                i.SoKm,
                i.TrangThaiId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "MaGoiBd" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(goibaoduongs, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Goibaoduong();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Goibaoduongs.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.MaGoiBd });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Goibaoduongs.FirstOrDefaultAsync(item => item.MaGoiBd == key);
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
            var model = await _context.Goibaoduongs.FirstOrDefaultAsync(item => item.MaGoiBd == key);

            _context.Goibaoduongs.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Goibaoduong model, IDictionary values) {
            string MA_GOI_BD = nameof(Goibaoduong.MaGoiBd);
            string TEN_GOI = nameof(Goibaoduong.TenGoi);
            string HANG_MUC = nameof(Goibaoduong.HangMuc);
            string CHI_PHI = nameof(Goibaoduong.ChiPhi);
            string SO_KM = nameof(Goibaoduong.SoKm);
            string TRANG_THAI_ID = nameof(Goibaoduong.TrangThaiId);

            if(values.Contains(MA_GOI_BD)) {
                model.MaGoiBd = Convert.ToInt32(values[MA_GOI_BD]);
            }

            if(values.Contains(TEN_GOI)) {
                model.TenGoi = Convert.ToString(values[TEN_GOI]);
            }

            if(values.Contains(HANG_MUC)) {
                model.HangMuc = Convert.ToString(values[HANG_MUC]);
            }

            if(values.Contains(CHI_PHI)) {
                model.ChiPhi = values[CHI_PHI] != null ? Convert.ToDouble(values[CHI_PHI], CultureInfo.InvariantCulture) : (double?)null;
            }

            if(values.Contains(SO_KM)) {
                model.SoKm = Convert.ToString(values[SO_KM]);
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