using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnPhysics.Models
{
    // UserQuiz table has a composite key.
    // The below ensures both fields are keys.
    public class UserQuiz
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int QuizId { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswers { get; set; }
        public int Percentage { get; set; }
    }
}
