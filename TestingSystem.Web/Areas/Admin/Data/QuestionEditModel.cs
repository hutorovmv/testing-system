using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.Admin.Data
{
    public class QuestionEditModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Question")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public Guid TestId { get; set; }
    }
}