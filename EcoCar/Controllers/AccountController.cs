using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EcoCar.Controllers
{
    public class AccountController : Controller
    {
        private DalPersonManagement dalPersonManagement;
        public AccountController()
        {
            dalPersonManagement = new DalPersonManagement();
        }

        //Login
        public IActionResult LoginAccount()
        {
            AccountViewModel viewModel = new AccountViewModel { Authentification = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                viewModel.Account = dalPersonManagement.GetAccount(userId);
                return Redirect("/home/index");
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult LoginAccount(AccountViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Account account = dalPersonManagement.Authentify(viewModel.Account.Username, viewModel.Account.Password);
                if (account != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Username),
                        new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                    HttpContext.SignInAsync(userPrincipal);

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/Home/Index");
                }
                ModelState.AddModelError("Account.Username", "Nom d'utilisateur et/ou mot de passe incorrect(s)");
            }
            return View();
        }


        //Creating a person
        public IActionResult CreatePerson()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            dalPersonManagement.CreatePerson(person.Name, person.LastName, person.ProfilePictureURL);
            return Redirect("/Financial/CreateBankDetails");
        }

        //Creating a user based on a person
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            dalPersonManagement.CreateUser(user.Email, user.BirthDate, user.PhoneNumber, user.IdentityCardNumber, user.DrivingPermitNumber);
            return Redirect("/Account/CreateAccount");
        }

        //Creating an account based on a user
        public IActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            dalPersonManagement.CreateAccount(account.Username, account.Password, account.IsActive);
            return Redirect("/Home/Index");
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }

        //Updating Account
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

        public ActionResult UserProfile()
        {
            return View();
        }

    }
}



