using Dapper;
using ESchoolDemo.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchoolDemo.Services
{
    public class StudentService
    {
        protected static string connectionString = "host=localhost;port=3306;user id=root;password=root;database=eschooldb;";

        public static List<StudentClass> StudentByNameSurname(string name, string surname)
        {
            List<StudentClass> students = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select * from student_table where studentname like @studentname and studentsurname like @studentsurname";
                students = connection.Query<StudentClass>(query, new {studentname = name + "%", studentsurname = surname + "%"}).ToList();
            }
            return students;
        }

        public static List<StudentClass> StudentsBySchoolNumber(int? schoolNo)
        {
            List<StudentClass> students = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select * from student_table where schoolnumber=@schoolnumber";
                students = connection.Query<StudentClass>(query, new { schoolnumber = schoolNo }).ToList();
            }
            return students;
        }

        public static StudentClass StudentBySchoolNumber(int? schoolNumber)
        {
            StudentClass students = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select * from student_table where schoolnumber=@schoolnumber";
                students = connection.QuerySingleOrDefault<StudentClass>(query, new { SchoolNumber = schoolNumber });
            }
            return students;
        }

        public static StudentClass StudentsById(int? id)
        {
            StudentClass students = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select * from student_table where Id=@Id";
                students = connection.QuerySingleOrDefault<StudentClass>(query, new { Id = id });
            }
            return students;
        }

        public static List<StudentClass> SearchForUpdate(int id, int schoolNumber)
        {
            List<StudentClass> students = null;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = "select * from student_table where Id=@Id and SchoolNumber=@SchoolNumber";
                students = connection.Query<StudentClass>(query, new { Id = id, SchoolNumber = schoolNumber }).ToList();
            }
            return students;
        }

        public static List<StudentClass> SelectLastTenStudent()
        {
            List<StudentClass> students = null;
            string query = "select * from student_table order by Id desc limit 10";
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                students = connection.Query<StudentClass>(query).ToList();
            }
            return students;
        }

        public int AddStudent(StudentClass student)
        {
            int numberOfAddStudent;
            string query = "insert into student_table (studentname, studentsurname, schoolnumber) values (@studentname, @studentsurname, @schoolnumber)";
            using(IDbConnection connection=new MySqlConnection(connectionString))
            {
                numberOfAddStudent = connection.Execute(query, student);
            }
            return numberOfAddStudent;
        }

        public int UpdateStudent(StudentClass student)
        {
            int numberOfUpdateStudent;
            string query = "update student_table set studentname=@studentname, studentsurname=@studentsurname, schoolnumber=@schoolnumber where Id=@Id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfUpdateStudent = connection.Execute(query, student);
            }
            return numberOfUpdateStudent;
        }

        public int DeleteStudent(int id)
        {
            int numberOfDeleteStudent;
            string query = "delete from student_table where Id=@Id";
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                numberOfDeleteStudent = connection.Execute(query, new { Id = id });
            }
            return numberOfDeleteStudent;
        }
    }
}
