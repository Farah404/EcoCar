// MainAuthors : Farah&FrancoisNoel

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

    }
}