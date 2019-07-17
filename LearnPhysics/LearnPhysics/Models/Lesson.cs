using System;
using System.Collections.Generic;

namespace LearnPhysics.Models
{
    public partial class Lesson
    {
        public int LessonId { get; set; }
        public int TopicId { get; set; }
        public string LessonName { get; set; }

        public Topic Topic { get; set; }
    }
}
