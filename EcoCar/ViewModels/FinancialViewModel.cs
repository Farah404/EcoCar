﻿using EcoCar.Models.FinancialManagement;

namespace EcoCar.ViewModels
{
    public class FinancialViewModel
    {
        public EcoStore EcoStore { get; set; }
        public BankDetails BankDetails { get; set; }
        public BillingAddress BillingAddress { get; set; }

    }
}