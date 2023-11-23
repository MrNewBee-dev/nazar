using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Models.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace Nazar1988.Areas.MyMaster.Controllers
{
    [Area("MyMaster")]
   // [Authorize(Roles = "مدیر")]
    public class RoleManagersController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleManagersController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index(int row = 10, int pages = 1)
        {
            var Role = _roleManager.Roles.Select(r => new RolesViewModel
            {
                RoleID = r.Id,
                RoleName = r.Name
            }).ToList();
            var paginList = PagingList.Create(Role, row, pages);
            paginList.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };
            return View(paginList);

        }



        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RolesViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var Result = await _roleManager.CreateAsync(new ApplicationRole(ViewModel.RoleName));
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "در ذخیره اطلاعات خطایی رخ داده است.";
                return View(ViewModel);
            }

            return View(ViewModel);
        }
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }
            RolesViewModel RoleVm = new RolesViewModel
            {
                RoleID = Role.Id,
                RoleName = Role.Name
            };
            return View(RoleVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RolesViewModel role)
        {
            if (ModelState.IsValid)
            {
                var Role = await _roleManager.FindByIdAsync(role.RoleID);
                if (Role == null)
                {
                    return NotFound();
                }
                Role.Name = role.RoleName.Trim();
                var Result = await _roleManager.UpdateAsync(Role);
                if (Result.Succeeded)
                {
                   return RedirectToAction("index");
                }
                else
                {
                    ViewBag.Error = "در ذخیره اطلاعات خطایی رخ داده است.";
                    return View(role);
                }
            }
            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }

            RolesViewModel ViewModel = new RolesViewModel()
            {
                RoleID = Role.Id,
                RoleName = Role.Name,
            };

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteRole")]
        public async Task<IActionResult> DeletedRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound();
            }

            var Result = await _roleManager.DeleteAsync(Role);
            if (Result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = "در حذف اطلاعات خطایی رخ داده است.";

            RolesViewModel ViewModel = new RolesViewModel()
            {
                RoleID = Role.Id,
                RoleName = Role.Name,
            };

            return View(ViewModel);
        }


    }
}
