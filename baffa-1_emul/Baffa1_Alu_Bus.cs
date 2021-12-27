using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Alu_Bus
    {
        public byte x_bus{get;set;} //alu entrada
        public byte y_bus{get;set;} //alu entrada

        public byte z_bus{get;set;} //alu saida

        ////////////////////////////////////////////////
        public byte alu_zf{get;set;} // flags do alu
        public byte alu_cf{get;set;}
        public byte alu_of{get;set;}

        public byte alu_final_cf{get;set;}
        public byte alu_output{get;set;}
    }
}
