using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Register_8Bit
    {

        private byte reg;

        public Baffa1_Register_8Bit()
        {
            this.reg = 0x0;
        }

        public byte Value()
        {
            return this.reg;
        }

        public void Reset()
        {
            this.reg = 0x0;
        }

        public void Set(byte v)
        {
            this.reg = v;
        }
    }
}
