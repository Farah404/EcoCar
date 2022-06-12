using EcoCar.Models.DataBase;
using EcoCar.Models.FinancialManagement;
using EcoCar.Models.MessagingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using static EcoCar.Models.MessagingManagement.UserReporting;

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
            return _bddContext.Messages.ToList();
        }

        //Create Message
        public int CreateMessage(string messageContent)
        {
            Message message = new Message() { MessageContent = messageContent};
            _bddContext.Messages.Add(message);
            _bddContext.SaveChanges();
            return message.Id;
        }
        public void CreateMessage(Message message)
        {
            _bddContext.Messages.Update(message);
            _bddContext.SaveChanges();
        }

        //Update Message
        public void UpdateMessage(int id, string messageContent)
        {
            Message message = _bddContext.Messages.Find(id);

            if (message != null)
            {
                message.Id = id;
                message.MessageContent = messageContent;
                _bddContext.SaveChanges();
            }
        }
        public void UpdatePerson(Message message)
        {
            _bddContext.Messages.Update(message);
            _bddContext.SaveChanges();
        }

        //Delete Message
        public void DeleteMessage(int id)
        {
            Message message = _bddContext.Messages.Find(id);

            if (message != null)
            {
                _bddContext.Messages.Remove(message);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        # region CRUD Reporting
        public List<Reporting> GetAllReportings()
        {
            return _bddContext.Reportings.ToList();
        }

        //Create Reporting
        public int CreateReporting(int referenceNumber, DateTime reportingDateTime)
        {
            Reporting reporting = new Reporting() { ReferenceNumber = referenceNumber, ReportingDateTime = reportingDateTime };
            _bddContext.Reportings.Add(reporting);
            _bddContext.SaveChanges();
            return reporting.Id;
        }
        public void CreateReporting(Reporting reporting)
        {
            _bddContext.Reportings.Update(reporting);
            _bddContext.SaveChanges();
        }

        //Update Reporting
        public void UpdateReporting(int id, int referenceNumber, DateTime reportingDateTime)
        {
            Reporting reporting = _bddContext.Reportings.Find(id);

            if (reporting != null)
            {
                reporting.Id = id;
                reporting.ReferenceNumber = referenceNumber;
                reporting.ReportingDateTime = reportingDateTime;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateReporting(Reporting reporting)
        {
            _bddContext.Reportings.Update(reporting);
            _bddContext.SaveChanges();
        }

        //Delete Reporting
        public void DeleteReporting(int id)
        {
            Reporting reporting = _bddContext.Reportings.Find(id);

            if (reporting != null)
            {
                _bddContext.Reportings.Remove(reporting);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        # region CRUD UserReporting
        public List<UserReporting> GetAllUserReportings()
        {
            return _bddContext.UserReportings.ToList();
        }

        //Create UserReporting
        public int CreateUserReporting(string comment, ReportingReason selectReportingReason)
        {
            UserReporting userReporting = new UserReporting() { Comment = comment, SelectReportingReason = selectReportingReason };
            _bddContext.UserReportings.Add(userReporting);
            _bddContext.SaveChanges();
            return userReporting.Id;
        }
        public void CreateUserReporting(UserReporting userReporting)
        {
            _bddContext.UserReportings.Update(userReporting);
            _bddContext.SaveChanges();
        }

        //Update UserReporting
        public void UpdateUserReporting(int id, string comment, ReportingReason selectReportingReason)
        {
            UserReporting userReporting = _bddContext.UserReportings.Find(id);

            if (userReporting != null)
            {
                userReporting.Id = id;
                userReporting.Comment = comment;
                userReporting.SelectReportingReason = selectReportingReason;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateUserReporting(UserReporting userReporting)
        {
            _bddContext.UserReportings.Update(userReporting);
            _bddContext.SaveChanges();
        }

        //Delete UserReporting
        public void DeleteUserReporting(int id)
        {
            UserReporting userReporting = _bddContext.UserReportings.Find(id);

            if (userReporting != null)
            {
                _bddContext.UserReportings.Remove(userReporting);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD HelpReporting
        public List<HelpReporting> GetAllHelpReportings()
        {
            return _bddContext.HelpReportings.ToList();
        }

        //Create HelpReporting
        public int CreateHelpReporting(string helpMessageContent)
        {
            HelpReporting helpReporting = new HelpReporting() { HelpMessageContent = helpMessageContent};
            _bddContext.HelpReportings.Add(helpReporting);
            _bddContext.SaveChanges();
            return helpReporting.Id;
        }
        public void CreateHelpReporting(HelpReporting helpReporting)
        {
            _bddContext.HelpReportings.Update(helpReporting);
            _bddContext.SaveChanges();
        }

        //Update HelpReporting
        public void UpdateHelpReporting(int id, string helpMessageContent)
        {
            HelpReporting helpReporting = _bddContext.HelpReportings.Find(id);

            if (helpReporting != null)
            {
                helpReporting.Id = id;
                helpReporting.HelpMessageContent = helpMessageContent;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateHelpReporting(HelpReporting helpReporting)
        {
            _bddContext.HelpReportings.Update(helpReporting);
            _bddContext.SaveChanges();
        }

        //Delete HelpReporting
        public void DeleteHelpReporting(int id)
        {
            HelpReporting helpReporting = _bddContext.HelpReportings.Find(id);

            if (helpReporting != null)
            {
                _bddContext.HelpReportings.Remove(helpReporting);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD AdministratorResponse
        public List<AdministratorResponse> GetAllAdministratorResponses()
        {
            return _bddContext.AdministratorResponses.ToList();
        }

        //Create AdministratorResponse
        public int CreateAdministratorResponse(string responseContent)
        {
            AdministratorResponse administratorResponse = new AdministratorResponse() { ResponseContent = responseContent };
            _bddContext.AdministratorResponses.Add(administratorResponse);
            _bddContext.SaveChanges();
            return administratorResponse.Id;
        }
        public void CreateAdministratorResponse(AdministratorResponse administratorResponse)
        {
            _bddContext.AdministratorResponses.Update(administratorResponse);
            _bddContext.SaveChanges();
        }

        //Update AdministratorResponse
        public void UpdateAdministratorResponse(int id, string responseContent)
        {
            AdministratorResponse administratorResponse = _bddContext.AdministratorResponses.Find(id);

            if (administratorResponse != null)
            {
                administratorResponse.Id = id;
                administratorResponse.ResponseContent = responseContent;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateAdministratorResponse(AdministratorResponse administratorResponse)
        {
            _bddContext.AdministratorResponses.Update(administratorResponse);
            _bddContext.SaveChanges();
        }

        //Delete AdministratorResponse
        public void DeleteAdministratorResponse(int id)
        {
            AdministratorResponse administratorResponse = _bddContext.AdministratorResponses.Find(id);

            if (administratorResponse != null)
            {
                _bddContext.AdministratorResponses.Remove(administratorResponse);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

    }
}
