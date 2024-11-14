using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using ESchoolDemo.Models;
using ESchoolDemo.Pages;
using ESchoolDemo.Services;
using ESchoolDemo.Validations;
using ESchoolDemo.ViewModel;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ESchoolDemo.UserControllers
{
    /// <summary>
    /// Interaction logic for NoteTransactions.xaml
    /// </summary>
    public partial class NoteTransactions : UserControl
    {
        StudentClass selectedStudent;
        CourseService courseS;
        NoteService nService;
        StudentService studentS;
        SelectedStudentWindow selectedStudentWindow;
        PreviewWindow previewWindow;
        NoteClass noteClass;
        public NoteTransactions()
        {
            InitializeComponent();
            CleanTextbox();
            selectedStudentWindow = new SelectedStudentWindow();
            selectedStudentWindow.StudentListBox.SelectionChanged += StudentListBox_SelectionChanged;

            previewWindow = new PreviewWindow();

            courseS = new CourseService();
            CourseCB.ItemsSource = CourseService.GetLessons();
            CourseCB.SelectedValuePath = "Id";
            CourseCB.DisplayMemberPath = "CourseContent";

            nService = new NoteService();
            NoteDT.ItemsSource = NoteService.GetNote();
        }

        private void StudentListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(selectedStudentWindow.StudentListBox.SelectedItem != null)
            {
                selectedStudent = selectedStudentWindow.StudentListBox.SelectedItem as StudentClass;
                SelectedTXT.Text = selectedStudent.ToString();
            }
        }

        private void BT_Click(object sender, RoutedEventArgs e)
        {
            selectedStudentWindow.ShowDialog();
        }

        private void PrintBTN_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudent != null)
            {
                var note = NoteService.SearchScoreByStudentId(selectedStudent.Id);
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

        void CleanTextbox()
        {
            ErrorList.Items.Clear();
            StudentNoteID.Clear();
            SelectedTXT.Clear();
            CourseCB.SelectedItem = null;
            selectedStudent = null;
            noteClass = null;
            Exam1TXT.Clear();
            Exam2TXT.Clear();
            Performance1TXT.Clear();
            Performance2TXT.Clear();
            AverageTXT.Clear();
            SituationTXT.Clear();
        }

        private void AddBTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorList.Items.Clear();
            if (selectedStudent != null && CourseCB.SelectedItem != null)
            {
                int sId = selectedStudent.Id;
                int cId = (CourseCB.SelectedItem as CourseClass).Id;

                NoteClass AddNote = new NoteClass()
                {
                    CourseId = cId,
                    StudentId = sId,
                    Exam1 = (Int32.TryParse(Exam1TXT.Text, out int exam1) ? exam1 : (int?)null),
                    Exam2 = (Int32.TryParse(Exam2TXT.Text, out int exam2) ? exam2 : (int?)null),
                    Performance1 = (Int32.TryParse(Performance1TXT.Text, out int performance1) ? performance1 : (int?)null),
                    Performance2 = (Int32.TryParse(Performance2TXT.Text, out int performance2) ? performance2 : (int?)null),
                };

                AddNote.CalculateAverage();

                NoteValidation validation = new NoteValidation();
                ValidationResult validationResult = validation.Validate(AddNote);
                if (validationResult.IsValid.Equals(false))
                {
                    foreach(ValidationFailure item in validationResult.Errors)
                        ErrorList.Items.Add(item.ErrorMessage);
                }
                else
                {
                    NoteClass notes = NoteService.SearchByStudenAndCourse(sId, cId);
                    if (notes == null)
                    {
                        int numberOfNote = NoteService.AddNote(AddNote);
                        if (numberOfNote.Equals(1))
                        {
                            MessageClass.ShowInfoMsg("Note added successfully!", "Successfully");
                            NoteDT.ItemsSource = NoteService.GetNote();
                            CleanTextbox();
                        }
                        else
                            MessageClass.ShowErrorMsg("Error", "Error");
                    } 
                    else
                        MessageClass.ShowErrorMsg("Error", "Error");
                }
            }
            else
                ErrorList.Items.Add("Please choose student!");
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorList.Items.Clear();
            if(StudentNoteID.Text != "")
            {
                string deleteMessage = $"{SelectedTXT.Text} deleting, are you sure?";
                MessageBoxResult answer = MessageClass.ShowQuestionMsg(deleteMessage, "Question?");
                if(answer == MessageBoxResult.Yes)
                {
                    int numberOfDelete = NoteService.DeleteNote(Convert.ToInt32(StudentNoteID.Text));
                    if (numberOfDelete.Equals(1))
                    {
                        MessageClass.ShowInfoMsg("Note delete successfully!", "Successfully");
                        NoteDT.ItemsSource = NoteService.GetNote();
                        CleanTextbox();
                    }
                    else MessageClass.ShowErrorMsg("Error", "Error");
                }
            }
        }

        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorList.Items.Clear();
            if (StudentNoteID.Text != "")
            {
                NoteClass UpdateNote = new NoteClass()
                {
                    Id = Convert.ToInt32(StudentNoteID.Text),
                    Exam1 = (Int32.TryParse(Exam1TXT.Text, out int exam1) ? exam1 : (int?)null),
                    Exam2 = (Int32.TryParse(Exam2TXT.Text, out int exam2) ? exam2 : (int?)null),
                    Performance1 = (Int32.TryParse(Performance1TXT.Text, out int performance1) ? performance1 : (int?)null),
                    Performance2 = (Int32.TryParse(Performance2TXT.Text, out int performance2) ? performance2 : (int?)null),
                };

                UpdateNote.CalculateAverage();

                NoteValidation validation = new NoteValidation();
                ValidationResult validationResult = validation.Validate(UpdateNote);
                if (validationResult.IsValid.Equals(false))
                {
                    foreach (ValidationFailure item in validationResult.Errors)
                        ErrorList.Items.Add(item.ErrorMessage);
                }
                else
                {
                    string updateMessage = $"{SelectedTXT.Text} updating, are you sure?";
                    MessageBoxResult answer = MessageClass.ShowQuestionMsg(updateMessage, "Question?");
                    if (answer.Equals(MessageBoxResult.Yes))
                    {
                        int numberOfUpdate = NoteService.UpdateNote(UpdateNote);
                        if (numberOfUpdate.Equals(1))
                        {
                            MessageClass.ShowInfoMsg("Note updated successfully!", "Successfully");
                            NoteDT.ItemsSource = NoteService.GetNote();
                            CleanTextbox();
                        }
                        else
                            MessageClass.ShowErrorMsg("Error", "Error");
                    }
                }
            }
            else
                MessageClass.ShowErrorMsg("Please choose student!", "Error");
        }

        private void SearchStudentBTN_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudent != null)
            {
                var note = NoteService.SearchScoreByStudentId(selectedStudent.Id);
                if (note.Count.Equals(0))
                    MessageClass.ShowInfoMsg("Not found", "Not found");
                else
                    NoteDT.ItemsSource = note;
            }
            else
                MessageClass.ShowInfoMsg("Please choose student", "");
        }

        private void SearchLessonBTN_Click(object sender, RoutedEventArgs e)
        {
            if (CourseCB.SelectedItem != null)
            {
                int cId = Int32.Parse(CourseCB.SelectedValue.ToString());
                var note = NoteService.SearchScoreByCourseId(cId);
                if (note.Count.Equals(0))
                    MessageClass.ShowInfoMsg("Not found", "Not found");
                else
                    NoteDT.ItemsSource = note;
            }
            else
                MessageClass.ShowInfoMsg("Please choose student", "");
        }

        private void ClearBTN_Click(object sender, RoutedEventArgs e)
        {
            CleanTextbox();
        }

        private void NoteDT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            noteClass = NoteDT.SelectedItem as NoteClass;
            if(noteClass != null)
            {
                StudentNoteID.Text = noteClass.Id.ToString();
                Exam1TXT.Text = noteClass.Exam1.ToString();
                Exam2TXT.Text = noteClass.Exam2.ToString();
                Performance1TXT.Text = noteClass.Performance1.ToString();
                Performance2TXT.Text = noteClass.Performance2.ToString();

                AverageTXT.Text = noteClass.Average.ToString();
                SituationTXT.Text = noteClass.Situation.ToString();

                selectedStudent = StudentService.StudentsById(noteClass.StudentId);
                SelectedTXT.Text = selectedStudent.ToString();

                CourseCB.SelectedValue = noteClass.CourseId;
            }
        }
    }
}
