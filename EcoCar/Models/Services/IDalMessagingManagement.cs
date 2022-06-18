using EcoCar.Models.MessagingManagement;
using System;
using System.Collections.Generic;
//using static EcoCar.Models.MessagingManagement.UserReporting;

namespace EcoCar.Models.Services
{
    public interface IDalMessagingManagement : IDisposable
    {
        //Message
        List<Message> GetAllMessages();
        Message GetUserMessage(int userId);
        Message CreateMessage(string messageContent, int serviceConcernedId, int userSenderId, int userInitializingConversation);


        ////-------------------------------------------------------------------------------------------------

        ////Reporting
        //List<Reporting> GetAllReportings();
        //Reporting GetReportingByUser(int userId);
        //int CreateReporting(int referenceNumber, DateTime reportingDateTime, string reportContent, int administratorId, int administratorResponseId);

        ////-------------------------------------------------------------------------------------------------

        ////UserReporting
        //List<UserReporting> GetAllUserReportings();
        //UserReporting GetUserReportingByUser(int userId);
        //int CreateUserReporting(UserReportingType selectUserReportingType, int reportingId);

        ////-------------------------------------------------------------------------------------------------

        ////HelpReporting
        //List<HelpReporting> GetAllHelpReportings();
        //HelpReporting GetHelpReportingByUser(int userId);
        //int CreateHelpReporting(int userId, int reportingId);

        ////-------------------------------------------------------------------------------------------------

        ////AdministratorResponse
        //List<AdministratorResponse> GetAllAdministratorResponses();
        //AdministratorResponse GetAdministratorResponseByUser(int userId);
        //int CreateAdministratorResponse(string responseContent, int userId, int reportingId);

        ////-------------------------------------------------------------------------------------------------

    }
}