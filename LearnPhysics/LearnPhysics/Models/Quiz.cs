using System;
using System.Collections.Generic;

namespace LearnPhysics.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuizQuestion = new HashSet<QuizQuestion>();
        }

        public int QuizId { get; set; }
        public int TopicId { get; set; }
        public string QuizName { get; set; }

        public Topic Topic { get; set; }
        public ICollection<QuizQuestion> QuizQuestion { get; set; }
    }
}
