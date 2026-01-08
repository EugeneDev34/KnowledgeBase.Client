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

            // Настройка WebBrowser
            webBrowser.AllowNavigation = false;
            webBrowser.AllowWebBrowserDrop = false;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.WebBrowserShortcutsEnabled = false;

            // Настройка статус бара
            statusLabel.Text = "Готово";
            lblArticleCount.Text = "Статей: 0";

            // Установка обработчиков
            SetupEventHandlers();
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

        private void SetupEventHandlers()
        {
            treeViewSections.AfterSelect += TreeViewSections_AfterSelect;
            listViewArticles.SelectedIndexChanged += ListViewArticles_SelectedIndexChanged;
            listViewArticles.DoubleClick += ListViewArticles_DoubleClick;

            btnNewArticle.Click += BtnNewArticle_Click;
            btnEditArticle.Click += BtnEditArticle_Click;
            btnDeleteArticle.Click += BtnDeleteArticle_Click;
            btnManageSections.Click += BtnManageSections_Click;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnImages.Click += BtnImages_Click;

            newArticleToolStripMenuItem.Click += BtnNewArticle_Click;
            editArticleToolStripMenuItem.Click += BtnEditArticle_Click;
            deleteArticleToolStripMenuItem.Click += BtnDeleteArticle_Click;
            manageSectionsToolStripMenuItem.Click += BtnManageSections_Click;
            searchToolStripMenuItem.Click += BtnSearch_Click;
            refreshToolStripMenuItem.Click += BtnRefresh_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Загрузка данных...";

                await Task.WhenAll(LoadSectionsAsync(), LoadArticlesAsync());

                statusLabel.Text = "Готово";
                lblArticleCount.Text = $"Статей: {_articles.Count}";
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка загрузки данных: {ex.Message}");
                statusLabel.Text = "Ошибка загрузки";
            }
            finally
            {
                Cursor = Cursors.Default;
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
                _articles = await _apiClient.GetArticlesAsync();
                UpdateArticlesList();
            }
            catch (Exception ex)
            {
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
        }

        private async void TreeViewSections_AfterSelect(object sender, TreeViewEventArgs e)
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
                Cursor = Cursors.WaitCursor;
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
                Cursor = Cursors.Default;
            }
        }

        private void ListViewArticles_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ListViewArticles_DoubleClick(object sender, EventArgs e)
        {
            if (_currentArticle != null)
            {
                OpenArticleEditor(_currentArticle);
            }
        }

        private void DisplayArticle(Article article)
        {
            txtTitle.Text = article.Title;

            string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ 
                            font-family: 'Segoe UI', Arial, sans-serif; 
                            padding: 20px;
                            line-height: 1.6;
                            color: #333;
                        }}
                        h1 {{ 
                            color: #2c3e50; 
                            border-bottom: 2px solid #eee;
                            padding-bottom: 10px;
                        }}
                        h2 {{ color: #34495e; }}
                        h3 {{ color: #7f8c8d; }}
                        p {{ margin: 10px 0; }}
                        ul, ol {{ margin: 10px 0 10px 20px; }}
                        li {{ margin: 5px 0; }}
                        img {{ 
                            max-width: 100%; 
                            height: auto;
                            border: 1px solid #ddd;
                            border-radius: 4px;
                            padding: 5px;
                        }}
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
                            font-family: 'Consolas', monospace;
                        }}
                        blockquote {{
                            border-left: 4px solid #ddd;
                            margin: 10px 0;
                            padding-left: 15px;
                            color: #666;
                        }}
                        table {{
                            border-collapse: collapse;
                            width: 100%;
                            margin: 15px 0;
                        }}
                        th, td {{
                            border: 1px solid #ddd;
                            padding: 8px;
                            text-align: left;
                        }}
                        th {{
                            background-color: #f2f2f2;
                        }}
                    </style>
                </head>
                <body>
                    {article.Content}
                </body>
                </html>";

            webBrowser.DocumentText = htmlContent;

            lblArticleInfo.Text = $"Автор: {article.AuthorName} | " +
                                $"Раздел: {article.SectionName} | " +
                                $"Создана: {article.CreatedDate:dd.MM.yyyy HH:mm} | " +
                                $"Обновлена: {article.UpdatedDate:dd.MM.yyyy HH:mm}";
        }

        private void ClearArticleDisplay()
        {
            txtTitle.Text = "Выберите статью для просмотра";
            webBrowser.DocumentText = "<html><body style='font-family: Segoe UI; color: #666; text-align: center; padding: 50px;'>" +
                                    "<h3>Выберите статью из списка</h3>" +
                                    "<p>Для просмотра содержимого выберите статью в списке выше</p></body></html>";
            lblArticleInfo.Text = string.Empty;
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = _currentArticle != null;

            btnEditArticle.Enabled = hasSelection;
            btnDeleteArticle.Enabled = hasSelection;
            btnImages.Enabled = hasSelection;

            editArticleToolStripMenuItem.Enabled = hasSelection;
            deleteArticleToolStripMenuItem.Enabled = hasSelection;
        }

        private void BtnNewArticle_Click(object sender, EventArgs e)
        {
            var editorForm = new ArticleEditorForm(_apiClient);
            if (editorForm.ShowDialog() == DialogResult.OK)
            {
                _ = LoadArticlesAsync();
            }
        }

        private void BtnEditArticle_Click(object sender, EventArgs e)
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

        private async void BtnDeleteArticle_Click(object sender, EventArgs e)
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
                        Cursor = Cursors.WaitCursor;
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
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void BtnManageSections_Click(object sender, EventArgs e)
        {
            var sectionsForm = new SectionManagerForm(_apiClient);
            if (sectionsForm.ShowDialog() == DialogResult.OK)
            {
                _ = LoadSectionsAsync();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchForm = new SearchForm(_apiClient);
            searchForm.ShowDialog();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private void BtnImages_Click(object sender, EventArgs e)
        {
            if (_currentArticle != null)
            {
                var imagesForm = new ArticleImageManagerForm(_apiClient, _currentArticle.ArticleId);
                imagesForm.ShowDialog();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KnowledgeBase Client\nВерсия 1.0\n\n" +
                          "Система управления базой знаний\n" +
                          "© 2024",
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