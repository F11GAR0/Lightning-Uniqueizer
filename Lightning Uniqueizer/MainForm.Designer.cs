﻿namespace Lightning_Uniqueizer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.bOpenFolder = new System.Windows.Forms.Button();
            this.cbMultithread = new System.Windows.Forms.CheckBox();
            this.nMaxThreads = new System.Windows.Forms.NumericUpDown();
            this.cbAddWatermark = new System.Windows.Forms.CheckBox();
            this.bChoiceWaterFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWaterFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nMaxWaters = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nMaxPixels = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.fBD = new System.Windows.Forms.FolderBrowserDialog();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxWaters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxPixels)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.label4);
            this.gbSettings.Controls.Add(this.nMaxPixels);
            this.gbSettings.Controls.Add(this.label3);
            this.gbSettings.Controls.Add(this.nMaxWaters);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.tbWaterFolder);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.bChoiceWaterFolder);
            this.gbSettings.Controls.Add(this.cbAddWatermark);
            this.gbSettings.Controls.Add(this.nMaxThreads);
            this.gbSettings.Controls.Add(this.cbMultithread);
            this.gbSettings.Enabled = false;
            this.gbSettings.Location = new System.Drawing.Point(12, 41);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(349, 186);
            this.gbSettings.TabIndex = 0;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Настройки";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 15);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(261, 20);
            this.tbPath.TabIndex = 1;
            // 
            // bOpenFolder
            // 
            this.bOpenFolder.Location = new System.Drawing.Point(279, 12);
            this.bOpenFolder.Name = "bOpenFolder";
            this.bOpenFolder.Size = new System.Drawing.Size(81, 23);
            this.bOpenFolder.TabIndex = 2;
            this.bOpenFolder.Text = "Выбрать";
            this.bOpenFolder.UseVisualStyleBackColor = true;
            this.bOpenFolder.Click += new System.EventHandler(this.bOpenFolder_Click);
            // 
            // cbMultithread
            // 
            this.cbMultithread.AutoSize = true;
            this.cbMultithread.Location = new System.Drawing.Point(6, 20);
            this.cbMultithread.Name = "cbMultithread";
            this.cbMultithread.Size = new System.Drawing.Size(120, 17);
            this.cbMultithread.TabIndex = 0;
            this.cbMultithread.Text = "Мультипоточность";
            this.cbMultithread.UseVisualStyleBackColor = true;
            this.cbMultithread.CheckedChanged += new System.EventHandler(this.cbMultithread_CheckedChanged);
            // 
            // nMaxThreads
            // 
            this.nMaxThreads.Enabled = false;
            this.nMaxThreads.Location = new System.Drawing.Point(267, 38);
            this.nMaxThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMaxThreads.Name = "nMaxThreads";
            this.nMaxThreads.Size = new System.Drawing.Size(69, 20);
            this.nMaxThreads.TabIndex = 1;
            this.nMaxThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMaxThreads.ValueChanged += new System.EventHandler(this.nMaxThreads_ValueChanged);
            // 
            // cbAddWatermark
            // 
            this.cbAddWatermark.AutoSize = true;
            this.cbAddWatermark.Location = new System.Drawing.Point(6, 81);
            this.cbAddWatermark.Name = "cbAddWatermark";
            this.cbAddWatermark.Size = new System.Drawing.Size(145, 17);
            this.cbAddWatermark.TabIndex = 2;
            this.cbAddWatermark.Text = "Добавлять вотермарку";
            this.cbAddWatermark.UseVisualStyleBackColor = true;
            this.cbAddWatermark.CheckedChanged += new System.EventHandler(this.cbAddWatermark_CheckedChanged);
            // 
            // bChoiceWaterFolder
            // 
            this.bChoiceWaterFolder.Enabled = false;
            this.bChoiceWaterFolder.Location = new System.Drawing.Point(267, 96);
            this.bChoiceWaterFolder.Name = "bChoiceWaterFolder";
            this.bChoiceWaterFolder.Size = new System.Drawing.Size(69, 23);
            this.bChoiceWaterFolder.TabIndex = 3;
            this.bChoiceWaterFolder.Text = "Выбрать";
            this.bChoiceWaterFolder.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Максимальное количество потоков:";
            // 
            // tbWaterFolder
            // 
            this.tbWaterFolder.Location = new System.Drawing.Point(141, 98);
            this.tbWaterFolder.Name = "tbWaterFolder";
            this.tbWaterFolder.ReadOnly = true;
            this.tbWaterFolder.Size = new System.Drawing.Size(120, 20);
            this.tbWaterFolder.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Папка с вотермарками:";
            // 
            // nMaxWaters
            // 
            this.nMaxWaters.Enabled = false;
            this.nMaxWaters.Location = new System.Drawing.Point(267, 125);
            this.nMaxWaters.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMaxWaters.Name = "nMaxWaters";
            this.nMaxWaters.Size = new System.Drawing.Size(69, 20);
            this.nMaxWaters.TabIndex = 7;
            this.nMaxWaters.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMaxWaters.ValueChanged += new System.EventHandler(this.nMaxWaters_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Количество вотермарок на одно фото:";
            // 
            // nMaxPixels
            // 
            this.nMaxPixels.Location = new System.Drawing.Point(267, 156);
            this.nMaxPixels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMaxPixels.Name = "nMaxPixels";
            this.nMaxPixels.Size = new System.Drawing.Size(69, 20);
            this.nMaxPixels.TabIndex = 9;
            this.nMaxPixels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMaxPixels.ValueChanged += new System.EventHandler(this.nMaxPixels_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Количество пикселей для замены:";
            // 
            // bStart
            // 
            this.bStart.Enabled = false;
            this.bStart.Location = new System.Drawing.Point(12, 240);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 3;
            this.bStart.Text = "Начать";
            this.bStart.UseVisualStyleBackColor = true;
            // 
            // fBD
            // 
            this.fBD.Description = "Выберите папку";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 275);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.bOpenFolder);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.gbSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Lightning Uniqueizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxWaters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxPixels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nMaxPixels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nMaxWaters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbWaterFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bChoiceWaterFolder;
        private System.Windows.Forms.CheckBox cbAddWatermark;
        private System.Windows.Forms.NumericUpDown nMaxThreads;
        private System.Windows.Forms.CheckBox cbMultithread;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button bOpenFolder;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.FolderBrowserDialog fBD;
    }
}
