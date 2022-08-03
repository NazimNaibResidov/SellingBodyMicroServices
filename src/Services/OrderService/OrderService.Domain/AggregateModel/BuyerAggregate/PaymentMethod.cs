using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace OrderService.Domain.AggregateModel.BuyerAggregate
{
    //public class Buyer
    //    : BaseEntity, IAggregateRoot
    //{
    //    public string IdentityGuid { get; private set; }

    //    public string Name { get; private set; }

    //    private List<PaymentMethod> _paymentMethods;

    //    public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    //    protected Buyer()
    //    {
    //        _paymentMethods = new List<PaymentMethod>();
    //    }

    //    public Buyer(string identity, string name) : this()
    //    {
    //        IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
    //        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    //    }

    //    public PaymentMethod VerifyOrAddPaymentMethod(
    //        int cardTypeId, string alias, string cardNumber,
    //        string securityNumber, string cardHolderName, DateTime expiration, int orderId)
    //    {
    //        var existingPayment = _paymentMethods
    //            .SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expiration));

    //        if (existingPayment != null)
    //        {
    //            AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, existingPayment, orderId));

    //            return existingPayment;
    //        }

    //        var payment = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);

    //        _paymentMethods.Add(payment);

    //        AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, payment, orderId));

    //        return payment;
    //    }
    //}
    public class PaymentMethod
    : BaseEntity
    {
        private string Alias;
        private string CardNumber;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime Expiration;

        private int CardTypeId;
        public CardType CardType { get; private set; }

        protected PaymentMethod()
        { }

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            CardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new OrderingDomainException(nameof(cardNumber));
            _securityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new OrderingDomainException(nameof(securityNumber));
            _cardHolderName = !string.IsNullOrWhiteSpace(cardHolderName) ? cardHolderName : throw new OrderingDomainException(nameof(cardHolderName));

            if (expiration < DateTime.UtcNow)
            {
                throw new OrderingDomainException(nameof(expiration));
            }

            Alias = alias;
            Expiration = expiration;
            CardTypeId = cardTypeId;
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            return CardTypeId == cardTypeId
                && CardNumber == cardNumber
                && Expiration == expiration;
        }
    }
}