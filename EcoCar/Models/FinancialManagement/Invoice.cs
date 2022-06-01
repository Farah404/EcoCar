//Class Description
//The Invoice.cs is the parent class of the EcoStoreInvoice.cs and the ServiceInvoice.cs

using System;

namespace EcoCar.Models.FinancialManagement
{
    public class Invoice
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int InvoiceNumber { get; set; }
        public string InvoiceDescription { get; set; }
        public DateTime InvoiceIssueDate { get; set; }


    }
}
