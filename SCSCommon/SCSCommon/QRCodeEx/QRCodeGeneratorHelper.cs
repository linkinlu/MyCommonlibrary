using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using ZXing;

namespace SCSCommon.QRCodeEx
{
    /// <summary>
    /// Refer from https://github.com/codebude/QRCoder 
    /// </summary>
    public class QRCodeGeneratorHelper
    {
        /// <summary>
        /// Generators the qr code.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="insideIcon">The inside icon.</param>
        /// <param name="insidePercent">The inside percent.</param>
        /// <returns></returns>
        public static Bitmap GeneratorQRCode(string text , Bitmap insideIcon = null, int insidePercent = 15)
        {
            //默认容错率在15% 由于手机摄像头分辨率的不同 会导致读不出QRcode 算法也有不同
            //ECCLevel写在这里也可让调用方不用引用qrcoder.dll 
            QRCodeGenerator.ECCLevel level = QRCodeGenerator.ECCLevel.L;
            using (var generaoter = new QRCodeGenerator())
            {
                using (QRCodeData data = generaoter.CreateQrCode(text, level)) 
                {
                    QRCode code = new QRCode(data);
                    //  return code.GetGraphic(20, Color.Black, Color.White, insideIcon, insidePercent);
                    return null;
                }
            }
        }

        public static string DecodeQRCode(string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return string.Empty;
            }

            var actualBase64 = base64.Replace("data:image/jpg;base64,", string.Empty);
    
            var file = Convert.FromBase64String(actualBase64);
            using (var ms = new MemoryStream(file, 0, file.Length))
            {
                var bt = new Bitmap(new MemoryStream(file));
             //   IBarcodeReader reader = new BarcodeReader();
                //TODO
                //b
                //var res = reader.Decode(bt.b, bt.Width,bt.Height, bt.RawFormat);
              //  return res != null ? res.Text : string.Empty;
            }

            return string.Empty;
        }


        public static string DecodeQRCodeFromFile(string imageFilePath)
        {
            if (File.Exists(imageFilePath))
            {
               // IBarcodeReader reader = new BarcodeReader();
                ///var bt = (Bitmap)Image.FromFile(imageFilePath);
                //TODO
               // var res = reader.Decode(bt);
                //return res != null ? res.Text : string.Empty;
            }

            return "";

           
        }
    }

}
