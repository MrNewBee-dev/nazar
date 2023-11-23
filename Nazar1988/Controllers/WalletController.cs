using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Areas.Identity.Data.Services;
using Nazar1988.Models.ViewModels;

namespace Nazar1988.Controllers
{
    public class WalletController : Controller
    {
        private readonly _WalletService _userService;
        private readonly UserManager<Nazar1988User> _context;


        public WalletController(_WalletService userService, UserManager<Nazar1988User> _context)
    {
        _userService = userService;
            this._context = _context;
    }



    [Route("UserPanel/Wallet")]
    public IActionResult Index()
    {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserFound = _context.Users.SingleOrDefault(x => x.Id == UserId).Id;
            if (UserFound ==null)
            {
                return NotFound();
            }
            ViewBag.ListWallet = _userService.GetWalletUser(UserFound);
        return View();
    }

    [Route("UserPanel/Wallet")]
    [HttpPost]
    public ActionResult Index(ChargeWalletViewModel charge)
    {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserFound = _context.Users.SingleOrDefault(x => x.Id == UserId).Id;
            if (UserFound == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
        {
               
                ViewBag.ListWallet = _userService.GetWalletUser(UserFound);
            return View(charge);
        }

            int walletid = _userService.ChargeWallet(UserFound, charge.Amount, "شارژ حساب");
            
            var payment = new ZarinpalSandbox.Payment(charge.Amount);

            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {walletid}",
                "https://localhost:44394/Home/OnlinePaymentWallet/" + walletid);
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }


            //ToDO Online Payment
            
    }
}
}
