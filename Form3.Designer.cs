namespace WindowsFormsApplication1
{
    partial class AddBasisForm
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
            this.TableBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MaskBox = new System.Windows.Forms.TextBox();
            this.btnAddFunc = new System.Windows.Forms.Button();
            this.BasisBox = new System.Windows.Forms.TextBox();
            this.btnAddBasis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TableBox
            // 
            this.TableBox.Location = new System.Drawing.Point(142, 37);
            this.TableBox.Name = "TableBox";
            this.TableBox.Size = new System.Drawing.Size(123, 20);
            this.TableBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Таблица истинности";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Маска функции:";
            // 
            // MaskBox
            // 
            this.MaskBox.Location = new System.Drawing.Point(142, 68);
            this.MaskBox.Name = "MaskBox";
            this.MaskBox.Size = new System.Drawing.Size(123, 20);
            this.MaskBox.TabIndex = 3;
            // 
            // btnAddFunc
            // 
            this.btnAddFunc.Location = new System.Drawing.Point(142, 107);
            this.btnAddFunc.Name = "btnAddFunc";
            this.btnAddFunc.Size = new System.Drawing.Size(123, 23);
            this.btnAddFunc.TabIndex = 4;
            this.btnAddFunc.Text = "Добавить функцию";
            this.btnAddFunc.UseVisualStyleBackColor = true;
            this.btnAddFunc.Click += new System.EventHandler(this.button1_Click);
            // 
            // BasisBox
            // 
            this.BasisBox.Location = new System.Drawing.Point(27, 162);
            this.BasisBox.Multiline = true;
            this.BasisBox.Name = "BasisBox";
            this.BasisBox.Size = new System.Drawing.Size(237, 188);
            this.BasisBox.TabIndex = 5;
            // 
            // btnAddBasis
            // 
            this.btnAddBasis.Location = new System.Drawing.Point(74, 356);
            this.btnAddBasis.Name = "btnAddBasis";
            this.btnAddBasis.Size = new System.Drawing.Size(138, 23);
            this.btnAddBasis.TabIndex = 6;
            this.btnAddBasis.Text = "добавить базис";
            this.btnAddBasis.UseVisualStyleBackColor = true;
            this.btnAddBasis.Click += new System.EventHandler(this.btnAddBasis_Click);
            // 
            // AddBasisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 390);
            this.Controls.Add(this.btnAddBasis);
            this.Controls.Add(this.BasisBox);
            this.Controls.Add(this.btnAddFunc);
            this.Controls.Add(this.MaskBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TableBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AddBasisForm";
            this.Text = "Добавление базиса в библиотеку";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TableBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MaskBox;
        private System.Windows.Forms.Button btnAddFunc;
        private System.Windows.Forms.TextBox BasisBox;
        private System.Windows.Forms.Button btnAddBasis;
    }
}