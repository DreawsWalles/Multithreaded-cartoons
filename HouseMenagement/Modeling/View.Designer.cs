namespace Modeling
{
    partial class View
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxCommittee = new System.Windows.Forms.RichTextBox();
            this.richTextBoxDispatcher = new System.Windows.Forms.RichTextBox();
            this.labelDispatcher = new System.Windows.Forms.Label();
            this.labelCommittee = new System.Windows.Forms.Label();
            this.richTextBoxDepartments = new System.Windows.Forms.RichTextBox();
            this.labelDepartments = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonStart = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelCanvas.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxCommittee
            // 
            this.richTextBoxCommittee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxCommittee.Location = new System.Drawing.Point(16, 73);
            this.richTextBoxCommittee.Name = "richTextBoxCommittee";
            this.richTextBoxCommittee.Size = new System.Drawing.Size(369, 605);
            this.richTextBoxCommittee.TabIndex = 3;
            this.richTextBoxCommittee.Text = "";
            // 
            // richTextBoxDispatcher
            // 
            this.richTextBoxDispatcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxDispatcher.Location = new System.Drawing.Point(411, 73);
            this.richTextBoxDispatcher.Name = "richTextBoxDispatcher";
            this.richTextBoxDispatcher.Size = new System.Drawing.Size(369, 605);
            this.richTextBoxDispatcher.TabIndex = 3;
            this.richTextBoxDispatcher.Text = "";
            // 
            // labelDispatcher
            // 
            this.labelDispatcher.AutoSize = true;
            this.labelDispatcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDispatcher.Location = new System.Drawing.Point(545, 26);
            this.labelDispatcher.Name = "labelDispatcher";
            this.labelDispatcher.Size = new System.Drawing.Size(109, 24);
            this.labelDispatcher.TabIndex = 4;
            this.labelDispatcher.Text = "Диспетчер";
            // 
            // labelCommittee
            // 
            this.labelCommittee.AutoSize = true;
            this.labelCommittee.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCommittee.Location = new System.Drawing.Point(118, 26);
            this.labelCommittee.Name = "labelCommittee";
            this.labelCommittee.Size = new System.Drawing.Size(135, 24);
            this.labelCommittee.TabIndex = 4;
            this.labelCommittee.Text = "Приём заявок";
            // 
            // richTextBoxDepartments
            // 
            this.richTextBoxDepartments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxDepartments.Location = new System.Drawing.Point(807, 73);
            this.richTextBoxDepartments.Name = "richTextBoxDepartments";
            this.richTextBoxDepartments.Size = new System.Drawing.Size(369, 605);
            this.richTextBoxDepartments.TabIndex = 3;
            this.richTextBoxDepartments.Text = "";
            // 
            // labelDepartments
            // 
            this.labelDepartments.AutoSize = true;
            this.labelDepartments.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDepartments.Location = new System.Drawing.Point(939, 26);
            this.labelDepartments.Name = "labelDepartments";
            this.labelDepartments.Size = new System.Drawing.Size(81, 24);
            this.labelDepartments.TabIndex = 4;
            this.labelDepartments.Text = "Отделы";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Location = new System.Drawing.Point(0, 1);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1193, 744);
            this.tabControlMain.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelCanvas);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1185, 718);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Анимация";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelCanvas
            // 
            this.panelCanvas.Controls.Add(this.buttonStart);
            this.panelCanvas.Location = new System.Drawing.Point(8, 6);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(1169, 706);
            this.panelCanvas.TabIndex = 0;
//            this.panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCanvas_Paint);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.labelCommittee);
            this.tabPage2.Controls.Add(this.labelDepartments);
            this.tabPage2.Controls.Add(this.richTextBoxCommittee);
            this.tabPage2.Controls.Add(this.richTextBoxDepartments);
            this.tabPage2.Controls.Add(this.richTextBoxDispatcher);
            this.tabPage2.Controls.Add(this.labelDispatcher);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1185, 718);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Логика";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(517, 643);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(124, 43);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Начать";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 744);
            this.Controls.Add(this.tabControlMain);
            this.DoubleBuffered = true;
            this.Name = "View";
            this.Text = "Модель";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.View_FormClosed);
            this.Load += new System.EventHandler(this.View_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelCanvas.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxCommittee;
        private System.Windows.Forms.RichTextBox richTextBoxDispatcher;
        private System.Windows.Forms.Label labelDispatcher;
        private System.Windows.Forms.Label labelCommittee;
        private System.Windows.Forms.RichTextBox richTextBoxDepartments;
        private System.Windows.Forms.Label labelDepartments;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.Button buttonStart;
    }
}

