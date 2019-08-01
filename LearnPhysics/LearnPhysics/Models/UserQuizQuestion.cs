using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnPhysics.Models
{
    // UserLesson table has a composite key.
    // The below ensures both fields are keys.
    public class UserQuizQuestion
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int QuizQuestionId { get; set; }
        public bool Correct { get; set; }
        public string AnswerText { get; set; }
    }
}
