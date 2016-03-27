using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQGen.UI
{
    public partial class FunctionDisplay : UserControl
    {
        public int size = 2000;
        private string curfunction = "";
        private bool IsLatex = true;
        public int PaddingResponsive
        {
            get {
                if (Padding.Horizontal == Padding.Vertical)
                {
                    return Padding.All;
                }
                else
                {
                   return Padding.Horizontal;
                }
            }
            set { Padding = new Padding(value); }
        }
        public FunctionDisplay()
        {
            InitializeComponent();
         //   LoadFunction("ax^2+bx+c");
           // CalcSize();
        }

        public FunctionDisplay(string func, bool latex)
        {
            IsLatex = latex;
            InitializeComponent();
            LoadFunction(func);
            CalcSize();
        }

        public void LoadFunction(string func)
        {
            curfunction = func;
            size = 6000 / func.Length;
            if (size < 1000)
            {
                size = 1000;
            }

            if (IsLatex&&!func.Contains("www."))
            {
                pictureBox1.Load(new Uri(@"http://www.texrendr.com/cgi-bin/mathtex.cgi?\dpi{" + size.ToString() + @"}" + func).AbsoluteUri);
            }
            else
            {
                //pictureBox1.Image = (WebRenderer.RenderHTML(func));
                this.Controls.Clear();
                WebBrowser wb = new WebBrowser();
                wb.ScriptErrorsSuppressed = true;
                wb.Dock = DockStyle.Fill;
                if (!func.StartsWith(@"http://"))
                {
                    if (func.Contains("<html>"))
                    {
                        wb.DocumentText = func;
                    }
                    else
                    {
                        wb.DocumentText = "<html><head></head><body style=\"\r\n    position: absolute;\r\n    left: 50%;\r\n    top: 50%;        /*    Nope =(    margin-left: -25%;    margin-top: -25%;    */      /*     Yep!    */\r\n    transform: translate(-50%, -50%);        /*    Not even necessary really.     e.g. Height could be left out!    */\r\n    width: 40%;\r\n    height: 50%;\r\n\">" + func + "</body></html>";
                    }
                }
                else
                {
                    wb.Navigate(func);
                }
                this.Controls.Add(wb);
             //   wb.Document.Body.Style = "zoom:300%;";
            }
        }
        public void CalcSize()
        {
            //y=2^(x/41000) where y = image scaling factor and x = pixel count<<< FAIL!!
            //size = (int)Math.Pow(2, (ClientSize.Width * ClientSize.Height) / 41000);
            // Console.WriteLine((ClientSize.Width * ClientSize.Height)/1000);
            //if (curfunction != "")
            //{
            //    size = (ClientSize.Width * (ClientSize.Height / 3)) / 300;
            //    // Console.WriteLine(size);
            //    LoadFunction(curfunction);
            //}
        }

        private void LaTeXDisplay_Resize(object sender, EventArgs e)
        {
            //CalcSize();
           // Console.WriteLine(curfunction.Length);
            int w = (int)( ClientSize.Width/curfunction.Length);
            int h = (int)(ClientSize.Height / curfunction.Length);
            this.Padding = new Padding(h, h, h, h);
        }
    }
}
