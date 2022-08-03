using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.AggregateModel.OrderAggreage
{
    public class OrderItem
     : IAggregateRoot
    {
        
        public string ProductName;

        public string PictureUrl;
        public decimal UnitPrice;
        public decimal Discount;
        public int Units;

        public int ProductId { get;  set; }

        protected OrderItem()
        { }

        public OrderItem(string productName, string pictureUrl, decimal unitPrice, decimal discount, int units, int productId)
        {
            ProductName = productName;
            PictureUrl = pictureUrl;
            UnitPrice = unitPrice;
            Discount = discount;
            Units = units;
            ProductId = productId;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var results = new List<ValidationResult>();
            if (Units<=0)
            {
                results.Add(new ValidationResult("Invalid number if units", new[] { "units" }));
            }
            return results;
        }
        public string GetOrderItemProductName() => ProductName;
        public int GetUnits()
        {
            return Units;
        }

        public decimal GetUnitPrice()
        {
            return UnitPrice;
        }
    }
  }
