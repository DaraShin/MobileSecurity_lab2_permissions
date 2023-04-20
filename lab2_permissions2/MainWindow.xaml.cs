using System.Diagnostics;
using System;
using System.Windows;
using System.Security.Principal;

namespace lab2_permissions2
{
    public partial class MainWindow : Window
    {
        private static readonly string zipFileProcess = "ZipProcess.exe";
        private static readonly string hashCodeProcess = "HashCodeProcess.exe";
        private static readonly string convertToPngProcess = "ConvertToPngProcess.exe";

        public MainWindow()
        {
            //Console.WriteLine(IsAdministrator());
            InitializeComponent();
            if (IsAdministrator())
            {
                MessageBox.Show("Нельзя запустить приложение с правами администартора");
                Application.Current.Shutdown();
            }
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void OnPerformActionBtnClick(object sender, RoutedEventArgs e)
        {
            string file = fileTextBox.Text;
            errorLabel.Visibility = Visibility.Collapsed;
            
            if (file.Equals(""))
            {
                errorLabel.Content = "Введите путь к файлу";
                errorLabel.Visibility = Visibility.Visible;
                return;
            }

            if (zipRadioBtn.IsChecked == true)
            {
                PerformAction(zipFileProcess, file);
            }
            else if (hashRadioBtn.IsChecked == true)
            {
                PerformAction(hashCodeProcess, file);
            }
            else if (pngRadioBtn.IsChecked == true)
            {
                PerformAction(convertToPngProcess, file);
            }
        }

        private void PerformAction(string processFileName, string filePath)
        {
            filePath = "\"" + filePath + "\"";

            int exitCode = StartProcess(processFileName, filePath);
            if (exitCode == -2)
            {
                exitCode = StartProcess(processFileName, filePath, true);
                if (exitCode == -2)
                {
                    errorLabel.Content = "Нет доступа";
                    errorLabel.Visibility = Visibility.Visible;
                }
            }

            if (exitCode == -1)
            {
                errorLabel.Content = "Ошибка выполнения операции";
                errorLabel.Visibility = Visibility.Visible;
            }
            else if(exitCode == -3)
            {
                errorLabel.Content = "Выберите файл изображения";
                errorLabel.Visibility = Visibility.Visible;
            }
        }

        private int StartProcess(string processFileName, string arguments, bool adminPermissions = false)
        {
            Process process = new Process();
            process.StartInfo.FileName = processFileName;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.Verb = adminPermissions ? "runas" : "open";
            process.StartInfo.Arguments = arguments;

            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

    }
}
