using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentFTP;
using SCSCommon.Web;

namespace SCSCommon.IO
{
    public class FtpUtil
    {
        static async Task<T> OnFtp<T>(FtpServerBase ftp, Func<FtpServerBase, Task<T>> onFtp)
        {

            if (ftp == null || string.IsNullOrWhiteSpace(ftp.Host))
                throw new Exception("Ftp server has not been configurated.");
            return await onFtp(ftp);
        }

        public static async Task<FtpFile> Upload(FtpServerBase ftpServer, byte[] data, string directory, string name)
        {
            return await OnFtp(ftpServer, async (ftp) => await ftp.Upload(data, directory, name));
        }

        public static async Task<bool> Delete(FtpServerBase ftpServer, string directory, string name)
        {
            return await OnFtp(ftpServer, async (ftp) => await ftp.Delete(directory, name));
        }

        public static async Task<Stream> DownloadFileStream(FtpServerBase ftpServer, string directory, string name)
        {
            return await OnFtp(ftpServer, async (ftp) => await ftp.Download(directory, name));
        }

        public static async Task<HttpResponseMessage> Download(FtpServerBase ftpServer, string directory, string name)
        {
            var stream = await OnFtp(ftpServer, async (ftp) => await ftp.Download(directory, name));
            var result = CreateHttpResponseMessage(stream, name);
            return result;
        }


        static HttpResponseMessage CreateHttpResponseMessage(Stream content, string fileName)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(content)
            };

            var lastDot = fileName.LastIndexOf('.');
            var extension = lastDot >= 0 ? fileName.Substring(lastDot, fileName.Length - lastDot) : string.Empty;

            var contentType = WebUtils.GetImageContentType(extension);
            if (contentType == null)
            {
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            else
            {
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }

            return result;
        }
    }


    public class FtpServer : FtpServerBase
    {
        public FtpServer(string host, int port, string user, string password) : base(host, port, user, password)
        {

        }
        static async Task Recycle(IFtpClient ftp, string oldDirectory, string oldName)
        {
            var newDirectory = PathCombine(RecycleBin, DateTime.Now.ToString(yyyyMMdd), oldDirectory);

            if (!await ftp.DirectoryExistsAsync(newDirectory))
                await ftp.CreateDirectoryAsync(newDirectory);

            var oldPath = PathCombine(oldDirectory, oldName);
            var newPath = newDirectory + Slash + RemoveStartAndEndSlash(oldName);

            await ftp.MoveFileAsync(oldPath, newPath);
        }

    
       

        void DeleteRecursively(IFtpClient ftp, string path)
        {
            foreach (var item in ftp.GetListing(path))
            {
                switch (item.Type)
                {
                    case FtpFileSystemObjectType.File:
                        ftp.DeleteFile(item.FullName);
                        break;

                    case FtpFileSystemObjectType.Directory:
                        DeleteRecursively(ftp, item.FullName);
                        break;
                }
            }

            ftp.DeleteDirectory(path);
        }

        public override async Task<FtpFile> Upload(byte[] data, string directory, string name)
        {
         

            using (var ftp = new FtpClient(this.Host, this.Port, this.User, this.Password))
            {
                await ftp.ConnectAsync();

                var path = PathCombineFolder(directory);

                if (!await ftp.DirectoryExistsAsync(path))
                    await ftp.CreateDirectoryAsync(path);

                await ftp.SetWorkingDirectoryAsync(path);

                name = RemoveStartAndEndSlash(name);

                await ftp.UploadAsync(data, name);

                return new FtpFile
                {
                    Directory = path,
                    Name = name
                };
            }
        }

        public override async Task<bool> Delete(string directory, string name)
        {
          

            using (var ftp = new FtpClient(this.Host, this.Port, this.User, this.Password))
            {
                await ftp.ConnectAsync();

                var path = PathCombine(directory, name);
                if (await ftp.FileExistsAsync(path))
                {
                    await Recycle(ftp, directory, name);
                    return true;
                }

                path = PathCombineFolder(directory, name);
                if (await ftp.FileExistsAsync(path))
                {
                    await Recycle(ftp, PathCombine(directory), name);
                    return true;
                }

                return true;
            }
        }

        public override async Task<Stream> Download(string directory, string name)
        {
            using (var ftp = new FtpClient(this.Host, this.Port, this.User, this.Password))
            {
                await ftp.ConnectAsync();

                var data = new MemoryStream();

                do
                {
                    var path = PathCombine(directory, name);
                    if (await ftp.FileExistsAsync(path))
                    {
                        await ftp.DownloadAsync(data, path);
                        break;
                    }

                    path = PathCombineFolder(directory, name);
                    if (await ftp.FileExistsAsync(path))
                    {
                        await ftp.DownloadAsync(data, path);
                        break;
                    }
                } while (false);

                data.Position = 0;
                return data;
            }
        }
    }

    public abstract class FtpServerBase
    {
        protected const string Slash = "/";
        protected const string yyyyMMdd = "yyyy-MM-dd";
      
        protected static DateTime? lastRelease;
        //protected static readonly object syncObj = new object();
        protected const string RecycleBin = "/_RecycleBin";
        //protected static readonly Regex regexFolder = new Regex(@"^\d{4}-\d{2}-\d{2}$");

        public FtpServerBase(string host, int port, string user, string password)
        {
            this.Host = host;
            this.Port = port;
            this.User = user;
            this.Password = password;
        }
        public int Port { get; }
        public string Host { get; }
        public string User { get; }
        public string Password { get; }

        public virtual async Task<FtpFile> Upload(byte[] data, string directory, string name)
        {
            return await Task.FromResult(default(FtpFile));
        }


        public virtual async Task<bool> Delete(string directory, string name)
        {
            return await Task.FromResult(default(bool));
        }

        public virtual async Task<Stream> Download(string directory, string name)
        {
            return await Task.FromResult(default(Stream));
        }


        public static string RemoveStartAndEndSlash(string path)
        {
            if (path == null)
                path = string.Empty;

            path = path.Trim();
            if (path.StartsWith(Slash))
                path = path.Remove(0, 1);

            if (path.EndsWith(Slash))
                path = path.Remove(path.Length - 1, 1);

            return path;
        }

        public static string PathCombine(params string[] paths)
        {
            return Slash + (paths == null || paths.Length == 0 ? string.Empty : string.Join(Slash, paths.Select(RemoveStartAndEndSlash)));
        }

        public static string PathCombineFolder(string folder, params string[] paths)
        {
            folder = Slash + RemoveStartAndEndSlash(folder);

            var directory = PathCombine(paths);

            return directory.StartsWith(folder, StringComparison.OrdinalIgnoreCase) ? directory : folder + directory;
        }

    }


    public class FtpFile
    {
        public string Directory { get; set; }
        public string Name { get; set; }
    }
}
