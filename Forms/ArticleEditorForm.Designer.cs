namespace KnowledgeBase.Client.Forms
{
    partial class ArticleEditorForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Panel panelContent;
        private TextBox txtContent;
        private Panel panelHeader;
        private ComboBox cmbSection;
        private Label lblSection;
        private TextBox txtTitle;
        private Label lblTitle;
        private Panel panelFormatting;
        private Button btnFormatBold;
        private Button btnFormatItalic;
        private Button btnFormatUnderline;
        private Button btnFormatHeader1;
        private Button btnFormatHeader2;
        private Button btnFormatHeader3;
        private Button btnFormatList;
        private Button btnFormatOrderedList;
        private Button btnFormatLink;
        private Button btnFormatCode;
        private Button btnPreview;
        private Panel panelFooter;
        private Button btnSave;
        private Button btnCancel;
        private Label lblStatus;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArticleEditorForm));
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.lblSection = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFormatting = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnFormatCode = new System.Windows.Forms.Button();
            this.btnFormatLink = new System.Windows.Forms.Button();
            this.btnFormatOrderedList = new System.Windows.Forms.Button();
            this.btnFormatList = new System.Windows.Forms.Button();
            this.btnFormatHeader3 = new System.Windows.Forms.Button();
            this.btnFormatHeader2 = new System.Windows.Forms.Button();
            this.btnFormatHeader1 = new System.Windows.Forms.Button();
            this.btnFormatUnderline = new System.Windows.Forms.Button();
            this.btnFormatItalic = new System.Windows.Forms.Button();
            this.btnFormatBold = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFormatting.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Controls.Add(this.panelFormatting);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 500);
            this.panelMain.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.txtContent);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 80);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(10);
            this.panelContent.Size = new System.Drawing.Size(800, 370);
            this.panelContent.TabIndex = 2;
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtContent.Location = new System.Drawing.Point(10, 10);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(780, 350);
            this.txtContent.TabIndex = 2;
            this.txtContent.WordWrap = false;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.cmbSection);
            this.panelHeader.Controls.Add(this.lblSection);
            this.panelHeader.Controls.Add(this.txtTitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 30);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(10);
            this.panelHeader.Size = new System.Drawing.Size(800, 50);
            this.panelHeader.TabIndex = 1;
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(570, 13);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(217, 23);
            this.cmbSection.TabIndex = 1;
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(516, 16);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(48, 15);
            this.lblSection.TabIndex = 2;
            this.lblSection.Text = "Раздел:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(56, 13);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(454, 23);
            this.txtTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(10, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Заголовок:";
            // 
            // panelFormatting
            // 
            this.panelFormatting.Controls.Add(this.btnPreview);
            this.panelFormatting.Controls.Add(this.btnFormatCode);
            this.panelFormatting.Controls.Add(this.btnFormatLink);
            this.panelFormatting.Controls.Add(this.btnFormatOrderedList);
            this.panelFormatting.Controls.Add(this.btnFormatList);
            this.panelFormatting.Controls.Add(this.btnFormatHeader3);
            this.panelFormatting.Controls.Add(this.btnFormatHeader2);
            this.panelFormatting.Controls.Add(this.btnFormatHeader1);
            this.panelFormatting.Controls.Add(this.btnFormatUnderline);
            this.panelFormatting.Controls.Add(this.btnFormatItalic);
            this.panelFormatting.Controls.Add(this.btnFormatBold);
            this.panelFormatting.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFormatting.Location = new System.Drawing.Point(0, 0);
            this.panelFormatting.Name = "panelFormatting";
            this.panelFormatting.Padding = new System.Windows.Forms.Padding(5);
            this.panelFormatting.Size = new System.Drawing.Size(800, 30);
            this.panelFormatting.TabIndex = 0;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(710, 3);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(80, 24);
            this.btnPreview.TabIndex = 10;
            this.btnPreview.Text = "Просмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnFormatCode
            // 
            this.btnFormatCode.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFormatCode.Location = new System.Drawing.Point(630, 3);
            this.btnFormatCode.Name = "btnFormatCode";
            this.btnFormatCode.Size = new System.Drawing.Size(40, 24);
            this.btnFormatCode.TabIndex = 9;
            this.btnFormatCode.Text = "</>";
            this.btnFormatCode.UseVisualStyleBackColor = true;
            this.btnFormatCode.Click += new System.EventHandler(this.btnFormatCode_Click);
            // 
            // btnFormatLink
            // 
            this.btnFormatLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFormatLink.Location = new System.Drawing.Point(580, 3);
            this.btnFormatLink.Name = "btnFormatLink";
            this.btnFormatLink.Size = new System.Drawing.Size(40, 24);
            this.btnFormatLink.TabIndex = 8;
            this.btnFormatLink.Text = "🔗";
            this.btnFormatLink.UseVisualStyleBackColor = true;
            this.btnFormatLink.Click += new System.EventHandler(this.btnFormatLink_Click);
            // 
            // btnFormatOrderedList
            // 
            this.btnFormatOrderedList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFormatOrderedList.Location = new System.Drawing.Point(540, 3);
            this.btnFormatOrderedList.Name = "btnFormatOrderedList";
            this.btnFormatOrderedList.Size = new System.Drawing.Size(40, 24);
            this.btnFormatOrderedList.TabIndex = 7;
            this.btnFormatOrderedList.Text = "1.";
            this.btnFormatOrderedList.UseVisualStyleBackColor = true;
            this.btnFormatOrderedList.Click += new System.EventHandler(this.btnFormatOrderedList_Click);
            // 
            // btnFormatList
            // 
            this.btnFormatList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFormatList.Location = new System.Drawing.Point(500, 3);
            this.btnFormatList.Name = "btnFormatList";
            this.btnFormatList.Size = new System.Drawing.Size(40, 24);
            this.btnFormatList.TabIndex = 6;
            this.btnFormatList.Text = "•";
            this.btnFormatList.UseVisualStyleBackColor = true;
            this.btnFormatList.Click += new System.EventHandler(this.btnFormatList_Click);
            // 
            // btnFormatHeader3
            // 
            this.btnFormatHeader3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFormatHeader3.Location = new System.Drawing.Point(460, 3);
            this.btnFormatHeader3.Name = "btnFormatHeader3";
            this.btnFormatHeader3.Size = new System.Drawing.Size(40, 24);
            this.btnFormatHeader3.TabIndex = 5;
            this.btnFormatHeader3.Text = "H3";
            this.btnFormatHeader3.UseVisualStyleBackColor = true;
            this.btnFormatHeader3.Click += new System.EventHandler(this.btnFormatHeader3_Click);
            // 
            // btnFormatHeader2
            // 
            this.btnFormatHeader2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFormatHeader2.Location = new System.Drawing.Point(420, 3);
            this.btnFormatHeader2.Name = "btnFormatHeader2";
            this.btnFormatHeader2.Size = new System.Drawing.Size(40, 24);
            this.btnFormatHeader2.TabIndex = 4;
            this.btnFormatHeader2.Text = "H2";
            this.btnFormatHeader2.UseVisualStyleBackColor = true;
            this.btnFormatHeader2.Click += new System.EventHandler(this.btnFormatHeader2_Click);
            // 
            // btnFormatHeader1
            // 
            this.btnFormatHeader1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFormatHeader1.Location = new System.Drawing.Point(380, 3);
            this.btnFormatHeader1.Name = "btnFormatHeader1";
            this.btnFormatHeader1.Size = new System.Drawing.Size(40, 24);
            this.btnFormatHeader1.TabIndex = 3;
            this.btnFormatHeader1.Text = "H1";
            this.btnFormatHeader1.UseVisualStyleBackColor = true;
            this.btnFormatHeader1.Click += new System.EventHandler(this.btnFormatHeader1_Click);
            // 
            // btnFormatUnderline
            // 
            this.btnFormatUnderline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.btnFormatUnderline.Location = new System.Drawing.Point(80, 3);
            this.btnFormatUnderline.Name = "btnFormatUnderline";
            this.btnFormatUnderline.Size = new System.Drawing.Size(40, 24);
            this.btnFormatUnderline.TabIndex = 2;
            this.btnFormatUnderline.Text = "U";
            this.btnFormatUnderline.UseVisualStyleBackColor = true;
            this.btnFormatUnderline.Click += new System.EventHandler(this.btnFormatUnderline_Click);
            // 
            // btnFormatItalic
            // 
            this.btnFormatItalic.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.btnFormatItalic.Location = new System.Drawing.Point(40, 3);
            this.btnFormatItalic.Name = "btnFormatItalic";
            this.btnFormatItalic.Size = new System.Drawing.Size(40, 24);
            this.btnFormatItalic.TabIndex = 1;
            this.btnFormatItalic.Text = "I";
            this.btnFormatItalic.UseVisualStyleBackColor = true;
            this.btnFormatItalic.Click += new System.EventHandler(this.btnFormatItalic_Click);
            // 
            // btnFormatBold
            // 
            this.btnFormatBold.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFormatBold.Location = new System.Drawing.Point(0, 3);
            this.btnFormatBold.Name = "btnFormatBold";
            this.btnFormatBold.Size = new System.Drawing.Size(40, 24);
            this.btnFormatBold.TabIndex = 0;
            this.btnFormatBold.Text = "B";
            this.btnFormatBold.UseVisualStyleBackColor = true;
            this.btnFormatBold.Click += new System.EventHandler(this.btnFormatBold_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.lblStatus);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 500);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(10);
            this.panelFooter.Size = new System.Drawing.Size(800, 50);
            this.panelFooter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(10, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(400, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(710, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(620, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ArticleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "ArticleEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор статей";
            this.Load += new System.EventHandler(this.ArticleEditorForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFormatting.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}