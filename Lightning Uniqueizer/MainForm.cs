﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightning_Uniqueizer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Log.Clear();
            Globals.settings.Load();
            nMaxThreads.Value = Globals.settings.Instance.iThreadCount;
            nMaxWaters.Value = Globals.settings.Instance.iWatermarksCount;
            nMaxPixels.Value = Globals.settings.Instance.iPixelCount;
            nDirectoriesCount.Value = Globals.settings.Instance.iDirectoriesCount;
            cbAddWatermark.Checked = Globals.settings.Instance.bUseWatermark;
            cbMultithread.Checked = Globals.settings.Instance.bUseMultithread;
            cbRandomCrop.Checked = Globals.settings.Instance.bRandomCrop;
            cbRandomRotate.Checked = Globals.settings.Instance.bRandomRotate;
            tbPath.Text = Globals.settings.Instance.sDefaultPicturesFolder;
            tbWaterFolder.Text = Globals.settings.Instance.sWatermarkFolder;
            Globals.uniq.defaultWatermarkPosition = Uniqueizer.eWatermarkPosition.RANDOM;
            Globals.settings.SetWatermarkResolution(3);
            if (Directory.Exists(tbPath.Text))
            {
                EnableSettings();
            }
        }

        private void EnableSettings()
        {
            gbSettings.Enabled = true;
            bStart.Enabled = true;
        }

        private void ToggleWatermarkSettings()
        {
            bChoiceWaterFolder.Enabled ^= true;
            nMaxWaters.Enabled ^= true;
            Globals.settings.SetUseWatermark(cbAddWatermark.Checked);
        }

        private void ToggleMultithreadSettings()
        {
            nMaxThreads.Enabled ^= true;
            Globals.settings.SetUseMultithread(cbMultithread.Checked);
        }

        private void bOpenFolder_Click(object sender, EventArgs e)
        {
            if(fBD.ShowDialog() == DialogResult.OK)
            {
                Globals.settings.SetDefaultPicturesFolder(fBD.SelectedPath);
                tbPath.Text = fBD.SelectedPath;
                EnableSettings();
            }
        }

        private void cbMultithread_CheckedChanged(object sender, EventArgs e)
        {
            ToggleMultithreadSettings();
        }

        private void cbAddWatermark_CheckedChanged(object sender, EventArgs e)
        {
            ToggleWatermarkSettings();
        }

        private void nMaxThreads_ValueChanged(object sender, EventArgs e)
        {
            Globals.settings.SetThreadsCount((int)nMaxThreads.Value);
        }

        private void nMaxWaters_ValueChanged(object sender, EventArgs e)
        {
            Globals.settings.SetWatermarksCount((int)nMaxWaters.Value);
        }

        private void nMaxPixels_ValueChanged(object sender, EventArgs e)
        {
            Globals.settings.SetPixelCount((int)nMaxPixels.Value);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.settings.Save();
        }

        private void cbRandomCrop_CheckedChanged(object sender, EventArgs e)
        {
            Globals.settings.SetRandomCrop(cbRandomCrop.Checked);
        }

        private void cbRandomRotate_CheckedChanged(object sender, EventArgs e)
        {
            Globals.settings.SetRandomRotate(cbRandomRotate.Checked);
        }

        private void bChoiceWaterFolder_Click(object sender, EventArgs e)
        {
            if (fBD.ShowDialog() == DialogResult.OK)
            {
                Globals.settings.SetWatermarkFolder(fBD.SelectedPath);
                tbWaterFolder.Text = fBD.SelectedPath;
                EnableSettings();
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            Globals.uniq.Load(Globals.settings.Instance.sDefaultPicturesFolder);
            Globals.uniq.Start();
        }

        private void nDirectoriesCount_ValueChanged(object sender, EventArgs e)
        {
            Globals.settings.SetDirectoriesCount((int)nDirectoriesCount.Value);
        }

        private void cbWatermarkPosition_TextUpdate(object sender, EventArgs e)
        {
            Globals.uniq.defaultWatermarkPosition = (Uniqueizer.eWatermarkPosition)cbWatermarkPosition.SelectedIndex;
        }

        private void cbWatermarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.uniq.defaultWatermarkPosition = (Uniqueizer.eWatermarkPosition)cbWatermarkPosition.SelectedIndex;
        }

        private void trbResolution_Scroll(object sender, EventArgs e)
        {
            lResolution.Text = "1:" + ((double)trbResolution.Value / 100).ToString();
            Globals.settings.SetWatermarkResolution((double)trbResolution.Value / 100);
        }
    }
}
