using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ApiServer.Models
{
    public class FileModel
    {
        public void CreateFileFromStream(string filepath, Stream stream)
        {
            stream.Position = 0;
            using (var fileStream = System.IO.File.Create(filepath))
            {
                stream.CopyTo(fileStream);
            }
        }

        public void UnZip(string zipPath, string extractPath)
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        public void AttachToClient(Guid clientId, string filePath)
        {
            Upload recentUpload = new Upload() { Id = Guid.NewGuid(), FilePath = filePath, Created = DateTime.Now, FileName = Path.GetFileName(filePath) };
            ClientModel.clients.FirstOrDefault(x => x.Id == clientId).RecentUploads.Add(recentUpload);
        }
    }
}