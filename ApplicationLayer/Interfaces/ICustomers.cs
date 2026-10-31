using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Models;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using TitaniumBackendApi.Data;

namespace ApplicationLayer.Interfaces
{
    public interface ICustomers
    {
        Task<CustomersModel> GetCustomerByNationalId(string Nationalid);
        Task<CustomerDto> GetCustomerById(int CustomerId);
        Task<GeneralResponse> AddNewCustomer(CustomersModel AddedCustomerObject);
    }




    public class CustomersRepository : ICustomers
    {
        private readonly AmanaDbContext _Db;
        public CustomersRepository(AmanaDbContext DbContext)
        {
            _Db = DbContext;
        }

        public async Task<GeneralResponse> AddNewCustomer(CustomersModel AddedCustomerObject)
        {
            await _Db.CustomersTable.AddAsync(AddedCustomerObject);
            await _Db.SaveChangesAsync();
            return new GeneralResponse
            {
                IsSuccess = true,
                Message = "Customer Added Successfully"
            };
        }

        public async Task<CustomerDto> GetCustomerById(int CustomerId)
        {
            var CustomerDetails = await _Db.CustomersTable.FirstOrDefaultAsync(a => a.Id == CustomerId);
            if (CustomerDetails is null)
            {
                return new CustomerDto();
            }
            else
            {
                var DTO = new CustomerDto
                {
                    FullName = CustomerDetails.FullName,
                    NationalId = CustomerDetails.NationalId,
                    Email = CustomerDetails.Email,
                    PhoneNumber = CustomerDetails.PhoneNumber
                };

                return DTO;
            }
        }



        public async Task<CustomersModel> GetCustomerByNationalId(string Nationalid)
        {
            var CheckIfNationalIdExists = await _Db.CustomersTable.FirstOrDefaultAsync(a => a.NationalId == Nationalid); // 4ms
            if (CheckIfNationalIdExists is null)
            {
                return new CustomersModel();
            }else
            {
                return CheckIfNationalIdExists;
            }
        }



    }






}
