using System;
using System.Collections.Generic;

namespace LearnPhysics.Models
{
    public partial class QuizQuestion
    {
        public int QuizQuestionId { get; set; }
        public int QuizId { get; set; }
        public int QuestionNumber { get; set; }

        public Quiz Quiz { get; set; }
    }
}
