using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RFQA_ConnectionTest{
    public partial class Form1 : Form{
        // CMD vars
        public string selectedIP = "78.46.38.131";
        //const string rainforestQaIP = "rainforestqa.com";
        //const string rainforestAppIP = "rainforestapp.com";
        //const string amazonws3IP = "s3.amazonaws.com";

        const uint amtOfCycles = 10;

        public string par1 = "";
        public string par2 = "";

        public static bool WIP;
        public static bool WIP2;
        //===========================
        // Ping var's
        public bool alreadyRun;
        const string tPref = "v";
        const string tSuf = ".ext.prd.rnfrst.com";
        int offlineAmount = 0;

        public Form1(){
            InitializeComponent();

            textBox1.ForeColor = SystemColors.GrayText;
            textBox1.Text = "custom domain/IP";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.textBox1.Enter += new EventHandler(this.textBox1_Enter);

            // Populate drop down list
            for(int i=1; i<=125; i++){
                comboBox1.Items.Add("@rf_VM [" + i.ToString("000") + "]");
            }
            // @rainforest_qa
            // @rainforest_app
            // @amazon_ws3

        }

        private void Form1_Load(object sender, EventArgs e){
            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e){

        }

        public void button1_Click(object sender, EventArgs e){
            par1 = "ping ";
            par2 = " -n " + amtOfCycles;

            StartThread();
        }
        private void button2_Click(object sender, EventArgs e){
            par1 = "tracert ";
            par2 = "";

            StartThread();
        }
        public void StartThread(){
            Thread cmdThread;
            cmdThread = new Thread(DoWork);
            cmdThread.IsBackground = true;
            cmdThread.Start(42);
        }
        public void StartThreadB(){
            Thread pingThread;
            pingThread = new Thread(DoPing);
            pingThread.IsBackground = true;
            pingThread.Start(42);
        }
        // Thread
        public void DoWork(object data){
            if (!WIP) WIP = true;
            else return;

            //custom domain/IP
            if (textBox1.Text != "custom domain/IP" && textBox1.Text != ""){
                selectedIP = textBox1.Text;
            }

            textBox2.Text = "";

            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = "cmd.exe";
            cmdStartInfo.RedirectStandardOutput = true;
            cmdStartInfo.RedirectStandardError = true;
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            cmdStartInfo.CreateNoWindow = true;

            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.ErrorDataReceived += cmd_Error;
            cmdProcess.OutputDataReceived += cmd_DataReceived;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();
            cmdProcess.StandardInput.WriteLine(par1 + selectedIP + par2);
            cmdProcess.StandardInput.WriteLine("exit");                  

            cmdProcess.WaitForExit();

            WIP = false;

        }
        public void DoPing(object data){
            if (!WIP2) WIP2 = true;
            else return;

            try{
                for (int i = 1; i <= 125; i++){
                    // Ignore Terminal 69
                    SetText("RF_VM " + i.ToString("000") + ": " + PingHost(tPref + i.ToString("000") + tSuf) + Environment.NewLine);
                }
                // RainforestQA
                //SetText("RainQA:         " + PingHost(rainforestQaIP) + Environment.NewLine);
                // RainforestApp
                //SetText("RainApp:        " + PingHost(rainforestAppIP) + Environment.NewLine);
                // AmazonWS
                //SetText("AmazWS3:        " + PingHost(amazonws3IP) + Environment.NewLine);
               
                alreadyRun = true;
            }
            catch(Exception ex) { }

            WIP2 = false;
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.richTextBox1.InvokeRequired){
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }else{
                if (alreadyRun) {
                    richTextBox1.Text = "";
                    alreadyRun = false;
                }this.richTextBox1.Text += text;
            }
        }

        public string PingHost(string nameOrAddress){
            bool pingable = false;
            string pingToStr = "DEAD";
            Ping pinger = new Ping();
            try{
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException){
                // Skip on false
            }
            if (pingable) pingToStr = "ALIVE";
            else {
                offlineAmount++;
            }

            return pingToStr;
        }
        public void cmd_DataReceived(object sender, DataReceivedEventArgs e){
            CheckForIllegalCrossThreadCalls = false;
            Console.SetOut(new TextBoxWriter(textBox2));
            Console.WriteLine(e.Data);
        }

        void cmd_Error(object sender, DataReceivedEventArgs e){
            CheckForIllegalCrossThreadCalls = false;
            Console.SetOut(new TextBoxWriter(textBox2));
            Console.WriteLine(e.Data);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e){

        }

        private void textBox2_TextChanged(object sender, EventArgs e){

        }

        private void label1_Click(object sender, EventArgs e){

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            try{
                switch (comboBox1.SelectedIndex){
                    /*case 125:
                    selectedIP = rainforestQaIP.ToString();
                        break;
                    case 126:
                        selectedIP = rainforestAppIP.ToString();
                        break;
                    case 127:
                        selectedIP = amazonws3IP.ToString();
                        break;*/
                    default:
                    //selectedIP = Dns.GetHostAddresses(tPref + (comboBox1.SelectedIndex + 1).ToString("000") + tSuf)[0].ToString();
                    selectedIP = tPref + (comboBox1.SelectedIndex + 1).ToString("000") + tSuf.ToString();
                        break;
                }
            }
            catch (Exception ex) { }
           
        }

        private void button3_Click(object sender, EventArgs e){
            
        }
        private void button4_Click(object sender, EventArgs e){
            offlineAmount = 0;
            StartThreadB();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e){
            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();

            HighlightPhrase(richTextBox1, "ALIVE", Color.Green);
            HighlightPhrase(richTextBox1, "DEAD", Color.Red);

            label2.Text = "OFFLINE: " + offlineAmount.ToString("000");
        }
        
        static void HighlightPhrase(RichTextBox box, string phrase, Color color){
            int pos = box.SelectionStart;
            string s = box.Text;
            for (int ix = 0; ;){
                int jx = s.IndexOf(phrase, ix, StringComparison.CurrentCultureIgnoreCase);
                if (jx < 0) break;
                box.SelectionStart = jx;
                box.SelectionLength = phrase.Length;
                box.SelectionColor = color;
                ix = jx + 1;
            }
            box.SelectionStart = pos;
            box.SelectionLength = 0;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e){
            
        }

        private void textBox1_Leave(object sender, EventArgs e){
            if (textBox1.Text.Length == 0){
                textBox1.Text = "custom domain/IP";
                textBox1.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e){
            if (textBox1.Text == "custom domain/IP"){
                textBox1.Text = "";
                textBox1.ForeColor = SystemColors.WindowText;
            }
        }

        private void label2_Click(object sender, EventArgs e) {

        }
    }
    public class TextBoxWriter : TextWriter{
        TextBox _output = null;

        public TextBoxWriter(TextBox output){
            _output = output;
        }

        public override void Write(char value){
            base.Write(value);
            _output.AppendText(value.ToString());
        }

        public override Encoding Encoding{
            get { return Encoding.UTF8; }
        }
    }
}
