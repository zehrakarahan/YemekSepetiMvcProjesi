using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MarketE_Commerce_Site.IdentityCore;
using MarketE_Commerce_Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketE_Commerce_Site.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUsers> userManager;
        private SignInManager<ApplicationUsers> signManager;
        public AccountController(UserManager<ApplicationUsers> _userManager, SignInManager<ApplicationUsers> _signManager)
        {
            userManager = _userManager;
            signManager = _signManager;
        }
        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await signManager.SignOutAsync();
                    var result = await signManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                TempData["AccountError"] = "Invalid Email or Password";
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAccount(UserAndRoleModels model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmedPassword)
                {
                    var checkEamilIsExist = userManager.FindByEmailAsync(model.Email);
                    ApplicationUsers user = new ApplicationUsers()
                    {
                        Name = model.Name.ToLower(),
                        Surname = model.Surname.ToLower(),
                        Email = model.Email,
                        UserName= model.Name.ToLower(),
                        PasswordHash = model.Password,
                        //EmailConfirmed= model.Email
                        PhoneNumber = model.PhoneNumber
                    };
                    IdentityResult result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        string errorResult = "";
                        foreach (var res in result.Errors)
                        {
                            errorResult +=  res.Description + Environment.NewLine ;
                        }
                        TempData["AccountError"] = errorResult;
                        return View(model);
                    }
                }
                else
                {
                    TempData["AccountError"] = "Passwords Not Match!!";
                    return View(model);
                }

            }
            else
            {
                return View(model);
            }
        }


        public async Task<IActionResult> Logout()
        {
            await signManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccessDeniedPage()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View(new LoginModel());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginModel EmailAddres, string CurrentPassword, string SendNewPassword)
        {
            if (EmailAddres.Email != null)
            {
                Random rndm = new Random();
                int newPassword = rndm.Next(1000000, 3000000);
                var user = await userManager.FindByEmailAsync(EmailAddres.Email);
                if (user == null)
                    ModelState.AddModelError("EMail", "Invalid Email");

                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await userManager.ResetPasswordAsync(user, resetToken, newPassword.ToString());
                if (result.Succeeded)
                {
                    try
                    {
                        MailMessage mailmessage = new MailMessage();
                        mailmessage.From = new MailAddress("ilkerdoner2@gmail.com");
                        mailmessage.To.Add(user.Email);
                        mailmessage.Subject = "Remember Your Password ";
                        mailmessage.IsBodyHtml = true;
                        mailmessage.Body = "Hi " + user.UserName + " ; <br>" + "Your New Password is <br> " + "<b>" + newPassword + "</b> <br>You can Login in This Password Or Change to User Panel Your Password";

                        SmtpClient client = new SmtpClient("smtp.gmail.com");
                        client.Port = 587;
                        client.UseDefaultCredentials = true;
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential("ilkerDoner2@gmail.com", "********");
                        client.Send(mailmessage);
                        @TempData["SuccesCart"] = $"Your Password Change And Send A Email.";
                        return RedirectToAction("Index", "Accounts");
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                        //Gmail Gunvelik ayar sorunu
                        return View(EmailAddres);
                    }
           
                }
                else
                {
                    return View(EmailAddres);
                }
            }
            else
            {
                var user = await userManager.GetUserAsync(User);
                var newpassword = await userManager.ChangePasswordAsync(user, CurrentPassword, SendNewPassword);
                if (newpassword.Succeeded)
                    return View();
               TempData["Error"] = "Error : Passwords Not Match ";
               return RedirectToAction("Index","User");
                
            }


        }

    }
}