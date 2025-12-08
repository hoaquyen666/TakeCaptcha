using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Assembly_CSharp.Collector
{
    public class CaptchaCollector
    {
        private static string folderName = "acceptCaptcha";

        public static void SaveCaptcha(Image img)
        {
            try
            {
                if (img == null)
                {
                    Debug.Log("Captcha image is null, cannot save.");
                    return;
                }

                string path = folderName;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Texture2D tex = img.texture;

                if (tex != null)
                {
                    byte[] bytes = tex.EncodeToPNG();

                    string fileName = "captcha_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".png";
                    string fullPath = Path.Combine(path, fileName);

                    File.WriteAllBytes(fullPath, bytes);

                    Debug.Log("Saved Captcha to: " + fullPath);
                }
                else
                {
                    Debug.Log("Texture in Image is null.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error saving captcha: " + ex.Message);
            }
        }
    }
}
