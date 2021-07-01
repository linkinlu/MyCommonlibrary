using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.IO
{
    public class ImageUtils
    {
        public void ToThumbNail(string fileWithFullPath, string outputdir, int? height = 800, int? width = 600)
        {
            var fileName = Path.GetFileNameWithoutExtension(fileWithFullPath);
            using (Image image = Image.FromFile(fileWithFullPath))
            {
                var count = image.GetFrameCount(FrameDimension.Page);
                if (count < 2)
                {
                    Image thumb = image.GetThumbnailImage(width.Value, height.Value, () => false, IntPtr.Zero);
                    thumb.Save($"{outputdir}\\{fileName}.jpg");
                }
                else
                {
                    var index = 0;
                    var offsetWidth = 0;
                    List<Image> images = new List<Image>();
                    for (int i = 0; i < count; i++)
                    {
                        image.SelectActiveFrame(FrameDimension.Page, i);
                        Image thumb = image.GetThumbnailImage(width.Value, height.Value, () => false, IntPtr.Zero);
                        images.Add(thumb);
                    }

                    using (var bt = new Bitmap(width.Value, height.Value * count))
                    {
                        using (var g = Graphics.FromImage(bt))
                        {
                            g.Clear(SystemColors.AppWorkspace);
                            foreach (var imageItem in images)
                            {
                                if (index == 0)
                                {
                                    g.DrawImage(imageItem, new Point(0, 0));
                                    index++;
                                    offsetWidth = height.Value;
                                }
                                else
                                {
                                    g.DrawImage(imageItem, new Point(0, offsetWidth));
                                    offsetWidth += height.Value;
                                }

                                imageItem.Dispose();
                            }
                        }
                        bt.Save($"{outputdir}\\{fileName}.jpg");

                    }



                }


            }
        }
    }
}
