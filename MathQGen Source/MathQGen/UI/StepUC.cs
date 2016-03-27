using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibMathQGen;

namespace MathQGen.UI
{
    public partial class StepUC : UserControl
    {
        public int descsize
        {
            get { return panel1.Size.Width; }
            set { panel1.Size = new Size(value, panel1.Size.Height); }
        }
        public bool open = false;
        public StepUC()
        {
            InitializeComponent();
        }
        public StepUC(MQStepData data)
        {
            InitializeComponent();
            LoadData(data);
        }

        public void LoadData(MQStepData data)
        {
            SetDesc(data.Description);
            Display.Controls.Add(new FunctionDisplay(data.function,data.IsLatex) { Dock = DockStyle.Fill });
            // Console.WriteLine(this.Display.Controls[0].Controls[0]);
            this.Display.Controls[0].Controls[0].Click += OnClick;
        }

        public void SetDesc(string txt)
        {
            //metroLabel1.Text="";
            //foreach (string s in Split(txt, 31))
            //{
            //    //metroLabel1.Text += s + Environment.NewLine;
            //}
            //metroLabel1.Text = txt;
            richTextBox1.Text = txt;
        }
        private IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public void OnClick(object sender, EventArgs e)
        {
            open = !open;
            if (open)
            {
                Transitions.Transition.run(this, "descsize", this.Size.Width/3, new Transitions.TransitionType_EaseInEaseOut((int)(700 * Settings.Default.AnimationSpeed)));
            }
            else
            {
                Transitions.Transition.run(this, "descsize", 0, new Transitions.TransitionType_EaseInEaseOut((int)(700 * Settings.Default.AnimationSpeed)));
            }
        }

        private void StepUC_Resize(object sender, EventArgs e)
        {
            if (open)
            {
                Transitions.Transition.run(this, "descsize", this.Size.Width / 3, new Transitions.TransitionType_EaseInEaseOut((int)(700 * Settings.Default.AnimationSpeed)));
            }
        }
    }
}
