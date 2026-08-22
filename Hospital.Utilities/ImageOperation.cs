using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Hospital.Utilities
{
    public class ImageOperations
    {
        private readonly IWebHostEnvironment _env;

        public ImageOperations(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string ImageUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }

            // إنشاء اسم جديد للصورة لمنع التكرار
            string fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(file.FileName);

            // إنشاء مجلد الصور إذا لم يكن موجودًا
            string folderPath = Path.Combine(
                _env.WebRootPath,
                "images",
                "doctors"
            );

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // المسار النهائي للصورة
            string filePath = Path.Combine(
                folderPath,
                fileName
            );

            // حفظ الصورة
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // إرجاع اسم الصورة فقط
            return fileName;
        }

        public void DeleteImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return;

            string filePath = Path.Combine(
                _env.WebRootPath,
                "images",
                "doctors",
                imageName
            );

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}