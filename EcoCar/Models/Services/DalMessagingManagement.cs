using EcoCar.Models.DataBase;
using EcoCar.Models.MessagingManagement;
using EcoCar.Models.PersonManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using static EcoCar.Models.MessagingManagement.UserReporting;

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

        //-------------------------------------------------------------------------------------------------

        #region CRUD Message
        public List<Message> GetAllMessages()
        {
            return _bddContext.Messages.Include(e=>e.ServiceConcerned).Include(e=>e.UserSender).ToList();
        }

        public Message GetUserMessage(int userId)
        {
            User user = _bddContext.Users.FirstOrDefault(x => x.Id == userId);
            Message message = _bddContext.Messages.Find(userId);
            return message;
        }

        //Create Message
        public Message CreateMessage(string messageContent, int serviceConcernedId, int userSenderId)
        {
            Message message = new Message() { 
                MessageContent = messageContent,
                ServiceConcerned = _bddContext.Services.FirstOrDefault(s=>s.Id==serviceConcernedId),
                UserSender = _bddContext.Users.FirstOrDefault(s=>s.Id==userSenderId)
            };
            _bddContext.Messages.Add(message);
            _bddContext.SaveChanges();
            return message;
        }
        public void CreateMessage(Message message)
        {
            _bddContext.Messages.Update(message);
            _bddContext.SaveChanges();
        }
        #endregion

        ////-------------------------------------------------------------------------------------------------

        //# region CRUD Reporting
        //public List<Reporting> GetAllReportings()
        //{
        //    return _bddContext.Reportings.ToList();
        //}

        //public Reporting GetReportingByUser(int userId)
        //{
        //    User user = _bddContext.Users.FirstOrDefault(x => x.Id == userId);
        //    Reporting reporting = _bddContext.Reportings.Find(userId);
        //    return reporting;
        //}

        ////Create Reporting
        //public int CreateReporting(int referenceNumber, DateTime reportingDateTime, string reportContent, int administratorId, int administratorResponseId)
        //{
        //    Reporting reporting = new Reporting() { 
        //        ReferenceNumber = referenceNumber, 
        //        ReportingDateTime = reportingDateTime,
        //        ReportContent = reportContent,
        //        Administrator = _bddContext.Administrators.FirstOrDefault(a => a.Id == administratorId),
        //        AdministratorResponse = _bddContext.AdministratorResponses.FirstOrDefault(a=>a.Id == administratorResponseId),
        //    };
        //    _bddContext.Reportings.Add(reporting);
        //    _bddContext.SaveChanges();
        //    return reporting.Id;
        //}
        //public void CreateReporting(Reporting reporting)
        //{
        //    _bddContext.Reportings.Update(reporting);
        //    _bddContext.SaveChanges();
        //}

    
        //#endregion

        ////-------------------------------------------------------------------------------------------------

        //# region CRUD UserReporting
        //public List<UserReporting> GetAllUserReportings()
        //{
        //    return _bddContext.UserReportings.ToList();
        //}
        //public UserReporting GetUserReportingByUser(int userId)
        //{
        //    User user = _bddContext.Users.FirstOrDefault(x => x.Id == userId);
        //    UserReporting userReporting = _bddContext.UserReportings.Find(userId);
        //    return userReporting;
        //}
        ////Create UserReporting
        //public int CreateUserReporting(UserReportingType selectUserReportingType, int reportingId)
        //{
        //    UserReporting userReporting = new UserReporting() { SelectUserReportingType = selectUserReportingType, Reporting=_bddContext.Reportings.First(b => b.Id == reportingId) };
        //    _bddContext.UserReportings.Add(userReporting);
        //    _bddContext.SaveChanges();
        //    return userReporting.Id;
        //}
        //public void CreateUserReporting(UserReporting userReporting)
        //{
        //    _bddContext.UserReportings.Update(userReporting);
        //    _bddContext.SaveChanges();
        //}

        //#endregion

        ////-------------------------------------------------------------------------------------------------

        //#region CRUD HelpReporting
        //public List<HelpReporting> GetAllHelpReportings()
        //{
        //    return _bddContext.HelpReportings.ToList();
        //}
        //public HelpReporting GetHelpReportingByUser (int userId)
        //{
        //    User user = _bddContext.Users.FirstOrDefault(x => x.Id == userId);
        //    HelpReporting helpReporting = _bddContext.HelpReportings.Find(userId);
        //    return helpReporting;
        //}

        ////Create HelpReporting
        //public int CreateHelpReporting(int userId, int reportingId)
        //{
        //    HelpReporting helpReporting = new HelpReporting() 
        //    {
        //        User = _bddContext.Users.FirstOrDefault(u => u.Id == userId),
        //        Reporting = _bddContext.Reportings.FirstOrDefault(b => b.Id == reportingId)
        //    };
        //    _bddContext.HelpReportings.Add(helpReporting);
        //    _bddContext.SaveChanges();
        //    return helpReporting.Id;
        //}
        //public void CreateHelpReporting(HelpReporting helpReporting)
        //{
        //    _bddContext.HelpReportings.Update(helpReporting);
        //    _bddContext.SaveChanges();
        //}

        //#endregion

        ////-------------------------------------------------------------------------------------------------

        //#region CRUD AdministratorResponse
        //public List<AdministratorResponse> GetAllAdministratorResponses()
        //{
        //    return _bddContext.AdministratorResponses.ToList();
        //}
        //public AdministratorResponse GetAdministratorResponseByUser(int userId)
        //{
        //    User user = _bddContext.Users.FirstOrDefault(x => x.Id == userId);
        //    AdministratorResponse administratorResponse = _bddContext.AdministratorResponses.Find(userId);
        //    return administratorResponse;
        //}
        ////Create AdministratorResponse
        //public int CreateAdministratorResponse(string responseContent, int userId, int reportingId)
        //{
        //    AdministratorResponse administratorResponse = new AdministratorResponse()
        //    {
        //        ResponseContent = responseContent,
        //        User = _bddContext.Users.FirstOrDefault(u => u.Id == userId),
        //        Reporting = _bddContext.Reportings.FirstOrDefault(b => b.Id == reportingId)
        //    };
        //    _bddContext.AdministratorResponses.Add(administratorResponse);
        //    _bddContext.SaveChanges();
        //    return administratorResponse.Id;
        //}
        //public void CreateAdministratorResponse(AdministratorResponse administratorResponse)
        //{
        //    _bddContext.AdministratorResponses.Update(administratorResponse);
        //    _bddContext.SaveChanges();
        //}

        //#endregion

        //-------------------------------------------------------------------------------------------------

    }
}