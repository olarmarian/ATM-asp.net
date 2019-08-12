﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class CheckingAccount
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName =("varchar"))]
        [Display(Name="Account #")]
        public string AccountNumber{ get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
        [DataType(DataType.Currency)]
        public decimal Balance{ get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

    }
}