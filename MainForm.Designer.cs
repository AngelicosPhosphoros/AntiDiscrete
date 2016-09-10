namespace WindowsFormsApplication1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DerivativePage = new System.Windows.Forms.TabPage();
            this.DerivativesBox = new System.Windows.Forms.TextBox();
            this.MinimFormsPage = new System.Windows.Forms.TabPage();
            this.MinimFormsBox = new System.Windows.Forms.TextBox();
            this.TaylorPage = new System.Windows.Forms.TabPage();
            this.TaylorBoxEqv = new System.Windows.Forms.TextBox();
            this.TaylorBoxXor = new System.Windows.Forms.TextBox();
            this.PostPage = new System.Windows.Forms.TabPage();
            this.PostBox = new System.Windows.Forms.TextBox();
            this.ElemFuncPage = new System.Windows.Forms.TabPage();
            this.ElemFuncBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьРезультатыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьБазисВБиблиотекуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.truthTableBox = new System.Windows.Forms.TextBox();
            this.btnSoluteMarix = new System.Windows.Forms.Button();
            this.SoluteAll = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.DerivativePage.SuspendLayout();
            this.MinimFormsPage.SuspendLayout();
            this.TaylorPage.SuspendLayout();
            this.PostPage.SuspendLayout();
            this.ElemFuncPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DerivativePage);
            this.tabControl1.Controls.Add(this.MinimFormsPage);
            this.tabControl1.Controls.Add(this.TaylorPage);
            this.tabControl1.Controls.Add(this.PostPage);
            this.tabControl1.Controls.Add(this.ElemFuncPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(857, 428);
            this.tabControl1.TabIndex = 0;
            // 
            // DerivativePage
            // 
            this.DerivativePage.Controls.Add(this.DerivativesBox);
            this.DerivativePage.Location = new System.Drawing.Point(4, 22);
            this.DerivativePage.Name = "DerivativePage";
            this.DerivativePage.Padding = new System.Windows.Forms.Padding(3);
            this.DerivativePage.Size = new System.Drawing.Size(849, 402);
            this.DerivativePage.TabIndex = 0;
            this.DerivativePage.Text = "Производные";
            this.DerivativePage.UseVisualStyleBackColor = true;
            this.DerivativePage.Resize += new System.EventHandler(this.DerivativePage_Resize);
            // 
            // DerivativesBox
            // 
            this.DerivativesBox.Location = new System.Drawing.Point(12, 21);
            this.DerivativesBox.Multiline = true;
            this.DerivativesBox.Name = "DerivativesBox";
            this.DerivativesBox.Size = new System.Drawing.Size(819, 375);
            this.DerivativesBox.TabIndex = 0;
            // 
            // MinimFormsPage
            // 
            this.MinimFormsPage.Controls.Add(this.MinimFormsBox);
            this.MinimFormsPage.Location = new System.Drawing.Point(4, 22);
            this.MinimFormsPage.Name = "MinimFormsPage";
            this.MinimFormsPage.Padding = new System.Windows.Forms.Padding(3);
            this.MinimFormsPage.Size = new System.Drawing.Size(849, 402);
            this.MinimFormsPage.TabIndex = 1;
            this.MinimFormsPage.Text = "Разложения по базисам";
            this.MinimFormsPage.UseVisualStyleBackColor = true;
            this.MinimFormsPage.Resize += new System.EventHandler(this.MinimFormsPage_Resize);
            // 
            // MinimFormsBox
            // 
            this.MinimFormsBox.Location = new System.Drawing.Point(9, 14);
            this.MinimFormsBox.Multiline = true;
            this.MinimFormsBox.Name = "MinimFormsBox";
            this.MinimFormsBox.Size = new System.Drawing.Size(834, 375);
            this.MinimFormsBox.TabIndex = 1;
            // 
            // TaylorPage
            // 
            this.TaylorPage.Controls.Add(this.TaylorBoxEqv);
            this.TaylorPage.Controls.Add(this.TaylorBoxXor);
            this.TaylorPage.Location = new System.Drawing.Point(4, 22);
            this.TaylorPage.Name = "TaylorPage";
            this.TaylorPage.Size = new System.Drawing.Size(849, 402);
            this.TaylorPage.TabIndex = 2;
            this.TaylorPage.Text = "Ряды Тейлора";
            this.TaylorPage.UseVisualStyleBackColor = true;
            // 
            // TaylorBoxEqv
            // 
            this.TaylorBoxEqv.Location = new System.Drawing.Point(391, 3);
            this.TaylorBoxEqv.Multiline = true;
            this.TaylorBoxEqv.Name = "TaylorBoxEqv";
            this.TaylorBoxEqv.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.TaylorBoxEqv.Size = new System.Drawing.Size(448, 380);
            this.TaylorBoxEqv.TabIndex = 7;
            // 
            // TaylorBoxXor
            // 
            this.TaylorBoxXor.Location = new System.Drawing.Point(3, 3);
            this.TaylorBoxXor.Multiline = true;
            this.TaylorBoxXor.Name = "TaylorBoxXor";
            this.TaylorBoxXor.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.TaylorBoxXor.Size = new System.Drawing.Size(382, 379);
            this.TaylorBoxXor.TabIndex = 8;
            // 
            // PostPage
            // 
            this.PostPage.Controls.Add(this.PostBox);
            this.PostPage.Location = new System.Drawing.Point(4, 22);
            this.PostPage.Name = "PostPage";
            this.PostPage.Size = new System.Drawing.Size(849, 402);
            this.PostPage.TabIndex = 3;
            this.PostPage.Text = "Критерии Поста";
            this.PostPage.UseVisualStyleBackColor = true;
            this.PostPage.Resize += new System.EventHandler(this.PostPage_Resize);
            // 
            // PostBox
            // 
            this.PostBox.Location = new System.Drawing.Point(9, 14);
            this.PostBox.Multiline = true;
            this.PostBox.Name = "PostBox";
            this.PostBox.Size = new System.Drawing.Size(837, 375);
            this.PostBox.TabIndex = 1;
            // 
            // ElemFuncPage
            // 
            this.ElemFuncPage.Controls.Add(this.ElemFuncBox);
            this.ElemFuncPage.Location = new System.Drawing.Point(4, 22);
            this.ElemFuncPage.Name = "ElemFuncPage";
            this.ElemFuncPage.Size = new System.Drawing.Size(849, 402);
            this.ElemFuncPage.TabIndex = 4;
            this.ElemFuncPage.Text = "Разложение бинарных функций по F";
            this.ElemFuncPage.UseVisualStyleBackColor = true;
            this.ElemFuncPage.Resize += new System.EventHandler(this.ElemFunc_Resize);
            // 
            // ElemFuncBox
            // 
            this.ElemFuncBox.Location = new System.Drawing.Point(9, 14);
            this.ElemFuncBox.Multiline = true;
            this.ElemFuncBox.Name = "ElemFuncBox";
            this.ElemFuncBox.Size = new System.Drawing.Size(837, 375);
            this.ElemFuncBox.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(917, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьРезультатыToolStripMenuItem,
            this.добавитьБазисВБиблиотекуToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьРезультатыToolStripMenuItem
            // 
            this.сохранитьРезультатыToolStripMenuItem.Name = "сохранитьРезультатыToolStripMenuItem";
            this.сохранитьРезультатыToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.сохранитьРезультатыToolStripMenuItem.Text = "Сохранить результаты";
            this.сохранитьРезультатыToolStripMenuItem.Click += new System.EventHandler(this.сохранитьРезультатыToolStripMenuItem_Click);
            // 
            // добавитьБазисВБиблиотекуToolStripMenuItem
            // 
            this.добавитьБазисВБиблиотекуToolStripMenuItem.Name = "добавитьБазисВБиблиотекуToolStripMenuItem";
            this.добавитьБазисВБиблиотекуToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.добавитьБазисВБиблиотекуToolStripMenuItem.Text = "Добавить базис в библиотеку";
            this.добавитьБазисВБиблиотекуToolStripMenuItem.Click += new System.EventHandler(this.добавитьБазисВБиблиотекуToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(234, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.helpToolStripMenuItem.Text = "Помощь";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // truthTableBox
            // 
            this.truthTableBox.Location = new System.Drawing.Point(96, 30);
            this.truthTableBox.Name = "truthTableBox";
            this.truthTableBox.Size = new System.Drawing.Size(138, 20);
            this.truthTableBox.TabIndex = 2;
            this.truthTableBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.truthTableBox_KeyDown);
            // 
            // btnSoluteMarix
            // 
            this.btnSoluteMarix.Location = new System.Drawing.Point(738, 27);
            this.btnSoluteMarix.Name = "btnSoluteMarix";
            this.btnSoluteMarix.Size = new System.Drawing.Size(127, 23);
            this.btnSoluteMarix.TabIndex = 3;
            this.btnSoluteMarix.Text = "Решить уравнение";
            this.btnSoluteMarix.UseVisualStyleBackColor = true;
            this.btnSoluteMarix.Click += new System.EventHandler(this.btnSoluteMarix_Click);
            // 
            // SoluteAll
            // 
            this.SoluteAll.Location = new System.Drawing.Point(240, 27);
            this.SoluteAll.Name = "SoluteAll";
            this.SoluteAll.Size = new System.Drawing.Size(75, 23);
            this.SoluteAll.TabIndex = 4;
            this.SoluteAll.Text = "Решить все";
            this.SoluteAll.UseVisualStyleBackColor = true;
            this.SoluteAll.Click += new System.EventHandler(this.SoluteAll_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStatus.Location = new System.Drawing.Point(333, 27);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 24);
            this.labelStatus.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Введите\nдвоичный код";
            // 
            // MainForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 487);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.SoluteAll);
            this.Controls.Add(this.btnSoluteMarix);
            this.Controls.Add(this.truthTableBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "АнтиДискра™ V1.1.0.0 by Angelicos Phosphoros";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl1.ResumeLayout(false);
            this.DerivativePage.ResumeLayout(false);
            this.DerivativePage.PerformLayout();
            this.MinimFormsPage.ResumeLayout(false);
            this.MinimFormsPage.PerformLayout();
            this.TaylorPage.ResumeLayout(false);
            this.TaylorPage.PerformLayout();
            this.PostPage.ResumeLayout(false);
            this.PostPage.PerformLayout();
            this.ElemFuncPage.ResumeLayout(false);
            this.ElemFuncPage.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DerivativePage;
        private System.Windows.Forms.TabPage MinimFormsPage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox truthTableBox;
        private System.Windows.Forms.Button btnSoluteMarix;
        private System.Windows.Forms.TextBox DerivativesBox;
        private System.Windows.Forms.TabPage TaylorPage;
        private System.Windows.Forms.TabPage PostPage;
        private System.Windows.Forms.TabPage ElemFuncPage;
        private System.Windows.Forms.Button SoluteAll;
        private System.Windows.Forms.TextBox MinimFormsBox;
        private System.Windows.Forms.TextBox PostBox;
        private System.Windows.Forms.TextBox ElemFuncBox;
        private System.Windows.Forms.ToolStripMenuItem добавитьБазисВБиблиотекуToolStripMenuItem;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьРезультатыToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox TaylorBoxEqv;
        private System.Windows.Forms.TextBox TaylorBoxXor;
        private System.Windows.Forms.Label label1;

    }
}

