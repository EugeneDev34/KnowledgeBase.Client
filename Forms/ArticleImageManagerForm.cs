using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Client.Services;
using KnowledgeBase.Client.Services.Models;

namespace KnowledgeBase.Client.Forms
{
    public partial class ArticleImageManagerForm : Form
    {
        private readonly ApiClient _apiClient;
        private readonly int _articleId;
        private List<ArticleImage> _images = new List<ArticleImage>();
        private ImageList _imageList;

        public ArticleImageManagerForm(ApiClient apiClient, int articleId)
        {
            _apiClient = apiClient;
            _articleId = articleId;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = $"Изображения для статьи #{_articleId}";

            // Настройка списка изображений
            _imageList = new ImageList
            {
                ImageSize = new Size(100, 100),
                ColorDepth = ColorDepth.Depth32Bit
            };

            listViewImages.LargeImageList = _imageList;
            listViewImages.View = View.LargeIcon;
        }

        private async void ArticleImageManagerForm_Load(object sender, EventArgs e)
        {
            await LoadImagesAsync();
        }

        private async Task LoadImagesAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Загрузка изображений...";

                _images = await _apiClient.GetImagesByArticleAsync(_articleId);
                DisplayImages();

                lblStatus.Text = $"Загружено изображений: {_images.Count}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка загрузки";
                MessageBox.Show($"Ошибка загрузки изображений: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DisplayImages()
        {
            listViewImages.BeginUpdate();
            listViewImages.Items.Clear();
            _imageList.Images.Clear();

            foreach (var image in _images)
            {
                try
                {
                    // Добавляем миниатюру
                    var thumbnail = CreateThumbnail(image);
                    if (thumbnail != null)
                    {
                        _imageList.Images.Add(thumbnail);
                    }
                    else
                    {
                        // Используем иконку по умолчанию
                        _imageList.Images.Add(CreateDefaultThumbnail());
                    }

                    // Добавляем элемент в ListView
                    var item = new ListViewItem
                    {
                        Text = $"{image.FileName}\n{image.UploadDate:dd.MM.yyyy}",
                        ImageIndex = _imageList.Images.Count - 1,
                        Tag = image
                    };

                    listViewImages.Items.Add(item);
                }
                catch (Exception ex)
                {
                    // Пропускаем изображения с ошибками
                    Console.WriteLine($"Ошибка загрузки изображения {image.FileName}: {ex.Message}");
                }
            }

            listViewImages.EndUpdate();
        }

        private Image? CreateThumbnail(ArticleImage image)
        {
            try
            {
                // В реальном приложении здесь была бы загрузка изображения
                // Для примера создаем простую заглушку
                return CreateDefaultThumbnail();
            }
            catch
            {
                return null;
            }
        }

        private Image CreateDefaultThumbnail()
        {
            Bitmap bmp = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (SolidBrush brush = new SolidBrush(Color.LightGray))
                {
                    g.FillRectangle(brush, 0, 0, 100, 100);
                }
                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    g.DrawRectangle(pen, 5, 5, 90, 90);
                }
                using (Font font = new Font("Arial", 10, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.DarkGray))
                {
                    g.DrawString("IMG", font, textBrush, new RectangleF(0, 0, 100, 100),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            }
            return bmp;
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Все файлы (*.*)|*.*";
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Выберите изображения для загрузки";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await UploadImagesAsync(openFileDialog.FileNames);
                }
            }
        }

        private async Task UploadImagesAsync(string[] filePaths)
        {
            int successCount = 0;
            int errorCount = 0;

            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Загрузка изображений...";

                foreach (var filePath in filePaths)
                {
                    try
                    {
                        var result = await _apiClient.UploadImageAsync(_articleId, filePath);
                        if (result != null)
                        {
                            successCount++;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        MessageBox.Show($"Ошибка загрузки файла {Path.GetFileName(filePath)}: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (successCount > 0)
                {
                    await LoadImagesAsync();
                    lblStatus.Text = $"Загружено: {successCount}, ошибок: {errorCount}";
                }
                else
                {
                    lblStatus.Text = "Не удалось загрузить изображения";
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewImages.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите изображение для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = listViewImages.SelectedItems[0];
            if (selectedItem.Tag is ArticleImage image)
            {
                var result = MessageBox.Show($"Удалить изображение \"{image.FileName}\"?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Удаление изображения...";

                        var success = await _apiClient.DeleteImageAsync(image.ImageId);

                        if (success)
                        {
                            await LoadImagesAsync();
                            lblStatus.Text = "Изображение удалено";
                        }
                        else
                        {
                            lblStatus.Text = "Ошибка удаления";
                            MessageBox.Show("Не удалось удалить изображение", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Ошибка";
                        MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (listViewImages.SelectedItems.Count > 0 &&
                listViewImages.SelectedItems[0].Tag is ArticleImage image)
            {
                // В реальном приложении здесь был бы просмотр изображения
                MessageBox.Show($"Просмотр изображения:\n\n" +
                              $"Имя файла: {image.FileName}\n" +
                              $"Дата загрузки: {image.UploadDate:dd.MM.yyyy HH:mm}\n" +
                              $"Путь: {image.FilePath}",
                    "Информация об изображении",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void listViewImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasSelection = listViewImages.SelectedItems.Count > 0;
            btnDelete.Enabled = hasSelection;
            btnView.Enabled = hasSelection;
        }

        private void listViewImages_DoubleClick(object sender, EventArgs e)
        {
            btnView_Click(sender, e);
        }
    }
}