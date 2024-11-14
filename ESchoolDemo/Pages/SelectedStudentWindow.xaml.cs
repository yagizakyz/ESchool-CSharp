using ESchoolDemo.Models;
using ESchoolDemo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace ESchoolDemo.Pages
{
    /// <summary>
    /// Interaction logic for SelectedStudentWindow.xaml
    /// </summary>
    public partial class SelectedStudentWindow : Window
    {
        StudentService studentS;
        public SelectedStudentWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void NameTXT_TextChanged(object sender, TextChangedEventArgs e)
        {
            StudentList();
        }

        private void SurnameTXT_TextChanged(object sender, TextChangedEventArgs e)
        {
            StudentList();
        }

        private void SchoolNumberTXT_TextChanged(object sender, TextChangedEventArgs e)
        {
            StudentList();
        }

        private void StudentList()
        {
            int? schoolNumber = (Int32.TryParse(SchoolNumberTXT.Text, out int schoolNo) ? schoolNo : (int?)null);

            List<StudentClass> students = null;
            if(schoolNumber != null)
                students = StudentService.StudentsBySchoolNumber(schoolNumber);
            else
                students = StudentService.StudentByNameSurname(NameTXT.Text.Trim(), SurnameTXT.Text.Trim());

            StudentListBox.ItemsSource = students;
        }
    }
}
