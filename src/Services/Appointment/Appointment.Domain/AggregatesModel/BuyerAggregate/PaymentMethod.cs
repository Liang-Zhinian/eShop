using Appointment.Domain.Exceptions;
using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;
using System;

namespace SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.BuyerAggregate
{
    public class PaymentMethod
        : Entity
    {
        private string _alias;
        private string _cardNumber;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime _expiration;

        private int _cardTypeId;
        public CardType CardType { get; private set; }


        protected PaymentMethod() { }

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {

            _cardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new AppointmentDomainException(nameof(cardNumber));
            _securityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new AppointmentDomainException(nameof(securityNumber));
            _cardHolderName = !string.IsNullOrWhiteSpace(cardHolderName) ? cardHolderName : throw new AppointmentDomainException(nameof(cardHolderName));

            if (expiration < DateTime.UtcNow)
            {
                throw new AppointmentDomainException(nameof(expiration));
            }

            _alias = alias;
            _expiration = expiration;
            _cardTypeId = cardTypeId;
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber,DateTime expiration)
        {
            return _cardTypeId == cardTypeId
                && _cardNumber == cardNumber
                && _expiration == expiration;
        }
    }
}
