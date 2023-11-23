using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Areas.Identity.Data.Services;
using Nazar1988.Models.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace Nazar1988.Areas.MyMaster.Controllers
{
    [Area("MyMaster")]
    //[Authorize(Roles = "مدیر کل")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<Nazar1988User> _userManager;
        private readonly Nazar1988Context _TypeId;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly _WalletService _userService;
        private readonly IConfiguration _configuration;
        public UserManagerController(UserManager<Nazar1988User> userManager, RoleManager<ApplicationRole> roleManager, _WalletService _userService, Nazar1988Context _TypeId, IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            this._userService = _userService;
            this._TypeId = _TypeId;
        }
        public IActionResult Index(int row = 10, int pages = 1)
        {
            var Users = _userManager.Users.Select(x => new UsersViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                RegisterDate = x.RegisterDate,
                Roles = x.Roles.Select(u => u.Role.Name),
                KifePool = x.KifePool


            }).ToList();
            var paginList = PagingList.Create(Users, row, pages);
            paginList.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };
            return View(paginList);

        }
        public async Task<IActionResult> UserManagerEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Users = await _userManager.Users.Where(y=>y.Id == id).Select(x => new EditProfileManagerViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Roles = x.Roles.Select(u => u.Role.Name),
                KifePool = x.KifePool
            }).FirstOrDefaultAsync();
            ViewBag.AllRoles = _roleManager.Roles.ToList();
            return View(Users);
        }
        [HttpPost]
        public async Task<IActionResult> UserManagerEdit(EditProfileManagerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    IdentityResult Result;
                    var RecentRoles = await _userManager.GetRolesAsync(user);
                    var DeleteRoles = RecentRoles.Except(viewModel.Roles);
                    var AddRoles = viewModel.Roles.Except(RecentRoles);
                    Result = await _userManager.RemoveFromRolesAsync(user, DeleteRoles);
                    if (Result.Succeeded)
                    {
                        Result = await _userManager.AddToRolesAsync(user, AddRoles);
                        if (Result.Succeeded)
                        {
                            user.FirstName = viewModel.FirstName;
                            user.LastName = viewModel.LastName;
                            user.KifePool = viewModel.KifePool;
                            user.PhoneNumber = viewModel.PhoneNumber;
                            
                            

                            Result = await _userManager.UpdateAsync(user);
                            if (Result.Succeeded)
                            {
                                return RedirectToAction("index");
                            }
                        }
                    }

                    if (Result != null)
                    {
                        foreach (var item in Result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }

            ViewBag.AllRoles = _roleManager.Roles.ToList(); 
            return View(viewModel);
        }

        public async Task<IActionResult> AddWalletCart(string id) {

            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.TypeId = new SelectList(_TypeId.walleTTypes, "TypeId", "TypeTitle");
            ViewBag.Id = id;
            return View();

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddWalletCart(WalletViewModelAdmin walletViewModel)
        {
            if (ModelState.IsValid)
            {

                if (walletViewModel.Type == 2)
                {
                    _userService.ChargeWallet(walletViewModel.UserId, walletViewModel.Amount, walletViewModel.Description, true, walletViewModel.CheckId, walletViewModel.Type, false);
                    return RedirectToAction("index");
                }

                if (walletViewModel.PardakhtOni)
                {
                    _userService.ChargeWallet(walletViewModel.UserId, walletViewModel.Amount, walletViewModel.Description, true);
                    return RedirectToAction("index");
                }


                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Nazar1988ContextConnection"))) {

                    

                    try
                    {
                        SqlCommand cmd = new SqlCommand("User_EtebaarWallet ", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = walletViewModel.UserId;
                        cmd.Parameters.Add("@SumAmountersali", SqlDbType.Int).Value = walletViewModel.Amount;
                        cmd.Parameters.Add("@Out", SqlDbType.Bit).Direction = ParameterDirection.ReturnValue;
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int ret = int.Parse(cmd.Parameters["@Out"].Value.ToString());
                        if (ret == 0)
                        {
                            _userService.ChargeWallet(walletViewModel.UserId, walletViewModel.Amount, walletViewModel.Description, true, walletViewModel.CheckId, walletViewModel.Type, true);
                        }
                        else {
                            return Content("اعتبار شما به حداکثر میزان خودش رسیده لطفا با پشتیبانی تماس بگیرید");
                        
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    
                    }
                    
                }
                    

                return RedirectToAction("index");
            }
            ViewBag.TypeId = new SelectList(_TypeId.walleTTypes, "TypeId", "TypeTitle");
            ViewBag.Id = walletViewModel.UserId;
            return View(walletViewModel);

        }
    }
}
    
