using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace EcoCar.Controllers.PersonManagement
{
    public class LoginController : Controller
    {
        private IDalUser dalUser;

        public LoginController()
        {
            this.dalUser = new DalUser();
        }



        public ActionResult Index()
        {
            UserViewModel viewModel = new UserViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                viewModel.User = dalUser.ObtenirUser(userId);
                return Redirect("/account/index");
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(UserViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = dalUser.Authentifier(viewModel.User.Prenom, viewModel.User.Password);
                if (user != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Prenom),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                    HttpContext.SignInAsync(userPrincipal);



                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/");
                }
                ModelState.AddModelError("User.Prenom", "Prénom et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }

        public ActionResult CreerCompte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(User user)
        {
            if (ModelState.IsValid)
            {
                User userCreated = dalUser.AjouterUser(user.Prenom, user.Password);

                var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, userCreated.Prenom),
                        new Claim(ClaimTypes.NameIdentifier, userCreated.Id.ToString()),
                        new Claim(ClaimTypes.Role, userCreated.Role.ToString())
                    };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return Redirect("/");
            }
            return View(user);
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
