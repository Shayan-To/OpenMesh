﻿namespace OpenMesh
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
             System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
             this.menuStrip1 = new System.Windows.Forms.MenuStrip();
             this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
             this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
             this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
             this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
             this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
             this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
             this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
             this.panel3 = new System.Windows.Forms.Panel();
             this.trackBar1 = new System.Windows.Forms.TrackBar();
             this.button4 = new System.Windows.Forms.Button();
             this.button3 = new System.Windows.Forms.Button();
             this.button7 = new System.Windows.Forms.Button();
             this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
             this.button8 = new System.Windows.Forms.Button();
             this.tabControl1 = new System.Windows.Forms.TabControl();
             this.tabPage1 = new System.Windows.Forms.TabPage();
             this.tabPage2 = new System.Windows.Forms.TabPage();
             this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
             this.textBox1 = new System.Windows.Forms.TextBox();
             this.panel2 = new System.Windows.Forms.Panel();
             this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
             this.listBox1 = new System.Windows.Forms.ListBox();
             this.groupBox2 = new System.Windows.Forms.GroupBox();
             this.label3 = new System.Windows.Forms.Label();
             this.textBox4 = new System.Windows.Forms.TextBox();
             this.label5 = new System.Windows.Forms.Label();
             this.comboBox2 = new System.Windows.Forms.ComboBox();
             this.groupBox1 = new System.Windows.Forms.GroupBox();
             this.label4 = new System.Windows.Forms.Label();
             this.textBox3 = new System.Windows.Forms.TextBox();
             this.label2 = new System.Windows.Forms.Label();
             this.comboBox1 = new System.Windows.Forms.ComboBox();
             this.button2 = new System.Windows.Forms.Button();
             this.button1 = new System.Windows.Forms.Button();
             this.textBox2 = new System.Windows.Forms.TextBox();
             this.label1 = new System.Windows.Forms.Label();
             this.panel1 = new System.Windows.Forms.Panel();
             this.button9 = new System.Windows.Forms.Button();
             this.propertyEditor1 = new OpenMesh.PropertyEditor();
             this.shapeTreeViewer1 = new OpenMesh.ShapeTreeViewer();
             this.scalePane1 = new OpenMesh.ScalePane();
             this.shapeDrawingPane1 = new OpenMesh.ShapeDrawingPane();
             this.shapeDrawingPane2 = new OpenMesh.ShapeDrawingPane();
             this.shapeTreeViewer2 = new OpenMesh.ShapeTreeViewer();
             this.menuStrip1.SuspendLayout();
             this.tableLayoutPanel1.SuspendLayout();
             this.tableLayoutPanel3.SuspendLayout();
             this.panel3.SuspendLayout();
             ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
             this.flowLayoutPanel1.SuspendLayout();
             this.tabControl1.SuspendLayout();
             this.tabPage1.SuspendLayout();
             this.tabPage2.SuspendLayout();
             this.tableLayoutPanel2.SuspendLayout();
             this.panel2.SuspendLayout();
             this.tableLayoutPanel4.SuspendLayout();
             this.groupBox2.SuspendLayout();
             this.groupBox1.SuspendLayout();
             this.panel1.SuspendLayout();
             this.SuspendLayout();
             // 
             // menuStrip1
             // 
             this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
             this.menuStrip1.Location = new System.Drawing.Point(0, 0);
             this.menuStrip1.Name = "menuStrip1";
             this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
             this.menuStrip1.Size = new System.Drawing.Size(798, 24);
             this.menuStrip1.TabIndex = 1;
             this.menuStrip1.Text = "menuStrip1";
             // 
             // fileToolStripMenuItem1
             // 
             this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.toolStripSeparator,
            this.exitToolStripMenuItem1});
             this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
             this.fileToolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
             this.fileToolStripMenuItem1.Text = "&File";
             // 
             // openToolStripMenuItem1
             // 
             this.openToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
             this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
             this.openToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
             this.openToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
             this.openToolStripMenuItem1.Text = "&Open";
             this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
             // 
             // toolStripSeparator
             // 
             this.toolStripSeparator.Name = "toolStripSeparator";
             this.toolStripSeparator.Size = new System.Drawing.Size(149, 6);
             // 
             // exitToolStripMenuItem1
             // 
             this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
             this.exitToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
             this.exitToolStripMenuItem1.Text = "E&xit";
             this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
             // 
             // openFileDialog1
             // 
             this.openFileDialog1.FileName = "openFileDialog1";
             this.openFileDialog1.Filter = "Text Files (*.txt)|*.txt";
             this.openFileDialog1.RestoreDirectory = true;
             this.openFileDialog1.Title = "Open File";
             // 
             // tableLayoutPanel1
             // 
             this.tableLayoutPanel1.ColumnCount = 2;
             this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.53469F));
             this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.46532F));
             this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
             this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
             this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
             this.tableLayoutPanel1.Name = "tableLayoutPanel1";
             this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(7);
             this.tableLayoutPanel1.RowCount = 1;
             this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406F));
             this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 420);
             this.tableLayoutPanel1.TabIndex = 2;
             // 
             // tableLayoutPanel3
             // 
             this.tableLayoutPanel3.ColumnCount = 1;
             this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
             this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 7);
             this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
             this.tableLayoutPanel3.Name = "tableLayoutPanel3";
             this.tableLayoutPanel3.RowCount = 2;
             this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel3.Size = new System.Drawing.Size(187, 406);
             this.tableLayoutPanel3.TabIndex = 7;
             // 
             // panel3
             // 
             this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
             this.panel3.Location = new System.Drawing.Point(197, 10);
             this.panel3.Name = "panel3";
             this.panel3.Size = new System.Drawing.Size(573, 400);
             this.panel3.TabIndex = 8;
             // 
             // trackBar1
             // 
             this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.trackBar1.Location = new System.Drawing.Point(528, 3);
             this.trackBar1.Maximum = 400;
             this.trackBar1.Minimum = -400;
             this.trackBar1.Name = "trackBar1";
             this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
             this.trackBar1.Size = new System.Drawing.Size(42, 207);
             this.trackBar1.SmallChange = 20;
             this.trackBar1.TabIndex = 9;
             this.trackBar1.TickFrequency = 80;
             this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
             // 
             // button4
             // 
             this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
             this.button4.Location = new System.Drawing.Point(414, 374);
             this.button4.Name = "button4";
             this.button4.Size = new System.Drawing.Size(75, 23);
             this.button4.TabIndex = 7;
             this.button4.Text = "Fill View";
             this.button4.UseVisualStyleBackColor = true;
             this.button4.Click += new System.EventHandler(this.button4_Click);
             // 
             // button3
             // 
             this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
             this.button3.Location = new System.Drawing.Point(495, 374);
             this.button3.Name = "button3";
             this.button3.Size = new System.Drawing.Size(75, 23);
             this.button3.TabIndex = 6;
             this.button3.Text = "Reset View";
             this.button3.UseVisualStyleBackColor = true;
             this.button3.Click += new System.EventHandler(this.button3_Click);
             // 
             // button7
             // 
             this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.button7.Location = new System.Drawing.Point(3, 3);
             this.button7.Name = "button7";
             this.button7.Size = new System.Drawing.Size(131, 23);
             this.button7.TabIndex = 2;
             this.button7.Text = "Add Indicators";
             this.button7.UseVisualStyleBackColor = true;
             this.button7.Click += new System.EventHandler(this.button7_Click);
             // 
             // flowLayoutPanel1
             // 
             this.flowLayoutPanel1.Controls.Add(this.button7);
             this.flowLayoutPanel1.Controls.Add(this.button8);
             this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
             this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 5);
             this.flowLayoutPanel1.Name = "flowLayoutPanel1";
             this.flowLayoutPanel1.Size = new System.Drawing.Size(788, 446);
             this.flowLayoutPanel1.TabIndex = 0;
             // 
             // button8
             // 
             this.button8.Location = new System.Drawing.Point(3, 32);
             this.button8.Name = "button8";
             this.button8.Size = new System.Drawing.Size(131, 23);
             this.button8.TabIndex = 3;
             this.button8.Text = "Delete";
             this.button8.UseVisualStyleBackColor = true;
             this.button8.Click += new System.EventHandler(this.button8_Click);
             // 
             // tabControl1
             // 
             this.tabControl1.Controls.Add(this.tabPage1);
             this.tabControl1.Controls.Add(this.tabPage2);
             this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.tabControl1.Location = new System.Drawing.Point(5, 5);
             this.tabControl1.Name = "tabControl1";
             this.tabControl1.SelectedIndex = 0;
             this.tabControl1.Size = new System.Drawing.Size(788, 446);
             this.tabControl1.TabIndex = 4;
             this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
             // 
             // tabPage1
             // 
             this.tabPage1.Controls.Add(this.tableLayoutPanel1);
             this.tabPage1.Location = new System.Drawing.Point(4, 22);
             this.tabPage1.Name = "tabPage1";
             this.tabPage1.Size = new System.Drawing.Size(780, 420);
             this.tabPage1.TabIndex = 0;
             this.tabPage1.Text = "View";
             this.tabPage1.UseVisualStyleBackColor = true;
             // 
             // tabPage2
             // 
             this.tabPage2.Controls.Add(this.tableLayoutPanel2);
             this.tabPage2.Location = new System.Drawing.Point(4, 22);
             this.tabPage2.Name = "tabPage2";
             this.tabPage2.Size = new System.Drawing.Size(780, 420);
             this.tabPage2.TabIndex = 1;
             this.tabPage2.Text = "Code";
             this.tabPage2.UseVisualStyleBackColor = true;
             // 
             // tableLayoutPanel2
             // 
             this.tableLayoutPanel2.ColumnCount = 2;
             this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 0);
             this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
             this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
             this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
             this.tableLayoutPanel2.Name = "tableLayoutPanel2";
             this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(7);
             this.tableLayoutPanel2.RowCount = 1;
             this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
             this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406F));
             this.tableLayoutPanel2.Size = new System.Drawing.Size(780, 420);
             this.tableLayoutPanel2.TabIndex = 3;
             // 
             // textBox1
             // 
             this.textBox1.BackColor = System.Drawing.SystemColors.Window;
             this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.textBox1.Location = new System.Drawing.Point(393, 10);
             this.textBox1.Multiline = true;
             this.textBox1.Name = "textBox1";
             this.textBox1.ReadOnly = true;
             this.textBox1.Size = new System.Drawing.Size(377, 400);
             this.textBox1.TabIndex = 2;
             // 
             // panel2
             // 
             this.panel2.Controls.Add(this.tableLayoutPanel4);
             this.panel2.Controls.Add(this.button2);
             this.panel2.Controls.Add(this.button1);
             this.panel2.Controls.Add(this.textBox2);
             this.panel2.Controls.Add(this.label1);
             this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
             this.panel2.Location = new System.Drawing.Point(10, 10);
             this.panel2.Name = "panel2";
             this.panel2.Size = new System.Drawing.Size(377, 400);
             this.panel2.TabIndex = 3;
             // 
             // tableLayoutPanel4
             // 
             this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                         | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.tableLayoutPanel4.ColumnCount = 2;
             this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel4.Controls.Add(this.groupBox2, 1, 1);
             this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 1);
             this.tableLayoutPanel4.Controls.Add(this.listBox1, 0, 0);
             this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 46);
             this.tableLayoutPanel4.Name = "tableLayoutPanel4";
             this.tableLayoutPanel4.RowCount = 3;
             this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
             this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
             this.tableLayoutPanel4.Size = new System.Drawing.Size(371, 322);
             this.tableLayoutPanel4.TabIndex = 12;
             // 
             // listBox1
             // 
             this.tableLayoutPanel4.SetColumnSpan(this.listBox1, 2);
             this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.listBox1.FormattingEnabled = true;
             this.listBox1.Location = new System.Drawing.Point(3, 3);
             this.listBox1.Name = "listBox1";
             this.listBox1.Size = new System.Drawing.Size(365, 102);
             this.listBox1.TabIndex = 4;
             // 
             // groupBox2
             // 
             this.groupBox2.Controls.Add(this.label3);
             this.groupBox2.Controls.Add(this.textBox4);
             this.groupBox2.Controls.Add(this.label5);
             this.groupBox2.Controls.Add(this.comboBox2);
             this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
             this.groupBox2.Location = new System.Drawing.Point(188, 111);
             this.groupBox2.Name = "groupBox2";
             this.groupBox2.Size = new System.Drawing.Size(180, 100);
             this.groupBox2.TabIndex = 11;
             this.groupBox2.TabStop = false;
             this.groupBox2.Text = "Output";
             // 
             // label3
             // 
             this.label3.AutoSize = true;
             this.label3.Location = new System.Drawing.Point(6, 17);
             this.label3.Name = "label3";
             this.label3.Size = new System.Drawing.Size(57, 13);
             this.label3.TabIndex = 11;
             this.label3.Text = "File Name:";
             // 
             // textBox4
             // 
             this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.textBox4.Location = new System.Drawing.Point(6, 33);
             this.textBox4.Name = "textBox4";
             this.textBox4.Size = new System.Drawing.Size(168, 21);
             this.textBox4.TabIndex = 9;
             // 
             // label5
             // 
             this.label5.AutoSize = true;
             this.label5.Location = new System.Drawing.Point(6, 57);
             this.label5.Name = "label5";
             this.label5.Size = new System.Drawing.Size(35, 13);
             this.label5.TabIndex = 6;
             this.label5.Text = "Type:";
             // 
             // comboBox2
             // 
             this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
             this.comboBox2.FormattingEnabled = true;
             this.comboBox2.Location = new System.Drawing.Point(6, 73);
             this.comboBox2.Name = "comboBox2";
             this.comboBox2.Size = new System.Drawing.Size(168, 21);
             this.comboBox2.TabIndex = 5;
             // 
             // groupBox1
             // 
             this.groupBox1.Controls.Add(this.label4);
             this.groupBox1.Controls.Add(this.textBox3);
             this.groupBox1.Controls.Add(this.label2);
             this.groupBox1.Controls.Add(this.comboBox1);
             this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.groupBox1.Location = new System.Drawing.Point(3, 111);
             this.groupBox1.Name = "groupBox1";
             this.groupBox1.Size = new System.Drawing.Size(179, 100);
             this.groupBox1.TabIndex = 10;
             this.groupBox1.TabStop = false;
             this.groupBox1.Text = "Input";
             // 
             // label4
             // 
             this.label4.AutoSize = true;
             this.label4.Location = new System.Drawing.Point(6, 17);
             this.label4.Name = "label4";
             this.label4.Size = new System.Drawing.Size(57, 13);
             this.label4.TabIndex = 11;
             this.label4.Text = "File Name:";
             // 
             // textBox3
             // 
             this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.textBox3.Location = new System.Drawing.Point(6, 33);
             this.textBox3.Name = "textBox3";
             this.textBox3.Size = new System.Drawing.Size(167, 21);
             this.textBox3.TabIndex = 9;
             // 
             // label2
             // 
             this.label2.AutoSize = true;
             this.label2.Location = new System.Drawing.Point(6, 57);
             this.label2.Name = "label2";
             this.label2.Size = new System.Drawing.Size(35, 13);
             this.label2.TabIndex = 6;
             this.label2.Text = "Type:";
             // 
             // comboBox1
             // 
             this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
             this.comboBox1.FormattingEnabled = true;
             this.comboBox1.Location = new System.Drawing.Point(6, 73);
             this.comboBox1.Name = "comboBox1";
             this.comboBox1.Size = new System.Drawing.Size(167, 21);
             this.comboBox1.TabIndex = 5;
             // 
             // button2
             // 
             this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
             this.button2.Location = new System.Drawing.Point(299, 374);
             this.button2.Name = "button2";
             this.button2.Size = new System.Drawing.Size(75, 23);
             this.button2.TabIndex = 1;
             this.button2.Text = "&Run";
             this.button2.UseVisualStyleBackColor = true;
             this.button2.Click += new System.EventHandler(this.button2_Click);
             // 
             // button1
             // 
             this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.button1.Location = new System.Drawing.Point(299, 18);
             this.button1.Name = "button1";
             this.button1.Size = new System.Drawing.Size(75, 23);
             this.button1.TabIndex = 3;
             this.button1.Text = "Browse...";
             this.button1.UseVisualStyleBackColor = true;
             // 
             // textBox2
             // 
             this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                         | System.Windows.Forms.AnchorStyles.Right)));
             this.textBox2.Location = new System.Drawing.Point(3, 19);
             this.textBox2.Name = "textBox2";
             this.textBox2.Size = new System.Drawing.Size(290, 21);
             this.textBox2.TabIndex = 2;
             // 
             // label1
             // 
             this.label1.AutoSize = true;
             this.label1.Location = new System.Drawing.Point(3, 3);
             this.label1.Name = "label1";
             this.label1.Size = new System.Drawing.Size(64, 13);
             this.label1.TabIndex = 1;
             this.label1.Text = "Project File:";
             // 
             // panel1
             // 
             this.panel1.Controls.Add(this.tabControl1);
             this.panel1.Controls.Add(this.flowLayoutPanel1);
             this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.panel1.Location = new System.Drawing.Point(0, 24);
             this.panel1.Name = "panel1";
             this.panel1.Padding = new System.Windows.Forms.Padding(5);
             this.panel1.Size = new System.Drawing.Size(798, 456);
             this.panel1.TabIndex = 5;
             // 
             // button9
             // 
             this.button9.Location = new System.Drawing.Point(262, 25);
             this.button9.Name = "button9";
             this.button9.Size = new System.Drawing.Size(75, 23);
             this.button9.TabIndex = 3;
             this.button9.Text = "button9";
             this.button9.UseVisualStyleBackColor = true;
             this.button9.Click += new System.EventHandler(this.button9_Click);
             // 
             // propertyEditor1
             // 
             this.propertyEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.propertyEditor1.EditingObject = null;
             this.propertyEditor1.Location = new System.Drawing.Point(3, 206);
             this.propertyEditor1.Name = "propertyEditor1";
             this.propertyEditor1.Size = new System.Drawing.Size(181, 197);
             this.propertyEditor1.TabIndex = 0;
             // 
             // shapeTreeViewer1
             // 
             this.shapeTreeViewer1.BackColor = System.Drawing.SystemColors.Window;
             this.shapeTreeViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
             this.shapeTreeViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.shapeTreeViewer1.Location = new System.Drawing.Point(3, 3);
             this.shapeTreeViewer1.Name = "shapeTreeViewer1";
             this.shapeTreeViewer1.SelectedShape = null;
             this.shapeTreeViewer1.Size = new System.Drawing.Size(181, 197);
             this.shapeTreeViewer1.TabIndex = 1;
             this.shapeTreeViewer1.SelectedShapeChanged += new System.EventHandler(this.shapeTreeViewer1_SelectedShapeChanged);
             // 
             // scalePane1
             // 
             this.scalePane1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
             this.scalePane1.BackColor = System.Drawing.Color.White;
             this.scalePane1.ForeColor = System.Drawing.Color.Black;
             this.scalePane1.Location = new System.Drawing.Point(3, 374);
             this.scalePane1.Name = "scalePane1";
             this.scalePane1.ShapeDrawer = null;
             this.scalePane1.Size = new System.Drawing.Size(193, 23);
             this.scalePane1.TabIndex = 8;
             this.scalePane1.Text = "scalePane1";
             // 
             // shapeDrawingPane1
             // 
             this.shapeDrawingPane1.BackColor = System.Drawing.Color.White;
             this.shapeDrawingPane1.Dock = System.Windows.Forms.DockStyle.Fill;
             this.shapeDrawingPane1.ForeColor = System.Drawing.Color.Black;
             this.shapeDrawingPane1.IsInteractive = true;
             this.shapeDrawingPane1.Location = new System.Drawing.Point(0, 0);
             this.shapeDrawingPane1.Name = "shapeDrawingPane1";
             this.shapeDrawingPane1.Size = new System.Drawing.Size(573, 400);
             this.shapeDrawingPane1.TabIndex = 0;
             this.shapeDrawingPane1.Text = "shapeDrawingPane1";
             // 
             // shapeDrawingPane2
             // 
             this.shapeDrawingPane2.BackColor = System.Drawing.Color.White;
             this.shapeDrawingPane2.Dock = System.Windows.Forms.DockStyle.Fill;
             this.shapeDrawingPane2.IsInteractive = true;
             this.shapeDrawingPane2.Location = new System.Drawing.Point(188, 217);
             this.shapeDrawingPane2.Name = "shapeDrawingPane2";
             this.shapeDrawingPane2.Size = new System.Drawing.Size(180, 102);
             this.shapeDrawingPane2.TabIndex = 12;
             this.shapeDrawingPane2.Text = "shapeDrawingPane2";
             // 
             // shapeTreeViewer2
             // 
             this.shapeTreeViewer2.BackColor = System.Drawing.SystemColors.Window;
             this.shapeTreeViewer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
             this.shapeTreeViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
             this.shapeTreeViewer2.Location = new System.Drawing.Point(3, 217);
             this.shapeTreeViewer2.Name = "shapeTreeViewer2";
             this.shapeTreeViewer2.SelectedShape = null;
             this.shapeTreeViewer2.Size = new System.Drawing.Size(179, 102);
             this.shapeTreeViewer2.TabIndex = 13;
             this.shapeTreeViewer2.SelectedShapeChanged += new System.EventHandler(this.shapeTreeViewer2_SelectedShapeChanged);
             // 
             // MainForm
             // 
             this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.ClientSize = new System.Drawing.Size(798, 480);
             this.Controls.Add(this.button9);
             this.Controls.Add(this.panel1);
             this.Controls.Add(this.menuStrip1);
             this.MainMenuStrip = this.menuStrip1;
             this.Name = "MainForm";
             this.Text = "Open Mesh v0.8";
             this.Load += new System.EventHandler(this.Form1_Load);
             this.menuStrip1.ResumeLayout(false);
             this.menuStrip1.PerformLayout();
             this.tableLayoutPanel1.ResumeLayout(false);
             this.tableLayoutPanel3.ResumeLayout(false);
             this.panel3.ResumeLayout(false);
             this.panel3.PerformLayout();
             ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
             this.flowLayoutPanel1.ResumeLayout(false);
             this.tabControl1.ResumeLayout(false);
             this.tabPage1.ResumeLayout(false);
             this.tabPage2.ResumeLayout(false);
             this.tableLayoutPanel2.ResumeLayout(false);
             this.tableLayoutPanel2.PerformLayout();
             this.panel2.ResumeLayout(false);
             this.panel2.PerformLayout();
             this.tableLayoutPanel4.ResumeLayout(false);
             this.groupBox2.ResumeLayout(false);
             this.groupBox2.PerformLayout();
             this.groupBox1.ResumeLayout(false);
             this.groupBox1.PerformLayout();
             this.panel1.ResumeLayout(false);
             this.ResumeLayout(false);
             this.PerformLayout();

         }

         #endregion

         private ShapeDrawingPane shapeDrawingPane1;
         private System.Windows.Forms.MenuStrip menuStrip1;
         private System.Windows.Forms.OpenFileDialog openFileDialog1;
         private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
         private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
         private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
         private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
         private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
         private System.Windows.Forms.TabControl tabControl1;
         private System.Windows.Forms.TabPage tabPage1;
         private System.Windows.Forms.TabPage tabPage2;
         private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
         private System.Windows.Forms.Button button2;
         private System.Windows.Forms.Panel panel1;
         private System.Windows.Forms.Button button3;
         private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
         private System.Windows.Forms.Panel panel3;
         private System.Windows.Forms.Button button4;
         private ScalePane scalePane1;
         private System.Windows.Forms.TrackBar trackBar1;
         private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
         private System.Windows.Forms.Button button7;
         private System.Windows.Forms.Button button8;
         private PropertyEditor propertyEditor1;
         private System.Windows.Forms.Button button9;
         private ShapeTreeViewer shapeTreeViewer1;
         private System.Windows.Forms.TextBox textBox1;
         private System.Windows.Forms.Panel panel2;
         private System.Windows.Forms.Label label1;
         private System.Windows.Forms.Button button1;
         private System.Windows.Forms.TextBox textBox2;
         private System.Windows.Forms.Label label2;
         private System.Windows.Forms.ComboBox comboBox1;
         private System.Windows.Forms.ListBox listBox1;
         private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
         private System.Windows.Forms.GroupBox groupBox2;
         private System.Windows.Forms.Label label3;
         private System.Windows.Forms.TextBox textBox4;
         private System.Windows.Forms.Label label5;
         private System.Windows.Forms.ComboBox comboBox2;
         private System.Windows.Forms.GroupBox groupBox1;
         private System.Windows.Forms.Label label4;
         private System.Windows.Forms.TextBox textBox3;
         private ShapeDrawingPane shapeDrawingPane2;
         private ShapeTreeViewer shapeTreeViewer2;
     }
 }
