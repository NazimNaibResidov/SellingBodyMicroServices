using System;
using System.Collections.Generic;

namespace OrderService.Application.ViewModels
{
    public class OrderDatialViewModel
    {
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Descrriptiond { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Contry { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }
    }
}