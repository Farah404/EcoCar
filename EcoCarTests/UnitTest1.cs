//using EcoCar.Models.PersonManagement;
//using EcoCar.Models.Services;
//using System.Collections.Generic;
//using Xunit;

//namespace EcoCarTests
//{
//    public class UnitTest1
//    {
//        [Fact]
//        public void Create_Account_Verification()
//        {

//            using (DalPersonManagementt dalAccount = new DalPersonManagementt())
//            {

//                dalAccount.DeleteCreateDatabase();

//                dalAccount.CreateAccount("UserTest", "PasswordTest", true);

//                // Nous vérifions que le lieu a bien été créé
//                List<Account> account = dalAccount.GetAllAccount();
//                Assert.NotNull(account);
//                Assert.Single(account);
//                Assert.Equal("UserTest", account[0].Username);
//                Assert.Equal("PasswordTest", account[0].Password);
//                Assert.True(account[0].IsActive);
//            }
//        }
//    }
//}
