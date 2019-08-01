using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnPhysics.Models
{
    public class EnergyOpeningQuiz
    {

        public string Question1 { get; set; }
        public string Question1CorrectText { get; set; }

        public string Question2 { get; set; }
        public string Question2CorrectText { get; set; }

        public string Question3 { get; set; }
        public string Question3CorrectText { get; set; }

        public bool Question4a { get; set; }
        public bool Question4b { get; set; }
        public bool Question4c { get; set; }
        public bool Question4d { get; set; }
        public string Question4CorrectText { get; set; }

        public bool Question5a { get; set; }
        public bool Question5b { get; set; }
        public bool Question5c { get; set; }
        public bool Question5d { get; set; }
        public string Question5CorrectText { get; set; }

        public string Question6a { get; set; }
        public string Question6b { get; set; }
        public string Question6c { get; set; }
        public string Question6d { get; set; }
        public string Question6e { get; set; }
        public string Question6f { get; set; }
        public string Question6aCorrectText { get; set; }
        public string Question6bCorrectText { get; set; }
        public string Question6cCorrectText { get; set; }
        public string Question6dCorrectText { get; set; }
        public string Question6eCorrectText { get; set; }
        public string Question6fCorrectText { get; set; }

        public bool Question1Correct { get; set; }
        public bool Question2Correct { get; set; }
        public bool Question3Correct { get; set; }
        public bool Question4Correct { get; set; }
        public bool Question5Correct { get; set; }
        public bool Question6Correct { get; set; }

        public int CorrectAnswerTotal { get; set; }
        public int Percentage { get; set; }
    }
}
