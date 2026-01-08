using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Client.Services;
using KnowledgeBase.Client.Services.Models;

namespace KnowledgeBase.Client.Forms
{
    public partial class SearchForm : Form
    {
        private readonly ApiClient _apiClient;
        private List<Section> _sections = new List<Section>();
        private List<Article> _searchResults = new List<Article>();

        public SearchForm(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = btnSearch;

            dataGridViewResults.AutoGenerateColumns = false;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.MultiSelect = false;
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AllowUserToDeleteRows = false;
            dataGridViewResults.RowHeadersVisible = false;

            // Настройка столбцов
            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "ArticleId",
                Width = 50,
                ReadOnly = true
            });

            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTitle",
                HeaderText = "Заголовок",
                DataPropertyName = "Title",
                Width = 250,
                ReadOnly = true
            });

            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAuthor",
                HeaderText = "Автор",
                DataPropertyName = "AuthorName",
                Width = 150,
                ReadOnly = true
            });

            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSection",
                HeaderText = "Раздел",
                DataPropertyName = "SectionName",
                Width = 150,
                ReadOnly = true
            });

            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDate",
                HeaderText = "Дата создания",
                DataPropertyName = "CreatedDate",
                Width = 150,
                ReadOnly = true
            });
        }

        private async void SearchForm_Load(object sender, EventArgs e)
        {
            await LoadSectionsAsync();
        }

        private async Task LoadSectionsAsync()
        {
            try
            {
                _sections = await _apiClient.GetSectionsAsync();
                PopulateSectionsComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки разделов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateSectionsComboBox()
        {
            cmbSection.BeginUpdate();
            cmbSection.Items.Clear();
            cmbSection.Items.Add(new ComboBoxSectionItem(null, "-- Все разделы --"));

            foreach (var section in _sections)
            {
                cmbSection.Items.Add(new ComboBoxSectionItem(section, section.Name));
            }

            cmbSection.SelectedIndex = 0;
            cmbSection.EndUpdate();
        }

        private int? GetSelectedSectionId()
        {
            if (cmbSection.SelectedIndex > 0 && cmbSection.SelectedItem is ComboBoxSectionItem item)
            {
                return item.Section?.SectionId;
            }
            return null;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await PerformSearchAsync();
        }

        private async Task PerformSearchAsync()
        {
            var searchText = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Введите текст для поиска", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Поиск...";

                // Получаем все статьи
                var allArticles = await _apiClient.SearchArticlesAsync(searchText);

                // Фильтруем по разделу, если выбран
                var sectionId = GetSelectedSectionId();
                if (sectionId.HasValue)
                {
                    _searchResults = allArticles.FindAll(a => a.SectionId == sectionId.Value);
                }
                else
                {
                    _searchResults = allArticles;
                }

                DisplayResults();
                lblStatus.Text = $"Найдено: {_searchResults.Count} статей";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка поиска";
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DisplayResults()
        {
            dataGridViewResults.DataSource = null;
            dataGridViewResults.DataSource = _searchResults;

            // Форматируем даты
            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                if (row.DataBoundItem is Article article)
                {
                    row.Cells["colDate"].Value = article.CreatedDate.ToString("dd.MM.yyyy HH:mm");
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnSearch_Click(sender, e);
            }
        }

        private void dataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _searchResults.Count)
            {
                var article = _searchResults[e.RowIndex];
                OpenArticlePreview(article);
            }
        }

        private void OpenArticlePreview(Article article)
        {
            using (var previewForm = new ArticlePreviewForm(article))
            {
                previewForm.ShowDialog();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dataGridViewResults.DataSource = null;
            _searchResults.Clear();
            lblStatus.Text = "Готово к поиску";
            txtSearch.Focus();
        }

        // Вспомогательный класс для комбобокса
        private class ComboBoxSectionItem
        {
            public Section? Section { get; }
            public string DisplayText { get; }

            public ComboBoxSectionItem(Section? section, string displayText)
            {
                Section = section;
                DisplayText = displayText;
            }

            public override string ToString() => DisplayText;
        }

        // Форма предпросмотра статьи
        private class ArticlePreviewForm : Form
        {
            public ArticlePreviewForm(Article article)
            {
                InitializeComponent(article);
            }

            private void InitializeComponent(Article article)
            {
                this.Text = $"Просмотр: {article.Title}";
                this.Size = new Size(800, 600);
                this.StartPosition = FormStartPosition.CenterParent;
                this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

                var webBrowser = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    DocumentText = GetHtmlContent(article),
                    AllowNavigation = false
                };

                var panelInfo = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 60,
                    BackColor = Color.FromArgb(248, 249, 250)
                };

                var lblInfo = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = $"Автор: {article.AuthorName} | Раздел: {article.SectionName} | " +
                           $"Создана: {article.CreatedDate:dd.MM.yyyy HH:mm} | " +
                           $"Обновлена: {article.UpdatedDate:dd.MM.yyyy HH:mm}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point)
                };

                var btnClose = new Button
                {
                    Text = "Закрыть",
                    Dock = DockStyle.Bottom,
                    Height = 30,
                    BackColor = Color.FromArgb(0, 123, 255),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnClose.Click += (s, e) => this.Close();

                panelInfo.Controls.Add(lblInfo);

                this.Controls.Add(webBrowser);
                this.Controls.Add(panelInfo);
                this.Controls.Add(btnClose);
            }

            private string GetHtmlContent(Article article)
            {
                return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ 
            font-family: 'Segoe UI', Arial, sans-serif; 
            padding: 30px;
            line-height: 1.6;
            color: #333;
        }}
        h1 {{ 
            color: #2c3e50; 
            border-bottom: 2px solid #eee;
            padding-bottom: 15px;
            margin-bottom: 20px;
        }}
        h2 {{ color: #34495e; margin-top: 25px; }}
        h3 {{ color: #7f8c8d; }}
        p {{ margin: 15px 0; }}
        img {{ max-width: 100%; height: auto; }}
        pre {{ 
            background: #f8f9fa; 
            padding: 15px;
            border-radius: 5px;
            border-left: 4px solid #007bff;
            overflow-x: auto;
        }}
        code {{ 
            background: #f8f9fa; 
            padding: 2px 6px;
            border-radius: 3px;
        }}
    </style>
</head>
<body>
    {article.Content}
</body>
</html>";
            }
        }
    }
}