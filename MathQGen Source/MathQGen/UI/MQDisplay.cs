using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using LibMathQGen;

namespace MathQGen.UI
{
    public partial class MQDisplay : UserControl
    {
        public MQData data = null;
        public Form parent = null;
        public bool showinganswers = false;
        public MQDisplay(MQData mqdata, Form f)
        {
            parent = f;
            InitializeComponent();
            LoadData(mqdata);
        }
        public MQDisplay()
        {
            InitializeComponent();
        }

        public void LoadData(MQData mqdata)
        {
            data = mqdata;
            metroLabel1.Text = data.Instruction;
            if (data.Marks != null)
            {
                metroLabel2.Text = "(" + data.Marks + ")";
            }
            else
            {
                metroLabel2.Text = "";
            }
            DisplayPanel.Controls.Clear();
            DisplayPanel.Controls.Add(new FunctionDisplay(mqdata.function,mqdata.IsLatex) { Dock = DockStyle.Fill
            });

            metroButton1.Enabled = data.answers;

            DisplayPanel.Controls[0].Dock = DockStyle.Bottom;
            DisplayPanel.Controls[0].Height = 0;
            Transitions.Transition.run(DisplayPanel.Controls[0], "Height", DisplayPanel.Height, new Transitions.TransitionType_EaseInEaseOut((int)(700*Settings.Default.AnimationSpeed)));
            Task.Factory.StartNew(() => {
                Thread.Sleep((int)(800 * Settings.Default.AnimationSpeed));
                this.Invoke(new Action(() =>
                {
                    DisplayPanel.Controls[0].Dock = DockStyle.Fill;
                    metroButton1.Text = "Answer";
                    DisplayPanel.AutoScroll = false;
                }));
            });
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            showinganswers = !showinganswers;
            if (showinganswers)
            {
                DisplayPanel.Controls[0].Dock = DockStyle.Top;
                DisplayPanel.Controls[0].Height = DisplayPanel.Height;
                Transitions.Transition.run(DisplayPanel.Controls[0], "Height", 0, new Transitions.TransitionType_EaseInEaseOut((int)(700 * Settings.Default.AnimationSpeed)));
                //Transitions.Transition.run(DisplayPanel.Controls[0], "PaddingResponsive", ((LaTeXDisplay)DisplayPanel.Controls[0]).Width, new Transitions.TransitionType_EaseInEaseOut(700));
                // Transitions.Transition.run(parent, "Opacity", 0, new Transitions.TransitionType_EaseInEaseOut(700));
                //new System.Threading.Thread(() =>
                //{
                //    System.Threading.Thread.Sleep(700);
                //    metroLabel1.Text = "Answer:";
                //    metroLabel2.Text = "";
                //    metroButton1.Text = "Question";
                //    DisplayPanel.Controls.Clear();
                //    foreach (MQStepData step in data.steps)
                //    {
                //        StepUC suc = new StepUC(step) { Dock = DockStyle.Top };
                //        DisplayPanel.Controls.Add(suc);
                //    }
                //}).Start();
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep((int)(600 * Settings.Default.AnimationSpeed));
                    this.Invoke(new Action(() => {
                        metroLabel1.Text = "Answer:";
                        metroLabel2.Text = "";
                        metroButton1.Text = "Question";
                        DisplayPanel.Controls.Clear();
                        Plexiglass p = new Plexiglass(this.parent);
                      //  p.Opacity = 0.3;
                       // p.Controls.Add(new Label() { Text = "Loading...", Dock = DockStyle.Fill });
                        //Transitions.Transition.run(p, "Opacity", 0.0,1.0, new Transitions.TransitionType_EaseInEaseOut(100));
                        for (int i = data.steps.Count; i-- > 0;)
                        {
                            StepUC suc = new StepUC(data.steps[i]) { Dock = DockStyle.Top };
                            DisplayPanel.Controls.Add(suc);
                            Task.Factory.StartNew(() =>
                            {
                                Thread.Sleep(300);
                                this.Invoke(new Action(() => suc.OnClick(null, null)));
                            });
                        }
                        p.Close();
                        DisplayPanel.AutoScroll = true;
                    }));
                });
            }
            else
            {
                foreach (var c in DisplayPanel.Controls)
                {
                    //c.Height = 5;
                    Transitions.Transition.run(c, "Height", 0, new Transitions.TransitionType_EaseInEaseOut((int)(700 * Settings.Default.AnimationSpeed)));
                }
                Task.Factory.StartNew(() => {
                    Thread.Sleep((int)(DisplayPanel.Controls.Count * 50 *Settings.Default.AnimationSpeed));
                    this.Invoke(new Action(() => { 
                        LoadData(data);
                    }));
                });
            }
        }

        private void DisplayPanel_Resize(object sender, EventArgs e)
        {
            if (Settings.Default.AnswersDynamicHeight)
            {
                foreach (Control c in DisplayPanel.Controls)
                {
                    Transitions.Transition.run(c, "Height", DisplayPanel.Height / Settings.Default.AnswersPerPage, new Transitions.TransitionType_EaseInEaseOut((int)(700 * Settings.Default.AnimationSpeed)));
                }
            }
        }
    }
}
