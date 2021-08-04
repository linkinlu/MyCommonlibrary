using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SCSCommon.Extension;

namespace SCSCommon.Controller
{
    //上传到指定服务器目录
    public class UploadController : ApiController
    {


        [HttpPost]
        public async Task<IHttpActionResult> Upload([FromUri] string id)
        {

            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new Exception("请携带附件");
            }

            
            var root = AppConfigrationEx.GetValue<string>("ImageRoot", _ => _) ??
                           "C:\\VueEquipment";
                var folder = "test";
                var outputDir = string.Join(@"\", root.Trim(), folder);
                if (!string.IsNullOrEmpty(outputDir) && !System.IO.Directory.Exists(outputDir))
                {
                    System.IO.Directory.CreateDirectory(outputDir);
                }

                var provider = new FilenameMultipartFormDataStreamProvider(outputDir);
                await request.Content.ReadAsMultipartAsync(provider);

                foreach (var i in provider.FileData)
                {
                    {
                        var indexOfExtension = i.LocalFileName.LastIndexOf(".", StringComparison.Ordinal);
                        if (indexOfExtension > -1)
                        {
                            var fileNameIndex = i.LocalFileName.LastIndexOf("\\");
                            var fileName = string.Empty;
                            if (fileNameIndex > -1)
                            {
                                fileName = i.LocalFileName.Substring(fileNameIndex + 1,
                                    indexOfExtension - fileNameIndex - 1);
                            }

                            var extesion = i.LocalFileName.Substring(indexOfExtension + 1);
                            var accessory = new Accessory()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = fileName,
                                Path = i.LocalFileName,
                                LocalFileName = RemoveSlash(i.Headers?.ContentDisposition?.FileName)?.Trim('"'),
                                Size = (i.Headers?.ContentDisposition?.Size) ?? request.Content.Headers.ContentLength,
                                Extension = extesion,
                                OwnId = id
                            };


                        }
                    }
                }




                return Ok();

        }

        private string RemoveSlash(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || !token.StartsWith("\"", StringComparison.Ordinal) || (!token.EndsWith("\"", StringComparison.Ordinal) || token.Length <= 1))
                return token;
            return token.Substring(1, token.Length - 2);

        }
    }

    public class FilenameMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public FilenameMultipartFormDataStreamProvider(string path) : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            if (string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName))
            {
                return string.Empty;
            }

            var indexOfFile = headers.ContentDisposition.FileName.LastIndexOf(".", StringComparison.Ordinal);
            if (indexOfFile > 0)
            {
                var actualName = RemoveSlash(headers.ContentDisposition.FileName).Trim('"');
                var fileName = actualName.Substring(0, indexOfFile - 1);
                var extesion = actualName.Substring(indexOfFile);
                return string.Join(".", new string[] { Guid.NewGuid().ToString(), extesion });

                return string.Format("\"{0}\"", string.Join(".", new string[] { Guid.NewGuid().ToString(), extesion }));
            }




            return string.Empty;
            //  return headers.ContentDisposition.FileName;

            //string fileName = headers.ContentDisposition.FileName;
            //if (string.IsNullOrWhiteSpace(fileName))
            //{
            //    fileName = Guid.NewGuid().ToString() + ".data";
            //}
            //return fileName.Replace("\"", string.Empty);


            // return base.GetLocalFileName(headers);
        }

        private string RemoveSlash(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || !token.StartsWith("\"", StringComparison.Ordinal) || (!token.EndsWith("\"", StringComparison.Ordinal) || token.Length <= 1))
                return token;
            return token.Substring(1, token.Length - 2);

        }
    }
    public class Accessory

    {
        //public override object Id { get { return Id; } set { Id = value.ToString(); } }

        public string Id { get; set; }


        public string Name { get; set; }
        public string LocalFileName { get; set; }
        public long? Size { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string OwnId { get; set; }
        public bool IsDeleted { get; set; }

        public string CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string LastUpdateUserId { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}
