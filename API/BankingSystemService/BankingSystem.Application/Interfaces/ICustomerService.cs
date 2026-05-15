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
        Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO customer);
        Task DeleteCustomer(int customerId);
        Task<CustomerDTO> UpdateCustomer(int customerId, CreateCustomerDTO customer);
    }
}
