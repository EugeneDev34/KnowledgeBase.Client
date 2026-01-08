using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using KnowledgeBase.Client.Forms;
using KnowledgeBase.Client.Services;

namespace KnowledgeBase.Client
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try
            {
                using var apiClient = new ApiClient();

                using var loginForm = new LoginForm(apiClient);

                if (loginForm.ShowDialog() == DialogResult.OK && loginForm.IsAuthenticated)
                {
                    Application.Run(new MainForm(apiClient));
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("Критическая ошибка при запуске", ex);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowErrorDialog("Необработанное исключение", e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                ShowErrorDialog("Критическая ошибка", ex);
            }
        }

        private static void ShowErrorDialog(string title, Exception ex)
        {
            string errorMessage = $"{title}:\n\n{ex.Message}\n\n" +
                                $"Тип: {ex.GetType().Name}\n" +
                                $"StackTrace:\n{ex.StackTrace}";

            MessageBox.Show(errorMessage, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            LogErrorToFile(ex);
        }

        private static void LogErrorToFile(Exception ex)
        {
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.GetType().Name}: {ex.Message}\n" +
                                   $"StackTrace: {ex.StackTrace}\n" +
                                   new string('-', 80) + "\n";

                File.AppendAllText(logPath, logMessage);
            }
            catch
            {
                // Игнорируем ошибки записи в лог
            }
        }
    }
}