using KnowledgeBase.Client.Services;
using KnowledgeBase.Client.Services.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnowledgeBase.Client.Forms
{
    public partial class SectionManagerForm : Form
    {
        private readonly ApiClient _apiClient;
        private List<Section> _sections = new List<Section>();
        private TreeNode? _selectedNode;

        public SectionManagerForm(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            UpdateButtonStates();
        }

        private async void SectionManagerForm_Load(object sender, EventArgs e)
        {
            await LoadSectionsAsync();
        }

        private async Task LoadSectionsAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Загрузка разделов...";

                _sections = await _apiClient.GetSectionsTreeAsync();
                BuildTreeView();

                lblStatus.Text = $"Загружено разделов: {_sections.Count}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка загрузки";
                MessageBox.Show($"Ошибка загрузки разделов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
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
                        ImageKey = section.ParentSectionId == null ? "root" : "folder"
                    };

                    nodes.Add(node);
                    BuildTreeNodes(section.SectionId, node.Nodes);
                }
            }

            BuildTreeNodes(null, treeViewSections.Nodes);

            if (treeViewSections.Nodes.Count > 0)
            {
                treeViewSections.ExpandAll();
            }

            treeViewSections.EndUpdate();
        }

        private void treeViewSections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selectedNode = e.Node;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = _selectedNode != null;
            bool isRoot = _selectedNode?.Parent == null;

            btnRename.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection && !isRoot; // Нельзя удалять корневые разделы
            btnAdd.Enabled = true;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new AddSectionDialog(_sections))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Создание раздела...";

                        var createDto = new CreateSectionDto
                        {
                            Name = dialog.SectionName,
                            ParentSectionId = dialog.ParentSectionId
                        };

                        var result = await _apiClient.CreateSectionAsync(createDto);

                        if (result != null)
                        {
                            await LoadSectionsAsync();
                            lblStatus.Text = "Раздел создан";
                            MessageBox.Show("Раздел создан успешно", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            lblStatus.Text = "Ошибка создания";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Ошибка";
                        MessageBox.Show($"Ошибка создания раздела: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private async void btnRename_Click(object sender, EventArgs e)
        {
            if (_selectedNode?.Tag is Section section)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите новое название раздела:",
                    "Переименование раздела",
                    section.Name);

                if (!string.IsNullOrWhiteSpace(newName) && newName != section.Name)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Переименование...";

                        var updateDto = new UpdateSectionDto
                        {
                            Name = newName,
                            ParentSectionId = section.ParentSectionId
                        };

                        var result = await _apiClient.UpdateSectionAsync(section.SectionId, updateDto);

                        if (result != null)
                        {
                            await LoadSectionsAsync();
                            lblStatus.Text = "Раздел переименован";
                            MessageBox.Show("Раздел переименован", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            lblStatus.Text = "Ошибка переименования";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Ошибка";
                        MessageBox.Show($"Ошибка переименования: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedNode?.Tag is Section section)
            {
                var result = MessageBox.Show($"Удалить раздел \"{section.Name}\"?\n\n" +
                                           "Все подразделы также будут удалены.",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Удаление раздела...";

                        var success = await _apiClient.DeleteSectionAsync(section.SectionId);

                        if (success)
                        {
                            await LoadSectionsAsync();
                            lblStatus.Text = "Раздел удален";
                            MessageBox.Show("Раздел удален", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            lblStatus.Text = "Ошибка удаления";
                            MessageBox.Show("Не удалось удалить раздел. Возможно, в нем есть статьи.", "Ошибка",
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        // Вспомогательный класс для диалога добавления раздела
        private class AddSectionDialog : Form
        {
            private ComboBox cmbParent;
            private TextBox txtName;
            private Button btnOK;
            private Button btnCancel;
            private List<Section> _sections;

            public string SectionName => txtName.Text.Trim();

            public int? ParentSectionId
            {
                get
                {
                    if (cmbParent.SelectedIndex > 0 && cmbParent.SelectedItem is ComboBoxSectionItem item)
                        return item.Section.SectionId;
                    return null;
                }
            }

            public AddSectionDialog(List<Section> sections)
            {
                _sections = sections;
                InitializeComponent();
                PopulateParentComboBox();
            }

            private void InitializeComponent()
            {
                this.Text = "Добавить раздел";
                this.Size = new Size(400, 200);
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

                var lblName = new Label { Text = "Название:", Left = 20, Top = 20, Width = 100 };
                txtName = new TextBox { Left = 120, Top = 20, Width = 250 };

                var lblParent = new Label { Text = "Родительский раздел:", Left = 20, Top = 60, Width = 100 };
                cmbParent = new ComboBox { Left = 120, Top = 60, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

                btnOK = new Button { Text = "OK", Left = 200, Top = 120, Width = 80 };
                btnCancel = new Button { Text = "Отмена", Left = 290, Top = 120, Width = 80 };

                btnOK.Click += (s, e) => { if (ValidateInput()) DialogResult = DialogResult.OK; };
                btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

                this.Controls.AddRange(new Control[] { lblName, txtName, lblParent, cmbParent, btnOK, btnCancel });

                this.AcceptButton = btnOK;
                this.CancelButton = btnCancel;
            }

            private void PopulateParentComboBox()
            {
                cmbParent.Items.Clear();
                cmbParent.Items.Add(new ComboBoxSectionItem(null, "-- Корневой раздел --"));

                void AddSections(List<Section> sections, string prefix)
                {
                    foreach (var section in sections)
                    {
                        cmbParent.Items.Add(new ComboBoxSectionItem(section, $"{prefix}{section.Name}"));
                        if (section.ChildSections != null && section.ChildSections.Count > 0)
                        {
                            AddSections(section.ChildSections, $"{prefix}  ");
                        }
                    }
                }

                AddSections(_sections, "");
                cmbParent.SelectedIndex = 0;
            }

            private bool ValidateInput()
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название раздела", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return false;
                }

                return true;
            }

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
        }
    }
}