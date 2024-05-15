using Azure.Core;
using Lib_Models.Models_Table_Class.File;
using Microsoft.AspNetCore.Components.Routing;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

namespace API_Application.Service.FileService
{
    public static class FileInFolder
    {
        public static (byte[], string) ReadFile(string fileName, string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            string extension = TypeFile(fileName);
            string mimeType = GetMimeType(extension);

            if (extension == "docx")
            {
                // Thực hiện chuyển đổi từ docx sang PDF
                using (FileStream docStream = new FileStream(Path.GetFullPath(filePath), FileMode.Open, FileAccess.Read))
                {
                    // Tạo Word document từ stream
                    using (WordDocument wordDocument = new WordDocument(docStream, FormatType.Docx))
                    {
                        // Sử dụng DocIORenderer để chuyển đổi Word document sang PDF
                        using (DocIORenderer render = new DocIORenderer())
                        {
                            // Chuyển đổi Word document thành PDF document
                            PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);

                            // Lưu PDF document vào MemoryStream
                            MemoryStream stream = new MemoryStream();
                            pdfDocument.Save(stream);
                            stream.Position = 0;

                            // Trả về MemoryStream và kiểu MIME cho PDF
                            return (stream.ToArray(), "application/pdf");
                        }
                    }
                }
            }
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                Console.WriteLine("extension: "+ extension);
                return (fileBytes, mimeType);
            }

            return (null!, null!);
        }
        public static void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "File", "baitap", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            else
            {
                throw new FileNotFoundException("File not found", filePath);
            }
        }
        public static void AddFileInFolder_FileSrc(FileAddRequest file)
        {
            string path = file.path!;
            IFormFile fileAdd = file.file!;
            string fileNameRequest = fileAdd.FileName;
            string fileName = file.newFileName! + "." + TypeFile(fileNameRequest);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileAdd.CopyTo(fileStream);
            }
        }

        private static string TypeFile(string fileNameRequest)
        {
            int lenghtFileNameRequest = fileNameRequest.Length;
            int lastIndexOfDot = fileNameRequest.LastIndexOf('.');
            int count_Cut_FrommatFile = lenghtFileNameRequest - lastIndexOfDot;
            string text_Cut_FrommatFile = fileNameRequest.Substring(lastIndexOfDot + 1, count_Cut_FrommatFile - 1);
            return text_Cut_FrommatFile!;
        }

        private static string GetMimeType(string extension)
        {
            // Tùy thuộc vào extension, trả về định dạng mime tương ứng
            // Đây là một ví dụ, bạn có thể mở rộng để hỗ trợ nhiều loại file khác nhau
            switch (extension.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return "image/jpeg";
                case "png":
                    return "image/png";
                case "zip":
                    return "application/zip";
                case "pdf":
                    return "application/pdf";
                case "xlsx":
                    return "application/xlsx";
                case "docx":
                    return "application/pdf";
                case "doc":
                    return "application/msword";
                case "rtf":
                    return "application/rtf";
                case "odt":
                    return "application/vnd.oasis.opendocument.text";
                default:
                    return "application/octet-stream"; // Loại mặc định
            }
        }
    }
}
