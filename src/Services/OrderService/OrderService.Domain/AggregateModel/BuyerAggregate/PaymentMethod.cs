using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System;

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
        private string _alias;
        private string _cardNumber;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime _expiration;

        private int _cardTypeId;
        public CardType CardType { get; private set; }

        protected PaymentMethod()
        { }

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            _cardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new OrderingDomainException(nameof(cardNumber));
            _securityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new OrderingDomainException(nameof(securityNumber));
            _cardHolderName = !string.IsNullOrWhiteSpace(cardHolderName) ? cardHolderName : throw new OrderingDomainException(nameof(cardHolderName));

            if (expiration < DateTime.UtcNow)
            {
                throw new OrderingDomainException(nameof(expiration));
            }

            _alias = alias;
            _expiration = expiration;
            _cardTypeId = cardTypeId;
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            return _cardTypeId == cardTypeId
                && _cardNumber == cardNumber
                && _expiration == expiration;
        }
    }
}