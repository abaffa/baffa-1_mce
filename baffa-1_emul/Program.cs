using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    class Program
    {
        static void Main(string[] args)
        {
            Baffa1_Computer baffa1_computer = new Baffa1_Computer();

            baffa1_computer.Init();
            baffa1_computer.Run();
        }
    }
}
