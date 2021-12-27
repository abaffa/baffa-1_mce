using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baffa_1_emul
{
    public class HW_TTY
    {

        public bool Debug_call { get; set; }
        public bool started { get; set; }
        public bool console { get; set; }

        List<byte> tty_in = new List<byte>();

        public TextBox TextOutput {get;set;}


        public void SetInput(bool b)
        {
            this.console = b;
        }



        public void Print(String s)
        {
            if (this.started)
            {
                int i = 0;
                while (s[i] != '\0')
                {
                    if (s[i] == '\n')
                    {
                        Send((byte)'\r');
                        Send((byte)s[i]);
                    }
                    else
                        Send((byte)s[i]);
                    i++;
                }
            }
            if (TextOutput == null)
                Console.Write(s);
            else
                TextOutput.Text += s.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        public void Send(byte s)
        {

        }
        public byte Receive()
        {
            
            byte ch = 0x00;
            SetInput(true);
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ch = (byte)Console.ReadKey(true).KeyChar;
                    break;
                }
                else if (this.tty_in.Count > 0)
                {

                    byte data = this.tty_in[0]; this.tty_in.RemoveAt(0);
                    ch = data;
                    break;
                }

                //Sleep(10);
            }
            SetInput(false);

            return ch;
        }


        public string Gets(uint max_value)
        {
            char[] input = new char[257];

            int i = 0;
            for (i = 0; i < 256 && i < max_value;)
            {
                char cur_input = GetChar();
                if (cur_input == (char)8)
                {
                    if (i > 0)
                    {
                        Print(cur_input.ToString());
                        i--;
                    }
                }
                else if (cur_input != '\n' && cur_input != '\r')
                {
                    Print(cur_input.ToString().ToUpper());
                    input[i] = cur_input;
                    i++;
                }
                else
                {
                    Print("\r\n");
                    break;
                }
            }
            input[i] = '\0';

            return (new string(input)).Trim('\0');
        }

        public char GetChar()
        {
            return (char)Receive();
        }


        public string GetLine()
        {
            
            char[] input = new char[257];

            int i = 0;
            for (i = 0; i < 256;)
            {
                char cur_input = GetChar();
                if (cur_input == (char)8)
                {
                    if (i > 0)
                    {
                        Print(cur_input.ToString());
                        i--;
                    }
                }
                else if (cur_input != '\n' && cur_input != '\r')
                {
                    Print(cur_input.ToString().ToUpper());
                    input[i] = cur_input;
                    i++;
                }
                else
                {
                    Print("\r\n");
                    break;
                }
            }
            input[i] = '\0';

            return (new string(input)).Trim('\0');
        }
    }
}
