using System;
using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Web.Areas.Admin.Data
{
    public class AnswerEditModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Answer")]
        public string Text { get; set; }

        [Display(Name = "Correct")]
        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
    }
}