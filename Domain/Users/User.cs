using Domain.ProductRates;
using Domain.Users.Enums;
using Microsoft.AspNetCore.Identity;
using Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Domain.Users
{
    public class User : IdentityUser
    {
        #region Constructors

        private User()
        {

        }

        public User(string userName, string email, string phone, string fullName, Gender gender)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ValidationException(Localizations.RequiredFullName);

            UserName = userName;
            Email = email;
            PhoneNumber = phone;
            FullName = fullName;
            Gender = gender;
        }

        #endregion

        #region Members

        public string FullName { get; private set; }
        public Gender Gender { get; private set; }
        public ICollection<ProductRate> Rates { get; private set; } = new List<ProductRate>();

        #endregion

        #region Methods

        public void Update(string phone, string fullName, Gender gender)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ValidationException(Localizations.RequiredFullName);

            PhoneNumber = phone;
            FullName = fullName;
            Gender = gender;
        }

        #endregion
    }
}
