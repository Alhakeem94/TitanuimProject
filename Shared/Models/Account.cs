using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Currency { get; set; } = "IQD";
        public DateTime OpenedAt { get; set; } = DateTime.Now;
        public status Status { get; set; }


        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public CustomersModel Customer { get; set; }







    }


    public enum status
    {
        Active = 0,
        Frozen = 1,
        Closed = 2
    }


}
