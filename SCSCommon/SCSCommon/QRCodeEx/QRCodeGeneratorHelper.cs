using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;

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
            //写在这里也可让调用方不用引用qrcoder.dll 
            QRCodeGenerator.ECCLevel level = QRCodeGenerator.ECCLevel.L;
            using (var generaoter = new QRCodeGenerator())
            {
                using (QRCodeData data = generaoter.CreateQrCode(text, level)) 
                {
                    QRCode code = new QRCode(data);
                    return code.GetGraphic(20, Color.Black, Color.White, insideIcon, insidePercent);
                }
            }
        }
    }

}
