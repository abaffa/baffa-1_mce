using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class HW_IDE
    {



        int gambi_ide_total;
        int gambi_ide_read;

        public byte[] data { get; set; }

        byte[] memory { get; set; }


        public void Init()
        {

            this.data = new byte[8];

            this.memory = new byte[Baffa1_Config.BAFFA1_IDE_MEMORY_SIZE];

            for (int address = 0; address < Baffa1_Config.BAFFA1_IDE_MEMORY_SIZE; address++)
            {
                this.memory[address] = 0x00;
            }

            Reset();
        }

        public void Reset()
        {
            this.gambi_ide_total = 0;
            this.gambi_ide_read = 0;
        }


        public void Write()
        {
            if (this.data[7] == 0b00001000)
            {

                this.gambi_ide_total = this.data[2];

                ulong sec_address_lba = this.data[3];
                sec_address_lba = sec_address_lba | ((ulong)this.data[4]) << 8;
                sec_address_lba = sec_address_lba | ((ulong)this.data[5]) << 16;
                sec_address_lba = sec_address_lba | ((ulong)(this.data[6] & 0b00001111)) << 24;

                ulong sec_address_byte = sec_address_lba * 512;

                if (sec_address_byte < Baffa1_Config.BAFFA1_IDE_MEMORY_SIZE)
                {
                    this.memory[(int)sec_address_byte + this.gambi_ide_read] = this.data[0];

                    this.gambi_ide_read++;

                    if (this.gambi_ide_read > this.gambi_ide_total * 512)
                    {
                        this.data[7] = 0x00;
                        Reset();

                        Save_disk();
                    }
                }
                else
                {
                    this.data[7] = 0x34;
                    Reset();
                }
            }

        }



        public void Read()
        {

            if (this.data[7] == 0b00001000)
            {

                this.gambi_ide_total = this.data[2];

                ulong sec_address_lba = this.data[3];
                sec_address_lba = sec_address_lba | ((ulong)this.data[4]) << 8;
                sec_address_lba = sec_address_lba | ((ulong)this.data[5]) << 16;
                sec_address_lba = sec_address_lba | ((ulong)(this.data[6] & 0b00001111)) << 24;

                ulong sec_address_byte = sec_address_lba * 512;

                if (sec_address_byte < Baffa1_Config.BAFFA1_IDE_MEMORY_SIZE)
                {
                    this.data[0] = this.memory[(int)sec_address_byte + this.gambi_ide_read];

                    this.gambi_ide_read++;

                    if (this.gambi_ide_read > this.gambi_ide_total * 512)
                    {
                        this.data[7] = 0x00;
                        Reset();
                    }
                }
                else
                {
                    this.data[7] = 0x24;
                    Reset();
                }
            }

        }



        public void Save_disk()
        {

            using (FileStream fs = new FileStream(Baffa1_Config.WORKSPACE + "data.dsk", FileMode.Create))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {

                bw.Write(memory, 0, Baffa1_Config.BAFFA1_IDE_MEMORY_SIZE);
            }

        }

        public void Load_disk()
        {
            using (FileStream fs = new FileStream(Baffa1_Config.WORKSPACE + "data.dsk", FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {

                br.Read(memory, 0, Baffa1_Config.BAFFA1_IDE_MEMORY_SIZE);
            }
        }


        public string Print(string dir, int changed)
        {
            
            string print = String.Format(">>> IDE [{0}]: ", dir);
            int i = 0;

            for (; i < 7; i++)
            {

                if (changed == i)
                    print += String.Format("[{0}({1})", this.data[i].ToString("X2"), this.data[i]);
                else if (changed == i - 1)
                    print += String.Format("]{0}({1})", this.data[i].ToString("X2"), this.data[i]);
                else
                    print += String.Format(" {0}({1})", this.data[i].ToString("X2"), this.data[i]);
            }

            if (changed == i)
                print += String.Format("[{0}({1})", this.data[i].ToString("X2"), this.data[i]);
            else if (changed == i - 1)
                print += String.Format("]{0}({1})", this.data[i].ToString("X2"), this.data[i]);
            else
                print += String.Format(" {0}({1})", this.data[i].ToString("X2"), this.data[i]);

            if (changed == i)
                print += String.Format("]\n");
            else
                print += String.Format("\n");

            return print;
        }

    }
}
