﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MattEland.TestManager.Models
{
    [Display(Name = "Test Suite")]
    public class TestSuite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = true)]
        [Display(Name = "Created")]
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

        [HiddenInput(DisplayValue = true)]
        [Display(Name = "Last Modified")]
        public DateTime DateModifiedUtc { get; set; } = DateTime.UtcNow;

        public ICollection<TestCase> TestCases { get; set; }
    }
}