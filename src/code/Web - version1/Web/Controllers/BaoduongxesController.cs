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
    public class BaoduongxesController : Controller
    {
        private QlxeContext _context;

        public BaoduongxesController(QlxeContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var baoduongxes = _context.Baoduongxes.Select(i => new {
                i.Id,
                i.BienSxId,
                i.MaCtybdId,
                i.NgayBd,
                i.NgayKt,
                i.MaGoiBdId,
                i.DemNgay,
                i.TrangThaiId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(baoduongxes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Baoduongxe();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Baoduongxes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Baoduongxes.FirstOrDefaultAsync(item => item.Id == key);
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
            var model = await _context.Baoduongxes.FirstOrDefaultAsync(item => item.Id == key);

            _context.Baoduongxes.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Baoduongxe model, IDictionary values) {
            string ID = nameof(Baoduongxe.Id);
            string BIEN_SX_ID = nameof(Baoduongxe.BienSxId);
            string MA_CTYBD_ID = nameof(Baoduongxe.MaCtybdId);
            string NGAY_BD = nameof(Baoduongxe.NgayBd);
            string NGAY_KT = nameof(Baoduongxe.NgayKt);
            string MA_GOI_BD_ID = nameof(Baoduongxe.MaGoiBdId);
            string DEM_NGAY = nameof(Baoduongxe.DemNgay);
            string TRANG_THAI_ID = nameof(Baoduongxe.TrangThaiId);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(BIEN_SX_ID)) {
                model.BienSxId = Convert.ToString(values[BIEN_SX_ID]);
            }

            if(values.Contains(MA_CTYBD_ID)) {
                model.MaCtybdId = values[MA_CTYBD_ID] != null ? Convert.ToInt32(values[MA_CTYBD_ID]) : (int?)null;
            }

            if(values.Contains(NGAY_BD)) {
                model.NgayBd = values[NGAY_BD] != null ? Convert.ToDateTime(values[NGAY_BD]) : (DateTime?)null;
            }

            if(values.Contains(NGAY_KT)) {
                model.NgayKt = values[NGAY_KT] != null ? Convert.ToDateTime(values[NGAY_KT]) : (DateTime?)null;
            }

            if(values.Contains(MA_GOI_BD_ID)) {
                model.MaGoiBdId = values[MA_GOI_BD_ID] != null ? Convert.ToInt32(values[MA_GOI_BD_ID]) : (int?)null;
            }

            if(values.Contains(DEM_NGAY)) {
                model.DemNgay = values[DEM_NGAY] != null ? Convert.ToInt32(values[DEM_NGAY]) : (int?)null;
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