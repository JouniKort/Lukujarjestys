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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LukujarjestysDGV = new System.Windows.Forms.DataGridView();
            this.Puu = new System.Windows.Forms.TreeView();
            this.ButtonApu = new System.Windows.Forms.Button();
            this.TextBoxEtsi = new System.Windows.Forms.TextBox();
            this.CheckBoxOmat = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ButtonPoistaKaikki = new System.Windows.Forms.Button();
            this.LabelNimi = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.LabelViikko = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.LukujarjestysDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.LukujarjestysDGV.Location = new System.Drawing.Point(6, 0);
            this.LukujarjestysDGV.Name = "LukujarjestysDGV";
            this.LukujarjestysDGV.ReadOnly = true;
            this.LukujarjestysDGV.Size = new System.Drawing.Size(996, 621);
            this.LukujarjestysDGV.TabIndex = 0;
            this.LukujarjestysDGV.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.LukujarjestysDGV_CellMouseClick);
            this.LukujarjestysDGV.SelectionChanged += new System.EventHandler(this.LukujarjestysDGV_SelectionChanged);
            // 
            // Puu
            // 
            this.Puu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Puu.Location = new System.Drawing.Point(3, 29);
            this.Puu.Name = "Puu";
            this.Puu.ShowNodeToolTips = true;
            this.Puu.Size = new System.Drawing.Size(210, 575);
            this.Puu.TabIndex = 7;
            this.Puu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Puu_NodeMouseClick);
            // 
            // ButtonApu
            // 
            this.ButtonApu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonApu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonApu.Location = new System.Drawing.Point(1199, 3);
            this.ButtonApu.Name = "ButtonApu";
            this.ButtonApu.Size = new System.Drawing.Size(23, 22);
            this.ButtonApu.TabIndex = 13;
            this.ButtonApu.Text = "?";
            this.ButtonApu.UseVisualStyleBackColor = true;
            this.ButtonApu.Click += new System.EventHandler(this.ButtonApu_Click);
            this.ButtonApu.Leave += new System.EventHandler(this.ButtonApu_Leave);
            // 
            // TextBoxEtsi
            // 
            this.TextBoxEtsi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxEtsi.Location = new System.Drawing.Point(3, 3);
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
            this.CheckBoxOmat.Location = new System.Drawing.Point(3, 610);
            this.CheckBoxOmat.Name = "CheckBoxOmat";
            this.CheckBoxOmat.Size = new System.Drawing.Size(94, 23);
            this.CheckBoxOmat.TabIndex = 10;
            this.CheckBoxOmat.Text = "Näytä vain omat";
            this.CheckBoxOmat.UseVisualStyleBackColor = true;
            this.CheckBoxOmat.CheckedChanged += new System.EventHandler(this.RadioOmat_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ButtonPoistaKaikki);
            this.splitContainer1.Panel1.Controls.Add(this.CheckBoxOmat);
            this.splitContainer1.Panel1.Controls.Add(this.TextBoxEtsi);
            this.splitContainer1.Panel1.Controls.Add(this.Puu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LabelNimi);
            this.splitContainer1.Panel2.Controls.Add(this.LukujarjestysDGV);
            this.splitContainer1.Size = new System.Drawing.Size(1234, 636);
            this.splitContainer1.SplitterDistance = 216;
            this.splitContainer1.TabIndex = 12;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // ButtonPoistaKaikki
            // 
            this.ButtonPoistaKaikki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPoistaKaikki.Location = new System.Drawing.Point(119, 610);
            this.ButtonPoistaKaikki.Name = "ButtonPoistaKaikki";
            this.ButtonPoistaKaikki.Size = new System.Drawing.Size(94, 23);
            this.ButtonPoistaKaikki.TabIndex = 10;
            this.ButtonPoistaKaikki.Text = "Poista kaikki";
            this.ButtonPoistaKaikki.UseVisualStyleBackColor = true;
            this.ButtonPoistaKaikki.Click += new System.EventHandler(this.ButtonPoistaKaikki_Click);
            // 
            // LabelNimi
            // 
            this.LabelNimi.AutoSize = true;
            this.LabelNimi.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNimi.Location = new System.Drawing.Point(927, 624);
            this.LabelNimi.Name = "LabelNimi";
            this.LabelNimi.Size = new System.Drawing.Size(75, 12);
            this.LabelNimi.TabIndex = 9;
            this.LabelNimi.Text = "Jouni Kortelainen";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripProgressBar1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.LabelViikko,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton4,
            this.toolStripSeparator3,
            this.toolStripButton6,
            this.toolStripSeparator5,
            this.toolStripButton7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1234, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(46, 22);
            this.toolStripButton1.Text = "Päivitä";
            this.toolStripButton1.Click += new System.EventHandler(this.ButtonPaivita_Click);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Maximum = 12;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 22);
            this.toolStripProgressBar1.Step = 1;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(59, 22);
            this.toolStripButton2.Text = "Edellinen";
            this.toolStripButton2.Click += new System.EventHandler(this.ButtonEdellinen_Click);
            // 
            // LabelViikko
            // 
            this.LabelViikko.Name = "LabelViikko";
            this.LabelViikko.Size = new System.Drawing.Size(54, 22);
            this.LabelViikko.Text = "Viikko 00";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(58, 22);
            this.toolStripButton3.Text = "Seuraava";
            this.toolStripButton3.Click += new System.EventHandler(this.ButtonSeuraava_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(72, 22);
            this.toolStripButton4.Text = "Sovita solut";
            this.toolStripButton4.Click += new System.EventHandler(this.ButtonSovita_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(38, 22);
            this.toolStripButton6.Text = "Selaa";
            this.toolStripButton6.Click += new System.EventHandler(this.ButtonSelaa_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(58, 22);
            this.toolStripButton7.Text = "Ota kuva";
            this.toolStripButton7.Click += new System.EventHandler(this.ButtonKuva_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 667);
            this.Controls.Add(this.ButtonApu);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.LukujarjestysDGV)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView LukujarjestysDGV;
        private System.Windows.Forms.TreeView Puu;
        private System.Windows.Forms.TextBox TextBoxEtsi;
        private System.Windows.Forms.CheckBox CheckBoxOmat;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label LabelNimi;
        private System.Windows.Forms.Button ButtonApu;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripLabel LabelViikko;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.Button ButtonPoistaKaikki;
    }
}

