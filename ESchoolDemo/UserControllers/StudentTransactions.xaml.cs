using ESchoolDemo.Models;
using ESchoolDemo.Pages;
using ESchoolDemo.Services;
using ESchoolDemo.Validations;
using ESchoolDemo.ViewModel;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ESchoolDemo.UserControllers
{
    /// <summary>
    /// Interaction logic for StudentTransactions.xaml
    /// </summary>
    public partial class StudentTransactions : UserControl
    {
        StudentService studentS;
        PreviewWindow previewWindow;
        public StudentTransactions()
        {
            InitializeComponent();
            studentS = new StudentService();
            StudentDT.ItemsSource = StudentService.SelectLastTenStudent();
            previewWindow = new PreviewWindow();
        }

        void CleanByTextbox()
        {
            ErrorList.Items.Clear();
            StudentIdTXT.Clear();
            StudentNameTXT.Clear();
            StudentSurnameTXT.Clear();
            StudentNumberTXT.Clear();
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorList.Items.Clear();

            StudentClass addStudent = new StudentClass()
            {
                StudentName = StudentNameTXT.Text,
                StudentSurname = StudentSurnameTXT.Text,
                SchoolNumber = (Int32.TryParse(StudentNumberTXT.Text, out int schoolNumber) ? schoolNumber : (int?)null)
            };

            StudentValidation validations = new StudentValidation();
            ValidationResult validationResult = validations.Validate(addStudent);

            if(validationResult.IsValid == false)
            {
                foreach(ValidationFailure item in validationResult.Errors) 
                    ErrorList.Items.Add(item.ErrorMessage);
            }
            else
            {
                var student = StudentService.StudentBySchoolNumber(Convert.ToInt32(StudentNumberTXT.Text));
                if (student == null)
                {
                    int numberOfAddStudent = studentS.AddStudent(addStudent);
                    if (numberOfAddStudent == 1)
                    {
                        MessageClass.ShowInfoMsg("New student added successfully!", "Info");
                        StudentDT.ItemsSource = StudentService.SelectLastTenStudent();
                        CleanByTextbox();
                    }
                }
                else
                    MessageClass.ShowErrorMsg("Error", "Error");
            }
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            int studentId = Convert.ToInt32(StudentIdTXT.Text);
            if (Convert.ToString(studentId) != "")
            {
                string deleteMessage = $"{StudentNumberTXT.Text} : {StudentNameTXT.Text} : {StudentSurnameTXT.Text} delete, are you sure?";
                MessageBoxResult answer = MessageClass.ShowQuestionMsg(deleteMessage, "Question?");
                if(answer == MessageBoxResult.Yes)
                {
                    int numberOfStudent = studentS.DeleteStudent(studentId);
                    if (numberOfStudent == 1)
                    {
                        MessageClass.ShowInfoMsg("Student deletion successful", "Successful");
                        StudentDT.ItemsSource = StudentService.SelectLastTenStudent();
                        CleanByTextbox();
                    }
                    else
                        MessageClass.ShowErrorMsg("Error", "Error");
                }
            }
            else
                MessageClass.ShowErrorMsg("Error", "Error");
        }

        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorList.Items.Clear();

            StudentClass updateStudent = new StudentClass()
            {
                Id = Convert.ToInt32(StudentIdTXT.Text),
                StudentName = StudentNameTXT.Text,
                StudentSurname = StudentSurnameTXT.Text,
                SchoolNumber = (Int32.TryParse(StudentNumberTXT.Text, out int schoolNumber) ? schoolNumber : (int?)null)
            };

            StudentValidation validations = new StudentValidation();
            ValidationResult validationResult = validations.Validate(updateStudent);

            if (validationResult.IsValid == false)
            {
                foreach (ValidationFailure item in validationResult.Errors)
                    ErrorList.Items.Add(item.ErrorMessage);
            }
            else
            {
                var student = StudentService.SearchForUpdate(Convert.ToInt32(StudentIdTXT.Text), Convert.ToInt32(StudentNumberTXT.Text));
                if (student != null)
                {
                    string updateMessage = $"{StudentNumberTXT.Text} Are you sure you want to update?";
                    MessageBoxResult answer = MessageClass.ShowQuestionMsg(updateMessage, "Question?");
                    if(answer == MessageBoxResult.Yes)
                    {
                        int numberOfAddStudent = studentS.UpdateStudent(updateStudent);
                        if (numberOfAddStudent == 1)
                        {
                            MessageClass.ShowInfoMsg("Update student successfully!", "Info");
                            StudentDT.ItemsSource = StudentService.SelectLastTenStudent();
                            CleanByTextbox();
                        }
                    }
                }
                else
                    MessageClass.ShowErrorMsg("Error", "Error");
            }
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            int? schoolNo = (Int32.TryParse(StudentNumberTXT.Text, out int schNo) ? schNo : (int?)null);
            string name = StudentNameTXT.Text;
            string surname = StudentSurnameTXT.Text;
            if(schoolNo != null)
            {
                var students = StudentService.StudentsBySchoolNumber(schoolNo);
                if (students.Count.Equals(0))
                    MessageClass.ShowInfoMsg("Not found!", "Info");
                else
                    StudentDT.ItemsSource= students;
            }
            else
            {
                if(name.Trim() != "" || surname.Trim() != "")
                {
                    var students = StudentService.StudentByNameSurname(name, surname);
                    if(students.Count.Equals(0))
                        MessageClass.ShowInfoMsg("Not found!", "Info");
                    else
                        StudentDT.ItemsSource = students;
                }
            }
        }

        private void ClearBTN_Click(object sender, RoutedEventArgs e)
        {
            CleanByTextbox();
        }

        private void StudentDT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StudentClass selectedStudent = StudentDT.SelectedItem as StudentClass;
            if(selectedStudent != null)
            {
                StudentIdTXT.Text = selectedStudent.Id.ToString();
                StudentNameTXT.Text = selectedStudent.StudentName;
                StudentSurnameTXT.Text = selectedStudent.StudentSurname;
                StudentNumberTXT.Text = selectedStudent.SchoolNumber.ToString();
            }
        }

        private void PrintBTN_Click(object sender, RoutedEventArgs e)
        {
            if (StudentIdTXT.Text != null)
            {
                var note = NoteService.SearchScoreByStudentId(Convert.ToInt32(StudentIdTXT.Text));
                if (note.Count.Equals(0))
                    MessageClass.ShowErrorMsg("error", "error");
                else
                {
                    List<StudentCourseModel> snModel = note;
                    double? overallAverage = NoteService.CalculateAverage(snModel);
                    previewWindow.AddDataDocument(snModel, overallAverage);
                    previewWindow.ShowDialog();
                }
            }
            else
                MessageClass.ShowErrorMsg("Please choose student!", "Error");
        }
    }
}
