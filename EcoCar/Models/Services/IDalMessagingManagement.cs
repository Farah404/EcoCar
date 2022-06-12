
using EcoCar.Models.MessagingManagement;
using System;
using System.Collections.Generic;
using static EcoCar.Models.MessagingManagement.UserReporting;

namespace EcoCar.Models.Services
{
    public interface IDalMessagingManagement: IDisposable
    {
        //Message
        List<Message> GetAllMessages();
        int CreateMessage(string messageContent);
        void UpdateMessage(int id, string messageContent);
        void DeleteMessage(int id);

        //-------------------------------------------------------------------------------------------------

        //Reporting
        List<Reporting> GetAllReportings();
        int CreateReporting(int referenceNumber, DateTime reportingDateTime);
        void UpdateReporting(int id, int referenceNumber, DateTime reportingDateTime);
        void DeleteReporting(int id);

        //-------------------------------------------------------------------------------------------------

        //UserReporting
        List<UserReporting> GetAllUserReportings();
        int CreateUserReporting(string comment, ReportingReason selectReportingReason);
        void UpdateUserReporting(int id, string comment, ReportingReason selectReportingReason);
        void DeleteUserReporting(int id);

        //-------------------------------------------------------------------------------------------------
        
        //HelpReporting
        List<HelpReporting> GetAllHelpReportings();
        int CreateHelpReporting(string helpMessageContent);
        void UpdateHelpReporting(int id, string helpMessageContent);
        void DeleteHelpReporting(int id);

        //-------------------------------------------------------------------------------------------------

        //AdministratorResponse
        List<AdministratorResponse> GetAllAdministratorResponses();
        int CreateAdministratorResponse(string responseContent);
        void UpdateAdministratorResponse(int id, string responseContent);
        void DeleteAdministratorResponse(int id);

        //-------------------------------------------------------------------------------------------------

    }
}
