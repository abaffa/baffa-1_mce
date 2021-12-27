using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class HW_UART
    {

        //BAFFA1_BYTE status;

        public Queue<byte> uart_in = new Queue<byte>();
        public Queue<byte> uart_out = new Queue<byte>();
        public byte[] data = new byte[6];


        public Baffa1_CPU baffa1_cpu;


        public void Init(Baffa1_CPU baffa1_cpu)
        {

            this.data[5] = 0xFF;

            this.baffa1_cpu = baffa1_cpu;
        }

        public bool Write()
        {

            if (this.uart_in.Count > 0)
            {
                this.data[0] = this.uart_in.Dequeue();
                Console.Write((char)this.data[0]);

                return true;
            }

            return false;
        }

        public bool Read()
        {


            if (this.uart_out.Count > 0)
            {
                lock (this.data)
                {
                    this.data[0] = this.uart_out.Dequeue();
                }


                return true;
            }
            return false;
        }

        public byte GetLsr()
        {

            if ((this.data[5] & 0x20) == 0x00)
            {
                this.data[5] = 0x20;

                if (this.uart_in.Count >0)
                    this.data[5] |= 0x80;

                if (this.uart_out.Count > 0)
                    this.data[5] |= 0x41; //0x40;

            }
            else
                this.data[5] = (byte)((this.data[5] & 0xDF) | ((~(this.data[5] & 0x20)) & 0x20)); // | ~(this.data[5] & 0x20);

            return this.data[5];
        }


        public void Receive(byte data)
        {

            lock (uart_out) { 
            this.uart_out.Enqueue(data);

            this.baffa1_cpu.microcode.controller_bus.int_req = (byte)(this.baffa1_cpu.microcode.controller_bus.int_req | 0b10000000);
            }
        }



        public void Send(byte data)
        {
            this.uart_in.Enqueue(data);
        }


        public string Print(string dir, int changed)
        {

            int i = 0;
            string print = String.Format(">>> UART [{0}]:", dir);
            for (i = 0; i < 6; i++)
            {
                if (changed == i)
                    print += String.Format("[{0}", this.data[i].ToString("X2"));
                else if (changed == i - 1)
                    print += String.Format("]{0}", this.data[i].ToString("X2"));
                else
                    print += String.Format(" {0}", this.data[i].ToString("X2"));
            }
            if (changed == 5)
                print += String.Format("]\n");
            else
                print += String.Format("\n");

            return print;
        }

    }
}
