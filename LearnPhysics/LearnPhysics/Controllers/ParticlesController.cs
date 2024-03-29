﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearnPhysics.Models;

namespace LearnPhysics.Controllers
{
    public class ParticlesController : Controller
    {
        private readonly GilesContext _context;
        private readonly Common _common;

        public ParticlesController(GilesContext context, Common common)
        {
            _context = context;
            _common = common;
        }

        public IActionResult ParticlesIndex()
        {
            // Method to retrieve UserID from the database through the Common.cs GetUserId method.
            int userId = _common.GetUserId(HttpContext);
            IEnumerable<UserLesson> userLessons = _context.UserLesson
                        // SQL Query joined Lesson to UserLesson tables to ensure we have the correct topic ID.
                        .Join(_context.Lesson, UserLesson => UserLesson.LessonId, Lesson => Lesson.LessonId, (UserLesson, Lesson) => new { Lesson, UserLesson })
                        .Where(JoinedTable => JoinedTable.UserLesson.UserId == userId && JoinedTable.Lesson.TopicId == (int)Common.Topic.Particles)
                        .Select(JoinedTable => new UserLesson { LessonId = JoinedTable.UserLesson.LessonId });

            IEnumerable<UserQuiz> userQuizes = _context.UserQuiz
                        // SQL Query joined Quiz to UserQuiz tables to ensure we have the correct topic ID.
                        .Join(_context.Quiz, UserQuiz => UserQuiz.QuizId, Quiz => Quiz.QuizId, (UserQuiz, Quiz) => new { Quiz, UserQuiz })
                        .Where(JoinedTable => JoinedTable.UserQuiz.UserId == userId && JoinedTable.Quiz.TopicId == (int)Common.Topic.Particles)
                        .Select(JoinedTable => new UserQuiz
                        {
                            QuizId = JoinedTable.UserQuiz.QuizId,
                            CorrectAnswers = JoinedTable.UserQuiz.CorrectAnswers,
                            TotalAnswers = JoinedTable.UserQuiz.TotalAnswers,
                            Percentage = JoinedTable.UserQuiz.Percentage
                        });

            ParticlesIndex particlesIndex = new ParticlesIndex();

            // Using the Enums found in the Common.cs class to identify if a record exists in the database.
            // If the record exists, the page can be marked as read, as the user has navigated to the page.
            foreach (UserLesson userLesson in userLessons)
            {
                if (userLesson.LessonId == (int)Common.LessonParticles.Lesson1)
                    particlesIndex.Lesson1 = true;
                else if (userLesson.LessonId == (int)Common.LessonParticles.Lesson2)
                    particlesIndex.Lesson2 = true;
                else if (userLesson.LessonId == (int)Common.LessonParticles.Lesson3)
                    particlesIndex.Lesson3 = true;
                else if (userLesson.LessonId == (int)Common.LessonParticles.Lesson4)
                    particlesIndex.Lesson4 = true;
                else if (userLesson.LessonId == (int)Common.LessonParticles.Lesson5)
                    particlesIndex.Lesson5 = true;
                else if (userLesson.LessonId == (int)Common.LessonParticles.Lesson6)
                    particlesIndex.Lesson6 = true;
                else if (userLesson.LessonId == (int)Common.LessonParticles.Lesson7)
                    particlesIndex.Lesson7 = true;
            }

            foreach (UserQuiz userQuiz in userQuizes)
            {
                if (userQuiz.QuizId == (int)Common.QuizParticles.OpeningQuiz)
                {
                    particlesIndex.QuizOpening = true;
                    particlesIndex.QuizOpeningResults = string.Concat(userQuiz.CorrectAnswers, " of ", userQuiz.TotalAnswers, " correct (", userQuiz.Percentage, "%)");
                }

                else if (userQuiz.QuizId == (int)Common.QuizParticles.ChapterQuiz)
                {
                    particlesIndex.QuizChapter = true;
                    particlesIndex.QuizChapterResults = string.Concat(userQuiz.CorrectAnswers, " of ", userQuiz.TotalAnswers, " correct (", userQuiz.Percentage, "%)");
                }

            }

            return View();
        }

        public IActionResult OpeningQuiz()
        {
            int userId = _common.GetUserId(HttpContext);

            UserQuiz userQuiz = _context.UserQuiz
                       // SQL Query joined Quiz to UserQuiz tables to ensure we have the correct topic ID.
                       .Join(_context.Quiz, UserQuiz => UserQuiz.QuizId, Quiz => Quiz.QuizId, (UserQuiz, Quiz) => new { Quiz, UserQuiz })
                       .Where(JoinedTable => JoinedTable.UserQuiz.UserId == userId && JoinedTable.Quiz.QuizId == (int)Common.QuizParticles.OpeningQuiz)
                       .Select(JoinedTable => new UserQuiz
                       {
                           QuizId = JoinedTable.UserQuiz.QuizId,
                           CorrectAnswers = JoinedTable.UserQuiz.CorrectAnswers,
                           TotalAnswers = JoinedTable.UserQuiz.TotalAnswers,
                           Percentage = JoinedTable.UserQuiz.Percentage
                       }).SingleOrDefault();

            if (userQuiz != null)
            {
                EnergyOpeningQuiz quiz = new EnergyOpeningQuiz();

                quiz.CorrectAnswerTotal = userQuiz.CorrectAnswers;
                quiz.Percentage = userQuiz.Percentage;

                IEnumerable<UserQuizQuestion> userQuizQuestions = _context.UserQuizQuestion
                    .Join(_context.QuizQuestion, UserQuizQuestion => UserQuizQuestion.QuizQuestionId, QuizQuestion => QuizQuestion.QuizQuestionId, (UserQuizQuestion, QuizQuestion) => new { QuizQuestion, UserQuizQuestion })
                    .Where(JoinedTable => JoinedTable.UserQuizQuestion.UserId == userId && JoinedTable.QuizQuestion.QuizId == (int)Common.QuizEnergy.OpeningQuiz)
                    .Select(JoinedTable => new UserQuizQuestion
                    {
                        QuizQuestionId = JoinedTable.UserQuizQuestion.QuizQuestionId,
                        Correct = JoinedTable.UserQuizQuestion.Correct,
                        AnswerText = JoinedTable.UserQuizQuestion.AnswerText
                    });

                foreach (UserQuizQuestion userQuizQuestion in userQuizQuestions)
                {
                    if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesOpen.Question1)
                    {
                        quiz.Question1Correct = userQuizQuestion.Correct;
                        quiz.Question1CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesOpen.Question2)
                    {
                        quiz.Question2Correct = userQuizQuestion.Correct;
                        quiz.Question2CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesOpen.Question3)
                    {
                        quiz.Question3Correct = userQuizQuestion.Correct;
                        quiz.Question3CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesOpen.Question4)
                    {
                        quiz.Question4Correct = userQuizQuestion.Correct;
                        quiz.Question4CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesOpen.Question5)
                    {
                        quiz.Question5Correct = userQuizQuestion.Correct;
                        quiz.Question5CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesOpen.Question6)
                    {
                        quiz.Question6Correct = userQuizQuestion.Correct;
                        string text = userQuizQuestion.AnswerText;
                        string[] answers = text.Split(new char[] { '#' });

                        if (answers.Length == 6)
                        {
                            quiz.Question6aCorrectText = answers[0];
                            quiz.Question6bCorrectText = answers[1];
                            quiz.Question6cCorrectText = answers[2];
                            quiz.Question6dCorrectText = answers[3];
                            quiz.Question6eCorrectText = answers[4];
                            quiz.Question6fCorrectText = answers[5];
                        }
                    }
                }

                return View("OpeningQuizResults", quiz);
            }

            return View();
        }
        

             [HttpPost]
        public IActionResult OpeningQuiz(ParticlesOpeningQuiz quiz)
        {
            int count = 0;

            // Marking Question 1 correct or incorrect
            if (quiz.Question1 == "C")
            {
                quiz.Question1Correct = true;
                count++;
            }
            quiz.Question1CorrectText = quiz.Question1;
            if (quiz.Question1CorrectText == null)
                quiz.Question1CorrectText = "none";

            // Marking Question 2 correct or incorrect
            if (quiz.Question2 == "A")
            {
                quiz.Question2Correct = true;
                count++;
            }
            quiz.Question2CorrectText = quiz.Question2;
            if (quiz.Question2CorrectText == null)
                quiz.Question2CorrectText = "none";

            // Marking Question 3 correct or incorrect
            if (quiz.Question3 == "D")
            {
                quiz.Question3Correct = true;
                count++;
            }
            quiz.Question3CorrectText = quiz.Question3;
            if (quiz.Question3CorrectText == null)
                quiz.Question3CorrectText = "none";

            // Marking Question 4 correct or incorrect
            // If answers 4a, 4b and 4d are true (checked), then question 4 is correct.
            if (quiz.Question4a && quiz.Question4b && quiz.Question4d)
            {
                quiz.Question4Correct = true;
                count++;
            }
            string text = "";
            if (quiz.Question4a)
                text = "A";
            if (quiz.Question4b)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "B");
            }
            if (quiz.Question4c)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "C");
            }
            if (quiz.Question4d)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "D");
            }
            if (text == "")
                text = "none";
            quiz.Question4CorrectText = text;

            // Marking Question 5 correct or incorrect
            // Same as above but for question 5.
            if (quiz.Question5b && quiz.Question5d)
            {
                quiz.Question5Correct = true;
                count++;
            }
            text = "";
            if (quiz.Question5a)
                text = "A";
            if (quiz.Question5b)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "B");
            }
            if (quiz.Question5c)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "C");
            }
            if (quiz.Question5d)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "D");
            }
            if (text == "")
                text = "none";
            quiz.Question5CorrectText = text;


            // Marking Question 6 correct or incorrect
            if (quiz.Question6a == "A1" &&
                quiz.Question6b == "B2" &&
                quiz.Question6c == "C1" &&
                quiz.Question6d == "D2" &&
                quiz.Question6e == "E2" &&
                quiz.Question6f == "F1")
            {
                quiz.Question6Correct = true;
                count++;
            }

            if (quiz.Question6a == "A1")
                quiz.Question6aCorrectText = "renewable";
            else if (quiz.Question6a == "A2")
                quiz.Question6aCorrectText = "non-renewable";
            else
                quiz.Question6aCorrectText = "none";

            if (quiz.Question6b == "B1")
                quiz.Question6bCorrectText = "renewable";
            else if (quiz.Question6b == "B2")
                quiz.Question6bCorrectText = "non-renewable";
            else
                quiz.Question6bCorrectText = "none";

            if (quiz.Question6c == "C1")
                quiz.Question6cCorrectText = "renewable";
            else if (quiz.Question6c == "C2")
                quiz.Question6cCorrectText = "non-renewable";
            else
                quiz.Question6cCorrectText = "none";

            if (quiz.Question6d == "D1")
                quiz.Question6dCorrectText = "renewable";
            else if (quiz.Question6d == "D2")
                quiz.Question6dCorrectText = "non-renewable";
            else
                quiz.Question6dCorrectText = "none";

            if (quiz.Question6e == "E1")
                quiz.Question6eCorrectText = "renewable";
            else if (quiz.Question6e == "E2")
                quiz.Question6eCorrectText = "non-renewable";
            else
                quiz.Question6eCorrectText = "none";

            if (quiz.Question6f == "F1")
                quiz.Question6fCorrectText = "renewable";
            else if (quiz.Question6f == "F2")
                quiz.Question6fCorrectText = "non-renewable";
            else
                quiz.Question6fCorrectText = "none";

            UserQuiz userQuiz = new UserQuiz();
            userQuiz.UserId = _common.GetUserId(HttpContext);
            // Change to retrieve quiz id
            userQuiz.QuizId = (int)Common.QuizParticles.OpeningQuiz;
            userQuiz.CorrectAnswers = count;
            userQuiz.TotalAnswers = 6;

            decimal percentageCalc = ((decimal)userQuiz.CorrectAnswers / (decimal)userQuiz.TotalAnswers) * (decimal)100;
            decimal percentageRound = Math.Ceiling(percentageCalc);
            userQuiz.Percentage = Convert.ToInt32(percentageRound);

            // Sending variables to Energy Opening Quiz page to display results.
            quiz.CorrectAnswerTotal = userQuiz.CorrectAnswers;
            quiz.Percentage = userQuiz.Percentage;

            UserQuizQuestion userQuizQuestion1 = new UserQuizQuestion();
            userQuizQuestion1.UserId = userQuiz.UserId;
            userQuizQuestion1.QuizQuestionId = (int)Common.QuizParticlesOpen.Question1;
            userQuizQuestion1.Correct = quiz.Question1Correct;
            userQuizQuestion1.AnswerText = quiz.Question1CorrectText;

            UserQuizQuestion userQuizQuestion2 = new UserQuizQuestion();
            userQuizQuestion2.UserId = userQuiz.UserId;
            userQuizQuestion2.QuizQuestionId = (int)Common.QuizParticlesOpen.Question2;
            userQuizQuestion2.Correct = quiz.Question2Correct;
            userQuizQuestion2.AnswerText = quiz.Question2CorrectText;

            UserQuizQuestion userQuizQuestion3 = new UserQuizQuestion();
            userQuizQuestion3.UserId = userQuiz.UserId;
            userQuizQuestion3.QuizQuestionId = (int)Common.QuizParticlesOpen.Question3;
            userQuizQuestion3.Correct = quiz.Question3Correct;
            userQuizQuestion3.AnswerText = quiz.Question3CorrectText;

            UserQuizQuestion userQuizQuestion4 = new UserQuizQuestion();
            userQuizQuestion4.UserId = userQuiz.UserId;
            userQuizQuestion4.QuizQuestionId = (int)Common.QuizParticlesOpen.Question4;
            userQuizQuestion4.Correct = quiz.Question4Correct;
            userQuizQuestion4.AnswerText = quiz.Question4CorrectText;

            UserQuizQuestion userQuizQuestion5 = new UserQuizQuestion();
            userQuizQuestion5.UserId = userQuiz.UserId;
            userQuizQuestion5.QuizQuestionId = (int)Common.QuizParticlesOpen.Question5;
            userQuizQuestion5.Correct = quiz.Question5Correct;
            userQuizQuestion5.AnswerText = quiz.Question5CorrectText;

            UserQuizQuestion userQuizQuestion6 = new UserQuizQuestion();
            userQuizQuestion6.UserId = userQuiz.UserId;
            userQuizQuestion6.QuizQuestionId = (int)Common.QuizParticlesOpen.Question6;
            userQuizQuestion6.Correct = quiz.Question6Correct;
            userQuizQuestion6.AnswerText = string.Concat(quiz.Question6aCorrectText, "#",
                                                         quiz.Question6bCorrectText, "#",
                                                         quiz.Question6cCorrectText, "#",
                                                         quiz.Question6dCorrectText, "#",
                                                         quiz.Question6eCorrectText, "#",
                                                         quiz.Question6fCorrectText);

            try
            {
                _context.Reset();
                _context.UserQuiz.Add(userQuiz);
                _context.UserQuizQuestion.Add(userQuizQuestion1);
                _context.UserQuizQuestion.Add(userQuizQuestion2);
                _context.UserQuizQuestion.Add(userQuizQuestion3);
                _context.UserQuizQuestion.Add(userQuizQuestion4);
                _context.UserQuizQuestion.Add(userQuizQuestion5);
                _context.UserQuizQuestion.Add(userQuizQuestion6);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return View("OpeningQuizResults", quiz);
        }

        public IActionResult Lesson1()
        {
            return View();
        }

        // Method to mark Lesson 1 as read on topics page.
        public IActionResult Lesson1Complete()
        {
            UserLesson fin = new UserLesson();
            fin.UserId = _common.GetUserId(HttpContext);
            fin.LessonId = (int)Common.LessonParticles.Lesson1;

            // A user may navigate to a page already, so when writing to DB, might throw an error.
            // Try-Catch statement allows users to continuously return to pages without problems.
            try
            {
                _context.Reset();
                _context.UserLesson.Add(fin);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("ParticlesIndex", "Particles");
        }

        public IActionResult Lesson2()
        {
            return View();
        }

        public IActionResult Lesson2Complete()
        {
            UserLesson fin = new UserLesson();
            fin.UserId = _common.GetUserId(HttpContext);
            fin.LessonId = (int)Common.LessonParticles.Lesson2;

            try
            {
                _context.Reset();
                _context.UserLesson.Add(fin);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("ParticlesIndex", "Particles");
        }

        public IActionResult Lesson3()
        {
            return View();
        }

        public IActionResult Lesson3Complete()
        {
            UserLesson fin = new UserLesson();
            fin.UserId = _common.GetUserId(HttpContext);
            fin.LessonId = (int)Common.LessonParticles.Lesson3;

            try
            {
                _context.Reset();
                _context.UserLesson.Add(fin);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("ParticlesIndex", "Particles");
        }

        public IActionResult Lesson4()
        {
            return View();
        }

        public IActionResult Lesson4Complete()
        {
            UserLesson fin = new UserLesson();
            fin.UserId = _common.GetUserId(HttpContext);
            fin.LessonId = (int)Common.LessonParticles.Lesson4;

            try
            {
                _context.Reset();
                _context.UserLesson.Add(fin);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("ParticlesIndex", "Particles");
        }

        public IActionResult Lesson5()
        {
            return View();
        }

        public IActionResult Lesson5Complete()
        {
            UserLesson fin = new UserLesson();
            fin.UserId = _common.GetUserId(HttpContext);
            fin.LessonId = (int)Common.LessonParticles.Lesson5;

            try
            {
                _context.Reset();
                _context.UserLesson.Add(fin);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("ParticlesIndex", "Particles");
        }

        public IActionResult Lesson6()
        {
            return View();
        }

        public IActionResult Lesson6Complete()
        {
            UserLesson fin = new UserLesson();
            fin.UserId = _common.GetUserId(HttpContext);
            fin.LessonId = (int)Common.LessonParticles.Lesson6;

            try
            {
                _context.Reset();
                _context.UserLesson.Add(fin);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("ParticlesIndex", "Particles");
        }

        public IActionResult Lesson7()
        {
            return View();
        }

        public IActionResult Lesson7Complete()
        {
            UserLesson fin = new UserLesson();
            fin.UserId = _common.GetUserId(HttpContext);
            fin.LessonId = (int)Common.LessonParticles.Lesson7;

            try
            {
                _context.Reset();
                _context.UserLesson.Add(fin);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("ParticlesIndex", "Particles");
        }

        public IActionResult FurtherReading()
        {
            return View();
        }

        public IActionResult ChapterQuiz()
        {
            int userId = _common.GetUserId(HttpContext);

            UserQuiz userQuiz = _context.UserQuiz
                       // SQL Query joined Quiz to UserQuiz tables to ensure we have the correct topic ID.
                       .Join(_context.Quiz, UserQuiz => UserQuiz.QuizId, Quiz => Quiz.QuizId, (UserQuiz, Quiz) => new { Quiz, UserQuiz })
                       .Where(JoinedTable => JoinedTable.UserQuiz.UserId == userId && JoinedTable.Quiz.QuizId == (int)Common.QuizParticles.ChapterQuiz)
                       .Select(JoinedTable => new UserQuiz
                       {
                           QuizId = JoinedTable.UserQuiz.QuizId,
                           CorrectAnswers = JoinedTable.UserQuiz.CorrectAnswers,
                           TotalAnswers = JoinedTable.UserQuiz.TotalAnswers,
                           Percentage = JoinedTable.UserQuiz.Percentage
                       }).SingleOrDefault();

            if (userQuiz != null)
            {
                ParticlesChapterQuiz quiz = new ParticlesChapterQuiz();

                quiz.CorrectAnswerTotal = userQuiz.CorrectAnswers;
                quiz.Percentage = userQuiz.Percentage;

                IEnumerable<UserQuizQuestion> userQuizQuestions = _context.UserQuizQuestion
                    .Join(_context.QuizQuestion, UserQuizQuestion => UserQuizQuestion.QuizQuestionId, QuizQuestion => QuizQuestion.QuizQuestionId, (UserQuizQuestion, QuizQuestion) => new { QuizQuestion, UserQuizQuestion })
                    .Where(JoinedTable => JoinedTable.UserQuizQuestion.UserId == userId && JoinedTable.QuizQuestion.QuizId == (int)Common.QuizParticles.ChapterQuiz)
                    .Select(JoinedTable => new UserQuizQuestion
                    {
                        QuizQuestionId = JoinedTable.UserQuizQuestion.QuizQuestionId,
                        Correct = JoinedTable.UserQuizQuestion.Correct,
                        AnswerText = JoinedTable.UserQuizQuestion.AnswerText
                    });

                foreach (UserQuizQuestion userQuizQuestion in userQuizQuestions)
                {
                    if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question1)
                    {
                        quiz.Question1Correct = userQuizQuestion.Correct;
                        quiz.Question1CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question2)
                    {
                        quiz.Question2Correct = userQuizQuestion.Correct;
                        quiz.Question2CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question3)
                    {
                        quiz.Question3Correct = userQuizQuestion.Correct;
                        quiz.Question3CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question4)
                    {
                        quiz.Question4Correct = userQuizQuestion.Correct;
                        quiz.Question4CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question5)
                    {
                        quiz.Question5Correct = userQuizQuestion.Correct;
                        quiz.Question5CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question6)
                    {
                        quiz.Question6Correct = userQuizQuestion.Correct;
                        quiz.Question6CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question7)
                    {
                        quiz.Question7Correct = userQuizQuestion.Correct;
                        quiz.Question7CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question8)
                    {
                        quiz.Question8Correct = userQuizQuestion.Correct;
                        quiz.Question8CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question9)
                    {
                        quiz.Question9Correct = userQuizQuestion.Correct;
                        quiz.Question9CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question10)
                    {
                        quiz.Question10Correct = userQuizQuestion.Correct;
                        quiz.Question10CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question11)
                    {
                        quiz.Question11Correct = userQuizQuestion.Correct;
                        quiz.Question11CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question12)
                    {
                        quiz.Question12Correct = userQuizQuestion.Correct;
                        quiz.Question12CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question13)
                    {
                        quiz.Question13Correct = userQuizQuestion.Correct;
                        quiz.Question13CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question14)
                    {
                        quiz.Question14Correct = userQuizQuestion.Correct;
                        quiz.Question14CorrectText = userQuizQuestion.AnswerText;
                    }
                    else if (userQuizQuestion.QuizQuestionId == (int)Common.QuizParticlesChapter.Question15)
                    {
                        quiz.Question15Correct = userQuizQuestion.Correct;
                        quiz.Question15CorrectText = userQuizQuestion.AnswerText;
                    }

                }

                return View("ChapterQuizResults", quiz);
            }

            return View();
        }

        // MARKING QUESTIONS CORRECT OR INCORRECT ON ENERGY CHAPTER QUIZ
        [HttpPost]
        public IActionResult ChapterQuiz(ParticlesChapterQuiz quiz)
        {
            int count = 0;

            // Marking Question 1 correct or incorrect
            if (quiz.Question1 == "A")
            {
                quiz.Question1Correct = true;
                count++;
            }
            quiz.Question1CorrectText = quiz.Question1;
            if (quiz.Question1CorrectText == null)
                quiz.Question1CorrectText = "none";

            // Marking Question 2 correct or incorrect
            if (quiz.Question2 == "C")
            {
                quiz.Question2Correct = true;
                count++;
            }
            quiz.Question2CorrectText = quiz.Question2;
            if (quiz.Question2CorrectText == null)
                quiz.Question2CorrectText = "none";

            // Marking Question 3 correct or incorrect
            if (quiz.Question3 == "D")
            {
                quiz.Question3Correct = true;
                count++;
            }
            quiz.Question3CorrectText = quiz.Question3;
            if (quiz.Question3CorrectText == null)
                quiz.Question3CorrectText = "none";

            // Marking Question 4 correct or incorrect
            if (quiz.Question4 == "B")
            {
                quiz.Question4Correct = true;
                count++;
            }
            quiz.Question4CorrectText = quiz.Question4;
            if (quiz.Question4CorrectText == null)
                quiz.Question4CorrectText = "none";

            // Marking Question 5 correct or incorrect
            if (quiz.Question5 == "D")
            {
                quiz.Question5Correct = true;
                count++;
            }
            quiz.Question5CorrectText = quiz.Question5;
            if (quiz.Question5CorrectText == null)
                quiz.Question5CorrectText = "none";

            // Marking Question 6 correct or incorrect
            if (quiz.Question6 == "B")
            {
                quiz.Question6Correct = true;
                count++;
            }
            quiz.Question6CorrectText = quiz.Question6;
            if (quiz.Question6CorrectText == null)
                quiz.Question6CorrectText = "none";

            // Marking Question 7 correct or incorrect
            if (quiz.Question7 == "A")
            {
                quiz.Question7Correct = true;
                count++;
            }
            quiz.Question7CorrectText = quiz.Question7;
            if (quiz.Question7CorrectText == null)
                quiz.Question7CorrectText = "none";

            // Marking Question 8 correct or incorrect
            if (quiz.Question8 == "B")
            {
                quiz.Question8Correct = true;
                count++;
            }
            quiz.Question8CorrectText = quiz.Question8;
            if (quiz.Question8CorrectText == null)
                quiz.Question8CorrectText = "none";

            // Marking Question 9 correct or incorrect
            if (quiz.Question9 == "C")
            {
                quiz.Question9Correct = true;
                count++;
            }
            quiz.Question9CorrectText = quiz.Question9;
            if (quiz.Question9CorrectText == null)
                quiz.Question9CorrectText = "none";

            // Marking Question 10 correct or incorrect
            if (quiz.Question10 == "D")
            {
                quiz.Question10Correct = true;
                count++;
            }
            quiz.Question10CorrectText = quiz.Question10;
            if (quiz.Question10CorrectText == null)
                quiz.Question10CorrectText = "none";

            // Marking Question 11 correct or incorrect
            // If answers 11a and 11b are true (checked), then question 11 is correct.
            if (quiz.Question11a && quiz.Question11b)
            {
                quiz.Question11Correct = true;
                count++;
            }
            string text = "";
            if (quiz.Question11a)
                text = "A";
            if (quiz.Question11b)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "B");
            }
            if (quiz.Question11c)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "C");
            }
            if (quiz.Question11d)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "D");
            }
            if (text == "")
                text = "none";
            quiz.Question11CorrectText = text;

            // Marking Question 12 correct or incorrect
            if (quiz.Question12b && quiz.Question12c)
            {
                quiz.Question12Correct = true;
                count++;
            }
            text = "";
            if (quiz.Question12a)
                text = "A";
            if (quiz.Question12b)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "B");
            }
            if (quiz.Question12c)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "C");
            }
            if (quiz.Question12d)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "D");
            }
            if (text == "")
                text = "none";
            quiz.Question12CorrectText = text;

            // Marking Question 13 correct or incorrect
            if (quiz.Question13a && quiz.Question13b && quiz.Question13c && quiz.Question13d)
            {
                quiz.Question13Correct = true;
                count++;
            }
            text = "";
            if (quiz.Question13a)
                text = "A";
            if (quiz.Question13b)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "B");
            }
            if (quiz.Question13c)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "C");
            }
            if (quiz.Question13d)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "D");
            }
            if (text == "")
                text = "none";
            quiz.Question13CorrectText = text;

            // Marking Question 14 correct or incorrect
            if (quiz.Question14b && quiz.Question14d)
            {
                quiz.Question14Correct = true;
                count++;
            }
            text = "";
            if (quiz.Question14a)
                text = "A";
            if (quiz.Question14b)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "B");
            }
            if (quiz.Question14c)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "C");
            }
            if (quiz.Question14d)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "D");
            }
            if (text == "")
                text = "none";
            quiz.Question14CorrectText = text;

            // Marking Question 15 correct or incorrect
            if (quiz.Question15a && quiz.Question15c)
            {
                quiz.Question15Correct = true;
                count++;
            }
            text = "";
            if (quiz.Question15a)
                text = "A";
            if (quiz.Question15b)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "B");
            }
            if (quiz.Question15c)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "C");
            }
            if (quiz.Question15d)
            {
                if (text != "")
                {
                    text = string.Concat(text, ", ");
                }
                text = string.Concat(text, "D");
            }
            if (text == "")
                text = "none";
            quiz.Question15CorrectText = text;


            UserQuiz userQuiz = new UserQuiz();
            userQuiz.UserId = _common.GetUserId(HttpContext);
            // Change to retrieve quiz id
            userQuiz.QuizId = (int)Common.QuizParticles.ChapterQuiz;
            userQuiz.CorrectAnswers = count;
            userQuiz.TotalAnswers = 15;

            decimal percentageCalc = ((decimal)userQuiz.CorrectAnswers / (decimal)userQuiz.TotalAnswers) * (decimal)100;
            decimal percentageRound = Math.Ceiling(percentageCalc);
            userQuiz.Percentage = Convert.ToInt32(percentageRound);

            // Sending variables to Energy Opening Quiz page to display results.
            quiz.CorrectAnswerTotal = userQuiz.CorrectAnswers;
            quiz.Percentage = userQuiz.Percentage;

            UserQuizQuestion userQuizQuestion1 = new UserQuizQuestion();
            userQuizQuestion1.UserId = userQuiz.UserId;
            userQuizQuestion1.QuizQuestionId = (int)Common.QuizParticlesChapter.Question1;
            userQuizQuestion1.Correct = quiz.Question1Correct;
            userQuizQuestion1.AnswerText = quiz.Question1CorrectText;

            UserQuizQuestion userQuizQuestion2 = new UserQuizQuestion();
            userQuizQuestion2.UserId = userQuiz.UserId;
            userQuizQuestion2.QuizQuestionId = (int)Common.QuizParticlesChapter.Question2;
            userQuizQuestion2.Correct = quiz.Question2Correct;
            userQuizQuestion2.AnswerText = quiz.Question2CorrectText;

            UserQuizQuestion userQuizQuestion3 = new UserQuizQuestion();
            userQuizQuestion3.UserId = userQuiz.UserId;
            userQuizQuestion3.QuizQuestionId = (int)Common.QuizParticlesChapter.Question3;
            userQuizQuestion3.Correct = quiz.Question3Correct;
            userQuizQuestion3.AnswerText = quiz.Question3CorrectText;

            UserQuizQuestion userQuizQuestion4 = new UserQuizQuestion();
            userQuizQuestion4.UserId = userQuiz.UserId;
            userQuizQuestion4.QuizQuestionId = (int)Common.QuizParticlesChapter.Question4;
            userQuizQuestion4.Correct = quiz.Question4Correct;
            userQuizQuestion4.AnswerText = quiz.Question4CorrectText;

            UserQuizQuestion userQuizQuestion5 = new UserQuizQuestion();
            userQuizQuestion5.UserId = userQuiz.UserId;
            userQuizQuestion5.QuizQuestionId = (int)Common.QuizParticlesChapter.Question5;
            userQuizQuestion5.Correct = quiz.Question5Correct;
            userQuizQuestion5.AnswerText = quiz.Question5CorrectText;

            UserQuizQuestion userQuizQuestion6 = new UserQuizQuestion();
            userQuizQuestion6.UserId = userQuiz.UserId;
            userQuizQuestion6.QuizQuestionId = (int)Common.QuizParticlesChapter.Question6;
            userQuizQuestion6.Correct = quiz.Question6Correct;
            userQuizQuestion6.AnswerText = quiz.Question6CorrectText;

            UserQuizQuestion userQuizQuestion7 = new UserQuizQuestion();
            userQuizQuestion7.UserId = userQuiz.UserId;
            userQuizQuestion7.QuizQuestionId = (int)Common.QuizParticlesChapter.Question7;
            userQuizQuestion7.Correct = quiz.Question7Correct;
            userQuizQuestion7.AnswerText = quiz.Question7CorrectText;

            UserQuizQuestion userQuizQuestion8 = new UserQuizQuestion();
            userQuizQuestion8.UserId = userQuiz.UserId;
            userQuizQuestion8.QuizQuestionId = (int)Common.QuizParticlesChapter.Question8;
            userQuizQuestion8.Correct = quiz.Question8Correct;
            userQuizQuestion8.AnswerText = quiz.Question8CorrectText;

            UserQuizQuestion userQuizQuestion9 = new UserQuizQuestion();
            userQuizQuestion9.UserId = userQuiz.UserId;
            userQuizQuestion9.QuizQuestionId = (int)Common.QuizParticlesChapter.Question9;
            userQuizQuestion9.Correct = quiz.Question9Correct;
            userQuizQuestion9.AnswerText = quiz.Question9CorrectText;

            UserQuizQuestion userQuizQuestion10 = new UserQuizQuestion();
            userQuizQuestion10.UserId = userQuiz.UserId;
            userQuizQuestion10.QuizQuestionId = (int)Common.QuizParticlesChapter.Question10;
            userQuizQuestion10.Correct = quiz.Question10Correct;
            userQuizQuestion10.AnswerText = quiz.Question10CorrectText;

            UserQuizQuestion userQuizQuestion11 = new UserQuizQuestion();
            userQuizQuestion11.UserId = userQuiz.UserId;
            userQuizQuestion11.QuizQuestionId = (int)Common.QuizParticlesChapter.Question11;
            userQuizQuestion11.Correct = quiz.Question11Correct;
            userQuizQuestion11.AnswerText = quiz.Question11CorrectText;

            UserQuizQuestion userQuizQuestion12 = new UserQuizQuestion();
            userQuizQuestion12.UserId = userQuiz.UserId;
            userQuizQuestion12.QuizQuestionId = (int)Common.QuizParticlesChapter.Question12;
            userQuizQuestion12.Correct = quiz.Question12Correct;
            userQuizQuestion12.AnswerText = quiz.Question12CorrectText;

            UserQuizQuestion userQuizQuestion13 = new UserQuizQuestion();
            userQuizQuestion13.UserId = userQuiz.UserId;
            userQuizQuestion13.QuizQuestionId = (int)Common.QuizParticlesChapter.Question13;
            userQuizQuestion13.Correct = quiz.Question13Correct;
            userQuizQuestion13.AnswerText = quiz.Question13CorrectText;

            UserQuizQuestion userQuizQuestion14 = new UserQuizQuestion();
            userQuizQuestion14.UserId = userQuiz.UserId;
            userQuizQuestion14.QuizQuestionId = (int)Common.QuizParticlesChapter.Question14;
            userQuizQuestion14.Correct = quiz.Question14Correct;
            userQuizQuestion14.AnswerText = quiz.Question14CorrectText;

            UserQuizQuestion userQuizQuestion15 = new UserQuizQuestion();
            userQuizQuestion15.UserId = userQuiz.UserId;
            userQuizQuestion15.QuizQuestionId = (int)Common.QuizParticlesChapter.Question15;
            userQuizQuestion15.Correct = quiz.Question15Correct;
            userQuizQuestion15.AnswerText = quiz.Question15CorrectText;

            try
            {
                _context.Reset();
                _context.UserQuiz.Add(userQuiz);
                _context.UserQuizQuestion.Add(userQuizQuestion1);
                _context.UserQuizQuestion.Add(userQuizQuestion2);
                _context.UserQuizQuestion.Add(userQuizQuestion3);
                _context.UserQuizQuestion.Add(userQuizQuestion4);
                _context.UserQuizQuestion.Add(userQuizQuestion5);
                _context.UserQuizQuestion.Add(userQuizQuestion6);
                _context.UserQuizQuestion.Add(userQuizQuestion7);
                _context.UserQuizQuestion.Add(userQuizQuestion8);
                _context.UserQuizQuestion.Add(userQuizQuestion9);
                _context.UserQuizQuestion.Add(userQuizQuestion10);
                _context.UserQuizQuestion.Add(userQuizQuestion11);
                _context.UserQuizQuestion.Add(userQuizQuestion12);
                _context.UserQuizQuestion.Add(userQuizQuestion13);
                _context.UserQuizQuestion.Add(userQuizQuestion14);
                _context.UserQuizQuestion.Add(userQuizQuestion15);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return View("ChapterQuizResults", quiz);
        }
    }
}