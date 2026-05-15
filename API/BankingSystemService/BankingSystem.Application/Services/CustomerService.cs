using BankingSystem.Application.DTOs.Customer;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankingSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customer;
        public CustomerService(IRepository<Customer> customer)
        {
            _customer = customer;
        }
        public async Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName=customerDTO.FirstName,
                LastName=customerDTO.LastName,
                Email=customerDTO.Email
            };

            await _customer.AddAsync(customer);
            await _customer.SaveChangesAsync();

            return new CustomerDTO
            {
                Id = customer.Id,
                Name=$"{customer.FirstName} +{customer.LastName}",
                Email = customer.Email
            };
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await _customer.GetByIdAsync(customerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {customerId} not found");

            _customer.Delete(customer);
        }

        public async Task<CustomerDTO> UpdateCustomer(int customerId, CreateCustomerDTO customerDTO)
        {
            var customer = await _customer.GetByIdAsync(customerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {customerId} not found");

            var updatedCustomer = new Customer
            {
                FirstName= customerDTO.FirstName,
                LastName=customerDTO.LastName,
                //Email=customerDTO.Email

            };

            _customer.Update(updatedCustomer);
            await _customer.SaveChangesAsync();

            return new CustomerDTO
            {
                Id = customer.Id,
                Name = $"{customer.FirstName} +{customer.LastName}",
                Email = customer.Email
            };
        }
    }
}
