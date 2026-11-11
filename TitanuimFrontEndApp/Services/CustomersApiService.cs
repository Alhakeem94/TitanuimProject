using System.Net;
using System.Net.Http.Json;
using Shared.DTOs;
using Shared.Models;
using Shared.Responses;

namespace TitanuimFrontEndApp.Services
{
    /// <summary>
    /// Typed HTTP client that talks to the TitaniumBackendApi CustomersController.
    /// All backend calls from the UI go through this class so components never
    /// build URLs or parse JSON themselves.
    /// </summary>
    public class CustomersApiService
    {
        private readonly HttpClient _http;

        public CustomersApiService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// POST api/Customers/AddNewCustomer
        /// Sends a new customer to the backend and returns the GeneralResponse
        /// the API produced (success or "Customer Already Exists").
        /// </summary>
        public async Task<GeneralResponse> AddNewCustomerAsync(CustomersModel customer)
        {
            // The backend model has a non-nullable CustomerAccounts collection, so
            // model validation expects it to be present. Send an empty list.
            customer.CustomerAccounts ??= new List<Account>();

            try
            {
                var response = await _http.PostAsJsonAsync("api/Customers/AddNewCustomer", customer);

                var body = await response.Content.ReadFromJsonAsync<GeneralResponse>();
                if (body is not null)
                {
                    return body;
                }

                return new GeneralResponse
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = response.IsSuccessStatusCode
                        ? "Customer Added Successfully"
                        : $"Request failed with status {(int)response.StatusCode} {response.ReasonPhrase}"
                };
            }
            catch (HttpRequestException ex)
            {
                return new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"Could not reach the API: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                // The API returns a plain-text or ProblemDetails body on validation errors,
                // which cannot be read as GeneralResponse.
                return new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"Unexpected response from the API: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// GET api/Customers/GetCustomerDetails?CustomerId={id}
        /// Returns the customer DTO, or null when the request fails.
        /// The backend returns an empty DTO (all fields null) when no customer matches.
        /// </summary>
        public async Task<CustomerDto?> GetCustomerDetailsAsync(int customerId)
        {
            try
            {
                return await _http.GetFromJsonAsync<CustomerDto>(
                    $"api/Customers/GetCustomerDetails?CustomerId={customerId}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
