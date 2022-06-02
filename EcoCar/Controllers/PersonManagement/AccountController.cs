﻿using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EcoCar.Controllers.PersonManagement
{
    public class AccountController : Controller
    {
        private DalPersonManagement dalPersonManagement;
        public AccountController()
        {
            dalPersonManagement = new DalPersonManagement();
        }

        public IActionResult Index()
        {
            AccountViewModel viewModel = new AccountViewModel { Authentification = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentification)
            {
                viewModel.Account = dalPersonManagement.GetAccount(HttpContext.User.Identity.Name);
                return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(AccountViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Account account = dalPersonManagement.Authentify(viewModel.Account.Username, viewModel.Account.Password);
                if (account != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Id.ToString())
                    };
                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                    HttpContext.SignInAsync(userPrincipal);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/");
                }
                ModelState.AddModelError("Account.Username", "Username or password are incorrect");
            }
            return View(viewModel);
        }
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            if (ModelState.IsValid)
            {
                int id = dalPersonManagement.CreateAccount(account.Username, account.Password, account.IsActive);
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, id.ToString()),
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);
                return Redirect("/");
            }
            return View(account);
        }
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult UpdateAccount(int id)
        {
            if (id != 0)
            {
                using (IDalPersonManagement dal = new DalPersonManagement())
                {
                    Account account = dal.GetAllAccounts().Where(a => a.Id == id).FirstOrDefault();
                    if (account == null)
                    {
                        return View("Error");
                    }
                    return View(account);
                }
            }
            return View("Error");
        }



        [HttpPost]
        public IActionResult UpdateAccount(Account account)
        {
            if (!ModelState.IsValid)
                return View(account);


            if (account.Id != 0)
            {
                using (DalPersonManagement dal = new DalPersonManagement())
                {
                    dal.UpdateAccount(account);
                    return RedirectToAction("UpdateAccount", new { @id = account.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }

        //public IActionResult CreateAccount()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CreateAccount(Account account)
        //{
        //    if (!ModelState.IsValid)
        //        return View(account);

        //    using (DalPersonManagement dal = new DalPersonManagement())
        //    {
        //        dal.CreateAccount(account);
        //        return RedirectToAction("CreateAccount");

        //    }
        //}
    }
}


