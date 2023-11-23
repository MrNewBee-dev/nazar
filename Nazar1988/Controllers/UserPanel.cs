using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Areas.Identity.Data.Services;
using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace Nazar1988.Controllers
{
    public class UserPanel : Controller
    {
        private readonly UserManager<Nazar1988User> _context;
        private readonly NazarDbContext _contextProduct;
        private readonly _WalletService _walletService;
        public UserPanel(UserManager<Nazar1988User> _context, NazarDbContext _contextProduct, _WalletService _walletService)
        {
            this._context = _context;
            this._contextProduct = _contextProduct;
            this._walletService = _walletService;
        }
        public IActionResult Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserFound = _context.Users.SingleOrDefault(x => x.Id == UserId);
            if (UserFound == null)
            {
                return NotFound();
            }
            InformationUserViewModel informationUserViewModel = new InformationUserViewModel()
            {
                Email = UserFound.Email,
                RegisterDate = UserFound.RegisterDate,
                UserName = UserFound.FirstName + " " + UserFound.LastName,
                Wallet = _walletService.BalanceUserWallet(UserFound.Id)
            };
            return View(informationUserViewModel);
        }

        public IActionResult EditProfile()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserFound = _context.Users.SingleOrDefault(x => x.Id == UserId);
            if (UserFound == null)
            {
                return NotFound();
            }
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel()
            {
                Email = UserFound.Email,
                FirstName = UserFound.FirstName,
                LastName = UserFound.LastName,
                PhoneNumber = UserFound.PhoneNumber

            };
            return View(editProfileViewModel);

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editProfileViewModel)
        {
            if (ModelState.IsValid)
            {


                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var UserFound = await _context.FindByIdAsync(UserId);
                if (UserFound == null)
                {
                    return NotFound();
                }

                UserFound.UserName = editProfileViewModel.Email;
                UserFound.Email = editProfileViewModel.Email;
                UserFound.FirstName = editProfileViewModel.FirstName;
                UserFound.LastName = editProfileViewModel.LastName;
                UserFound.PhoneNumber = editProfileViewModel.PhoneNumber;


                IdentityResult result = await _context.UpdateAsync(UserFound);
                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }


            }

            return View(editProfileViewModel);
        }

        public  IActionResult FactorUser(int row = 10, int pages = 1)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var UserFactor = _contextProduct.View_User_Order.FromSqlRaw($"exec UserFactor @UserId='{UserId}'");

            if (UserId == null || UserFactor == null)
            {
                return NotFound();
            }
            var paginList = PagingList.Create(UserFactor, row, pages);
            paginList.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };

            return View(paginList);
        }
        public async Task<IActionResult> Details(int id)
        {

            var OrderDetails = await _contextProduct.OrderDetails.Where(x => x.OrderId == id).Select(y => new ShowOrderViewModel
            {
                Count = y.Count,
                Sum = y.Count * y.Price,
                Title = y.Product.ProductName,
                Price = y.Price

            }).ToListAsync();


            return View(OrderDetails);
        }
    }
}
