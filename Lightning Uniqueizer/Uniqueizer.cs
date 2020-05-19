using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightning_Uniqueizer
{
    class LightningEngine
    {
        private static Image getImage(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            Image ret = Image.FromStream(fileStream);
            fileStream.Close();
            return ret;
        }
        private static Image rotateImage(Image _in, int angle)
        {
            Graphics graphics = Graphics.FromImage(_in);
            graphics.RotateTransform(angle);
            graphics.DrawImage(_in, new Point(0, 0));
            graphics.Dispose();
            return _in;
        }
        private static Image cropImage(Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");
            return bmp.Clone(selection, bmp.PixelFormat);
        }
        public static Image addWaterMark(Image orig, Image waterMark, Random rnd, int count)
        {
            Point location = new Point();
            using (Graphics g = Graphics.FromImage(orig))
            {
                for (int i = 0; i < count; i++)
                {
                    location.X = rnd.Next(2, orig.Width - 5);
                    location.Y = rnd.Next(2, orig.Height - 5);
                    g.DrawImage(waterMark, location);
                }
            }
            waterMark.Dispose();
            return orig;
        }
        private static Image randomPixelSwapImage(Image image, int count, Random rnd)
        {
            Bitmap bmp = image as Bitmap;
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            int x = rnd.Next(1, image.Width);
            int y = rnd.Next(1, image.Height);
            int x2 = rnd.Next(1, image.Width);
            int y2 = rnd.Next(1, image.Height);

            for (int i = 0; i < count; i++)
            {
                Color tmp = bmp.GetPixel(x, y);
                bmp.SetPixel(x, y, bmp.GetPixel(x2, y2));
                bmp.SetPixel(x2, y2, tmp);
            }
            return bmp;
        }
        public static void ProcessPicture(string path, int pixel_count, string watermark_path = "" )
        {
            FileInfo fInfo = new FileInfo(path);
            DirectoryInfo dParentSave = fInfo.Directory;
            string save_dir = dParentSave.FullName + "_lightning";
            string save_file = save_dir + "\\0" + fInfo.Name;
            Random rand = new Random((int)DateTime.Now.Ticks);
            //MessageBox.Show(save_file);
            if (!Directory.Exists(save_dir))
            {
                Directory.CreateDirectory(save_dir);
            }
            Image image = getImage(path);
            if (Globals.settings.Instance.bUseWatermark)
            {
                image = addWaterMark(image, getImage(watermark_path), rand, Globals.settings.Instance.iWatermarksCount);
            }
            GC.Collect();
            if (Globals.settings.Instance.bRandomRotate)
            {
                image = rotateImage(image, rand.Next(-5, 5));
            }
            if (Globals.settings.Instance.bRandomCrop)
            {
                image = cropImage(image, new Rectangle(0 + rand.Next(5), 0 + rand.Next(5), image.Width - rand.Next(5), image.Height - rand.Next(5)));
            }
            image.Save(save_file);
        }
    }
    class Uniqueizer
    {
        private List<string> m_PathList = new List<string>();
        private List<string> m_WotermarksList = new List<string>();

        public void ProcDir(DirectoryInfo inf)
        {
            if (inf.GetDirectories().Length == 0)
            {
                foreach (FileInfo f in inf.GetFiles())
                {
                    if (f.Extension == ".png" || f.Extension == ".jpeg" || f.Extension == ".jpg")
                        m_PathList.Add(f.FullName);
                }
            }
            else
            {
                foreach (DirectoryInfo d in inf.GetDirectories())
                {
                    ProcDir(d);
                }
                foreach (FileInfo f in inf.GetFiles())
                {
                    if (f.Extension == ".png" || f.Extension == ".jpeg" || f.Extension == ".jpg")
                        m_PathList.Add(f.FullName);
                }
            }
        }
        public void Load(string working_directory)
        {
            m_PathList.Clear();
            m_WotermarksList.Clear();
            ProcDir(new DirectoryInfo(working_directory));
            if (Globals.settings.Instance.bUseWatermark)
            {
                foreach (FileInfo f in new DirectoryInfo(Globals.settings.Instance.sWatermarkFolder).GetFiles())
                {
                    if (f.Extension == ".png" || f.Extension == ".jpeg" || f.Extension == ".jpg")
                        m_WotermarksList.Add(f.FullName);
                }
            }
        }
        public void Start()
        {
            Random rand = new Random();
            if (Globals.settings.Instance.bUseMultithread)
            {
                ParallelOptions opt = new ParallelOptions();
                opt.MaxDegreeOfParallelism = Globals.settings.Instance.iThreadCount;

                if (Globals.settings.Instance.bUseWatermark)
                {
                    Parallel.For(0, m_PathList.Count, opt, k =>
                    {
                        LightningEngine.ProcessPicture(m_PathList[k], Globals.settings.Instance.iPixelCount, m_WotermarksList[rand.Next(m_WotermarksList.Count)]);
                    });
                }
                else
                {
                    Parallel.For(0, m_PathList.Count, opt, k =>
                    {
                        LightningEngine.ProcessPicture(m_PathList[k], Globals.settings.Instance.iPixelCount);
                    });
                }
            } else
            {
                if (!Globals.settings.Instance.bUseWatermark)
                {
                    foreach (var i in m_PathList)
                    {
                        if (i != "")
                            LightningEngine.ProcessPicture(i, Globals.settings.Instance.iPixelCount);
                    }
                }
                else
                {
                    foreach (var i in m_PathList)
                    {
                        if (i != "")
                            LightningEngine.ProcessPicture(i, Globals.settings.Instance.iPixelCount, m_WotermarksList[rand.Next(m_WotermarksList.Count)]);
                    }
                }
            }
            MessageBox.Show("Complete", "Success!", MessageBoxButtons.OK);
        }
    }
}
