using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MathQGen.UI
{
    public class WebRenderer
    {
        public static Bitmap RenderHTML(string html)
        {
            Bitmap ret = null;
            var th = new Thread(() =>
            {
                var webBrowser = new WebBrowser();
                webBrowser.ScrollBarsEnabled = false;
                webBrowser.DocumentCompleted += (object sender, WebBrowserDocumentCompletedEventArgs e) =>
                {
                    using (Bitmap bitmap = new Bitmap(webBrowser.Width,webBrowser.Height))
                    {
                        webBrowser.DrawToBitmap(bitmap,new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
                        // bitmap.Save(@"filename.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
                        ret = bitmap;
                    }
                };
                webBrowser.DocumentText = html;
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            return ret;
        }
    }
}