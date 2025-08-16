using System;
using System.Text.RegularExpressions;

namespace ClientManagerBackend.Dominio.ValueObjects
{
    public sealed class EmailVO : ValueObject
    {
        private const string EMAIL_REGEX = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        
        public string Address { get; private set; }

        public EmailVO(string address)
        {
            if (string.IsNullOrEmpty(address))
                return;

            if (!Regex.IsMatch(address, EMAIL_REGEX))
                throw new ArgumentException("email is invalid.");

            Address = address;
        }

        public override string ToString() => Address;

        protected override object GetEqualityComponents() => Address.ToLowerInvariant();

    }
}
