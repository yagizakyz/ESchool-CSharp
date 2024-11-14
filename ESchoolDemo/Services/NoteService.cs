using Dapper;
using ESchoolDemo.Models;
using ESchoolDemo.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ESchoolDemo.Services
{
    public class NoteService
    {
        protected static string connectionString = "host=localhost;port=3306;user id=root;password=root;database=eschooldb;";

        public static List<StudentCourseModel> GetNote()
        {
            List<StudentCourseModel> examScores = null;
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select course_table.CourseName, course_table.ClassHour, student_note_table.*, student_table.StudentName, student_table.StudentSurname, student_table.SchoolNumber from student_note_table" +
                    " inner join course_table on course_table.Id = student_note_table.CourseID " +
                    " inner join student_table on student_table.Id = student_note_table.StudentId; ";
                examScores = connection.Query<StudentCourseModel>(query).ToList();
            }
            return examScores;
        }

        public static List<StudentCourseModel> SearchScoreByStudentId(int studentId)
        {
            List<StudentCourseModel> examScores = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select course_table.CourseName, course_table.ClassHour, student_note_table.*, student_table.StudentName, student_table.StudentSurname, student_table.SchoolNumber from student_note_table" +
                    " inner join course_table on course_table.Id = student_note_table.CourseID " +
                    " inner join student_table on student_table.Id = student_note_table.StudentId where StudentId=@StudentId";
                examScores = connection.Query<StudentCourseModel>(query, new {StudentId = studentId}).ToList();
            }
            return examScores;
        }

        public static List<StudentCourseModel> SearchScoreByCourseId(int courseId)
        {
            List<StudentCourseModel> examScores = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select course_table.CourseName, course_table.ClassHour, student_note_table.*, student_table.StudentName, student_table.StudentSurname, student_table.SchoolNumber from student_note_table" +
                    " inner join course_table on course_table.Id = student_note_table.CourseID " +
                    " inner join student_table on student_table.Id = student_note_table.StudentId where CourseId=@CourseId";
                examScores = connection.Query<StudentCourseModel>(query, new { CourseId = courseId }).ToList();
            }
            return examScores;
        }

        public static NoteClass SearchByStudenAndCourse(int sId, int cId)
        {
            NoteClass note = null;
            string query = "select * from student_note_table where StudentId=@StudentId and CourseId=@CourseId";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                note = connection.QuerySingleOrDefault<StudentCourseModel>(query, new { StudentId = sId, CourseId = cId });
            }
            return note;
        }

        public static double? CalculateAverage(List<StudentCourseModel> examScores)
        {
            int totalLessonHours = 0;
            double? totalAverage = 0;
            foreach(var score in examScores)
            {
                totalLessonHours += score.ClassHour;
                totalAverage += score.ClassHour * score.Average;
            }
            return totalAverage / totalLessonHours;
        }

        public static int AddNote(NoteClass note)
        {
            int numberOfAddNote;
            string sorgu = "Insert into student_note_table(StudentId, CourseId, Exam1, Exam2, Performance1, Performance2, Average, Situation) " +
                "values (@StudentId, @CourseId, @Exam1, @Exam2, @Performance1, @Performance2, @Average, @Situation)";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfAddNote = connection.Execute(sorgu, note);
            }
            return numberOfAddNote;
        }

        public static int UpdateNote(NoteClass note)
        {
            int numberOfUpdateNote;
            string query = "update student_note_table set Exam1=@Exam1, Exam2=@Exam2, " +
                           "Performance1=@Performance1, Performance2=@Performance2, " +
                           "Average=@Average, Situation=@Situation  " +
                           "where Id=@Id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfUpdateNote = connection.Execute(query, note);
            }
            return numberOfUpdateNote;
        }

        public static int DeleteNote(int id)
        {
            int numberOfDeleteNote;
            string query = "delete  from student_note_table  where Id=@Id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfDeleteNote = connection.Execute(query, new { Id = id });
            }
            return numberOfDeleteNote;
        }
    }
}
