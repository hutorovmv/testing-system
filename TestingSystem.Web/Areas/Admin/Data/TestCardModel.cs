using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.Admin.Data
{
    public class TestCardModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Duration")]
        public TimeSpan? TimeRequired { get; set; }

        [Display(Name = "Created")]
        public DateTime DateTime { get; set; }

        public string AuthorId { get; set; }

        [Display(Name = "Author")]
        public string AuthorFullName { get; set; }
    }
}