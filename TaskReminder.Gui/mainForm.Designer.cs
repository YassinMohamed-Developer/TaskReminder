namespace TaskReminder.Gui
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addBtn = new Button();
            SuspendLayout();
            // 
            // addBtn
            // 
            addBtn.Cursor = Cursors.Hand;
            addBtn.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            addBtn.Location = new Point(30, 380);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(110, 44);
            addBtn.TabIndex = 0;
            addBtn.Text = "Add Task";
            addBtn.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(addBtn);
            Name = "mainForm";
            Text = "Task Reminder";
            ResumeLayout(false);
        }

        #endregion

        private Button addBtn;
    }
}
