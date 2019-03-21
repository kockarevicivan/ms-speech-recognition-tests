using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;

namespace SpeechRecognition
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();

        public Form1()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;
            stopBtn.Enabled = true;
            clist.Add(new string[]{"hello","how are you","close"});
            Grammar gr = new Grammar(new GrammarBuilder(clist));

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "hello": {
                    mojTekst.Text = "Hello,Ivan!";
                    ss.SpeakAsync("hello Ivan");
                    //ss.Pause();
                }
                    
                    break;
                case "how are you":
                    mojTekst.Text = "I am fine,how are you?";
                    break;
                case "close":
                    Application.Exit();
                    break;
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            startBtn.Enabled = true;
            stopBtn.Enabled = false;
        }
    }
}
