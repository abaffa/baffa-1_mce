﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa1_mce
{
    public class InstructionItem
    {
        public string Text;
        public string OpCode;
        public string Value;

        public override string ToString()
        {
            return this.Text;
        }

        public string InstructionCode()
        {
            return OpCode + ": " + Text;
        }
    }
}
