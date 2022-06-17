using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using EcoCar.Models.FinancialManagement;

namespace EcoCar.Controllers
{

    public class AccountController : Controller
    {
        private IDalPersonManagement dalPersonManagement;
        private IDalFinancialManagement dalFinancialManagement;
        private IDalServiceManagement dalServiceManagement;
        private IWebHostEnvironment _webEnv;

        public AccountController(IWebHostEnvironment environment)
        {
            dalPersonManagement = new DalPersonManagement();
            dalFinancialManagement = new DalFinancialManagement();
            dalServiceManagement = new DalServiceManagement();
            _webEnv = environment;
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
                    if (account.IsActive == true)
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

                            //SubTest

                            //int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                            Account userAccount = dalPersonManagement.GetUserAccount(account.Id);
                            EcoWallet ecoWallet = dalFinancialManagement.GetUserEcoWallet(account.Id);
                            EcoStore ecoStore = dalFinancialManagement.GetEcoStore(1);
                            int ecoWalletSub = ecoWallet.EcoCoinsAmount;
                            bool firstMonth = ecoWallet.FirstMonth;
                            bool secondMonth = ecoWallet.SecondMonth;
                            bool thirdMonth = ecoWallet.ThirdMonth;
                            bool fourthMonth = ecoWallet.FourthMonth;
                            bool fifthMonth = ecoWallet.FifthMonth;
                            bool sixthMonth = ecoWallet.SixthMonth;

                            if ((DateTime.Compare(userAccount.LastLoginDate, ecoWallet.EcoCoinsFirstMonth) >= 0))
                            {
                                if (ecoWallet.FirstMonth == true)
                                {
                                    firstMonth = false;
                                    ecoWalletSub = ecoWalletSub + ecoStore.MonthlySubscription;
                                }

                                else if ((DateTime.Compare(userAccount.LastLoginDate, ecoWallet.EcoCoinsSecondMonth) >= 0))
                                {
                                    if (ecoWallet.SecondMonth == true)
                                    {
                                        secondMonth = false;
                                        ecoWalletSub = ecoWalletSub + ecoStore.TrimestrialSubscription;
                                    }
                                    else if (DateTime.Compare(userAccount.LastLoginDate, ecoWallet.EcoCoinsThirdMonth) >= 0)
                                    {
                                        if (ecoWallet.ThirdMonth == true)
                                        {
                                            thirdMonth = false;
                                            ecoWalletSub = ecoWalletSub + ecoStore.TrimestrialSubscription;
                                        }
                                        else if (DateTime.Compare(userAccount.LastLoginDate, ecoWallet.EcoCoinsFourthMonth) >= 0)
                                        {
                                            if (ecoWallet.FourthMonth == true)
                                            {
                                                fourthMonth = false;
                                                ecoWalletSub = ecoWalletSub + ecoStore.SemestrialSubscription;
                                            }
                                            else if (DateTime.Compare(userAccount.LastLoginDate, ecoWallet.EcoCoinsFifthMonth) >= 0)
                                            {
                                                if (ecoWallet.FifthMonth == true)
                                                {
                                                    fifthMonth = false;
                                                    ecoWalletSub = ecoWalletSub + ecoStore.SemestrialSubscription;
                                                }
                                                else if (DateTime.Compare(userAccount.LastLoginDate, ecoWallet.EcoCoinsSixthMonth) >= 0)
                                                {
                                                    if (ecoWallet.SixthMonth == true)
                                                    {
                                                        sixthMonth = false;
                                                        ecoWalletSub = ecoWalletSub + ecoStore.SemestrialSubscription;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                dalFinancialManagement.UpdateEcoWallet(
                                    ecoWallet.Id,
                                    ecoWalletSub,
                                    ecoWallet.Subscription,
                                    0,
                                    ecoWallet.SubscriptionExpiration,
                                    ecoWallet.SubscriptionStart,
                                    ecoWallet.EcoCoinsFirstMonth,
                                    ecoWallet.FirstMonth,
                                    ecoWallet.EcoCoinsSecondMonth,
                                    secondMonth,
                                    ecoWallet.EcoCoinsThirdMonth,
                                    thirdMonth,
                                    ecoWallet.EcoCoinsFourthMonth,
                                    fourthMonth,
                                    ecoWallet.EcoCoinsFifthMonth,
                                    fifthMonth,
                                    ecoWallet.EcoCoinsSixthMonth,
                                    sixthMonth);
                            }
                            dalPersonManagement.UpdateAccount(userAccount.Id, userAccount.Username, userAccount.Password, userAccount.IsActive, DateTime.Now);
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
                CarRentalServices = dalServiceManagement.GetAllCarRentalServices(),
                EcoStoreInvoices = dalFinancialManagement.GetAllEcoStoreInvoices()
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
                    EcoWallet = dalFinancialManagement.GetUserEcoWallet((int)id),
                    CarPoolingServices = dalServiceManagement.GetAllUserCarPoolingServices((int)id),
                    CarRentalServices = dalServiceManagement.GetAllUserCarRentalServices((int)id),
                    ParcelServices = dalServiceManagement.GetAllUserParcelServices((int)id)
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
                dalPersonManagement.UpdateAccount(accountToBan.Id, accountToBan.Username, accountToBan.Password, false, accountToBan.LastLoginDate);
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
            int personId = dalPersonManagement.CreatePerson(person.Name, person.LastName);
            string url = "/Financial/CreateBankDetails" + "?personId=" + personId;
            return Redirect(url);
        }

        //Creating a user based on a person
        public IActionResult CreateUser(int bankDetailsId, int billingAddressId, int personId, int? vehiculeId, int? shoppingCartId, int? accountId)
        {
            User user = new User()
            {
                BankDetailsId = bankDetailsId,
                BillingAddressId = billingAddressId,
                PersonId = personId,
                VehiculeId = vehiculeId,
                ShoppingCartId = shoppingCartId,
                AccountId = accountId
            };
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            int userId = dalPersonManagement.CreateUser(
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
             user.ShoppingCartId,
             user.AccountId
             );

            string url = "/Account/CreateAccount" + "?userId=" + userId;
            return Redirect(url);
        }

        //Creating an account based on a user
        public IActionResult CreateAccount(int userId)
        {
            ViewBag.UserId = userId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateAccount(Account account, int userId)
        {
            int accountId = dalPersonManagement.CreateAccount(account.Username, account.Password, account.IsActive, account.CreationDate, DateTime.Now);
            User user = dalPersonManagement.GetUser(userId);
            int ecoWalletId = dalFinancialManagement.CreateEcoWallet(0, false, 0, DateTime.Now, DateTime.MaxValue, DateTime.MaxValue, false, DateTime.MaxValue, false, DateTime.MaxValue, false, DateTime.MaxValue, false, DateTime.MaxValue, false, DateTime.MaxValue, false);
            int shoppingCartId = dalFinancialManagement.CreateShoppingCart(0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0);
            int insuranceId = dalPersonManagement.CreateInsurance(null, DateTime.Now, null);
            int vehiculeId = dalPersonManagement.CreateVehicule(null, 0, null, false, false, DateTime.Now, 0, insuranceId);
            dalPersonManagement.UpdateUser(userId, user.Email, user.BirthDate, user.PhoneNumber, user.IdentityCardNumber, user.IdentityCardNumber, user.BankDetailsId, user.BillingAddressId, user.PersonId, vehiculeId, ecoWalletId, shoppingCartId, accountId);
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
            if (user.Person.ProfilePicture != null)
            {
                if (user.Person.ProfilePicture.Length != 0)
                {
                    string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                    string filePath = Path.Combine(uploads, user.Person.ProfilePicture.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        user.Person.ProfilePicture.CopyTo(fileStream);
                    }
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
                        user.ShoppingCartId,
                        user.AccountId
                        );
                    dalPersonManagement.UpdatePerson(user.PersonId, user.Person.Name, user.Person.LastName, "/images/" + user.Person.ProfilePicture.FileName);
                    dalFinancialManagement.UpdateBankDetails(user.BankDetailsId, user.BankDetails.BankName, user.BankDetails.Swift, user.BankDetails.Iban);
                    dalFinancialManagement.UpdateBillingAddress(user.BillingAddressId, user.BillingAddress.AddressLine, user.BillingAddress.City, user.BillingAddress.Region, user.BillingAddress.Country, user.BillingAddress.PostalCode);

                }
            }
            else
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
                    user.ShoppingCartId,
                    user.AccountId
                    );
                dalPersonManagement.UpdatePerson(user.PersonId, user.Person.Name, user.Person.LastName, user.Person.ProfilePicturePath);
                dalFinancialManagement.UpdateBankDetails(user.BankDetailsId, user.BankDetails.BankName, user.BankDetails.Swift, user.BankDetails.Iban);
                dalFinancialManagement.UpdateBillingAddress(user.BillingAddressId, user.BillingAddress.AddressLine, user.BillingAddress.City, user.BillingAddress.Region, user.BillingAddress.Country, user.BillingAddress.PostalCode);
            }
            return Redirect("/Account/UserProfilePersonal");
        }
        public IActionResult UserProfile(int id)
        {
            AccountViewModel viewModel = new AccountViewModel
            {
                User = dalPersonManagement.GetUser(id),
                Services = dalServiceManagement.GetAllServices(),
                Account = dalPersonManagement.GetAccount(id),
                CarPoolingServices = dalServiceManagement.GetAllUserCarPoolingServices(id),
                CarRentalServices = dalServiceManagement.GetAllUserCarRentalServices(id),
                ParcelServices = dalServiceManagement.GetAllUserParcelServices(id)
            };

            return View(viewModel);
        }



        public IActionResult UserProfilePersonal()
        {
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                AccountViewModel accountViewModel = new AccountViewModel
                {
                    User = dalPersonManagement.GetUser(userId),
                    Services = dalServiceManagement.GetAllServices(),
                    Account = dalPersonManagement.GetUserAccount(userId),
                    Vehicule = dalPersonManagement.GetUserVehicule(userId),
                    EcoWallet = dalFinancialManagement.GetUserEcoWallet(userId),
                    ShoppingCart = dalFinancialManagement.GetUserShoppingCart(userId),
                    CarPoolingServices = dalServiceManagement.GetAllUserCarPoolingServices(userId),
                    CarRentalServices = dalServiceManagement.GetAllUserCarRentalServices(userId),
                    ParcelServices = dalServiceManagement.GetAllUserParcelServices(userId),
                    EcoStoreInvoices = dalFinancialManagement.GetAllEcoStoreInvoices().Where(x => x.UserId == userId).ToList(),
                    ServiceInvoices = dalFinancialManagement.GetAllServiceInvoices().Where(x=>x.IdServiceProvider==userId || x.IdServiceConsumer==userId).ToList(),
                    Insurance = dalPersonManagement.GetUserInsurance(userId)
                };
                return View(accountViewModel);
            }

        }

        public ActionResult ForgotPassword()
        {
            return View();
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
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            User user = dalPersonManagement.GetUser(userId);
            Vehicule vehicule = dalPersonManagement.GetUserVehicule(userId);
            Insurance insuranceToUpdate = dalPersonManagement.GetInsurance(vehicule.InsuranceId);
            dalPersonManagement.UpdateInsurance(
                   insuranceToUpdate.Id,
                   insurance.InsuranceAgency,
                   insurance.InsuranceExpiration,
                   insurance.ContractNumber
                   ); ;
            string url = "/Account/CreateVehicule";
            return Redirect(url);
        }

        public ActionResult CreateVehicule()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateVehicule(Vehicule vehicule)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Vehicule vehiculeToUpdate = dalPersonManagement.GetUserVehicule(userId);
            dalPersonManagement.UpdateVehicule(
                   vehiculeToUpdate.Id,
                   vehicule.Brand,
                   vehicule.RegistrationNumber,
                   vehicule.Model,
                   vehicule.Hybrid,
                   vehicule.Electric,
                   vehicule.TechnicalTestExpiration,
                   vehicule.AvailableSeats,
                   vehiculeToUpdate.InsuranceId
                   );
            string url = "/Account/UserProfilePersonal";
            return Redirect(url);
        }
        #endregion
    }
}