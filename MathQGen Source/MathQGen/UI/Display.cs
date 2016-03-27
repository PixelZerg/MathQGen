using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Opulos.Core.UI;
using LibMathQGen;

namespace MathQGen.UI
{
    public partial class Display : MetroForm
    {
        object lsender = null;
        GeneratorData cursel = null;
        int curselindex = -1;
        public Display()
        {
            InitializeComponent();
            metroTabControl1.SelectedTab = metroTabPage1;

            Accordion acc = new Accordion();
            acc.CheckBoxMargin = new Padding(2);
            //acc.ContentMargin = new Padding(15, 5, 15, 5);
            acc.ContentPadding = new Padding(1);
            acc.Insets = new Padding(5);
            acc.ControlBackColor = Color.White;
            // acc.ContentBackColor = Color.CadetBlue;

            //List<List<IGenerator>> gens = new List<List<IGenerator>>();
            //foreach (Topic topic in Enum.GetValues(typeof(Topic)))
            //{
            //    List<IGenerator> glist = new List<IGenerator>();
            //    ListBox l = new ListBox { Dock = DockStyle.Fill };
            //    foreach (var gen in Serialiser.generators)
            //    {
            //        if (topic == gen.Key.GetTopic())
            //        {
            //            l.Items.Add(gen.Key.GetName());
            //            glist.Add(gen.Key);
            //        }
            //    }
            //    l.SelectedIndexChanged += (object sender, EventArgs e) =>
            //    {
            //        lsender = sender;
            //        try
            //        {
            //            cursel = glist[l.SelectedIndex];
            //            curselindex = l.SelectedIndex;
            //            RunButton.Enabled = true;
            //            LoadCurSelInfo();
            //        }
            //        catch (Exception ex){ Console.WriteLine(ex); }
            //    };
            //    acc.Add(l, topic.ToString("G"));
            //    gens.Add(glist);
            //}

            foreach (var pair in Serialiser.gens)
            {
                ListBox l = new ListBox() { Dock = DockStyle.Fill };
                foreach (GeneratorData gd in pair.Value)
                {
                    l.Items.Add(gd.generator.GetName());
                }
                l.SelectedIndexChanged += (object sender, EventArgs e) =>
                  {
                      lsender = sender;
                      try
                      {
                          cursel = pair.Value[l.SelectedIndex];
                          curselindex = l.SelectedIndex;
                          RunButton.Enabled = true;
                          LoadCurSelInfo();
                      }
                      catch (Exception ex)
                      {
                          Console.WriteLine(ex);
                      }
                  };
                acc.Add(l, pair.Key);
            }

            //this.Controls.Add(acc);
            // this.metroTabPage1.Controls[0]
            splitContainer1.Panel1.Controls.Add(acc);

            metroLabel2.Text = 2 - Settings.Default.AnimationSpeed + "x";
            metroTrackBar1.Value = (int)((200 * Settings.Default.AnimationSpeed) / 2);

            numericUpDown1.Value = Settings.Default.AnswersPerPage;
            metroToggle1.Checked = Settings.Default.AnswersDynamicHeight;
        }
        public void LoadCurSelInfo()
        {
            //long comp = -1;
            //int na = 0;
            //foreach (var gen in Serialiser.generators)
            //{
            //    if (gen.Key.GetTopic() == cursel.GetTopic())
            //    {
            //        if (na == curselindex)
            //        {
            //            comp = gen.Value;
            //        }
            //        na++;
            //    }
            //}
            Console.WriteLine("\t" + cursel + " selected!");
            InfoBox.Text = string.Format("Generator: {0}{1}{0}{0}Type: {0}{2}{0}{0}Algorithm Complexity: {0}{7}{0}{0}Extra Info: {0}{4}{0}{0}"
                + "Topic: {0}{3}{0}{0}Author: {0}{5}{0}{0}Description: {0}{6}{0}{0}",
                Environment.NewLine,cursel.generator.GetName(),cursel.generator.GetType(),cursel.generator.GetTopic(),cursel.generator.ToString(),cursel.generator.GetAuthor(),cursel.generator.GetDescription(),cursel.speed);
            LoadCurSel();
        }

        public void LoadCurSel()
        {
            // metroTabPage2.
            tab2.Controls.Clear();
            tab2.Controls.Add(new MQDisplay(cursel.generator.Generate(""),this) { Dock = DockStyle.Fill });
        }

        private void Display_ResizeEnd(object sender, EventArgs e)
        {
            //this.latex.CalcSize();
            //this.laTeXDisplay2.CalcSize();
        }

        private void Display_Shown(object sender, EventArgs e)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    // metroTabControl1.Refresh();
            //    //metroTabControl1.Update();
            //    //metroTabControl1.Focus();
            //    metroTabControl1.SelectTab(0);
            //    System.Threading.Thread.Sleep(10);
            //}
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, new Point(0, 0), new Point(0, splitContainer1.Panel2.Height));
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, new Point(splitContainer1.Panel1.Width, 0), new Point(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height));
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            //Size s = tab2.Size;
            //tab2.Height = 0;
            metroTabControl1.SelectedTab = metroTabPage2;
            //Transitions.Transition.run(tab2, "Height", s.Height, new Transitions.TransitionType_EaseInEaseOut(700));
            //tab2.Size = s;
            try
            {
                ((ListBox)lsender).ClearSelected();
            }
            catch { }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            LoadCurSel();
        }

        private void metroTrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Settings.Default.AnimationSpeed = (double)(metroTrackBar1.Value / 100m);// divided by 200 times 2
            metroLabel2.Text = 2-Settings.Default.AnimationSpeed+"x";
        }

        private void Display_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AnswersDynamicHeight = metroToggle1.Checked;
            Console.WriteLine(Settings.Default.AnswersDynamicHeight);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.AnswersPerPage = (int)numericUpDown1.Value;
        }
    }
}
