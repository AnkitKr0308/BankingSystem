using BankingSystem.Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDetailsDTO> CreateCustomerAsync(CreateCustomerDTO customer);
        Task DeleteCustomer(string CustomerId);
        Task<CustomerDetailsDTO> UpdateCustomer(string CustomerId, CreateCustomerDTO customer);
        Task<CustomerDetailsDTO> GetCustomerDataAsync(string CustomerId);
    }
}
