using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.Admin.Data
{
    public class TestEditModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Duration")]
        public TimeSpan? TimeRequired { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Created")]
        public DateTime DateTime { get; set; }

        public string AuthorId { get; set; }
    }
}