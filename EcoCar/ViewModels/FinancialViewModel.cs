using EcoCar.Models.FinancialManagement;
using System.Collections.Generic;

namespace EcoCar.ViewModels
{
    public class FinancialViewModel
    {
        public EcoStore EcoStore { get; set; }

        public EcoWallet EcoWallet { get; set; }

        public BankDetails BankDetails { get; set; }

        public BillingAddress BillingAddress { get; set; }

        public EcoStoreInvoice EcoStoreInvoice { get; set; }

        public ServiceInvoice ServiceInvoice { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public List<EcoStore> EcoStores { get; set; }

    }
}
