using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
#nullable enable

namespace JewelleryStoreManagementSystem.Data.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly OrderItemService _orderItemService;

        public OrderService(OrderRepository orderRepository, CustomerRepository customerRepository, OrderItemService orderItemService)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderItemService = orderItemService;
        }

        public Order GetOrder(int quantity, int productId, int customerId)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            if (customer.Orders != null && customer.Orders.Count == 0) 
            {
                Order order = new Order
                {
                    CustomerId = customer.CustomerId,
                    OrderDate = DateTime.Now
                };

                _orderRepository.Add(order);
                _orderRepository.SaveChanges();

                _orderItemService.AddOrderItem(quantity, order.OrderId, productId);
                return order;
            }
            else
            {
                Order existingOrder = _orderRepository.GetOrderByCustomerId(customer.CustomerId);
                _orderItemService.AddOrderItem(quantity, existingOrder.OrderId, productId);
                return existingOrder;
            }
        }

        public Order GetOrder(int customerId)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            if (customer.Orders != null && customer.Orders.Count == 0)
            {
                Order order = new Order
                {
                    CustomerId = customer.CustomerId,
                    OrderDate = DateTime.Now
                };

                _orderRepository.Add(order);
                _orderRepository.SaveChanges();
                return order;
            }
            else
            {
                Order existingOrder = _orderRepository.GetOrderByCustomerId(customer.CustomerId);
                return existingOrder;
            }
        }

        public string GenerateEmailHtml(Order order)
        {
            decimal totalPrice = 0;
            int totalItems = 0;

            string htmlContent = $@"
            <html>
            <head>
            </head>

            <body>
                <h1>Thank you for your order!</h1>
                <br/>
                <br/>
                <h2>Order Summary</h2>";

            foreach (OrderItem item in order.OrderItems)
            {
                decimal priceOfOrderItem = item.Quantity * item.Product.Price;
                totalPrice += priceOfOrderItem;
                totalItems += item.Quantity;

                htmlContent += $@"
                    <div>
                        <p><strong>Product:</strong> {item.Product.Name} </p>
                        <p><strong>Quantity:</strong> {item.Quantity} </p>
                        <p><strong>Price:</strong> {string.Format("{0:C}", item.Product.Price * item.Quantity)} </p>
                    </div>
                    <br/>";
            }

            htmlContent += $@"
                    <div>
                        <p><strong>SUBTOTAL:</strong> {string.Format("{0:C}", totalPrice)}</p>
                        <p><strong>Total Items:</strong> {totalItems}</p>
                    </div>
              
            </body>
            </html>";

            return htmlContent;
        }

        public async Task SendEmailAsync(int customerId, string htmlContent)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            string toEmail = customer.Email;
            string subject = "Order Confirmation";

            string fromEmail = "foranassessmentplsignore@gmail.com";
            string password = "augl shub ewxt kijx";

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlContent;
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(fromEmail);

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, password);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);

            }
        }

        public async Task CompleteOrderAsync(int customerId)
        {
            Order order = GetOrder(customerId);
            string htmlContent = GenerateEmailHtml(order);
            await SendEmailAsync(customerId, htmlContent);
        }
    }
}
