using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Models;
using Shared.Responses;
using TitaniumBackendApi.Data;
using TitaniumBackendApi.Interfaces;

namespace TitaniumBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomers _Customers;
        public CustomersController(ICustomers customers)
        {
            _Customers = customers;
        }



        [HttpPost("AddNewCustomer")]
        public async Task<ActionResult<GeneralResponse>> AddNewCustomer([FromBody] CustomersModel AddedCustomerObject)
        {
            if (ModelState.IsValid)
            {
                var CheckCustomer = await _Customers.GetCustomerByNationalId(AddedCustomerObject.NationalId);
                if (CheckCustomer is null)
                {
                    await _Customers.AddNewCustomer(AddedCustomerObject);
                    return Ok(new GeneralResponse
                    {
                        IsSuccess = true,
                        Message = "Customer Added Successfully"
                    });
                }
                else
                {
                    return BadRequest(new GeneralResponse
                    {
                        IsSuccess = false,
                        Message = "Customer Already Exists"
                    });
                }
            }
            else
            {
                return NotFound(new GeneralResponse
                {
                    IsSuccess = false,
                    Message = "Invalid Customer Data"
                });
            }
        }



        [HttpGet("GetCustomerDetails")]
        public async Task<ActionResult<CustomerDto>> FetchCustomerFromDatabase([FromQuery] int CustomerId)
        {
            return Ok(await _Customers.GetCustomerById(CustomerId));
        }




        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult> FetchAllCustomers()
        {
            return Ok(await _Customers.getAllCustomers());
        }



        [HttpGet("GetCustomersbyName")]
        public async Task<ActionResult<List<CustomerDto>>> FetchCustomersByName([FromQuery] string CustomerName)
        {
            return Ok(await _Customers.GetCustomersByName(CustomerName));
        }



        [HttpDelete("DeleteCustomer")]
        public async Task<ActionResult<GeneralResponse>> DeleteCustomerByNationalId([FromQuery] string NationalId)
        {
            var CheckDeletionProcess = await _Customers.DeleteCustomer(NationalId);
            if (CheckDeletionProcess)
            {
                return Ok(new GeneralResponse()
                {
                    IsSuccess = true,
                    Message = "Customer Deleted Successfully"
                });
            }
            else;
            {
                return BadRequest(new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = "Customer Deletion Failed"
                });
            }
        }




        [HttpGet("GetCustomerDetailsByNid")]
        public async Task<ActionResult<CustomerDto>> FetchCustomerFromDatabaseByNid([FromQuery] string NationalId)
        {

            var CustomerDetails = await _Customers.GetCustomerByNationalId(NationalId);

            var Dto = new CustomerDto
            {
                Id = CustomerDetails.Id,
                FullName = CustomerDetails.FullName,
                NationalId = CustomerDetails.NationalId,
                Email = CustomerDetails.Email,
                PhoneNumber = CustomerDetails.PhoneNumber
            };

            return Ok(Dto);

        }




        [HttpPut("UpdateCustomerDetails")]
        public async Task<ActionResult<GeneralResponse>> UpdateCustomerDetails([FromBody] CustomerDto UpdatedCustomerObject)
        {
            
             return Ok(await _Customers.UpdateCustomerDetails(UpdatedCustomerObject));
          
        }






    }
}
