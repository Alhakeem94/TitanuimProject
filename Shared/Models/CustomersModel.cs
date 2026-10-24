using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Models
{
    public class CustomersModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(12)]
        public string NationalId { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
