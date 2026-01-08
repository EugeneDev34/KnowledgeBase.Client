using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Client.Services;
using KnowledgeBase.Client.Services.Models;

namespace KnowledgeBase.Client.Forms
{
    public partial class LoginForm : Form
    {
        private readonly ApiClient _apiClient;

        public string? AuthToken { get; private set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(AuthToken);

        public LoginForm(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;

            txtLogin.Text = "admin";
            txtPassword.Text = "admin123";

            txtLogin.SelectAll();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await LoginAsync();
        }

        private async Task LoginAsync()
        {
            if (!ValidateInput())
                return;

            try
            {
                SetUIState(false);
                lblStatus.Text = "Выполняется вход...";
                lblStatus.ForeColor = Color.Blue;

                var token = await _apiClient.LoginAsync(txtLogin.Text.Trim(), txtPassword.Text);

                if (token != null)
                {
                    AuthToken = token;
                    _apiClient.SetToken(token);

                    lblStatus.Text = "Успешный вход!";
                    lblStatus.ForeColor = Color.Green;

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    ShowError("Неверный логин или пароль");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка подключения: {ex.Message}");
            }
            finally
            {
                SetUIState(true);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void SetUIState(bool enabled)
        {
            btnLogin.Enabled = enabled;
            txtLogin.Enabled = enabled;
            txtPassword.Enabled = enabled;
            btnCancel.Enabled = enabled;
        }

        private void ShowError(string message)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = Color.Red;

            MessageBox.Show(message, "Ошибка авторизации",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnLogin_Click(sender, e);
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Функция регистрации будет реализована в следующей версии.\n" +
                          "Для тестирования используйте логин: admin, пароль: admin123",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtLogin.Select();
            txtLogin.SelectAll();
        }
    }
}