namespace Lukujärjestys
{
    partial class Form1
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
            this.LukujarjestysDGV = new System.Windows.Forms.DataGridView();
            this.LabelViikko = new System.Windows.Forms.Label();
            this.ButtonEdellinen = new System.Windows.Forms.Button();
            this.ButtonSeuraava = new System.Windows.Forms.Button();
            this.ButtonPaivita = new System.Windows.Forms.Button();
            this.Puu = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonSovita = new System.Windows.Forms.Button();
            this.ButtonApu = new System.Windows.Forms.Button();
            this.LabelPaivitys = new System.Windows.Forms.Label();
            this.ButtonSelaa = new System.Windows.Forms.Button();
            this.TextBoxEtsi = new System.Windows.Forms.TextBox();
            this.CheckBoxOmat = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LabelNimi = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LukujarjestysDGV)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LukujarjestysDGV
            // 
            this.LukujarjestysDGV.AllowUserToAddRows = false;
            this.LukujarjestysDGV.AllowUserToDeleteRows = false;
            this.LukujarjestysDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LukujarjestysDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LukujarjestysDGV.Location = new System.Drawing.Point(6, 44);
            this.LukujarjestysDGV.Name = "LukujarjestysDGV";
            this.LukujarjestysDGV.ReadOnly = true;
            this.LukujarjestysDGV.Size = new System.Drawing.Size(996, 562);
            this.LukujarjestysDGV.TabIndex = 0;
            this.LukujarjestysDGV.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.LukujarjestysDGV_CellMouseClick);
            this.LukujarjestysDGV.SelectionChanged += new System.EventHandler(this.LukujarjestysDGV_SelectionChanged);
            // 
            // LabelViikko
            // 
            this.LabelViikko.AutoSize = true;
            this.LabelViikko.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelViikko.Location = new System.Drawing.Point(155, 12);
            this.LabelViikko.Name = "LabelViikko";
            this.LabelViikko.Size = new System.Drawing.Size(73, 20);
            this.LabelViikko.TabIndex = 1;
            this.LabelViikko.Text = "Viikko 00";
            this.LabelViikko.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonEdellinen
            // 
            this.ButtonEdellinen.Location = new System.Drawing.Point(79, 13);
            this.ButtonEdellinen.Name = "ButtonEdellinen";
            this.ButtonEdellinen.Size = new System.Drawing.Size(70, 22);
            this.ButtonEdellinen.TabIndex = 2;
            this.ButtonEdellinen.Text = "Edellinen";
            this.ButtonEdellinen.UseVisualStyleBackColor = true;
            this.ButtonEdellinen.Click += new System.EventHandler(this.ButtonEdellinen_Click);
            // 
            // ButtonSeuraava
            // 
            this.ButtonSeuraava.Location = new System.Drawing.Point(234, 13);
            this.ButtonSeuraava.Name = "ButtonSeuraava";
            this.ButtonSeuraava.Size = new System.Drawing.Size(70, 22);
            this.ButtonSeuraava.TabIndex = 3;
            this.ButtonSeuraava.Text = "Seuraava";
            this.ButtonSeuraava.UseVisualStyleBackColor = true;
            this.ButtonSeuraava.Click += new System.EventHandler(this.ButtonSeuraava_Click);
            // 
            // ButtonPaivita
            // 
            this.ButtonPaivita.Location = new System.Drawing.Point(6, 13);
            this.ButtonPaivita.Name = "ButtonPaivita";
            this.ButtonPaivita.Size = new System.Drawing.Size(70, 22);
            this.ButtonPaivita.TabIndex = 5;
            this.ButtonPaivita.Text = "Päivitä";
            this.ButtonPaivita.UseVisualStyleBackColor = true;
            this.ButtonPaivita.Click += new System.EventHandler(this.ButtonPaivita_Click);
            // 
            // Puu
            // 
            this.Puu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Puu.Location = new System.Drawing.Point(3, 44);
            this.Puu.Name = "Puu";
            this.Puu.ShowNodeToolTips = true;
            this.Puu.Size = new System.Drawing.Size(210, 533);
            this.Puu.TabIndex = 7;
            this.Puu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Puu_NodeMouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ButtonSovita);
            this.panel1.Controls.Add(this.ButtonApu);
            this.panel1.Controls.Add(this.LabelPaivitys);
            this.panel1.Controls.Add(this.ButtonSeuraava);
            this.panel1.Controls.Add(this.ButtonSelaa);
            this.panel1.Controls.Add(this.LabelViikko);
            this.panel1.Controls.Add(this.ButtonEdellinen);
            this.panel1.Controls.Add(this.ButtonPaivita);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 35);
            this.panel1.TabIndex = 8;
            // 
            // ButtonSovita
            // 
            this.ButtonSovita.Location = new System.Drawing.Point(819, 13);
            this.ButtonSovita.Name = "ButtonSovita";
            this.ButtonSovita.Size = new System.Drawing.Size(75, 22);
            this.ButtonSovita.TabIndex = 14;
            this.ButtonSovita.Text = "Sovita solut";
            this.ButtonSovita.UseVisualStyleBackColor = true;
            this.ButtonSovita.Click += new System.EventHandler(this.ButtonSovita_Click);
            // 
            // ButtonApu
            // 
            this.ButtonApu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonApu.Location = new System.Drawing.Point(900, 13);
            this.ButtonApu.Name = "ButtonApu";
            this.ButtonApu.Size = new System.Drawing.Size(23, 22);
            this.ButtonApu.TabIndex = 13;
            this.ButtonApu.Text = "?";
            this.ButtonApu.UseVisualStyleBackColor = true;
            this.ButtonApu.Click += new System.EventHandler(this.ButtonApu_Click);
            this.ButtonApu.Leave += new System.EventHandler(this.ButtonApu_Leave);
            // 
            // LabelPaivitys
            // 
            this.LabelPaivitys.AutoSize = true;
            this.LabelPaivitys.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPaivitys.Location = new System.Drawing.Point(386, 13);
            this.LabelPaivitys.Name = "LabelPaivitys";
            this.LabelPaivitys.Size = new System.Drawing.Size(0, 20);
            this.LabelPaivitys.TabIndex = 12;
            // 
            // ButtonSelaa
            // 
            this.ButtonSelaa.Location = new System.Drawing.Point(929, 13);
            this.ButtonSelaa.Name = "ButtonSelaa";
            this.ButtonSelaa.Size = new System.Drawing.Size(70, 22);
            this.ButtonSelaa.TabIndex = 11;
            this.ButtonSelaa.Text = "Selaa";
            this.ButtonSelaa.UseVisualStyleBackColor = true;
            this.ButtonSelaa.Click += new System.EventHandler(this.ButtonSelaa_Click);
            // 
            // TextBoxEtsi
            // 
            this.TextBoxEtsi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxEtsi.Location = new System.Drawing.Point(3, 15);
            this.TextBoxEtsi.Name = "TextBoxEtsi";
            this.TextBoxEtsi.Size = new System.Drawing.Size(210, 20);
            this.TextBoxEtsi.TabIndex = 9;
            this.TextBoxEtsi.Enter += new System.EventHandler(this.TextBoxEtsi_Enter);
            this.TextBoxEtsi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEtsi_KeyDown);
            // 
            // CheckBoxOmat
            // 
            this.CheckBoxOmat.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxOmat.AutoSize = true;
            this.CheckBoxOmat.Location = new System.Drawing.Point(3, 583);
            this.CheckBoxOmat.Name = "CheckBoxOmat";
            this.CheckBoxOmat.Size = new System.Drawing.Size(94, 23);
            this.CheckBoxOmat.TabIndex = 10;
            this.CheckBoxOmat.Text = "Näytä vain omat";
            this.CheckBoxOmat.UseVisualStyleBackColor = true;
            this.CheckBoxOmat.CheckedChanged += new System.EventHandler(this.RadioOmat_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.CheckBoxOmat);
            this.splitContainer1.Panel1.Controls.Add(this.TextBoxEtsi);
            this.splitContainer1.Panel1.Controls.Add(this.Puu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LabelNimi);
            this.splitContainer1.Panel2.Controls.Add(this.LukujarjestysDGV);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1234, 618);
            this.splitContainer1.SplitterDistance = 216;
            this.splitContainer1.TabIndex = 12;
            // 
            // LabelNimi
            // 
            this.LabelNimi.AutoSize = true;
            this.LabelNimi.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNimi.Location = new System.Drawing.Point(927, 606);
            this.LabelNimi.Name = "LabelNimi";
            this.LabelNimi.Size = new System.Drawing.Size(75, 12);
            this.LabelNimi.TabIndex = 9;
            this.LabelNimi.Text = "Jouni Kortelainen";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 618);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.LukujarjestysDGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView LukujarjestysDGV;
        private System.Windows.Forms.Label LabelViikko;
        private System.Windows.Forms.Button ButtonEdellinen;
        private System.Windows.Forms.Button ButtonSeuraava;
        private System.Windows.Forms.Button ButtonPaivita;
        private System.Windows.Forms.TreeView Puu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TextBoxEtsi;
        private System.Windows.Forms.CheckBox CheckBoxOmat;
        private System.Windows.Forms.Button ButtonSelaa;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label LabelPaivitys;
        private System.Windows.Forms.Label LabelNimi;
        private System.Windows.Forms.Button ButtonApu;
        private System.Windows.Forms.Button ButtonSovita;
    }
}

