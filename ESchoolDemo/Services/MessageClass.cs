using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ESchoolDemo.Services
{
    public class MessageClass
    {
        public static MessageBoxResult ShowErrorMsg(string description, string header)
        {
            return MessageBox.Show(description, header, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowQuestionMsg(string description, string header)
        {
            return MessageBox.Show(description, header, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }
        public static MessageBoxResult ShowInfoMsg(string description, string header)
        {
            return MessageBox.Show(description, header, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
