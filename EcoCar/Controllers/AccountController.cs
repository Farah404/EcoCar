using EcoCar.Models.PersonManagement;
using EcoCar.Models.FinancialManagement;
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
        private IDalPersonManagement dalPersonManagement;
        private IDalFinancialManagement dalFinancialManagement;
        public AccountController()
        {
            dalPersonManagement = new DalPersonManagement();
            dalFinancialManagement = new DalFinancialManagement();
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
            return View(viewModel);
        }

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
                user.BankDetailsId, 
                user.BillingAddressId, 
                user.PersonId,
                user.VehiculeId);

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
            dalPersonManagement.CreateAccount(account.Username, account.Password, account.IsActive, account.PersonId);
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

                //string fileName = sejour.ImagePath.Split('/').Last();
                //string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                //string filePath = Path.Combine(uploads, fileName);
                //using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    var file = new FormFile(fileStream, 0, fileStream.Length, null, fileName);
                //    sejour.Image = file;

                //}
                return View(user);
            }
            else
                return NotFound();
            //int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //List<User> users = dalPersonManagement.GetAllUsers();
            //users = users.Where(u => u.Id == userId).ToList();
            //return View(users.ToList());

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
                user.PersonId
                //user.BankDetails.BankName,
                //user.BankDetails.Iban,
                //user.BankDetails.Swift
                //user.BillingAddress.City,
                //user.BillingAddress.Country,
                //user.BillingAddress.Region,
                //user.BillingAddress.AddressLine,
                //user.BillingAddress.PostalCode,
                //user.Person.Name,
                //user.Person.LastName,
                //user.Person.ProfilePictureURL
                );
            dalPersonManagement.UpdatePerson(user.PersonId, user.Person.Name, user.Person.LastName, user.Person.ProfilePictureURL);
            dalFinancialManagement.UpdateBankDetails(user.BankDetailsId, user.BankDetails.BankName, user.BankDetails.Swift, user.BankDetails.Iban);
            dalFinancialManagement.UpdateBillingAddress(user.BillingAddressId, user.BillingAddress.AddressLine, user.BillingAddress.City, user.BillingAddress.Region, user.BillingAddress.Country, user.BillingAddress.PostalCode);
            
            return Redirect("/Account/UserProfilePersonal");
        }

        public IActionResult UserProfile()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<User> users = dalPersonManagement.GetAllUsers();
            users = users.Where(u => u.Id==userId).ToList();
            return View(users.ToList()) ;
        }



        public IActionResult UserProfilePersonal()
        {
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                List<User> users = dalPersonManagement.GetAllUsers();
                users = users.Where(u => u.Id == userId).ToList();
                return View(users.ToList());
            }
          
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult CreateInsurance()
        {
            return View();
        }

        public IActionResult LoginAdmin()
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
            dalPersonManagement.CreateVehicule(
                   vehicule.Brand,
                   vehicule.RegistrationNumber,
                   vehicule.Model,
                   vehicule.Hybrid,
                   vehicule.Electric,
                   vehicule.TechnicalTestExpiration,
                   vehicule.AvailableSeats,
                   vehicule.InsuranceId
                   );
            string url = "/Account/UserProfilePersonal";
            return Redirect(url);
        }

    }
}



