using EcoCar.Models.PersonManagement;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.MessagingManagement
{
    public class UserReporting
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string Comment { get; set; }

        [Required]
        public virtual int ReportingReasonId
        {
            get
            {
                return (int)this.ReportingReason;
            }
            set
            {
                ReportingReason = (ReportingReasonType)value;
            }
        }
        [EnumDataType(typeof(ReportingReasonType))]
        public ReportingReasonType ReportingReason { get; set; }
        public enum ReportingReasonType
        {
            FalseIdentity = 0,
            FalseAccount = 1,
            InappropriateContent = 2,
            OffensiveLanguage = 3,
            InappropriateProfilePicture = 4
        }

        //Foreign Keys


        //Inheritance Class
        public int ReportingId { get; set; }
        public Reporting Reporting { get; set; }
    }
}
