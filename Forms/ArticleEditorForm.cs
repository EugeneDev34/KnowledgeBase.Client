using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Client.Services;
using KnowledgeBase.Client.Services.Models;

namespace KnowledgeBase.Client.Forms
{
    public partial class ArticleEditorForm : Form
    {
        private readonly ApiClient _apiClient;
        private readonly Article? _existingArticle;
        private List<Section> _sections = new List<Section>();
        private Button? btnInsertImage; // Делаем nullable

        public ArticleEditorForm(ApiClient apiClient, Article? existingArticle = null)
        {
            _apiClient = apiClient;
            _existingArticle = existingArticle;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

            // Создаем кнопку для вставки изображений
            btnInsertImage = new Button
            {
                Text = "🖼️",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                Size = new Size(40, 24),
                Location = new Point(340, 3)
                // ToolTip удален, так как вызывает ошибку
            };
            btnInsertImage.Click += btnInsertImage_Click;
            panelFormatting.Controls.Add(btnInsertImage);

            // Перемещаем остальные кнопки
            MoveFormattingButtons();

            if (_existingArticle != null)
            {
                this.Text = "Редактирование статьи";
                LoadArticleData(_existingArticle);
            }
            else
            {
                this.Text = "Создание новой статьи";
            }

            txtTitle.Select();
        }

        private void MoveFormattingButtons()
        {
            btnFormatHeader1.Location = new Point(380, 3);
            btnFormatHeader2.Location = new Point(420, 3);
            btnFormatHeader3.Location = new Point(460, 3);
            btnFormatList.Location = new Point(500, 3);
            btnFormatOrderedList.Location = new Point(540, 3);
            btnFormatLink.Location = new Point(580, 3);
            btnFormatCode.Location = new Point(620, 3);
            btnPreview.Location = new Point(710, 3);
        }

        private async void ArticleEditorForm_Load(object sender, EventArgs e)
        {
            await LoadSectionsAsync();
        }

        private async Task LoadSectionsAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Загрузка разделов...";
                _sections = await _apiClient.GetSectionsAsync();
                PopulateSectionsComboBox();
                lblStatus.Text = "Готово";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка загрузки";
                MessageBox.Show($"Ошибка загрузки разделов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void PopulateSectionsComboBox()
        {
            cmbSection.BeginUpdate();
            cmbSection.Items.Clear();

            foreach (var section in _sections)
            {
                cmbSection.Items.Add(new ComboBoxSectionItem(section));
            }

            if (_existingArticle != null)
            {
                var sectionItem = FindSectionItem(_existingArticle.SectionId);
                if (sectionItem != null)
                {
                    cmbSection.SelectedItem = sectionItem;
                }
            }
            else if (cmbSection.Items.Count > 0)
            {
                cmbSection.SelectedIndex = 0;
            }

            cmbSection.EndUpdate();
        }

        private ComboBoxSectionItem? FindSectionItem(int sectionId)
        {
            foreach (ComboBoxSectionItem item in cmbSection.Items)
            {
                if (item.Section.SectionId == sectionId)
                    return item;
            }
            return null;
        }

        private void LoadArticleData(Article article)
        {
            txtTitle.Text = article.Title;
            txtContent.Text = article.Content;
        }

        private int GetSelectedSectionId()
        {
            if (cmbSection.SelectedItem is ComboBoxSectionItem sectionItem)
            {
                return sectionItem.Section.SectionId;
            }
            return _sections.Count > 0 ? _sections[0].SectionId : 0;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите заголовок статьи", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (cmbSection.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите раздел для статьи", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSection.Focus();
                return false;
            }

            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                SetUIState(false);
                lblStatus.Text = "Сохранение...";

                var sectionId = GetSelectedSectionId();
                Article? result;

                if (_existingArticle != null)
                {
                    var updateDto = new UpdateArticleDto
                    {
                        Title = txtTitle.Text.Trim(),
                        Content = txtContent.Text,
                        SectionId = sectionId
                    };

                    result = await _apiClient.UpdateArticleAsync(_existingArticle.ArticleId, updateDto);
                }
                else
                {
                    var createDto = new CreateArticleDto
                    {
                        Title = txtTitle.Text.Trim(),
                        Content = txtContent.Text,
                        SectionId = sectionId
                    };

                    result = await _apiClient.CreateArticleAsync(createDto);
                }

                if (result != null)
                {
                    lblStatus.Text = "Сохранено успешно!";
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblStatus.Text = "Ошибка сохранения";
                    MessageBox.Show("Не удалось сохранить статью", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка";
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetUIState(true);
            }
        }

        private void SetUIState(bool enabled)
        {
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
            txtTitle.Enabled = enabled;
            txtContent.Enabled = enabled;
            cmbSection.Enabled = enabled;
            panelFormatting.Enabled = enabled;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (HasChanges() &&
                MessageBox.Show("Есть несохраненные изменения. Закрыть форму?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool HasChanges()
        {
            if (_existingArticle == null)
            {
                return !string.IsNullOrWhiteSpace(txtTitle.Text) ||
                       !string.IsNullOrWhiteSpace(txtContent.Text);
            }

            return txtTitle.Text != _existingArticle.Title ||
                   txtContent.Text != _existingArticle.Content ||
                   GetSelectedSectionId() != _existingArticle.SectionId;
        }

        // НОВЫЙ МЕТОД: Вставка изображения
        private async void btnInsertImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите изображение для вставки";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Загрузка изображения...";

                        // Показываем диалог для ввода параметров изображения
                        using (var imageDialog = new ImageInsertDialog())
                        {
                            if (imageDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Загружаем изображение на сервер
                                string imageUrl = await _apiClient.UploadImageAsync(openFileDialog.FileName);

                                // Формируем HTML тег изображения
                                string imgTag = $"<img src=\"{imageUrl}\" alt=\"{imageDialog.AltText}\"";

                                if (!string.IsNullOrEmpty(imageDialog.ImageWidth))
                                    imgTag += $" width=\"{imageDialog.ImageWidth}\"";

                                if (!string.IsNullOrEmpty(imageDialog.ImageHeight))
                                    imgTag += $" height=\"{imageDialog.ImageHeight}\"";

                                imgTag += ">";

                                // Вставляем тег в текст
                                InsertFormatting(imgTag, "", "изображение");

                                lblStatus.Text = "Изображение вставлено";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при вставке изображения: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = "Ошибка вставки";
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void InsertFormatting(string openTag, string closeTag, string sampleText = "текст")
        {
            if (txtContent.SelectionLength > 0)
            {
                string selectedText = txtContent.SelectedText;
                txtContent.SelectedText = $"{openTag}{selectedText}{closeTag}";
            }
            else
            {
                int position = txtContent.SelectionStart;
                txtContent.Text = txtContent.Text.Insert(position, $"{openTag}{sampleText}{closeTag}");
                txtContent.SelectionStart = position + openTag.Length;
                txtContent.SelectionLength = sampleText.Length;
            }

            txtContent.Focus();
        }

        private void btnFormatBold_Click(object sender, EventArgs e) => InsertFormatting("<b>", "</b>", "жирный текст");
        private void btnFormatItalic_Click(object sender, EventArgs e) => InsertFormatting("<i>", "</i>", "курсивный текст");
        private void btnFormatUnderline_Click(object sender, EventArgs e) => InsertFormatting("<u>", "</u>", "подчеркнутый текст");
        private void btnFormatHeader1_Click(object sender, EventArgs e) => InsertFormatting("<h1>", "</h1>", "Заголовок 1");
        private void btnFormatHeader2_Click(object sender, EventArgs e) => InsertFormatting("<h2>", "</h2>", "Заголовок 2");
        private void btnFormatHeader3_Click(object sender, EventArgs e) => InsertFormatting("<h3>", "</h3>", "Заголовок 3");

        private void btnFormatList_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                string selectedText = txtContent.SelectedText;
                string[] lines = selectedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string formattedList = "<ul>\r\n";

                foreach (string line in lines)
                {
                    formattedList += $"  <li>{line.Trim()}</li>\r\n";
                }

                formattedList += "</ul>";
                txtContent.SelectedText = formattedList;
            }
            else
            {
                InsertFormatting("<ul>\r\n  <li>", "</li>\r\n</ul>", "элемент списка");
            }

            txtContent.Focus();
        }

        private void btnFormatOrderedList_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                string selectedText = txtContent.SelectedText;
                string[] lines = selectedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string formattedList = "<ol>\r\n";

                foreach (string line in lines)
                {
                    formattedList += $"  <li>{line.Trim()}</li>\r\n";
                }

                formattedList += "</ol>";
                txtContent.SelectedText = formattedList;
            }
            else
            {
                InsertFormatting("<ol>\r\n  <li>", "</li>\r\n</ol>", "элемент списка");
            }

            txtContent.Focus();
        }

        private void btnFormatLink_Click(object sender, EventArgs e)
        {
            string url = Microsoft.VisualBasic.Interaction.InputBox("Введите URL ссылки:", "Вставка ссылки", "https://");

            if (!string.IsNullOrWhiteSpace(url))
            {
                InsertFormatting($"<a href=\"{url}\">", "</a>", "ссылка");
            }
        }

        private void btnFormatCode_Click(object sender, EventArgs e)
        {
            InsertFormatting("<pre><code>", "</code></pre>", "код");
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            string htmlContent = $"<!DOCTYPE html>\n<html>\n<head>\n<meta charset='utf-8'>\n" +
                $"<style>body {{ font-family: 'Segoe UI', Arial, sans-serif; padding: 20px; }}</style>\n</head>\n<body>\n" +
                $"{txtContent.Text}\n</body>\n</html>";

            using (var previewForm = new Form())
            {
                previewForm.Text = "Предпросмотр статьи";
                previewForm.StartPosition = FormStartPosition.CenterParent;
                previewForm.Size = new Size(800, 600);
                previewForm.MinimumSize = new Size(400, 300);

                var webBrowser = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    DocumentText = htmlContent,
                    AllowNavigation = false
                };

                var btnClose = new Button
                {
                    Text = "Закрыть",
                    Dock = DockStyle.Bottom,
                    Height = 30
                };

                btnClose.Click += (s, ev) => previewForm.Close();

                previewForm.Controls.Add(webBrowser);
                previewForm.Controls.Add(btnClose);
                previewForm.ShowDialog();
            }
        }

        // Вспомогательный класс для комбобокса
        private class ComboBoxSectionItem
        {
            public Section Section { get; }

            public ComboBoxSectionItem(Section section)
            {
                Section = section;
            }

            public override string ToString()
            {
                return Section.Name;
            }
        }
    }

    // Диалог для вставки изображения
    public class ImageInsertDialog : Form
    {
        private TextBox? txtAltText;
        private TextBox? txtWidth;
        private TextBox? txtHeight;
        private Button? btnOK;
        private Button? btnCancel;

        public string AltText => txtAltText?.Text ?? "";
        public string ImageWidth => txtWidth?.Text ?? ""; // Изменено имя
        public string ImageHeight => txtHeight?.Text ?? ""; // Изменено имя

        public ImageInsertDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Вставка изображения";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            var lblAltText = new Label { Text = "Альтернативный текст:", Left = 20, Top = 20, Width = 150 };
            txtAltText = new TextBox { Left = 180, Top = 20, Width = 180 };

            var lblWidth = new Label { Text = "Ширина (px, необязательно):", Left = 20, Top = 60, Width = 150 };
            txtWidth = new TextBox { Left = 180, Top = 60, Width = 80 };

            var lblHeight = new Label { Text = "Высота (px, необязательно):", Left = 20, Top = 100, Width = 150 };
            txtHeight = new TextBox { Left = 180, Top = 100, Width = 80 };

            var lblNote = new Label
            {
                Text = "Примечание: оставьте поля ширины и высоты пустыми\nдля сохранения оригинального размера изображения",
                Left = 20,
                Top = 140,
                Width = 340,
                Height = 40,
                Font = new Font("Segoe UI", 8F, FontStyle.Italic, GraphicsUnit.Point)
            };

            btnOK = new Button { Text = "OK", Left = 200, Top = 180, Width = 80 };
            btnCancel = new Button { Text = "Отмена", Left = 290, Top = 180, Width = 80 };

            btnOK.Click += (s, e) => { if (ValidateInput()) DialogResult = DialogResult.OK; };
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] {
                lblAltText, txtAltText,
                lblWidth, txtWidth,
                lblHeight, txtHeight,
                lblNote,
                btnOK, btnCancel
            });

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtAltText?.Text))
            {
                MessageBox.Show("Введите альтернативный текст для изображения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAltText?.Focus();
                return false;
            }

            return true;
        }
    }
}