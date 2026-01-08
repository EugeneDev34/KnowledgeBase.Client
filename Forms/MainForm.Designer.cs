namespace KnowledgeBase.Client.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private SplitContainer splitContainerMain;
        private Panel panelLeft;
        private TreeView treeViewSections;
        private Label lblSections;
        private Panel panelRight;
        private SplitContainer splitContainerRight;
        private Panel panelArticles;
        private ListView listViewArticles;
        private ColumnHeader colTitle;
        private ColumnHeader colAuthor;
        private ColumnHeader colDate;
        private ColumnHeader colSection;
        private Label lblArticles;
        private Panel panelArticle;
        private TextBox txtTitle;
        private WebBrowser webBrowser;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel lblArticleCount;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem articlesToolStripMenuItem;
        private ToolStripMenuItem newArticleToolStripMenuItem;
        private ToolStripMenuItem editArticleToolStripMenuItem;
        private ToolStripMenuItem deleteArticleToolStripMenuItem;
        private ToolStripMenuItem sectionsToolStripMenuItem;
        private ToolStripMenuItem manageSectionsToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStrip toolStrip;
        private ToolStripButton btnNewArticle;
        private ToolStripButton btnEditArticle;
        private ToolStripButton btnDeleteArticle;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnManageSections;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnSearch;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnImages;
        private ToolStripSeparator toolStripSeparator3;
        private Label lblArticleInfo;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.treeViewSections = new System.Windows.Forms.TreeView();
            this.lblSections = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.panelArticles = new System.Windows.Forms.Panel();
            this.listViewArticles = new System.Windows.Forms.ListView();
            this.colTitle = new System.Windows.Forms.ColumnHeader();
            this.colAuthor = new System.Windows.Forms.ColumnHeader();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colSection = new System.Windows.Forms.ColumnHeader();
            this.lblArticles = new System.Windows.Forms.Label();
            this.panelArticle = new System.Windows.Forms.Panel();
            this.lblArticleInfo = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblArticleCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.articlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newArticleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editArticleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteArticleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageSectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNewArticle = new System.Windows.Forms.ToolStripButton();
            this.btnEditArticle = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteArticle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnManageSections = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImages = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            this.panelArticles.SuspendLayout();
            this.panelArticle.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 49);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelLeft);
            this.splitContainerMain.Panel1MinSize = 250;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1000, 551);
            this.splitContainerMain.SplitterDistance = 300;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.treeViewSections);
            this.panelLeft.Controls.Add(this.lblSections);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(300, 551);
            this.panelLeft.TabIndex = 0;
            // 
            // treeViewSections
            // 
            this.treeViewSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSections.Location = new System.Drawing.Point(0, 30);
            this.treeViewSections.Name = "treeViewSections";
            this.treeViewSections.Size = new System.Drawing.Size(300, 521);
            this.treeViewSections.TabIndex = 1;
            // 
            // lblSections
            // 
            this.lblSections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblSections.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSections.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSections.ForeColor = System.Drawing.Color.White;
            this.lblSections.Location = new System.Drawing.Point(0, 0);
            this.lblSections.Name = "lblSections";
            this.lblSections.Size = new System.Drawing.Size(300, 30);
            this.lblSections.TabIndex = 0;
            this.lblSections.Text = "РАЗДЕЛЫ";
            this.lblSections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.splitContainerRight);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(696, 551);
            this.panelRight.TabIndex = 0;
            // 
            // splitContainerRight
            // 
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRight.Name = "splitContainerRight";
            this.splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.Controls.Add(this.panelArticles);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.panelArticle);
            this.splitContainerRight.Size = new System.Drawing.Size(696, 551);
            this.splitContainerRight.SplitterDistance = 250;
            this.splitContainerRight.TabIndex = 0;
            // 
            // panelArticles
            // 
            this.panelArticles.Controls.Add(this.listViewArticles);
            this.panelArticles.Controls.Add(this.lblArticles);
            this.panelArticles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelArticles.Location = new System.Drawing.Point(0, 0);
            this.panelArticles.Name = "panelArticles";
            this.panelArticles.Size = new System.Drawing.Size(696, 250);
            this.panelArticles.TabIndex = 0;
            // 
            // listViewArticles
            // 
            this.listViewArticles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colAuthor,
            this.colDate,
            this.colSection});
            this.listViewArticles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewArticles.FullRowSelect = true;
            this.listViewArticles.GridLines = true;
            this.listViewArticles.Location = new System.Drawing.Point(0, 30);
            this.listViewArticles.Name = "listViewArticles";
            this.listViewArticles.Size = new System.Drawing.Size(696, 220);
            this.listViewArticles.TabIndex = 1;
            this.listViewArticles.UseCompatibleStateImageBehavior = false;
            this.listViewArticles.View = System.Windows.Forms.View.Details;
            // 
            // colTitle
            // 
            this.colTitle.Text = "Заголовок";
            this.colTitle.Width = 300;
            // 
            // colAuthor
            // 
            this.colAuthor.Text = "Автор";
            this.colAuthor.Width = 150;
            // 
            // colDate
            // 
            this.colDate.Text = "Дата создания";
            this.colDate.Width = 150;
            // 
            // colSection
            // 
            this.colSection.Text = "Раздел";
            this.colSection.Width = 150;
            // 
            // lblArticles
            // 
            this.lblArticles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblArticles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblArticles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblArticles.ForeColor = System.Drawing.Color.White;
            this.lblArticles.Location = new System.Drawing.Point(0, 0);
            this.lblArticles.Name = "lblArticles";
            this.lblArticles.Size = new System.Drawing.Size(696, 30);
            this.lblArticles.TabIndex = 0;
            this.lblArticles.Text = "СТАТЬИ";
            this.lblArticles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelArticle
            // 
            this.panelArticle.Controls.Add(this.lblArticleInfo);
            this.panelArticle.Controls.Add(this.webBrowser);
            this.panelArticle.Controls.Add(this.txtTitle);
            this.panelArticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelArticle.Location = new System.Drawing.Point(0, 0);
            this.panelArticle.Name = "panelArticle";
            this.panelArticle.Size = new System.Drawing.Size(696, 297);
            this.panelArticle.TabIndex = 0;
            // 
            // lblArticleInfo
            // 
            this.lblArticleInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.lblArticleInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblArticleInfo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblArticleInfo.Location = new System.Drawing.Point(0, 277);
            this.lblArticleInfo.Name = "lblArticleInfo";
            this.lblArticleInfo.Size = new System.Drawing.Size(696, 20);
            this.lblArticleInfo.TabIndex = 2;
            this.lblArticleInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 40);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(696, 237);
            this.webBrowser.TabIndex = 1;
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.White;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(696, 25);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.Text = "Выберите статью для просмотра";
            this.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.lblArticleCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 600);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(52, 17);
            this.statusLabel.Text = "Готово";
            // 
            // lblArticleCount
            // 
            this.lblArticleCount.Name = "lblArticleCount";
            this.lblArticleCount.Size = new System.Drawing.Size(933, 17);
            this.lblArticleCount.Spring = true;
            this.lblArticleCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.articlesToolStripMenuItem,
            this.sectionsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            // 
            // articlesToolStripMenuItem
            // 
            this.articlesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newArticleToolStripMenuItem,
            this.editArticleToolStripMenuItem,
            this.deleteArticleToolStripMenuItem});
            this.articlesToolStripMenuItem.Name = "articlesToolStripMenuItem";
            this.articlesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.articlesToolStripMenuItem.Text = "Статьи";
            // 
            // newArticleToolStripMenuItem
            // 
            this.newArticleToolStripMenuItem.Name = "newArticleToolStripMenuItem";
            this.newArticleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newArticleToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.newArticleToolStripMenuItem.Text = "Новая статья";
            // 
            // editArticleToolStripMenuItem
            // 
            this.editArticleToolStripMenuItem.Name = "editArticleToolStripMenuItem";
            this.editArticleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editArticleToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.editArticleToolStripMenuItem.Text = "Редактировать";
            // 
            // deleteArticleToolStripMenuItem
            // 
            this.deleteArticleToolStripMenuItem.Name = "deleteArticleToolStripMenuItem";
            this.deleteArticleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.deleteArticleToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.deleteArticleToolStripMenuItem.Text = "Удалить";
            // 
            // sectionsToolStripMenuItem
            // 
            this.sectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageSectionsToolStripMenuItem});
            this.sectionsToolStripMenuItem.Name = "sectionsToolStripMenuItem";
            this.sectionsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.sectionsToolStripMenuItem.Text = "Разделы";
            // 
            // manageSectionsToolStripMenuItem
            // 
            this.manageSectionsToolStripMenuItem.Name = "manageSectionsToolStripMenuItem";
            this.manageSectionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.manageSectionsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.manageSectionsToolStripMenuItem.Text = "Управление разделами";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.toolsToolStripMenuItem.Text = "Инструменты";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.searchToolStripMenuItem.Text = "Поиск";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.refreshToolStripMenuItem.Text = "Обновить";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.helpToolStripMenuItem.Text = "Справка";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewArticle,
            this.btnEditArticle,
            this.btnDeleteArticle,
            this.toolStripSeparator1,
            this.btnManageSections,
            this.toolStripSeparator2,
            this.btnSearch,
            this.btnRefresh,
            this.toolStripSeparator3,
            this.btnImages});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1000, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // btnNewArticle
            // 
            this.btnNewArticle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewArticle.Image = ((System.Drawing.Image)(resources.GetObject("btnNewArticle.Image")));
            this.btnNewArticle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewArticle.Name = "btnNewArticle";
            this.btnNewArticle.Size = new System.Drawing.Size(23, 22);
            this.btnNewArticle.Text = "Новая статья";
            // 
            // btnEditArticle
            // 
            this.btnEditArticle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditArticle.Image = ((System.Drawing.Image)(resources.GetObject("btnEditArticle.Image")));
            this.btnEditArticle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditArticle.Name = "btnEditArticle";
            this.btnEditArticle.Size = new System.Drawing.Size(23, 22);
            this.btnEditArticle.Text = "Редактировать";
            // 
            // btnDeleteArticle
            // 
            this.btnDeleteArticle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteArticle.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteArticle.Image")));
            this.btnDeleteArticle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteArticle.Name = "btnDeleteArticle";
            this.btnDeleteArticle.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteArticle.Text = "Удалить";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnManageSections
            // 
            this.btnManageSections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnManageSections.Image = ((System.Drawing.Image)(resources.GetObject("btnManageSections.Image")));
            this.btnManageSections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnManageSections.Name = "btnManageSections";
            this.btnManageSections.Size = new System.Drawing.Size(23, 22);
            this.btnManageSections.Text = "Управление разделами";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.Text = "Поиск";
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "Обновить";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImages
            // 
            this.btnImages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImages.Image = ((System.Drawing.Image)(resources.GetObject("btnImages.Image")));
            this.btnImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImages.Name = "btnImages";
            this.btnImages.Size = new System.Drawing.Size(23, 22);
            this.btnImages.Text = "Изображения";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 622);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KnowledgeBase - Система управления базой знаний";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            this.panelArticles.ResumeLayout(false);
            this.panelArticle.ResumeLayout(false);
            this.panelArticle.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}