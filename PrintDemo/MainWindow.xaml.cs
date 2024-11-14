using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel stackPanel;
        public MainWindow()
        {
            InitializeComponent();

            PageContent pageContent = new PageContent();
            fixedDocument1.Pages.Add(pageContent);

            FixedPage fixedPage = new FixedPage();
            fixedPage.Width = ConvertCmtoPixel(21);
            fixedPage.Height = ConvertCmtoPixel(29.7);

            pageContent.Child = fixedPage;

            stackPanel = new StackPanel();
            stackPanel.Width = ConvertCmtoPixel(19);
            stackPanel.Height = ConvertCmtoPixel(27.7);
            //stackPanel.Background = Brushes.Red;

            FixedPage.SetLeft(stackPanel, ConvertCmtoPixel(1));
            FixedPage.SetRight(stackPanel, ConvertCmtoPixel(1));

            fixedPage.Children.Add(stackPanel);

            stackPanel.Children.Add(Content("ABC High School"));
            stackPanel.Children.Add(Content("End of Term Report Card"));

            stackPanel.Children.Add(HorizontalStackedPanel("Şenol Sun", "1967"));
            stackPanel.Children.Add(CourseScoreDataGrid());
            stackPanel.Children.Add(AverageInfoPanel(85.25));
        }

        double ConvertCmtoPixel(double xCm)
        {
            return (1 / 2.54) * 96 * xCm;
        }

        TextBlock Content(string text)
        {
            TextBlock textBlock1 = new TextBlock();
            textBlock1.FontSize = 20;
            textBlock1.FontWeight = FontWeights.Bold;
            textBlock1.FontStretch = FontStretches.Medium;
            textBlock1.Text = text;
            textBlock1.Margin = new Thickness(0, 0, 0, 5);
            textBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            return textBlock1;
        }

        StackPanel HorizontalStackedPanel(string nameSurname, string schoolNo)
        {
            StackPanel stackPanel1 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 20, 0, 10)
            };

            stackPanel1.Children.Add(new TextBlock()
            {
                Text = "Name Surname : ",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 0, 0, 0)

            });

            stackPanel1.Children.Add(new TextBlock()
            {
                Text = nameSurname,
                Margin = new Thickness(2, 0, 25, 0)
            });

            stackPanel1.Children.Add(new TextBlock()
            {
                Text = "School Number : ",
                FontWeight = FontWeights.Bold,
            });
            stackPanel1.Children.Add(new TextBlock()
            {
                Text = schoolNo,
            });

            stackPanel1.Children.Add(new TextBlock()
            {
                Text = "Date : ",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(250, 0, 0, 0),
            });
            stackPanel1.Children.Add(new TextBlock()
            {
                Text = DateTime.Now.ToShortDateString(),
                Margin = new Thickness(2, 0, 25, 0)
            });

            stackPanel1.Children.Add(new TextBlock()
            {
                Text = "Time : ",
                FontWeight = FontWeights.Bold,


            });
            stackPanel1.Children.Add(new TextBlock()
            {
                Text = DateTime.Now.ToShortTimeString(),

            });
            return stackPanel1;
        }

        DataGrid CourseScoreDataGrid()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.IsReadOnly = true;
            dataGrid.Foreground = Brushes.Black;
            dataGrid.AutoGenerateColumns = false;
            dataGrid.Background = Brushes.Transparent;
            dataGrid.Margin = new Thickness(0, 20, 0, 0);
            dataGrid.IsHitTestVisible = false;

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("CourseName"),
                Header = "Course Name"
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("ClassHour"),
                Header = "Class Hour",
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("Exam1"),
                Header = "1. Exam"
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("Exam1"),
                Header = "2. Exam"
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("Performance1"),
                Header = "1. Perf."
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("Performance1"),
                Header = "2. Perf."
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("Average"),
                Header = "Average"
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Binding = new Binding("Situation"),
                Header = "Situation"
            });

            return dataGrid;
        }

        StackPanel AverageInfoPanel(double? average)
        {
            StackPanel stackPanel1 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 10, 115, 0)
            };

            stackPanel1.Children.Add(new TextBlock()
            {
                Text = "overall Average: ",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 0)

            });
            stackPanel1.Children.Add(new TextBlock()
            {
                Text = $"{average:##.##}",
                Margin = new Thickness(2, 0, 25, 0)
            });
            return stackPanel1;
        }
    }
}