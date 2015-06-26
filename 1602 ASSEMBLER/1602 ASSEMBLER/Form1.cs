using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1602_ASSEMBLER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < getLines().Length; i++)
            {
                string line = getLines()[i];
                if (line.ToUpper().IndexOf("HLT") != -1)
                {
                    textBox2.Text += " 0x00, ";
                }
                if (line.ToUpper().IndexOf("MOV") != -1)
                {
                    handleMov(line.ToLower());
                }
                if (line.ToUpper().IndexOf("MVX") != -1)
                {
                    handleMvx(line.ToLower());
                }
                if (line.ToUpper().IndexOf("LDX") != -1)
                {
                    handleLdx(line.ToLower());
                }
                if (line.ToUpper().IndexOf("STR") != -1)
                {
                    handleStr(line.ToLower());
                }
                if (line.ToUpper().IndexOf("STI") != -1)
                {
                    handleSti(line.ToLower());
                }
                if (line.ToUpper().IndexOf("CMP") != -1)
                {
                    handleCmp(line.ToLower());
                }
                if (line.ToUpper().IndexOf("CMI") != -1)
                {
                    handleCmi(line.ToLower());
                }
                if (line.ToUpper().IndexOf("JMP") != -1)
                {
                    string temp = line.ToLower().Replace("jmp", "").Replace(" ", "").Trim();
                    textBox2.Text += "0x07, " + temp + ", ";
                }
                if (line.ToUpper().IndexOf("CJP") != -1)
                {
                    handleCjp(line.ToLower());
                }
                if (line.ToUpper().IndexOf("ADD") != -1)
                {
                    handleAdd(line.ToLower());
                }
                if (line.ToUpper().IndexOf("SUB") != -1)
                {
                    handleSub(line.ToLower());
                }
                if (line.ToUpper().IndexOf("ADC") != -1)
                {
                    handleAdc(line.ToLower());
                }
                if (line.ToUpper().IndexOf("SBC") != -1)
                {
                    handleSbc(line.ToLower());
                }
                if (line.ToUpper().IndexOf("SDT") != -1)
                {
                    handleSdt(line.ToLower());
                }
                if (line.ToUpper().IndexOf("MDB") != -1)
                {
                    handleMdb(line.ToLower());
                }
                if (line.ToUpper().IndexOf("VRM") != -1)
                {
                    handleVrm(line.ToLower());
                }

            }
            string tt = textBox2.Text.Replace(" ", "").Trim();
            textBox2.Text = tt;
                
        }

        private void handleVrm(string p)
        {
            string temp = p.Replace(" ", "").Replace("vrm", "").Trim();
            if (temp.Length > 32) MessageBox.Show("Error","Text Exceeded Limit Of 32 Character");
            textBox2.Text += "0x1E, ";
        
            for (byte i = 0; i < 32; i++)
            {
                if (i < temp.Length)
                {
                    string hex = Convert.ToByte(temp[i]).ToString("X");
                    textBox2.Text += "0x" + hex + ", ";
                }
                else
                {
                    textBox2.Text += "0x00, ";
                }
            }
        }

        private void handleMdb(string p) // MDB 0xd5 -> address of the first byte 0x01,0x02 data seperated by comma 
        {
            string temp = p.Replace(" ", "").Replace("mdb", "").Trim();
            if (temp.IndexOf('"') != -1)
            {
                textBox2.Text += "0x0F, ";
                temp = temp.Replace("\"", "");
                string size;
                if (temp.Length < 9)
                {
                    size = temp.Length.ToString("X");
                    size = "0x0" + size + ", ";
                }
                else
                {
                    size = temp.Length.ToString("X");
                    size = "0x" + size + ", ";
                }
                textBox2.Text += size;
                for (int i = 0; i < temp.Length; i++)
                {
                    string hex = Convert.ToByte(temp[i]).ToString("X");
                    textBox2.Text += "0x" + hex + ", ";
                }
            }
            else
            {
                textBox2.Text += "0x0F, " + temp.Substring(0, 4) + ", " + temp.Substring(4) + ", ";
            }
        }

        private void handleSdt(string p)
        {
            string temp = p.Replace(" ", "").Replace("sdt","").Trim();
            textBox2.Text += "0x" + temp + "e, ";
        }
        private void handleSbc(string p)
        {
            string temp = p.Replace("sbc", "").Replace(" ", "").Trim();
            switch (temp)
            {
                case "ax":
                    textBox2.Text += "0x0C, ";
                    break;
                case "ah":
                    textBox2.Text += "0x1C, ";
                    break;
                case "al":
                    textBox2.Text += "0x2C, ";
                    break;
                case "bh":
                    textBox2.Text += "0x3C, ";
                    break;
                case "bl":
                    textBox2.Text += "0x4C, ";
                    break;
                case "lx":
                    textBox2.Text += "0x5C, ";
                    break;
            }
        } 
        private void handleAdc(string p)
        {
            string temp = p.Replace("adc", "").Replace(" ", "").Trim();
            switch (temp)
            {
                case "ax":
                    textBox2.Text += "0x0B, ";
                    break;
                case "ah":
                    textBox2.Text += "0x1B, ";
                    break;
                case "al":
                    textBox2.Text += "0x2B, ";
                    break;
                case "bh":
                    textBox2.Text += "0x3B, ";
                    break;
                case "bl":
                    textBox2.Text += "0x4B, ";
                    break;
                case "lx":
                    textBox2.Text += "0x5B, ";
                    break;
            }
        } 
        private void handleSub(string p)
        {
            string temp = p.Replace("sub", "").Replace(" ", "").Trim();
            switch (temp)
            {
                case "ax":
                    textBox2.Text += "0x0A, ";
                    break;
                case "ah":
                    textBox2.Text += "0x1A, ";
                    break;
                case "al":
                    textBox2.Text += "0x2A, ";
                    break;
                case "bh":
                    textBox2.Text += "0x3A, ";
                    break;
                case "bl":
                    textBox2.Text += "0x4A, ";
                    break;
                case "lx":
                    textBox2.Text += "0x5A, ";
                    break;
            }
        } 
        private void handleAdd(string p)
        {
            string temp = p.Replace("add", "").Replace(" ", "").Trim();
            switch (temp)
            {
                case "ax":
                    textBox2.Text += "0x09, ";
                    break;
                case "ah":
                    textBox2.Text += "0x19, ";
                    break;
                case "al":
                    textBox2.Text += "0x29, ";
                    break;
                case "bh":
                    textBox2.Text += "0x39, ";
                    break;
                case "bl":
                    textBox2.Text += "0x49, ";
                    break;
                case "lx":
                    textBox2.Text += "0x59, ";
                    break; 
            }
        } 

        private void handleCjp(string p)
        {

            string temp = p.Replace("cjp", "").Replace(" ", "").Trim();
            textBox2.Text += "0x" + temp.Substring(0, 1) + "8, " + temp.Substring(1) + ", ";
        }

        private void handleCmi(string p)
        {
            string temp = p.Replace("cmi", "").Replace(" ", "").Trim();
            textBox2.Text += "0x06, " + temp + ", ";
        }

        private void handleCmp(string p)
        {
            string temp = p.Replace("cmp", "").Replace(" ", "").Trim();
            switch (temp)
            {
                case "ax":
                    textBox2.Text += "0x05, ";
                    break;
                case "ah":
                    textBox2.Text += "0x15, ";
                    break;
                case "al":
                    textBox2.Text += "0x25, ";
                    break;
                case "bh":
                    textBox2.Text += "0x35, ";
                    break;
                case "bl":
                    textBox2.Text += "0x45, ";
                    break;
                case "lx":
                    textBox2.Text += "0x55, ";
                    break; 
            }
        }

        private void handleSti(string p)
        {
            string temp = p.Replace("sti", "").Replace(" ", "").Trim();
            textBox2.Text += "0x04, " + temp.Substring(0, 4) + ", " + temp.Substring(4) + ", ";
        }
        //ldx ax 0x05 mov ax 0x06
        private void handleStr(string p)
        {
            string temp = p.Replace("str", "").Replace(" ", "");
            switch (temp.Substring(0, 2))
            {
                case "ax":
                    textBox2.Text += "0x03, ";
                    break;
                case "ah":
                    textBox2.Text += "0x13, ";
                    break;
                case "al":
                    textBox2.Text += "0x23, ";
                    break;
                case "bh":
                    textBox2.Text += "0x33, ";
                    break;
                case "bl":
                    textBox2.Text += "0x43, ";
                    break;
                case "lx":
                    textBox2.Text += "0x53, ";
                    break; 
            }
            textBox2.Text += temp.Substring(2) + ", ";
        }

        private void handleLdx(string p)
        {
            string temp = p.Replace("ldx ", "").Replace(" ", "");
            switch (temp.Substring(0, 2))
            {
                case "ax":
                    textBox2.Text += "0x02, ";
                    break;
                case "ah":
                    textBox2.Text += "0x12, ";
                    break;
                case "al":
                    textBox2.Text += "0x22, ";
                    break;
                case "bh":
                    textBox2.Text += "0x32, ";
                    break;
                case "bl":
                    textBox2.Text += "0x42, ";
                    break;
                case "lx":
                    textBox2.Text += "0x52, ";
                    break; 
            }
            textBox2.Text += temp.Substring(2) + ", ";
            Console.WriteLine("askim  "+ temp.Substring(2));

        }

        private void handleMvx(string p)
        {
            textBox2.Text += "0x0D, ";
            string temp = p.Replace("mvx ", "").Replace(" ", "").Trim();
            switch (temp.Substring(0, 2))
            {
                case "ax":
                    textBox2.Text += "0x0";
                    break;
                case "ah":
                    textBox2.Text += "0x1";
                    break;
                case "al":
                    textBox2.Text += "0x2";
                    break;
                case "bh":
                    textBox2.Text += "0x3";
                    break;
                case "bl":
                    textBox2.Text += "0x4";
                    break;
                case "lx":
                    textBox2.Text += "0x5";
                    break;
            }
            switch (temp.Substring(2))
            {
                case "ax":
                    textBox2.Text += "0, ";
                    break;
                case "ah":
                    textBox2.Text += "1, ";
                    break;
                case "al":
                    textBox2.Text += "2, ";
                    break;
                case "bh":
                    textBox2.Text += "3, ";
            Console.WriteLine(temp.Substring(2));
                    break;
                case "bl":
                    textBox2.Text += "4, ";
                    break;
                case "lx":
                    textBox2.Text += "5, ";
                    break;
            }

            

        }

        private string[] getLines()
        {
            return textBox1.Text.Split('\n');
        }
        private void handleMov(string temp)
        {
            temp = temp.Replace("mov", "").Replace(" ","");
            if (temp.IndexOf("ax") != -1)
            {
                temp = temp.Replace("ax", "");
                textBox2.Text += "0x01, " + temp + ", ";
            }
            if (temp.IndexOf("ah") != -1)
            {
                temp = temp.Replace("ah", "");
                textBox2.Text += "0x11, " + temp + ", ";
            }
            if (temp.IndexOf("al") != -1)
            {
                temp = temp.Replace("al", "");
                textBox2.Text += "0x21, " + temp + ", ";
            }
            if (temp.IndexOf("bh") != -1)
            {
                temp = temp.Replace("bh", "");
                textBox2.Text += "0x31, " + temp + ", ";
            }
            if (temp.IndexOf("bl") != -1)
            {
                temp = temp.Replace("bl", "");
                textBox2.Text += "0x41, " + temp + ", ";
            }

            if (temp.IndexOf("lx") != -1)
            {
                temp = temp.Replace("lx", "");
                textBox2.Text += "0x51, " + temp + ", s";
            }
        }

        private void aboutTheAsseblerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "ASM files|*.asm";
            theDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(theDialog.FileName.ToString());
                StreamReader sr = new StreamReader(theDialog.FileName.ToString());
                string line;
                textBox1.Text = "";
                while ((line = sr.ReadLine()) != null)
                {
                    textBox1.Text += line + "\r\n";
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            using (System.IO.TextWriter tw = new System.IO.StreamWriter(saveFileDialog1.FileName))
            {
                foreach (string line in textBox1.Text.Split('\n'))
                {
                    tw.WriteLine(line);
                }
            }
        }
    }
}
