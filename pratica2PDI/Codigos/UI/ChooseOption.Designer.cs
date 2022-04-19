namespace pratica2PDI.Codigos.UI
{
    partial class ChooseOption
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
            this.title = new System.Windows.Forms.Label();
            this.channelsOptions = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(12, 12);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(221, 15);
            this.title.TabIndex = 1;
            this.title.Text = "title";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // channelsOptions
            // 
            this.channelsOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelsOptions.FormattingEnabled = true;
            this.channelsOptions.Location = new System.Drawing.Point(12, 27);
            this.channelsOptions.Name = "channelsOptions";
            this.channelsOptions.Size = new System.Drawing.Size(221, 23);
            this.channelsOptions.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 25);
            this.button1.TabIndex = 3;
            this.button1.Text = "Escolher";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChooseOption
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(245, 96);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.channelsOptions);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChooseOption";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChoseChannel";
            this.ResumeLayout(false);

        }

        #endregion
        private Label title;
        private ComboBox channelsOptions;
        private Button button1;
    }
}