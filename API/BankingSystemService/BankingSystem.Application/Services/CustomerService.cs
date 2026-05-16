using BankingSystem.Application.DTOs.Customer;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity;
using BankingSystem.Domain.Enums;
using FluentValidation;
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
        private readonly IValidator<CreateCustomerDTO> _customerValidator;
        public CustomerService(IRepository<Customer> customer, IValidator<CreateCustomerDTO> customerValidator)
        {
            _customer = customer;
            _customerValidator = customerValidator;
        }
        public async Task<CustomerDetailsDTO> CreateCustomerAsync(CreateCustomerDTO customerDTO)
        {
            var validationResult = await _customerValidator.ValidateAsync(customerDTO);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var customer = new Customer
            {
                FirstName=customerDTO.FirstName,
                LastName=customerDTO.LastName,
                Email=customerDTO.Email,
                Address=customerDTO.Address,
                ZipCode=customerDTO.ZipCode,
                PhoneNumber=customerDTO.PhoneNumber,
                status=Status.Active
            };

            await _customer.AddAsync(customer);
            await _customer.SaveChangesAsync();

            return new CustomerDetailsDTO
            {
                Id=customer.Id,
                CustomerId=customer.CustomerId,
                Name=$"{customer.FirstName} {customer.LastName}",
                Email = customer.Email
            };
        }

        public async Task DeleteCustomer(string customerId)
        {
            var customer = await _customer.FirstOrDefaultAsync(x=>x.CustomerId==customerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer ID {customerId} not found");

            _customer.Delete(customer);

            await _customer.SaveChangesAsync();
        }

        public async Task<CustomerDetailsDTO> GetCustomerDataAsync(string CustomerId)
        {
            var customer = await _customer.FirstOrDefaultAsync(x=>x.CustomerId==CustomerId);

            if (customer == null)
                throw new KeyNotFoundException($"{CustomerId} not found in the system");

            return new CustomerDetailsDTO
            {
                Id = customer.Id,
                CustomerId = customer.CustomerId,
                Name = $"{customer.FirstName} { customer.LastName}",
                Email = customer.Email,
                Address = customer.Address,
                ZipCode = customer.ZipCode,
                PhoneNumber = customer.PhoneNumber,
                Accounts = customer.BankAccounts.Select(a => new CustomerAccountDTO
                {
                    AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType
                }).ToList()
            };
        }

        public async Task<CustomerDetailsDTO> UpdateCustomer(string customerId, CreateCustomerDTO customerDTO)
        {
            var validationResult = await _customerValidator.ValidateAsync(customerDTO);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var customer = await _customer.FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {customerId} not found");


            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;
            customer.Address = customerDTO.Address;
            customer.ZipCode = customerDTO.ZipCode;
            customer.PhoneNumber = customerDTO.PhoneNumber;
   

            
            await _customer.SaveChangesAsync();

            return new CustomerDetailsDTO
            {
                CustomerId=customer.CustomerId,
                Name = $"{customer.FirstName} {customer.LastName}",
                Email = customer.Email
            };
        }
    }
}
