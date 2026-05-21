using BankingSystem.Application.DTOs.Customer;
using BankingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO customerDTO)
        {

                var customer = await _customerService.CreateCustomerAsync(customerDTO);
                return Ok(customer);           
        }


        [HttpPatch("deleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromQuery]string customerId)
        {
            await _customerService.DisableCustomer(customerId);
            return Ok($"Customer {customerId} deleted successfully");
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerDetail(string customerId)
        {
            var customer=await _customerService.GetCustomerDataAsync(customerId);
            return Ok(customer);
        }

        [HttpPut("updateCustomer/{customerId}")]
        public async Task<IActionResult> UpdateCustomerDetails(string customerId, [FromBody]CreateCustomerDTO customerDTO)
        {
            var customer = await _customerService.UpdateCustomer(customerId, customerDTO);
            return Ok(customer);
        }
    }
}
