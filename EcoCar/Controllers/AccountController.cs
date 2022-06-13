using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EcoCar.Controllers
{
    
    public class AccountController : Controller
    {
        private IDalPersonManagement dalPersonManagement;
        private IDalFinancialManagement dalFinancialManagement;
        private IDalServiceManagement dalServiceManagement;
        public AccountController()
        {
            dalPersonManagement = new DalPersonManagement();
            dalFinancialManagement = new DalFinancialManagement();
            dalServiceManagement = new DalServiceManagement();
        }

        #region Authentification
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
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult LoginAccount(AccountViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Account account = dalPersonManagement.Authentify(viewModel.Account.Username, viewModel.Account.Password);
                if (account != null)
                       if(account.IsActive==true)
                    { 
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
            return View();
        }
        #endregion
        #region Admin

        public IActionResult AdminHome()
        {
            AccountViewModel accountViewModel = new AccountViewModel
            {
                Users = dalPersonManagement.GetAllUsers(),
                Accounts = dalPersonManagement.GetAllAccounts(),
                Services = dalServiceManagement.GetAllServices(),
                CarPoolingServices = dalServiceManagement.GetAllCarPoolingServices(),
                ParcelServices = dalServiceManagement.GetAllParcelServices(),
                CarRentalServices = dalServiceManagement.GetAllCarRentalServices()
            };
            return View(accountViewModel);
        }

        public IActionResult LoginAdministrator()
        {
            AccountViewModel viewModel = new AccountViewModel { Authentification = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var administratorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                viewModel.Administrator = dalPersonManagement.GetAdministrator(administratorId);
                return Redirect("/Account/AdminHome");
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult LoginAdministrator(AccountViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Administrator administrator = dalPersonManagement.AuthentifyAdministrator(viewModel.Administrator.Username, viewModel.Administrator.Password);
                if (administrator != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, administrator.Username),
                        new Claim(ClaimTypes.NameIdentifier, administrator.Id.ToString()),
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                    HttpContext.SignInAsync(userPrincipal);

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/Account/AdminHome");
                }
                ModelState.AddModelError("Administrator.Username", "Nom d'utilisateur et/ou mot de passe incorrect(s)");
            }
            return Redirect("/Account/AdminHome");
        }
        public IActionResult UserProfilePersonalByAdmin(int? id)
        
        {
            if (id.HasValue)
            {
                AccountViewModel accountViewModel = new AccountViewModel
                {
                    User = dalPersonManagement.GetUser((int)id),
                    Services = dalServiceManagement.GetAllServices(),
                    Account = dalPersonManagement.GetAccount((int)id),
                    CarPoolingServices = dalServiceManagement.GetAllCarPoolingServices(),
                    CarRentalServices = dalServiceManagement.GetAllCarRentalServices(),
                    ParcelServices = dalServiceManagement.GetAllParcelServices()
                };
                return View(accountViewModel);
            }
            return NotFound();

        }
        [HttpPost]
        public IActionResult UserProfilePersonalByAdmin(int userid)
        {
            if (userid != null)
            {
                Account accountToBan = dalPersonManagement.GetAccount(userid);
                dalPersonManagement.UpdateAccount(accountToBan.Id, accountToBan.Username, accountToBan.Password, false, accountToBan.PersonId);
                return Redirect("/Account/adminHome");
            }
            return Redirect("/");
        }
        #endregion

        #region Creating a user
        //Creating a person
        public IActionResult CreatePerson()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            int personId = dalPersonManagement.CreatePerson(person.Name, person.LastName, person.ProfilePictureURL);
            string url = "/Financial/CreateBankDetails" + "?personId=" + personId;
            return Redirect(url);
        }

        //Creating a user based on a person
        public IActionResult CreateUser(int bankDetailsId, int billingAddressId, int personId, int? vehiculeId)
        {
            User user = new User()
            {
                BankDetailsId = bankDetailsId,
                BillingAddressId = billingAddressId,
                PersonId = personId,
                VehiculeId = vehiculeId
            };
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User user, int personId)
        {
                dalPersonManagement.CreateUser(
                user.Email,
                user.BirthDate,
                user.PhoneNumber,
                user.IdentityCardNumber,
                user.DrivingPermitNumber,
                user.UserRating,
                user.SelectEcoStatusType,
                user.BankDetailsId,
                user.BillingAddressId,
                user.PersonId,
                user.VehiculeId,
                user.EcoWalletId,
                user.AccountId
                );

                string url = "/Account/CreateAccount" + "?personId=" + personId;
                return Redirect(url);
        }

        //Creating an account based on a user
        public IActionResult CreateAccount(int personId)
        {
            ViewBag.PersonId = personId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateAccount(Account account, int personId)
        {
            dalPersonManagement.CreateAccount(account.Username, account.Password, account.IsActive,account.CreationDate, account.PersonId);
            return Redirect("/Account/LoginAccount");
        }

        public ActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }

        //Updating Account
        public IActionResult UpdateUser(int? id)
        
        {
            if (id.HasValue)
            {
                User user = dalPersonManagement.GetAllUsers().FirstOrDefault(r => r.Id == id.Value);
                if (user == null)
                    return View("Error");


                return View(user);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            dalPersonManagement.UpdateUser(
                user.Id,
                user.Email,
                user.BirthDate,
                user.PhoneNumber,
                user.IdentityCardNumber,
                user.DrivingPermitNumber,
                user.BankDetailsId,
                user.BillingAddressId,
                user.PersonId,
                user.VehiculeId,
                user.EcoWalletId,
                user.AccountId
                );
            dalPersonManagement.UpdatePerson(user.PersonId, user.Person.Name, user.Person.LastName, user.Person.ProfilePictureURL);
            dalFinancialManagement.UpdateBankDetails(user.BankDetailsId, user.BankDetails.BankName, user.BankDetails.Swift, user.BankDetails.Iban);
            dalFinancialManagement.UpdateBillingAddress(user.BillingAddressId, user.BillingAddress.AddressLine, user.BillingAddress.City, user.BillingAddress.Region, user.BillingAddress.Country, user.BillingAddress.PostalCode);
            
            return Redirect("/Account/UserProfilePersonal");
        }
        public IActionResult UserProfile(int id)
        {
            AccountViewModel viewModel = new AccountViewModel
            {
                User = dalPersonManagement.GetUser(id),
                Services = dalServiceManagement.GetAllServices(),
                Account = dalPersonManagement.GetAccount(id),
                CarPoolingServices = dalServiceManagement.GetAllCarPoolingServices(),
                CarRentalServices = dalServiceManagement.GetAllCarRentalServices(),
                ParcelServices = dalServiceManagement.GetAllParcelServices()
            };
            
            return View(viewModel) ;
        }



        public IActionResult UserProfilePersonal()
        {
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                AccountViewModel accountViewModel = new AccountViewModel
                {
                    User = dalPersonManagement.GetUser(userId),
                    Services = dalServiceManagement.GetAllServices(),
                    Account = dalPersonManagement.GetAccount(userId),
                    CarPoolingServices = dalServiceManagement.GetAllCarPoolingServices(),
                    CarRentalServices = dalServiceManagement.GetAllCarRentalServices(),
                    ParcelServices = dalServiceManagement.GetAllParcelServices()
                };
                return View(accountViewModel);
            }
          
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email, string password)
        {

            User user = dalPersonManagement.GetUserByEmail(email);
            if (user != null)
            {
                Account account = dalPersonManagement.GetAccount(user.Id);
                dalPersonManagement.UpdateAccountPassword(account.Id, password);
                return Redirect("/account/loginAccount");
            }

            return Redirect("/account/forgotpassword");
        }
        #endregion

        #region Creating a vehicule with a valid insurance
        public IActionResult CreateInsurance()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInsurance(Insurance insurance)
        {
            int insuranceId = dalPersonManagement.CreateInsurance(
                   insurance.InsuranceAgency,
                   insurance.InsuranceExpiration,
                   insurance.ContractNumber
                   );
            string url = "/Account/CreateVehicule" + "?insuranceId=" + insuranceId;
            return Redirect(url);
        }

        public ActionResult CreateVehicule(int insuranceId)
        {
            Vehicule vehicule = new Vehicule()
            {
                InsuranceId = insuranceId,
            };
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateVehicule(Vehicule vehicule)
        {
            int vehiculeId = dalPersonManagement.CreateVehicule(
                   vehicule.Brand,
                   vehicule.RegistrationNumber,
                   vehicule.Model,
                   vehicule.Hybrid,
                   vehicule.Electric,
                   vehicule.TechnicalTestExpiration,
                   vehicule.AvailableSeats,
                   vehicule.InsuranceId
                   );
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            dalPersonManagement.UpdateUserVehicule(userId, vehiculeId);
            string url = "/Account/UserProfilePersonal";
            return Redirect(url);
        }
        #endregion
    }
}



