using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Models;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using TitaniumBackendApi.Data;

namespace TitaniumBackendApi.Interfaces
{
    public interface ICustomers
    {
        Task<CustomersModel> GetCustomerByNationalId(string Nationalid);
        Task<CustomerDto> GetCustomerById(int CustomerId);
        Task<GeneralResponse> AddNewCustomer(CustomersModel AddedCustomerObject);
        Task<List<CustomerDto>> getAllCustomers();
        Task<List<CustomerDto>> GetCustomersByName(string Name);
        Task<bool> DeleteCustomer(string NationalId);
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

        public async Task<bool> DeleteCustomer(string NationalId)
        {
            var CustomerToBeDeleted = await GetCustomerByNationalId(NationalId);
            if (CustomerToBeDeleted is null)
            {
                return false;
            }else
            {
                _Db.CustomersTable.Remove(CustomerToBeDeleted);
                await _Db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<CustomerDto>> getAllCustomers()
        {
            return await _Db.CustomersTable.Select(a => new CustomerDto
            {
                Email = a.Email,
                FullName = a.FullName,
                NationalId = a.NationalId,
                PhoneNumber = a.PhoneNumber
            }).ToListAsync();

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
                return null;
            }
            else
            {
                return CheckIfNationalIdExists;
            }
        }

        public async Task<List<CustomerDto>> GetCustomersByName(string Name)
        {
            return await _Db.CustomersTable.Where(a => a.FullName.Contains(Name)).Select(a => new CustomerDto
            {
                FullName = a.FullName,
                NationalId = a.NationalId,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber
            })
            .ToListAsync();
        }






    }
}
