using ESchoolDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using MaterialDesignThemes.Wpf;

namespace ESchoolDemo.Services
{
    public class CourseService
    {
        protected static string connectionString = "host=localhost;port=3306;user id=root;password=root;database=eschooldb;";

        public static List<CourseClass> GetLessons()
        {
            List<CourseClass> courses = null;

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM course_table ORDER BY Id";
                courses = connection.Query<CourseClass>(query).ToList();
            }
            return courses;
        }

        public static List<CourseClass> GetLastTenCourse()
        {
            List<CourseClass> courses = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM course_table ORDER BY Id DESC LIMIT 10";
                courses = connection.Query<CourseClass>(query).ToList();
            }
            return courses;
        }

        public static int AddCourse(CourseClass addCourse)
        {
            int numberOfCoursesAdded;
            string query = "insert into course_table(coursecode, coursename, classhour) values (@coursecode, @coursename, @classhour)";
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfCoursesAdded = connection.Execute(query, addCourse);
            }
            return numberOfCoursesAdded;
        }

        public static CourseClass SelectCourseByCourseCode(string courseCode)
        {
            CourseClass course = null;
            string query = "select * from course_table where coursecode=@coursecode";
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                course = connection.QuerySingleOrDefault<CourseClass>(query, new {CourseCode = courseCode});
            }
            return course;
        }

        public static List<CourseClass> SearchByCourseNameAndCode(string courseName, string courseCode)
        {
            List<CourseClass> course = null;
            string query = "select * from course_table where coursecode like @coursecode and coursename like @coursename";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                course = connection.Query<CourseClass>(query, new { CourseCode = courseCode + "%", CourseName = courseName + "%" }).ToList();
            }
            return course;
        }

        public static CourseClass SearchCourseById(int id)
        {
            CourseClass course = null;
            string query = "select * from course_table where Id=@Id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                course = connection.QuerySingleOrDefault<CourseClass>(query, new { Id = id });
            }
            return course;
        }

        public static CourseClass SearchForUpdate(int id, string courseCode)
        {
            CourseClass course = null;
            string query = "select * from course_table where coursecode = @coursecode and Id = @Id";
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                course = connection.QuerySingleOrDefault<CourseClass>(query, new { Id = id, coursecode = courseCode });
            }
            return course;
        }

        public static int UpdateCours(CourseClass course)
        {
            int numberOfUpdateCourse;
            string query = "update course_table set coursecode = @coursecode, coursename = @coursename, classhour = @classhour where Id = @Id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfUpdateCourse = connection.Execute(query, course);
            }
            return numberOfUpdateCourse;
        }

        public static int DeleteCourse(int id)
        {
            int numberOfDeleteCourse;
            string query = "delete from course_table where Id = @Id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfDeleteCourse = connection.Execute(query, new { Id = id });
            }
            return numberOfDeleteCourse;
        }
    }
}
