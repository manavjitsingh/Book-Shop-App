using BookShoppingProject_1.Data;
using BookShoppingProject_1.Models;
using BookShoppingProject_1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShoppingProject_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.Include(c => c.Company).ToList();//Aspnetusers
            var roles = _context.Roles.ToList();//Aspnetroles
            var userRole = _context.UserRoles.ToList();//Aspnetuserroles
            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.Company==null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }
            //if (!User.IsInRole(SD.Role_Admin))
            //{
                var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
                userList.Remove(adminUser);
            //}
            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            bool isLocked = false;
            var userInDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (userInDb == null)
                return Json(new { success = false, message = "Something went wrong while Locking or Unlocking the user" });
            if(userInDb!=null && userInDb.LockoutEnd>DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = isLocked == true ? "User successfully Locked" : "User Successfully Unlocked" });
        }
        #endregion
    }
}
