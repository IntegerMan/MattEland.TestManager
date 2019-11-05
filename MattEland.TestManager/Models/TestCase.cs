using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace MattEland.TestManager.Models
{
    [Display(Name = "Test Case")]
    public class TestCase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Test Suite")]
        [ForeignKey("TestSuite")]
        public int TestSuiteId { get; set; }

        [Display(Name = "Test Suite")]
        public TestSuite TestSuite { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = true)]
        [Display(Name = "Created")]
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

        [HiddenInput(DisplayValue = true)]
        [Display(Name = "Last Modified")]
        public DateTime DateModifiedUtc { get; set; } = DateTime.UtcNow;

        public TestStatus Status { get; set; } = TestStatus.NotRun;
    }
}