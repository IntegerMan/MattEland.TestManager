using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MattEland.TestManager.Models
{
    [DisplayName("Test Suite")]
    public class TestSuite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = true)]
        [DisplayName("Created")]
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

        [HiddenInput(DisplayValue = true)]
        [DisplayName("Last Modified")]
        public DateTime DateModifiedUtc { get; set; } = DateTime.UtcNow;
    }
}