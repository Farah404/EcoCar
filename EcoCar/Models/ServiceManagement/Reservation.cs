// Link between a proposed service and the user who consumed it
// Usefull if we need to have that information without have X consumers in each Service Class
// MainAuthors : Farah&FrancoisNoel

using EcoCar.Models.PersonManagement;
using System;

namespace EcoCar.Models.ServiceManagement
{
    public class Reservation
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Keys
        public int ServiceConsumedId { get; set; }
        public Service ServiceConsumed { get; set; }
        public int ServiceUserConsumerId { get; set; }
        public User ServiceUserConsumer { get; set; }

    }
}