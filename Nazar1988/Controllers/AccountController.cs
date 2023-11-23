using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Models.ViewModels;

namespace Nazar1988.Controllers
{

    public class AccountController : Controller
    {
        
        private readonly UserManager<Nazar1988User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<Nazar1988User> _signInManager;

        public AccountController(UserManager<Nazar1988User> userManager, IEmailSender emailSender, SignInManager<Nazar1988User> _signInManager)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        
            this._signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Nazar1988User { UserName = ViewModel.Email, Email = ViewModel.Email, FirstName = ViewModel.FName, LastName = ViewModel.LName, PhoneNumber = ViewModel.PhoneNumber, RegisterDate = DateTime.Now };
                IdentityResult result = await _userManager.CreateAsync(user, ViewModel.Password);

                if (result.Succeeded)
                {
                    //var role = _roleManager.FindByNameAsync("مشتری");
                    //if (role == null)
                    //    await _roleManager.CreateAsync(new ApplicationRole("مشتری"));
                    if (ViewModel.Register == "مشتری")
                    {
                        result = await _userManager.AddToRoleAsync(user, "مشتری");
                    }
                   else if (ViewModel.Register == "توزیع کننده")
                    {
                        result = await _userManager.AddToRoleAsync(user, "توزیع کننده");
                    }

                    if (result.Succeeded)
                        return RedirectToAction("index", "home");
                   // {
  //                      var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //                    var callbackUrl = Url.Action("ConfirmEmail", "Account", values: new { userId = user.Id, code = code }, protocol: Request.Scheme);

      //                  await _emailSender.SendEmailAsync(ViewModel.Email, "تایید ایمیل حساب کاربری - سایت میزفا", $"<div dir='rtl' style='font-family:tahoma;font-size:14px'>لطفا با کلیک روی لینک رویه رو ایمیل خود را تایید کنید.  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>کلیک کنید</a></div>");

        //                return RedirectToAction("Index", "Home", new { id = "ConfirmEmail" });
                    //}
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                
            }
            return View();
        }
            public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound($"Unable to load user with ID '{userId}'");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                throw new InvalidOperationException($"Error Confirming email for user with ID '{userId}'");

            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(ViewModel.Email, ViewModel.Password, ViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "نام کاربری یا کلمه عبور شما صحیح نمی باشد.");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult  ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(ViewModel.Email);
                if (User == null)
                {
                    ModelState.AddModelError(string.Empty, "ایمیل شما صحیح نمی باشد.");
                }

                if (!await _userManager.IsEmailConfirmedAsync(User))
                {
                    ModelState.AddModelError(string.Empty, "لطفا با تایید ایمیل حساب کاربری خود را فعال کنید.");
                }

                var Code = await _userManager.GeneratePasswordResetTokenAsync(User);
                var CallbackUrl = Url.Action("ResetPassword", "Account", values: new { Code }, protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(ViewModel.Email, "بازیابی کلمه عبور", $"<p style='font-family:tahoma;font-size:14px'>برای بازنشانی کلمه عبور خود <a href='{HtmlEncoder.Default.Encode(CallbackUrl)}'>اینجا کلیک کنید</a></p>");

                return RedirectToAction("ForgetPasswordConfirmation");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string Code = null)
        {
            if (Code == null)
                return NotFound();
            else
            {
                var ViewModel = new ResetPasswordViewModel { Code = Code };
                return View(ViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var User = await _userManager.FindByEmailAsync(ViewModel.Email);
                if (User == null)
                {
                    ModelState.AddModelError(string.Empty, "ایمیل شما صحیح نمی باشد.");
                    return View();
                }
                var Result = await _userManager.ResetPasswordAsync(User, ViewModel.Code, ViewModel.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
