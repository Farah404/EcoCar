using EcoCar.Models.DataBase;
using EcoCar.Models.FinancialManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoCar.Models.Services
{
    public class DalMessagingManagement : IDalMessagingManagement
    {
        private BddContext _bddContext;
        public DalMessagingManagement()
        {
            _bddContext = new BddContext();
        }

       

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
