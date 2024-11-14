using ESchoolDemo.Models;
using ESchoolDemo.Services;
using ESchoolDemo.Validations;
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
    /// Interaction logic for Lessons.xaml
    /// </summary>
    public partial class Lessons : UserControl
    {
        CourseService cService;
        public Lessons()
        {
            InitializeComponent();
            cService = new CourseService();
            LessonsDT.ItemsSource = CourseService.GetLessons();
            ClassHourComboBox();
        }

        private void ClassHourComboBox()
        {
            ClassHourCB.Items.Clear();
            for (int i = 1; i < 15; i++) 
                ClassHourCB.Items.Add(i.ToString());

            ClassHourCB.SelectedIndex = 1;
        }

        private void ClearByTextbox()
        {
            ErrorList.Items.Clear();
            CourseIdTXT.Clear();
            CourseNameTXT.Clear();
            CourseCodeTXT.Clear();
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorList.Items.Clear();
            CourseClass addedCourse = new CourseClass()
            {
                CourseName = CourseNameTXT.Text,
                CourseCode = CourseCodeTXT.Text,
                ClassHour = Convert.ToInt32(ClassHourCB.Text)
            };

            CourseValidation validation = new CourseValidation();
            ValidationResult validationResult = validation.Validate(addedCourse);

            if(validationResult.IsValid == false)
            {
                foreach(var item in validationResult.Errors)
                {
                    ErrorList.Items.Add(item.ErrorMessage);
                }
            }
            else
            {
                var course = CourseService.SelectCourseByCourseCode(CourseCodeTXT.Text);
                if(course == null)
                {
                    int numberOfAddedCourse = CourseService.AddCourse(addedCourse);
                    if (numberOfAddedCourse.Equals(1))
                    {
                        MessageClass.ShowInfoMsg("Course Added Successfully", "Add Course");
                        LessonsDT.ItemsSource = CourseService.GetLessons();
                        ClearByTextbox();
                    }
                    else
                        MessageClass.ShowErrorMsg("Failed to add a course.", "Error");
                }
                else
                    MessageClass.ShowInfoMsg($"The course code {CourseCodeTXT.Text} has been added before.", "Error");
            }
        }

        private void ClearBTN_Click(object sender, RoutedEventArgs e)
        {
            ClearByTextbox();
        }

        private void LessonsDT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CourseClass selectedCourse = LessonsDT.SelectedItem as CourseClass;
            if (selectedCourse != null)
            {
                CourseIdTXT.Text = selectedCourse.Id.ToString();
                CourseNameTXT.Text = selectedCourse.CourseName.ToString();
                CourseCodeTXT.Text = selectedCourse.CourseCode.ToString();
                ClassHourCB.SelectedIndex = selectedCourse.ClassHour - 1;
            }
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!CourseIdTXT.Text.Equals(""))
            {
                string deleteMessage = $"{CourseCodeTXT.Text} : {CourseNameTXT.Text} Are you sure you want to delete the course?";
                MessageBoxResult answer = MessageClass.ShowQuestionMsg(deleteMessage, "Course Delete?");
                if (answer.Equals(MessageBoxResult.Yes))
                {
                    int numberOfDeleteCourse = CourseService.DeleteCourse(Convert.ToInt32(CourseIdTXT.Text));
                    if (numberOfDeleteCourse.Equals(1))
                    {
                        MessageClass.ShowInfoMsg("Course deletion successful!", "Successful");
                        LessonsDT.ItemsSource = CourseService.GetLastTenCourse();
                        ClearByTextbox();
                    }
                    else
                        MessageClass.ShowErrorMsg("An error occurred while deleting a course.", "Error");
                }
            }
            else
                MessageClass.ShowErrorMsg("Please select a course first!", "Error");
        }

        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            if (CourseIdTXT.Text != "")
            {
                ErrorList.Items.Clear();
                CourseClass updateCourse = new CourseClass()
                {
                    Id = Convert.ToInt32(CourseIdTXT.Text),
                    CourseName = CourseNameTXT.Text,
                    CourseCode = CourseCodeTXT.Text,
                    ClassHour = Convert.ToInt32(ClassHourCB.Text)
                };

                CourseValidation validation = new CourseValidation();
                ValidationResult validationResult = validation.Validate(updateCourse);

                if (validationResult.IsValid.Equals(false))
                {
                    foreach (ValidationFailure item in validationResult.Errors)
                        ErrorList.Items.Add(item.ErrorMessage);
                }
                else
                {
                    var course = CourseService.SearchForUpdate(Convert.ToInt32(CourseIdTXT.Text), CourseCodeTXT.Text);
                    if(course != null)
                    {
                        string updateMessage = $"Are you sure you want to update your {CourseCodeTXT.Text} : {CourseNameTXT.Text} course?";
                        MessageBoxResult answer = MessageClass.ShowQuestionMsg(updateMessage, "Update?");
                        if (answer == MessageBoxResult.Yes)
                        {
                            int numberOfUpdateCourse = CourseService.UpdateCours(updateCourse);
                            if (numberOfUpdateCourse == 1)
                            {
                                MessageClass.ShowInfoMsg("Course update successful!", "Successful");
                                LessonsDT.ItemsSource = CourseService.GetLastTenCourse();
                                ClearByTextbox();
                            }
                            else
                                MessageClass.ShowErrorMsg("Error", "Error");
                        }
                    }
                }
            }
            else
                MessageClass.ShowErrorMsg("Please select the course you want to update.", "Error");
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (CourseNameTXT.Text.Trim() != string.Empty || CourseCodeTXT.Text.Trim() != string.Empty)
            {
                var course = CourseService.SearchByCourseNameAndCode(CourseNameTXT.Text, CourseCodeTXT.Text);
                if (course.Count == 0)
                    MessageClass.ShowInfoMsg("No courses found that meet the criteria.", "Info");
                else
                    LessonsDT.ItemsSource = course;
            }
            else
                MessageClass.ShowInfoMsg("Please enter the name or code of the course you want to search.", "Info");
        }
    }
}
