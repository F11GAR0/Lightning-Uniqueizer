using System;
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
            Globals.settings.Load();
            nMaxThreads.Value = Globals.settings.Instance.iThreadCount;
            nMaxWaters.Value = Globals.settings.Instance.iWatermarksCount;
            nMaxPixels.Value = Globals.settings.Instance.iPixelCount;
            cbAddWatermark.Checked = Globals.settings.Instance.bUseWatermark;
            cbMultithread.Checked = Globals.settings.Instance.bUseMultithread;
            cbRandomCrop.Checked = Globals.settings.Instance.bRandomCrop;
            cbRandomRotate.Checked = Globals.settings.Instance.bRandomRotate;
            tbPath.Text = Globals.settings.Instance.sDefaultPicturesFolder;
            tbWaterFolder.Text = Globals.settings.Instance.sWatermarkFolder;
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
            Globals.settings.SetUseWatermark(bChoiceWaterFolder.Enabled);
        }

        private void ToggleMultithreadSettings()
        {
            nMaxThreads.Enabled ^= true;
            Globals.settings.SetUseMultithread(nMaxThreads.Enabled);
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
    }
}
