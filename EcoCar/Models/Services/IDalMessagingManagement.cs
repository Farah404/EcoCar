using EcoCar.Models.MessagingManagement;
using System;
using System.Collections.Generic;
using static EcoCar.Models.MessagingManagement.UserReporting;

namespace EcoCar.Models.Services
{
    public interface IDalMessagingManagement : IDisposable
    {
        //Message
        List<Message> GetAllMessages();
        int CreateMessage(string messageContent);

        //-------------------------------------------------------------------------------------------------

        //Reporting
        List<Reporting> GetAllReportings();
        int CreateReporting(int referenceNumber, DateTime reportingDateTime);

        //-------------------------------------------------------------------------------------------------

        //UserReporting
        List<UserReporting> GetAllUserReportings();
        int CreateUserReporting(string comment, ReportingReason selectReportingReason, int reportingId);

        //-------------------------------------------------------------------------------------------------

        //HelpReporting
        List<HelpReporting> GetAllHelpReportings();
        int CreateHelpReporting(string helpMessageContent);

        //-------------------------------------------------------------------------------------------------

        //AdministratorResponse
        List<AdministratorResponse> GetAllAdministratorResponses();
        int CreateAdministratorResponse(string responseContent);

        //-------------------------------------------------------------------------------------------------

    }
}