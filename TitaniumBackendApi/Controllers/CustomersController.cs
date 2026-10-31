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
        public async Task<ActionResult<GeneralResponse>> AddNewCustomer([FromBody]CustomersModel AddedCustomerObject)
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
                return BadRequest("");
            }
        }



        [HttpGet("GetCustomerDetails")]
        public async Task<ActionResult<CustomerDto>> FetchCustomerFromDatabase([FromQuery]int CustomerId)
        {
            return Ok(await _Customers.GetCustomerById(CustomerId));
        }














      



    }
}
