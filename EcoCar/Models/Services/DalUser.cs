using EcoCar.Models.PersonManagement;
using System;
using System.Text;
using XSystem.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using EcoCar.Models.DataBase;

namespace EcoCar.Models.Services
{
    public class DalUser : IDalUser
    {
        private BddContext _bddContext;

        // added for authentification

        public User AjouterUser(string prenom, string password, Role role = Role.ReadWrite)
		//public User AjouterUser(string prenom, string password)
		{
			string motDePasse = EncodeMD5(password);
			User customer = new User() { Prenom = prenom, Password = motDePasse }; 
			//User customer = new User() { Prenom = prenom, Password = motDePasse, Role = role };
			this._bddContext.Users.Add(customer);
			this._bddContext.SaveChanges();

			return customer;
		}

		public User Authentifier(string prenom, string password)
		{
			string motDePasse = EncodeMD5(password);
			User customer = this._bddContext.Users.FirstOrDefault(u => u.Prenom == prenom && u.Password == motDePasse);
			return customer;
		}

		public User ObtenirUser(int id)
		{
			return this._bddContext.Users.FirstOrDefault(u => u.Id == id); 
		}

		public User ObtenirUser (string idStr)
		{
			int id;
			

        if (int.TryParse(idStr, out id))
			{
				return this.ObtenirUser(id);
			}
			return null;
		}

		public static string EncodeMD5(string motDePasse)
		{
			string motDePasseSel = "EcoCar" + motDePasse + "ASP.NET MVC";
			return BitConverter.ToString(new XSystem.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
		}

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
