using System;
using System.Collections.Generic;

namespace LearnPhysics.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Lesson = new HashSet<Lesson>();
            Quiz = new HashSet<Quiz>();
        }

        public int TopicId { get; set; }
        public string TopicTitle { get; set; }

        public ICollection<Lesson> Lesson { get; set; }
        public ICollection<Quiz> Quiz { get; set; }
    }
}
