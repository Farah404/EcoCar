//Class Description
//The Invoice.cs is the parent class of the EcoStoreInvoice.cs and the ServiceInvoice.cs

using System;
using System.ComponentModel.DataAnnotations;

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
        public InvoiceType SelectInvoiceType { get; set; }
        public enum InvoiceType
        {
            [Display(Name = "Facture EcoStore")]
            EcoStoreInvoice,
            [Display(Name = "Facture de Prestation")]
            Serviceinvoice
        }


    }
}