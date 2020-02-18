using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.Admin.Data
{
    public class QuestionTableModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Question")]
        public string Text { get; set; }

        public Guid TestId { get; set; }
    }
}