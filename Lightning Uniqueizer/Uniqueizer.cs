using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightning_Uniqueizer
{
    class LightningEngine
    {
        public static void ProcessPicture(string path, int pixel_count, string watermark_path = "", int water_count = 0 )
        {

        }
    }
    class Uniqueizer
    {
        private List<string> m_PathList;
        private List<string> m_WotermarksList;

        public void ProcDir(DirectoryInfo inf)
        {
            if (inf.GetDirectories().Length == 0)
            {
                foreach (FileInfo f in inf.GetFiles())
                {
                    if (f.Extension == "png" || f.Extension == "jpeg" || f.Extension == "jpg")
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
                    if (f.Extension == "png" || f.Extension == "jpeg" || f.Extension == "jpg")
                        m_PathList.Add(f.FullName);
                }
            }
        }
        public void Load(string working_directory)
        {
            ProcDir(new DirectoryInfo(working_directory));
            if (Globals.settings.Instance.bUseWatermark)
            {
                foreach (FileInfo f in new DirectoryInfo(Globals.settings.Instance.sWatermarkFolder).GetFiles())
                {
                    if (f.Extension == "png" || f.Extension == "jpeg" || f.Extension == "jpg")
                        m_PathList.Add(f.FullName);
                }
            }
        }
        public void Start()
        {
            ParallelOptions opt = new ParallelOptions();
            opt.MaxDegreeOfParallelism = Globals.settings.Instance.iThreadCount;
            Random rand = new Random();
            if (Globals.settings.Instance.bUseWatermark)
            {
                Parallel.For(0, m_PathList.Count, opt, k =>
                {
                    LightningEngine.ProcessPicture(m_PathList[k], Globals.settings.Instance.iPixelCount, m_WotermarksList[rand.Next(m_WotermarksList.Count)], Globals.settings.Instance.iWatermarksCount);
                });
            }
            else
            {
                Parallel.For(0, m_PathList.Count, opt, k =>
                {
                    LightningEngine.ProcessPicture(m_PathList[k], Globals.settings.Instance.iPixelCount);
                });
            }
        }
    }
}
