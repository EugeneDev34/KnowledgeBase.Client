using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Client.Services;
using KnowledgeBase.Client.Services.Models;

namespace KnowledgeBase.Client.Forms
{
    public partial class MainForm : Form
    {
        private readonly ApiClient _apiClient;
        private List<Section> _sections = new List<Section>();
        private List<Article> _articles = new List<Article>();
        private Article? _currentArticle;

        public MainForm(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Настройка иконок для TreeView
            ImageList imageList = new ImageList();
            imageList.Images.Add("folder", CreateFolderIcon(Color.SteelBlue));
            imageList.Images.Add("folder_open", CreateFolderIcon(Color.DodgerBlue));
            imageList.Images.Add("document", CreateDocumentIcon(Color.Gray));

            treeViewSections.ImageList = imageList;
            listViewArticles.SmallImageList = imageList;

            // Настройка ListView
            listViewArticles.FullRowSelect = true;
            listViewArticles.View = View.Details;
            listViewArticles.MultiSelect = false;

            // Добавляем колонки в ListView
            listViewArticles.Columns.Add("Заголовок", 300);
            listViewArticles.Columns.Add("Автор", 150);
            listViewArticles.Columns.Add("Дата", 150);
            listViewArticles.Columns.Add("Раздел", 200);

            // Настройка WebBrowser
            webBrowser.AllowNavigation = false;
            webBrowser.AllowWebBrowserDrop = false;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.WebBrowserShortcutsEnabled = false;
            // Включаем отображение изображений
            webBrowser.AllowWebBrowserDrop = true;
            webBrowser.IsWebBrowserContextMenuEnabled = true;

            // Настройка статус бара
            statusLabel.Text = "Готово";
            lblArticleCount.Text = "Статей: 0";
        }

        private Image CreateFolderIcon(Color color)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, 2, 4, 12, 10);
                    g.FillPolygon(brush, new Point[] { new Point(2, 4), new Point(8, 0), new Point(14, 4) });
                }
            }
            return bmp;
        }

        private Image CreateDocumentIcon(Color color)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, 2, 2, 12, 12);
                    g.FillRectangle(SystemBrushes.Window, 4, 4, 8, 8);
                }
            }
            return bmp;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Загрузка данных...";

                // Параллельная загрузка разделов и статей
                await Task.WhenAll(LoadSectionsAsync(), LoadArticlesAsync());

                // Автоматически выбираем первый раздел, если ничего не выбрано
                if (treeViewSections.Nodes.Count > 0 && treeViewSections.SelectedNode == null)
                {
                    treeViewSections.SelectedNode = treeViewSections.Nodes[0];
                }

                statusLabel.Text = $"Готово. Статей: {_articles.Count}";
                lblArticleCount.Text = $"Статей: {_articles.Count}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки данных: {ex.Message}");
                ShowError($"Ошибка загрузки данных: {ex.Message}");
                statusLabel.Text = "Ошибка загрузки";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task LoadSectionsAsync()
        {
            try
            {
                _sections = await _apiClient.GetSectionsTreeAsync();
                BuildTreeView();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка загрузки разделов: {ex.Message}", ex);
            }
        }

        private async Task LoadArticlesAsync()
        {
            try
            {
                Console.WriteLine("Начало загрузки статей...");

                _articles = await _apiClient.GetArticlesAsync();

                // Логирование для отладки
                Console.WriteLine($"Загружено статей: {_articles.Count}");
                if (_articles.Count > 0)
                {
                    Console.WriteLine($"Первая статья: {_articles[0].Title}");
                    Console.WriteLine($"Последняя статья: {_articles[_articles.Count - 1].Title}");
                }

                UpdateArticlesList();

                Console.WriteLine("Список статей обновлен в UI");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки статей: {ex.Message}");
                throw new Exception($"Ошибка загрузки статей: {ex.Message}", ex);
            }
        }

        private void BuildTreeView()
        {
            treeViewSections.BeginUpdate();
            treeViewSections.Nodes.Clear();

            void BuildTreeNodes(int? parentId, TreeNodeCollection nodes)
            {
                var childSections = _sections.FindAll(s => s.ParentSectionId == parentId);
                foreach (var section in childSections)
                {
                    var node = new TreeNode(section.Name)
                    {
                        Tag = section,
                        ImageKey = "folder",
                        SelectedImageKey = "folder_open"
                    };
                    nodes.Add(node);
                    BuildTreeNodes(section.SectionId, node.Nodes);
                }
            }

            BuildTreeNodes(null, treeViewSections.Nodes);

            if (treeViewSections.Nodes.Count > 0)
            {
                treeViewSections.ExpandAll();
                treeViewSections.SelectedNode = treeViewSections.Nodes[0];
            }

            treeViewSections.EndUpdate();
        }

        private void UpdateArticlesList()
        {
            listViewArticles.BeginUpdate();
            listViewArticles.Items.Clear();

            foreach (var article in _articles)
            {
                var item = new ListViewItem(article.Title)
                {
                    Tag = article,
                    ImageKey = "document"
                };
                item.SubItems.Add(article.AuthorName);
                item.SubItems.Add(article.CreatedDate.ToString("dd.MM.yyyy HH:mm"));
                item.SubItems.Add(article.SectionName);

                listViewArticles.Items.Add(item);
            }

            listViewArticles.EndUpdate();
            lblArticleCount.Text = $"Статей: {_articles.Count}";

            // Сбрасываем текущую статью при обновлении списка
            _currentArticle = null;
            ClearArticleDisplay();
            UpdateButtonStates();
        }

        private async void treeViewSections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Section section)
            {
                await LoadArticlesBySectionAsync(section.SectionId);
            }
        }

        private async Task LoadArticlesBySectionAsync(int sectionId)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Загрузка статей раздела...";
                _articles = await _apiClient.GetArticlesBySectionAsync(sectionId);
                UpdateArticlesList();
                statusLabel.Text = $"Загружено статей: {_articles.Count}";
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка загрузки статей: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void listViewArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewArticles.SelectedItems.Count > 0 &&
                listViewArticles.SelectedItems[0].Tag is Article article)
            {
                _currentArticle = article;
                DisplayArticle(article);
            }
            else
            {
                _currentArticle = null;
                ClearArticleDisplay();
            }
            UpdateButtonStates();
        }

        private void listViewArticles_DoubleClick(object sender, EventArgs e)
        {
            if (_currentArticle != null)
            {
                OpenArticleEditor(_currentArticle);
            }
        }

        private void DisplayArticle(Article article)
        {
            txtTitle.Text = article.Title;
            // Генерируем полный HTML контент с поддержкой изображений
            string htmlContent = GenerateHtmlContent(article);
            webBrowser.DocumentText = htmlContent;
            lblArticleInfo.Text = $"Автор: {article.AuthorName} | " +
                                 $"Раздел: {article.SectionName} | " +
                                 $"Создана: {article.CreatedDate:dd.MM.yyyy HH:mm} | " +
                                 $"Обновлена: {article.UpdatedDate:dd.MM.yyyy HH:mm}";
        }

        // Генерация HTML контента с поддержкой изображений
        private string GenerateHtmlContent(Article article)
        {
            // Обрабатываем изображения в контенте
            string processedContent = ProcessImageTags(article.Content);

            // Используем упрощенный HTML чтобы избежать ошибок парсинга
            string htmlTemplate = "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "<meta charset='utf-8'>" +
                "<style>" +
                "body { font-family: 'Segoe UI', Arial, sans-serif; padding: 30px; line-height: 1.6; color: #333; max-width: 1200px; margin: 0 auto; }" +
                "h1 { color: #2c3e50; border-bottom: 2px solid #eee; padding-bottom: 15px; margin-bottom: 25px; }" +
                "h2 { color: #34495e; margin-top: 30px; margin-bottom: 15px; }" +
                "h3 { color: #7f8c8d; margin-top: 25px; margin-bottom: 10px; }" +
                "p { margin: 15px 0; text-align: justify; }" +
                "ul, ol { margin: 15px 0 15px 25px; }" +
                "li { margin: 8px 0; }" +
                "img { max-width: 100%; height: auto; border: 1px solid #ddd; border-radius: 8px; padding: 8px; margin: 20px auto; display: block; box-shadow: 0 4px 8px rgba(0,0,0,0.1); background-color: #f8f9fa; transition: transform 0.3s ease; }" +
                "img:hover { transform: scale(1.02); box-shadow: 0 6px 12px rgba(0,0,0,0.15); }" +
                "pre { background: #f8f9fa; padding: 20px; border-radius: 8px; border-left: 5px solid #007bff; overflow-x: auto; margin: 20px 0; font-family: 'Consolas', 'Monaco', monospace; font-size: 14px; line-height: 1.5; }" +
                "code { background: #f8f9fa; padding: 3px 8px; border-radius: 4px; font-family: 'Consolas', 'Monaco', monospace; font-size: 14px; color: #e83e8c; }" +
                "blockquote { border-left: 4px solid #ddd; margin: 20px 0; padding-left: 20px; color: #666; font-style: italic; background-color: #f9f9f9; padding: 15px 20px; border-radius: 0 8px 8px 0; }" +
                "table { border-collapse: collapse; width: 100%; margin: 25px 0; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }" +
                "th, td { border: 1px solid #dee2e6; padding: 12px 15px; text-align: left; }" +
                "th { background-color: #f2f2f2; font-weight: 600; }" +
                "tr:nth-child(even) { background-color: #f8f9fa; }" +
                ".image-caption { text-align: center; font-style: italic; color: #666; margin-top: -15px; margin-bottom: 25px; font-size: 14px; }" +
                ".article-header { background: linear-gradient(135deg, #6a11cb 0%, #2575fc 100%); color: white; padding: 30px; border-radius: 10px; margin-bottom: 30px; }" +
                ".article-title { font-size: 2.5em; margin-bottom: 10px; font-weight: 700; }" +
                ".article-meta { font-size: 0.9em; opacity: 0.9; margin-top: 10px; }" +
                ".content-wrapper { background: white; padding: 40px; border-radius: 10px; box-shadow: 0 5px 15px rgba(0,0,0,0.05); }" +
                "@media (max-width: 768px) { body { padding: 15px; } .content-wrapper { padding: 20px; } .article-title { font-size: 1.8em; } }" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div class='article-header'>" +
                $"<h1 class='article-title'>{article.Title}</h1>" +
                "<div class='article-meta'>" +
                $"Автор: {article.AuthorName} | " +
                $"Раздел: {article.SectionName} | " +
                $"Создана: {article.CreatedDate:dd.MM.yyyy HH:mm} | " +
                $"Обновлена: {article.UpdatedDate:dd.MM.yyyy HH:mm}" +
                "</div>" +
                "</div>" +
                "<div class='content-wrapper'>" +
                $"{processedContent}" +
                "</div>" +
                "<script>" +
                "document.addEventListener('DOMContentLoaded', function() {" +
                "var images = document.querySelectorAll('img');" +
                "images.forEach(function(img) {" +
                "img.addEventListener('click', function() {" +
                "this.classList.toggle('zoomed');" +
                "});" +
                "img.addEventListener('error', function() {" +
                "this.src = 'data:image/svg+xml;utf8,<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"400\" height=\"300\" viewBox=\"0 0 400 300\"><rect width=\"400\" height=\"300\" fill=\"#f8f9fa\"/><text x=\"200\" y=\"150\" font-family=\"Arial\" font-size=\"16\" text-anchor=\"middle\" fill=\"#666\">Изображение не загружено</text></svg>';" +
                "this.alt = 'Изображение не загружено';" +
                "});" +
                "});" +
                "});" +
                "</script>" +
                "</body>" +
                "</html>";

            return htmlTemplate;
        }

        // Обработка тегов изображений в контенте
        private string ProcessImageTags(string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;

            // Простая обработка - можно добавить более сложную логику при необходимости
            // Например, добавление подписей к изображениям, ленивую загрузку и т.д.
            return content;
        }

        private void ClearArticleDisplay()
        {
            txtTitle.Text = "Выберите статью для просмотра";
            string noSelectionHtml = "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "<meta charset='utf-8'>" +
                "<style>" +
                "body { font-family: 'Segoe UI', Arial, sans-serif; color: #666; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); }" +
                ".container { text-align: center; background: white; padding: 50px; border-radius: 15px; box-shadow: 0 10px 30px rgba(0,0,0,0.2); max-width: 600px; }" +
                "h3 { color: #333; margin-bottom: 20px; font-size: 24px; }" +
                "p { color: #666; line-height: 1.6; margin-bottom: 30px; }" +
                ".icon { font-size: 48px; margin-bottom: 20px; color: #764ba2; }" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div class='container'>" +
                "<div class='icon'>📚</div>" +
                "<h3>Выберите статью из списка</h3>" +
                "<p>Для просмотра содержимого выберите статью в списке выше.<br>" +
                "Вы можете создавать новые статьи, редактировать существующие<br>" +
                "и вставлять изображения в содержимое статей.</p>" +
                "</div>" +
                "</body>" +
                "</html>";

            webBrowser.DocumentText = noSelectionHtml;
            lblArticleInfo.Text = string.Empty;
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = _currentArticle != null;
            btnEditArticle.Enabled = hasSelection;
            btnDeleteArticle.Enabled = hasSelection;
            btnImages.Enabled = hasSelection;

            // Удалите эти строки, если у вас нет таких элементов меню:
            // editArticleToolStripMenuItem.Enabled = hasSelection;
            // deleteArticleToolStripMenuItem.Enabled = hasSelection;
        }
        private async Task RefreshArticlesWithNotification()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Сохраняем текущее состояние
                int? selectedSectionId = null;
                if (treeViewSections.SelectedNode?.Tag is Section section)
                {
                    selectedSectionId = section.SectionId;
                    statusLabel.Text = $"Обновление раздела: {section.Name}";
                }
                else
                {
                    statusLabel.Text = "Обновление всех статей...";
                }

                // Загружаем разделы заново (на случай, если были изменения)
                await LoadSectionsAsync();

                // Восстанавливаем выбор раздела
                if (selectedSectionId.HasValue)
                {
                    var nodeToSelect = FindTreeNodeBySectionId(treeViewSections.Nodes, selectedSectionId.Value);
                    if (nodeToSelect != null)
                    {
                        treeViewSections.SelectedNode = nodeToSelect;
                        await LoadArticlesBySectionAsync(selectedSectionId.Value);
                    }
                }
                else
                {
                    await LoadArticlesAsync();
                }

                statusLabel.Text = $"Обновлено. Статей: {_articles.Count}";

                // Показываем краткое уведомление (можно использовать StatusStrip или Label)
                ShowTemporaryStatus("Список статей обновлен", 2000);
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка обновления: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // Вспомогательный метод для поиска узла TreeView по ID раздела
        private TreeNode FindTreeNodeBySectionId(TreeNodeCollection nodes, int sectionId)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is Section section && section.SectionId == sectionId)
                    return node;

                var foundInChildren = FindTreeNodeBySectionId(node.Nodes, sectionId);
                if (foundInChildren != null)
                    return foundInChildren;
            }
            return null;
        }

        // Метод для временного сообщения в статус-баре
        private async void ShowTemporaryStatus(string message, int durationMs)
        {
            var originalText = statusLabel.Text;
            statusLabel.Text = message;

            await Task.Delay(durationMs);

            if (statusLabel.Text == message) // Если текст не изменился другим процессом
            {
                statusLabel.Text = originalText;
            }
        }
        private async void btnNewArticle_Click(object sender, EventArgs e)
        {
            var editorForm = new ArticleEditorForm(_apiClient);
            if (editorForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    statusLabel.Text = "Обновление списка статей...";

                    // Полная перезагрузка всех данных
                    await LoadDataAsync();

                    // Если есть выбранный раздел, подсвечиваем его статьи
                    if (treeViewSections.SelectedNode?.Tag is Section selectedSection)
                    {
                        await LoadArticlesBySectionAsync(selectedSection.SectionId);
                    }
                    else
                    {
                        // Если нет выбранного раздела, загружаем все статьи
                        await LoadArticlesAsync();
                    }

                    // Показываем сообщение об успехе
                    statusLabel.Text = "Статья успешно добавлена!";

                    // Автоматически выделяем последнюю добавленную статью (если есть)
                    if (listViewArticles.Items.Count > 0)
                    {
                        listViewArticles.Items[listViewArticles.Items.Count - 1].Selected = true;
                        listViewArticles.EnsureVisible(listViewArticles.Items.Count - 1);
                    }
                }
                catch (Exception ex)
                {
                    ShowError($"Ошибка при обновлении списка: {ex.Message}");
                    statusLabel.Text = "Ошибка обновления";
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btnEditArticle_Click(object sender, EventArgs e)
        {
            if (_currentArticle != null)
            {
                OpenArticleEditor(_currentArticle);
            }
        }

        private void OpenArticleEditor(Article article)
        {
            var editorForm = new ArticleEditorForm(_apiClient, article);
            if (editorForm.ShowDialog() == DialogResult.OK)
            {
                _ = LoadArticlesAsync();
            }
        }

        private async void btnDeleteArticle_Click(object sender, EventArgs e)
        {
            if (_currentArticle != null)
            {
                var result = MessageBox.Show($"Удалить статью \"{_currentArticle.Title}\"?\n\n" +
                    "Это действие нельзя отменить.",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        statusLabel.Text = "Удаление статьи...";
                        var success = await _apiClient.DeleteArticleAsync(_currentArticle.ArticleId);
                        if (success)
                        {
                            statusLabel.Text = "Статья удалена";
                            _ = LoadArticlesAsync();
                        }
                        else
                        {
                            ShowError("Не удалось удалить статью");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Ошибка удаления: {ex.Message}");
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void btnManageSections_Click(object sender, EventArgs e)
        {
            var sectionsForm = new SectionManagerForm(_apiClient);
            if (sectionsForm.ShowDialog() == DialogResult.OK)
            {
                _ = LoadSectionsAsync();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchForm = new SearchForm(_apiClient);
            searchForm.ShowDialog();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshArticlesWithNotification();
        }

        private void btnImages_Click(object sender, EventArgs e)
        {
            if (_currentArticle != null)
            {
                var imagesForm = new ArticleImageManagerForm(_apiClient, _currentArticle.ArticleId);
                imagesForm.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KnowledgeBase Client\nВерсия 1.1\n\n" +
                "Система управления базой знаний\n" +
                "Поддержка изображений в статьях\n" +
                "© 2026",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}