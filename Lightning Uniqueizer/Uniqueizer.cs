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
        public static Image getImage(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            Image ret = Bitmap.FromStream(fileStream);
            fileStream.Close();
            return ret;
        }
        private static Image rotateImage(Image _in, double angle)
        {
            //Bitmap bmp = _in as Bitmap;
            Graphics graphics = Graphics.FromImage(_in);
            graphics.RotateTransform((float)angle);
            graphics.DrawImage(_in, 0, 0, _in.Width - 1, _in.Height - 1);
            graphics.Dispose();
            return _in;
        }
        private static Image cropImage(Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;
            Image ret = bmp.Clone(selection, bmp.PixelFormat);
            bmp.Dispose();
            return ret;
        }
        public static Image addWaterMark(Image orig, Image waterMark, Random rnd, int count)
        {
            Point location = new Point();
            using (Graphics g = Graphics.FromImage(orig))
            {
                for (int i = 0; i < count; i++)
                {
                    location.X = rnd.Next(2, orig.Width);
                    location.Y = rnd.Next(2, orig.Height);

                    /*Point bounds = new Point(location.X + waterMark.Width, location.Y + waterMark.Height);
                    if (bounds.X > orig.Width || bounds.Y > orig.Width)
                    {
                        g.DrawImage(waterMark, location.X, location.Y, bounds.X - orig.Width - 1, bounds.Y - orig.Height - 1);
                    }
                    else
                    {
                    */
                        g.DrawImage(waterMark, location);
                    //}   
                }
                g.Dispose();
            }
            return orig;
        }
        private static Image randomPixelSwapImage(Image image, int count, Random rnd)
        {
            Bitmap bmp = image as Bitmap;

            int x = rnd.Next(1, image.Width);
            int y = rnd.Next(1, image.Height);
            int x2 = rnd.Next(1, image.Width);
            int y2 = rnd.Next(1, image.Height);

            for (int i = 0; i < count; i++)
            {
                Color tmp = bmp.GetPixel(x, y);
                bmp.SetPixel(x, y, bmp.GetPixel(x2, y2));
                bmp.SetPixel(x2, y2, tmp);
                bmp.SetPixel(rnd.Next(1, image.Width), rnd.Next(1, image.Height), Color.Black);
            }

            Image ret = bmp;
            bmp.Dispose();
            return ret;
        }
        public static void ProcessPicture(string dir, string path, int pixel_count, Image watermark)
        {
            try
            {
                Log.AddMessage("dir: " + dir + " path: " + path + " pixel_count: " + pixel_count.ToString() + " image: " + (watermark == null ? "null" : "normal " + watermark.RawFormat.ToString()));
                FileInfo fInfo = new FileInfo(path);
                DirectoryInfo dParentSave = fInfo.Directory;

                string save_dir = dir + "_lightning" + ((dParentSave != fInfo.Directory) ? "\\" + fInfo.Directory.Name : "");
                string save_file = save_dir + "\\0" + fInfo.Name;
                Random rand = new Random((int)DateTime.Now.Ticks + DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute);
                if (!Directory.Exists(save_dir))
                {
                    Directory.CreateDirectory(save_dir);
                }
                Image image = getImage(path);
                if (Globals.settings.Instance.bUseWatermark)
                {
                    image = addWaterMark(image, watermark, rand, Globals.settings.Instance.iWatermarksCount);
                }
                GC.Collect();
                if (Globals.settings.Instance.bRandomRotate)
                {
                    image = rotateImage(image, rand.NextDouble() + rand.Next(-1, 1));
                }
                if (Globals.settings.Instance.bRandomCrop)
                {
                    Point crop_right = new Point(rand.Next(3, 10), rand.Next(3, 10));
                    image = cropImage(image, new Rectangle(crop_right.X, crop_right.Y, image.Width - crop_right.X, image.Height - crop_right.Y));
                }
                image.Save(save_file);
                image.Dispose();
                GC.Collect();
            }
            catch(Exception e)
            {
                Log.AddMessage(e.Message + e.StackTrace);
            }
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
            DateTime now = DateTime.Now;
            if (Globals.settings.Instance.bUseMultithread)
            {
                ParallelOptions opt = new ParallelOptions();
               // opt.TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                opt.MaxDegreeOfParallelism = Globals.settings.Instance.iThreadCount;
                if (Globals.settings.Instance.bUseWatermark)
                {
                    try
                    {
                        Image watermark = LightningEngine.getImage(m_WotermarksList[rand.Next(m_WotermarksList.Count)]);
                        Log.AddMessage("Watermark state: " + (watermark == null ? "null" : "normal"));
                        Parallel.For(0, m_PathList.Count, opt, k =>
                        {
                            for (int i = 0; i < Globals.settings.Instance.iDirectoriesCount; i++)
                                if (m_PathList[k] != "")
                                    LightningEngine.ProcessPicture(Globals.settings.Instance.sDefaultPicturesFolder + i.ToString(), m_PathList[k].ToString(), Globals.settings.Instance.iPixelCount, (Image)watermark.Clone());
                        });
                        watermark.Dispose();
                    }
                    catch
                    {
                        //
                    }
                }
                else
                {
                    Parallel.For(0, m_PathList.Count, opt, k =>
                    {
                        for (int i = 0; i < Globals.settings.Instance.iDirectoriesCount; i++)
                            if (m_PathList[k] != "")
                                LightningEngine.ProcessPicture(Globals.settings.Instance.sDefaultPicturesFolder + i.ToString(), m_PathList[k], Globals.settings.Instance.iPixelCount, null);
                    });
                }
            } else
            {
                if (!Globals.settings.Instance.bUseWatermark)
                {
                    foreach (var i in m_PathList)
                    {
                        for(int k = 0; k < Globals.settings.Instance.iDirectoriesCount; k++)
                            if (i != "")
                                LightningEngine.ProcessPicture(Globals.settings.Instance.sDefaultPicturesFolder + k.ToString(), i, Globals.settings.Instance.iPixelCount, null);
                    }
                }
                else
                {
                    foreach (var i in m_PathList)
                    {
                        Image watermark = LightningEngine.getImage(m_WotermarksList[rand.Next(m_WotermarksList.Count)]);
                        for (int k = 0; k < Globals.settings.Instance.iDirectoriesCount; k++)
                        {
                            if (i != "")
                                LightningEngine.ProcessPicture(Globals.settings.Instance.sDefaultPicturesFolder + k.ToString(), i, Globals.settings.Instance.iPixelCount, watermark);
                            
                        }
                        watermark.Dispose();
                    }
                }
            }

            MessageBox.Show("Complete. Time: " + (DateTime.Now - now).ToString(), "Success!", MessageBoxButtons.OK);
        }
    }
}
