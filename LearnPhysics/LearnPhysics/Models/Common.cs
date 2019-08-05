using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LearnPhysics.Models
{
    public class Common
    {
        public enum Topic
        {
            MathsSkills= 1,
            Energy = 2,
            Particles = 3,
            Blog = 4
        }

        public enum LessonEnergy
        {
            Lesson1 = 1,
            Lesson2 = 2,
            Lesson3 = 6,
            Lesson4 = 7,
            Lesson5 = 8,
            Lesson6 = 9
        }

        public enum LessonParticles
        {
            Lesson1 = 11,
            Lesson2 = 12,
            Lesson3 = 13,
            Lesson4 = 14,
            Lesson5 = 15,
            Lesson6 = 16,
            Lesson7 = 17
        }

        public enum QuizEnergy
        {
            OpeningQuiz = 1,
            ChapterQuiz = 2
        }

        public enum QuizEnergyOpen
        {
            Question1 = 1,
            Question2 = 2,
            Question3 = 3,
            Question4 = 4,
            Question5 = 5,
            Question6 = 6
        }

        public enum QuizEnergyChapter
        {
            Question1 = 7,
            Question2 = 8,
            Question3 = 9,
            Question4 = 10,
            Question5 = 11,
            Question6 = 12,
            Question7 = 13,
            Question8 = 15,
            Question9 = 16,
            Question10 = 17,
            Question11 = 18,
            Question12 = 19,
            Question13 = 20,
            Question14 = 21,
            Question15 = 22,
        }

        public enum QuizParticles
        {
            OpeningQuiz = 5,
            ChapterQuiz = 6
        }

        // Method to transfer User ID from database to EnergyController.
        public int GetUserId(HttpContext httpContext)
        {
            // Information needs to be converted into bytes for TryGetValue to work.
            byte[] userIdBytes = new byte[4];
            httpContext.Session.TryGetValue("UserId", out userIdBytes);

            // BitConverter.IsLittleEndian highlights the least significant bit first.
            // The array contains the information we want in the far right element of the array.
            // Little Endian selects this bit, and we reverse the array to give us that element first.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(userIdBytes);

            // Convert the number we want back into an int.
            int userId = BitConverter.ToInt32(userIdBytes);

            return userId;
        }
    }
}
