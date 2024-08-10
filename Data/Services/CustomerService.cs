using JewelleryStoreManagementSystem.Data.Repositories;
using JewelleryStoreManagementSystem.Data.Models;

namespace JewelleryStoreManagementSystem.Data.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void AddCustomer(string name, string email, int streetNumber, string street, string city, string state, string phoneNumber, string password)
        {
            string address = $"{streetNumber} {street}, {city}, {state}";
            Customer customer = new Customer { FullName = name, Email = email, Password = password, PhoneNumber = phoneNumber, Address = address };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
        }
    }
}
