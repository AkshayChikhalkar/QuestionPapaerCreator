using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ToTextFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        StringBuilder sb_data = new();
        StringBuilder sb_data_lower = new();
        int Qcount = 0;

        private int QNo = 0;



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Qcount>40)
                {
                    MessageBoxResult result =  MessageBox.Show("40 Questions are already added, Do you still want to add more?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (result == MessageBoxResult.No)
                        return;

                }

                if (Validation())
                {
                    sb_data.AppendLine(txtQuestion.Text.ToUpper().Trim('?',' '));

                    sb_data.AppendLine("A. " + Op1.Text.ToUpper().Trim());
                    sb_data.AppendLine("B. " + Op2.Text.ToUpper().Trim());
                    sb_data.AppendLine("C. " + Op3.Text.ToUpper().Trim());
                    sb_data.AppendLine("D. " + Op4.Text.ToUpper().Trim());

                    sb_data.AppendLine("ANSWER: " + Ans.Text.ToUpper() + Environment.NewLine);

                    sb_data_lower.AppendLine(++Qcount +". "+ txtQuestion.Text);

                    sb_data_lower.AppendLine("A. " + Op1.Text.Trim());
                    sb_data_lower.AppendLine("B. " + Op2.Text.Trim());
                    sb_data_lower.AppendLine("C. " + Op3.Text.Trim());
                    sb_data_lower.AppendLine("D. " + Op4.Text.Trim() + Environment.NewLine);

                    //sb_data_lower.AppendLine("ANSWER: " + Op4.Text + Environment.NewLine);
                    lblQcount.Text = "Q. No. " + Qcount; 
                    MessageBox.Show("Question Added Successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    Ans.Text=txtQuestion.Text=Op1.Text = Op2.Text = Op3.Text = Op4.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        bool Validation()
        {
            if (string.IsNullOrEmpty(txtQuestion.Text))
            {
                MessageBox.Show("Question cannot be Empty!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(Op1.Text))
            {
                MessageBox.Show("Option 1 cannot be Empty!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Op2.Text))
            {
                MessageBox.Show("Option 2 cannot be Empty!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Op3.Text))
            {
                MessageBox.Show("Option 3 cannot be Empty!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Op4.Text))
            {
                MessageBox.Show("Option 4 cannot be Empty!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(Ans.Text))
            {
                MessageBox.Show("Answer cannot be Empty!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(SaveFile())
                MessageBox.Show("File Saved Successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(sb_data.ToString()))
            {
                MessageBoxResult result = MessageBox.Show("File is not Saved\nDo you want to Save the file?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if(SaveFile())
                        MessageBox.Show("File Saved Successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);                    
                }
                if (result==MessageBoxResult.No)
                {
                    this.Close();
                }
                if (result==MessageBoxResult.Cancel)
                {
                    return;
                }

            }
        }

        readonly SaveFileDialog saveFileDialog = new();
        bool SaveFile()
        {
            try
            {
                if (string.IsNullOrEmpty(sb_data.ToString()))
                {
                    MessageBox.Show("File cannot be saved due to empty data!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                saveFileDialog.ShowDialog();


                string fileName = saveFileDialog.FileName + ".txt";
                string fileName_u = saveFileDialog.FileName + "_Upload.txt";


                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();
                    File.Create(fileName_u).Dispose();

                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        tw.WriteLine(sb_data_lower);
                    }

                    using (TextWriter tw = new StreamWriter(fileName_u))
                    {
                        tw.WriteLine(sb_data);
                    }

                }
                else if (File.Exists(fileName))
                {
                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        tw.WriteLine(sb_data_lower);
                    }

                    using (TextWriter tw = new StreamWriter(fileName_u))
                    {
                        tw.WriteLine(sb_data);
                    }
                }
                sb_data.Clear();
                sb_data_lower.Clear();
                Qcount = 0;
                Ans.Text = txtQuestion.Text = Op1.Text = Op2.Text = Op3.Text = Op4.Text = string.Empty;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
