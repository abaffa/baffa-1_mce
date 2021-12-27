using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Tasm_Opcode
    {

        public string opcode { get; set; }
        public string desc { get; set; }
        public int size { get; set; }


        public Tasm_Opcode(string opcode, string desc, int size)
        {
            this.opcode = opcode;
            this.desc = desc;
            this.size = size;
        }
    }
}
