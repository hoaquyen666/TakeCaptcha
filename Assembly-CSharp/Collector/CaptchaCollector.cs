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
        // Tên folder muốn lưu
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

                // 1. Xác định đường dẫn folder
                // Lưu ở thư mục gốc của game (ngang hàng với file exe) hoặc PersistentDataPath trên mobile
                string path = folderName;

                // Nếu chạy trên Android/iOS thì nên dùng: Application.persistentDataPath + "/" + folderName;

                // 2. Kiểm tra và tạo folder nếu chưa tồn tại
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // 3. Lấy dữ liệu Texture từ đối tượng Image
                // Lưu ý: Trong source game của bạn, class Image thường bọc một Texture2D.
                // Hãy kiểm tra class Image.cs của bạn, nếu biến texture tên khác, hãy sửa lại 'img.texture' bên dưới.
                Texture2D tex = img.texture;

                if (tex != null)
                {
                    // 4. Mã hóa Texture thành PNG
                    byte[] bytes = tex.EncodeToPNG();

                    // 5. Tạo tên file duy nhất dựa trên thời gian thực để không bị trùng
                    string fileName = "captcha_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".png";
                    string fullPath = Path.Combine(path, fileName);

                    // 6. Ghi file xuống ổ đĩa
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
