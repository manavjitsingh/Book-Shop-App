using BookShoppingProject_1.DataAccess.Repository.IRepository;
using BookShoppingProject_1.Models.ViewModels;
using BookShoppingProject_1.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null) return View(coverType);
            //coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            var param = new DynamicParameters();
            param.Add("@Id", id.GetValueOrDefault());
            coverType = _unitOfWork.SP_CALL.OneRecord<CoverType>(SD.Proc_GetCoverType, param);
            if (coverType == null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null) return NotFound();
            if (!ModelState.IsValid) return View();
            var param = new DynamicParameters();
            param.Add("@Name", coverType.Name);
            if (coverType.Id == 0)
                _unitOfWork.SP_CALL.Execute(SD.Proc_CreateCoverType, param);
            //_unitOfWork.CoverType.Add(coverType);
            else
            {
                param.Add("@Id", coverType.Id);
                _unitOfWork.SP_CALL.Execute(SD.Proc_UpdateCoverType, param);
            }
            //    _unitOfWork.CoverType.Update(coverType);
            //_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var coverTypeList = _unitOfWork.SP_CALL.List<CoverType>(SD.Proc_GetCoverTypes);
            return Json(new { data = coverTypeList });
            //return Json(new { data = _unitOfWork.CoverType.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var coverTypeInDb = _unitOfWork.CoverType.Get(id);
            if (coverTypeInDb == null)
                return Json(new { success = false, message = "Error While Deleting Data!" });
            var param = new DynamicParameters();
            param.Add("@Id", id);
            _unitOfWork.SP_CALL.Execute(SD.Proc_DeleteCoverType, param);
            //_unitOfWork.CoverType.Remove(coverTypeInDb);
            //_unitOfWork.Save();
            return Json(new { success = true, message = "Data deleted successfully" });
        }

        #endregion
    }
}
