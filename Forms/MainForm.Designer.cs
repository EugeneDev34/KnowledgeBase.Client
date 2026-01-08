namespace KnowledgeBase.Client.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SplitContainer splitContainerMain;
        private Panel panelLeft;
        private TreeView treeViewSections;
        private Panel panelRight;
        private Panel panelTop;
        private TextBox txtTitle;
        private Panel panelCenter;
        private WebBrowser webBrowser;
        private Panel panelBottom;
        private Label lblArticleInfo;
        private Panel panelControls;
        private Button btnNewArticle;
        private Button btnEditArticle;
        private Button btnDeleteArticle;
        private Button btnImages;
        private Button btnManageSections;
        private Button btnSearch;
        private Button btnRefresh;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private Label lblArticleCount;
        private ListView listViewArticles;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.treeViewSections = new System.Windows.Forms.TreeView();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblArticleInfo = new System.Windows.Forms.Label();
            this.panelControls = new System.Windows.Forms.Panel();
            this.btnNewArticle = new System.Windows.Forms.Button();
            this.btnEditArticle = new System.Windows.Forms.Button();
            this.btnDeleteArticle = new System.Windows.Forms.Button();
            this.btnImages = new System.Windows.Forms.Button();
            this.btnManageSections = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblArticleCount = new System.Windows.Forms.Label();
            this.listViewArticles = new System.Windows.Forms.ListView();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";

            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";

            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);

            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.helpToolStripMenuItem.Text = "Помощь";

            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);

            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
            this.splitContainerMain.Name = "splitContainerMain";

            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelLeft);
            this.splitContainerMain.Panel1MinSize = 200;

            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1000, 576);
            this.splitContainerMain.SplitterDistance = 250;
            this.splitContainerMain.TabIndex = 1;

            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.treeViewSections);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(5);
            this.panelLeft.Size = new System.Drawing.Size(250, 576);
            this.panelLeft.TabIndex = 0;

            // treeViewSections
            // 
            this.treeViewSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSections.Location = new System.Drawing.Point(5, 5);
            this.treeViewSections.Name = "treeViewSections";
            this.treeViewSections.Size = new System.Drawing.Size(240, 566);
            this.treeViewSections.TabIndex = 0;
            this.treeViewSections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSections_AfterSelect);

            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelControls);
            this.panelRight.Controls.Add(this.listViewArticles);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(746, 576);
            this.panelRight.TabIndex = 0;

            // panelControls
            // 
            this.panelControls.Controls.Add(this.btnRefresh);
            this.panelControls.Controls.Add(this.btnSearch);
            this.panelControls.Controls.Add(this.btnManageSections);
            this.panelControls.Controls.Add(this.btnImages);
            this.panelControls.Controls.Add(this.btnDeleteArticle);
            this.panelControls.Controls.Add(this.btnEditArticle);
            this.panelControls.Controls.Add(this.btnNewArticle);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Padding = new System.Windows.Forms.Padding(5);
            this.panelControls.Size = new System.Drawing.Size(746, 50);
            this.panelControls.TabIndex = 1;

            // btnNewArticle
            // 
            this.btnNewArticle.Location = new System.Drawing.Point(5, 5);
            this.btnNewArticle.Name = "btnNewArticle";
            this.btnNewArticle.Size = new System.Drawing.Size(100, 40);
            this.btnNewArticle.TabIndex = 0;
            this.btnNewArticle.Text = "Новая статья";
            this.btnNewArticle.UseVisualStyleBackColor = true;
            this.btnNewArticle.Click += new System.EventHandler(this.btnNewArticle_Click);

            // btnEditArticle
            // 
            this.btnEditArticle.Location = new System.Drawing.Point(111, 5);
            this.btnEditArticle.Name = "btnEditArticle";
            this.btnEditArticle.Size = new System.Drawing.Size(100, 40);
            this.btnEditArticle.TabIndex = 1;
            this.btnEditArticle.Text = "Редактировать";
            this.btnEditArticle.UseVisualStyleBackColor = true;
            this.btnEditArticle.Click += new System.EventHandler(this.btnEditArticle_Click);

            // btnDeleteArticle
            // 
            this.btnDeleteArticle.Location = new System.Drawing.Point(217, 5);
            this.btnDeleteArticle.Name = "btnDeleteArticle";
            this.btnDeleteArticle.Size = new System.Drawing.Size(100, 40);
            this.btnDeleteArticle.TabIndex = 2;
            this.btnDeleteArticle.Text = "Удалить";
            this.btnDeleteArticle.UseVisualStyleBackColor = true;
            this.btnDeleteArticle.Click += new System.EventHandler(this.btnDeleteArticle_Click);

            // btnImages
            // 
            this.btnImages.Location = new System.Drawing.Point(323, 5);
            this.btnImages.Name = "btnImages";
            this.btnImages.Size = new System.Drawing.Size(120, 40);
            this.btnImages.TabIndex = 3;
            this.btnImages.Text = "Изображения";
            this.btnImages.UseVisualStyleBackColor = true;
            this.btnImages.Click += new System.EventHandler(this.btnImages_Click);

            // btnManageSections
            // 
            this.btnManageSections.Location = new System.Drawing.Point(449, 5);
            this.btnManageSections.Name = "btnManageSections";
            this.btnManageSections.Size = new System.Drawing.Size(120, 40);
            this.btnManageSections.TabIndex = 4;
            this.btnManageSections.Text = "Разделы";
            this.btnManageSections.UseVisualStyleBackColor = true;
            this.btnManageSections.Click += new System.EventHandler(this.btnManageSections_Click);

            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(575, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 40);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(661, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 40);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // listViewArticles
            // 
            this.listViewArticles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewArticles.FullRowSelect = true;
            this.listViewArticles.Location = new System.Drawing.Point(0, 50);
            this.listViewArticles.Name = "listViewArticles";
            this.listViewArticles.Size = new System.Drawing.Size(746, 526);
            this.listViewArticles.TabIndex = 0;
            this.listViewArticles.UseCompatibleStateImageBehavior = false;
            this.listViewArticles.View = System.Windows.Forms.View.Details;
            this.listViewArticles.SelectedIndexChanged += new System.EventHandler(this.listViewArticles_SelectedIndexChanged);
            this.listViewArticles.DoubleClick += new System.EventHandler(this.listViewArticles_DoubleClick);

            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 600);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";

            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Готово";

            // lblArticleCount
            // 
            this.lblArticleCount.AutoSize = true;
            this.lblArticleCount.Location = new System.Drawing.Point(880, 4);
            this.lblArticleCount.Name = "lblArticleCount";
            this.lblArticleCount.Size = new System.Drawing.Size(61, 15);
            this.lblArticleCount.TabIndex = 3;
            this.lblArticleCount.Text = "Статей: 0";

            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 622);
            this.Controls.Add(this.lblArticleCount);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KnowledgeBase Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelControls.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}