
using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;

using System.Linq;


namespace EcoCar.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult UpdateAccount(int id)
        {
            if (id != 0)
            {
                using (IDalPersonManagement dal = new DalPersonManagementt())
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
        public IActionResult UpdateAccount (Account account)
        {
            if (!ModelState.IsValid)
                return View(account);


            if (account.Id != 0)
            {
                using (DalPersonManagementt dal = new DalPersonManagementt())
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

        public IActionResult CreateAccount()
        {
                return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            if (!ModelState.IsValid)
                return View(account);

            using (DalPersonManagementt dal = new DalPersonManagementt())
            {
                dal.CreateAccount(account);
                return RedirectToAction("CreateAccount");

            }
        }
    }
}



