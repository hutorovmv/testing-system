using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Models
{
    public class TestResultTableModel
    {
        [Display(Name = "Correct (%)")]
        public double CorrectPercent => (double)CorrectAnswers / TotalCorrectAnswers * 100;

        [Display(Name = "Correct")]
        public int CorrectAnswers { get; set; }

        [Display(Name = "Total")]
        public int TotalCorrectAnswers { get; set; }

        public Guid TestId { get; set; }

        [Display(Name = "Test Name")]
        public string TestName { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; }

        [Display(Name = "Start Date and Time")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "End Date and Time")]
        public DateTime EndDateTime { get; set; }
    }
}