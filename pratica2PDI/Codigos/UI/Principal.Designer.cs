namespace pratica2PDI.Codigos.UI
{
    partial class Principal
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
            this.OpenImageButton = new System.Windows.Forms.Button();
            this.DiretorioText = new System.Windows.Forms.Label();
            this.Teste = new System.Windows.Forms.Button();
            this.erodeTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.estruturanteTamN = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.estruturanteTamM = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.contarQuadradosVermelhos = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.BackColor = System.Drawing.Color.ForestGreen;
            this.OpenImageButton.FlatAppearance.BorderSize = 0;
            this.OpenImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenImageButton.ForeColor = System.Drawing.Color.Black;
            this.OpenImageButton.Location = new System.Drawing.Point(15, 55);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(102, 23);
            this.OpenImageButton.TabIndex = 0;
            this.OpenImageButton.Text = "Abrir Imagem";
            this.OpenImageButton.UseVisualStyleBackColor = false;
            this.OpenImageButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // DiretorioText
            // 
            this.DiretorioText.AutoSize = true;
            this.DiretorioText.ForeColor = System.Drawing.Color.White;
            this.DiretorioText.Location = new System.Drawing.Point(123, 59);
            this.DiretorioText.Name = "DiretorioText";
            this.DiretorioText.Size = new System.Drawing.Size(0, 15);
            this.DiretorioText.TabIndex = 1;
            // 
            // Teste
            // 
            this.Teste.BackColor = System.Drawing.Color.White;
            this.Teste.FlatAppearance.BorderSize = 0;
            this.Teste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Teste.Location = new System.Drawing.Point(15, 200);
            this.Teste.Name = "Teste";
            this.Teste.Size = new System.Drawing.Size(125, 24);
            this.Teste.TabIndex = 2;
            this.Teste.Text = "Preencher Buracos";
            this.Teste.UseVisualStyleBackColor = false;
            this.Teste.Click += new System.EventHandler(this.preencherBuracosButton_click);
            // 
            // erodeTest
            // 
            this.erodeTest.BackColor = System.Drawing.Color.White;
            this.erodeTest.FlatAppearance.BorderSize = 0;
            this.erodeTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.erodeTest.Location = new System.Drawing.Point(15, 133);
            this.erodeTest.Name = "erodeTest";
            this.erodeTest.Size = new System.Drawing.Size(102, 23);
            this.erodeTest.TabIndex = 3;
            this.erodeTest.Text = "Remover Pontos";
            this.erodeTest.UseVisualStyleBackColor = false;
            this.erodeTest.Click += new System.EventHandler(this.removerPontos);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(174, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "dim. Estruturante(M)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(311, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "dim. Estruturante(N)";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(15, 257);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Fecho Convexo";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.fechoConvexoButton);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(161, 257);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(102, 23);
            this.button4.TabIndex = 16;
            this.button4.Text = "Esqueleto";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.skeletonButton_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label4.Location = new System.Drawing.Point(78, 29);
            this.label4.MaximumSize = new System.Drawing.Size(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(446, 1);
            this.label4.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.contarQuadradosVermelhos);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.estruturanteTamN);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.estruturanteTamM);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.OpenImageButton);
            this.panel1.Controls.Add(this.DiretorioText);
            this.panel1.Controls.Add(this.Teste);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.erodeTest);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 362);
            this.panel1.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label17.Location = new System.Drawing.Point(15, 240);
            this.label17.MaximumSize = new System.Drawing.Size(0, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(509, 1);
            this.label17.TabIndex = 34;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label16.Location = new System.Drawing.Point(15, 182);
            this.label16.MaximumSize = new System.Drawing.Size(0, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(509, 1);
            this.label16.TabIndex = 33;
            // 
            // estruturanteTamN
            // 
            this.estruturanteTamN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.estruturanteTamN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.estruturanteTamN.ForeColor = System.Drawing.Color.White;
            this.estruturanteTamN.Location = new System.Drawing.Point(311, 151);
            this.estruturanteTamN.Name = "estruturanteTamN";
            this.estruturanteTamN.PlaceholderText = "6";
            this.estruturanteTamN.Size = new System.Drawing.Size(90, 16);
            this.estruturanteTamN.TabIndex = 29;
            this.estruturanteTamN.Text = "14";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label13.Location = new System.Drawing.Point(297, 127);
            this.label13.MaximumSize = new System.Drawing.Size(1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 40);
            this.label13.TabIndex = 28;
            // 
            // estruturanteTamM
            // 
            this.estruturanteTamM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.estruturanteTamM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.estruturanteTamM.ForeColor = System.Drawing.Color.White;
            this.estruturanteTamM.Location = new System.Drawing.Point(174, 151);
            this.estruturanteTamM.Name = "estruturanteTamM";
            this.estruturanteTamM.PlaceholderText = "3";
            this.estruturanteTamM.Size = new System.Drawing.Size(95, 16);
            this.estruturanteTamM.TabIndex = 27;
            this.estruturanteTamM.Text = "15";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label12.Location = new System.Drawing.Point(161, 127);
            this.label12.MaximumSize = new System.Drawing.Size(1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 40);
            this.label12.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label11.Location = new System.Drawing.Point(161, 200);
            this.label11.MaximumSize = new System.Drawing.Size(1, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 25);
            this.label11.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(188, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "Limitar iterações à (opcional):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(15, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 19);
            this.label9.TabIndex = 23;
            this.label9.Text = "Processamento Morfologico";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label10.Location = new System.Drawing.Point(201, 103);
            this.label10.MaximumSize = new System.Drawing.Size(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(323, 1);
            this.label10.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(15, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 19);
            this.label7.TabIndex = 21;
            this.label7.Text = "Arquivo";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(357, 205);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(90, 16);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(28, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 19);
            this.label6.TabIndex = 20;
            this.label6.Text = "Prática 2";
            // 
            // contarQuadradosVermelhos
            // 
            this.contarQuadradosVermelhos.BackColor = System.Drawing.Color.White;
            this.contarQuadradosVermelhos.FlatAppearance.BorderSize = 0;
            this.contarQuadradosVermelhos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.contarQuadradosVermelhos.ForeColor = System.Drawing.Color.Black;
            this.contarQuadradosVermelhos.Location = new System.Drawing.Point(297, 257);
            this.contarQuadradosVermelhos.Name = "contarQuadradosVermelhos";
            this.contarQuadradosVermelhos.Size = new System.Drawing.Size(118, 23);
            this.contarQuadradosVermelhos.TabIndex = 35;
            this.contarQuadradosVermelhos.Text = "Contar Vermelhos";
            this.contarQuadradosVermelhos.UseVisualStyleBackColor = false;
            this.contarQuadradosVermelhos.Click += new System.EventHandler(this.contarQuadradosVermelhos_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(570, 379);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button OpenImageButton;
        private Label DiretorioText;
        private Button Teste;
        private Button erodeTest;
        private Label label1;
        private Label label2;
        private Button button3;
        private Button button4;
        private Label label4;
        private Panel panel1;
        private Label label6;
        private Label label11;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label7;
        private TextBox textBox2;
        private TextBox estruturanteTamN;
        private Label label13;
        private TextBox estruturanteTamM;
        private Label label12;
        private Label label16;
        private Label label17;
        private Button contarQuadradosVermelhos;
    }
}