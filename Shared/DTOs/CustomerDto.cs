using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string NationalId { get; set; }

        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

    }
}
