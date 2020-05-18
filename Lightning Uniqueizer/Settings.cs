using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lightning_Uniqueizer
{
    public struct stSettingsData
    {
        public bool bUseMultithread;
        public bool bUseWatermark;
        public string sWatermarkFolder;
        public string sDefaultPicturesFolder;
        public int iThreadCount;
        public int iWatermarksCount;
        public int iPixelCount;
    }
    class Settings
    {
        private stSettingsData m_Settings;
        public stSettingsData Instance {
            get
            {
                return m_Settings;
            }
            set
            {
                m_Settings = value;
            }
        }

        public void SetUseMultithread(bool val)
        {
            m_Settings.bUseMultithread = val;
        }

        public void SetUseWatermark(bool val)
        {
            m_Settings.bUseWatermark = val;
        }

        public void SetWatermarkFolder(string folder)
        {
            m_Settings.sWatermarkFolder = folder;
        }

        public void SetDefaultPicturesFolder(string folder)
        {
            m_Settings.sDefaultPicturesFolder = folder;
        }

        public void SetThreadsCount(int c)
        {
            m_Settings.iThreadCount = c;
        }
        public void SetWatermarksCount(int c)
        {
            m_Settings.iWatermarksCount = c;
        }
        public void SetPixelCount(int c)
        {
            m_Settings.iPixelCount = c;
        }
        private bool parse_get_bool(string str)
        {
            if (str == "true") return true;
            if (str == "false") return false;
            throw new Exception("unable to parse bool expression");
        }
        private int parse_get_int(string str)
        {
            int result = 0;
            if(int.TryParse(str, out result))
            {
                return result;
            }
            else
                throw new Exception("unable to parse integer expression");
        }
        private string save_get_bool(bool val)
        {
            return val ? "true" : "false";
        }
        public void Load()
        {
            string settings_path = Environment.CurrentDirectory + "\\settings.ini";
            if (File.Exists(settings_path))
            {
                var lines = File.ReadAllLines(settings_path);
                for(int i = 0, len = lines.Length; i < len; i++)
                {
                    if (lines[i].StartsWith("#")) continue;
                    var splitted = lines[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if(splitted.Length == 2)
                    {
                        splitted[0] = splitted[0].TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ' });
                        splitted[1] = splitted[1].TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ' });
                        if(splitted[0] == "use_multithread")
                        {
                            m_Settings.bUseMultithread = parse_get_bool(splitted[1]);
                            continue;
                        }
                        if(splitted[0] == "use_watermark")
                        {
                            m_Settings.bUseWatermark = parse_get_bool(splitted[1]);
                            continue;
                        }
                        if(splitted[0] == "watermark_folder")
                        {
                            m_Settings.sWatermarkFolder = splitted[1];
                            continue;
                        }
                        if (splitted[0] == "pictures_folder")
                        {
                            m_Settings.sDefaultPicturesFolder = splitted[1];
                            continue;
                        }
                        if (splitted[0] == "pixel_count")
                        {
                            m_Settings.iPixelCount = parse_get_int(splitted[1]);
                            continue;
                        }
                        if (splitted[0] == "max_threads_count")
                        {
                            m_Settings.iThreadCount = parse_get_int(splitted[1]);
                            continue;
                        }
                        if (splitted[0] == "watermarks_count")
                        {
                            m_Settings.iWatermarksCount = parse_get_int(splitted[1]);
                            continue;
                        }
                    }
                }
            } else
            {
                var writer = File.CreateText(settings_path);
                writer.WriteLine("use_multithread = true");
                writer.WriteLine("use_watermark = false");
                writer.WriteLine("watermark_folder = nil");
                writer.WriteLine("pictures_folder = nil");
                writer.WriteLine("pixel_count = 1");
                writer.WriteLine("max_threads_count = 1");
                writer.WriteLine("watermarks_count = 1");
                writer.Close();
                m_Settings.bUseMultithread = true;
                m_Settings.bUseWatermark = false;
                m_Settings.sWatermarkFolder = "nil";
                m_Settings.sDefaultPicturesFolder = "nil";
                m_Settings.iPixelCount = 1;
                m_Settings.iThreadCount = 1;
                m_Settings.iWatermarksCount = 1;
            }
        }
        public void Save()
        {
            string settings_path = Environment.CurrentDirectory + "\\settings.ini";
            File.Delete(settings_path);
            var writer = File.CreateText(settings_path);
            writer.WriteLine("use_multithread = " + save_get_bool(m_Settings.bUseMultithread));
            writer.WriteLine("use_watermark = " + save_get_bool(m_Settings.bUseWatermark));
            writer.WriteLine("watermark_folder = " + m_Settings.sWatermarkFolder);
            writer.WriteLine("pictures_folder = " + m_Settings.sDefaultPicturesFolder);
            writer.WriteLine("pixel_count = " + m_Settings.iPixelCount.ToString());
            writer.WriteLine("max_threads_count = " + m_Settings.iThreadCount.ToString());
            writer.WriteLine("watermarks_count = " + m_Settings.iWatermarksCount.ToString());
            writer.Close();
        }
    }
    class Globals
    {
        public static Settings settings = new Settings();
    }
}
