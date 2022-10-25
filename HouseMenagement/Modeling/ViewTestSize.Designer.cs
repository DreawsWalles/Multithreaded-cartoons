namespace Modeling
{
    partial class ViewTestSize
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
            this.comboBoxTestSize = new System.Windows.Forms.ComboBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.labelTestSize = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxTestSize
            // 
            this.comboBoxTestSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTestSize.FormattingEnabled = true;
            this.comboBoxTestSize.Location = new System.Drawing.Point(28, 57);
            this.comboBoxTestSize.Name = "comboBoxTestSize";
            this.comboBoxTestSize.Size = new System.Drawing.Size(237, 21);
            this.comboBoxTestSize.TabIndex = 0;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(108, 96);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 1;
            this.buttonSubmit.Text = "Готово";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // labelTestSize
            // 
            this.labelTestSize.AutoSize = true;
            this.labelTestSize.Location = new System.Drawing.Point(25, 32);
            this.labelTestSize.Name = "labelTestSize";
            this.labelTestSize.Size = new System.Drawing.Size(240, 13);
            this.labelTestSize.TabIndex = 2;
            this.labelTestSize.Text = "Выберите размер симуляции (кол-во заявок):";
            // 
            // ViewTestSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 139);
            this.Controls.Add(this.labelTestSize);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.comboBoxTestSize);
            this.Name = "ViewTestSize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Размер симуляции";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTestSize;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label labelTestSize;
    }
}