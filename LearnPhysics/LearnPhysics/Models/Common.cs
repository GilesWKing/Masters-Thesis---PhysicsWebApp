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
            Question1 = 1,
            Question2 = 2,
            Question3 = 3,
            Question4 = 4,
            Question5 = 5,
            Question6 = 6,
            Question7 = 7,
            Question8 = 8,
            Question9 = 9,
            Question10 = 10,
            Question11 = 11,
            Question12 = 12,
            Question13 = 13,
            Question14 = 14,
            Question15 = 15,
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
