// MainAuthors : Farah&FrancoisNoel

using EcoCar.Models.DataBase;
using EcoCar.Models.MessagingManagement;
using EcoCar.Models.PersonManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public Message CreateMessage(string messageContent, int serviceConcernedId, int userSenderId, int userInitializingConversation)
        {
            Message message = new Message()
            {
                MessageContent = messageContent,
                ServiceConcerned = _bddContext.Services.FirstOrDefault(s => s.Id == serviceConcernedId),
                UserSender = _bddContext.Users.FirstOrDefault(s => s.Id == userSenderId),
                UserInitializingConversation = _bddContext.Users.FirstOrDefault(s => s.Id == userInitializingConversation)
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

    }
}