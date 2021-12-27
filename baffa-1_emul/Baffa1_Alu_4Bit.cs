using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Alu_4Bit
    {
        public byte alu_output { get; set; }
        public byte COUT { get; set; } //carry out

        public byte EQ { get; set; } //EQ
        public byte P { get; set; }

        public byte G { get; set; }

        // INVERTER
        public static byte IC_74LS04(byte A)
        {
            return Utils.GetByteBit((byte)~A, 0);
        }

        public static byte IC74LS181_B1(byte nA, byte nB, byte S, int block)
        {

            byte S0 = Utils.GetByteBit(S, 0);
            byte S1 = Utils.GetByteBit(S, 1);

            byte andA = Utils.GetByteBit(nA, block);
            byte andB = (byte)(Utils.GetByteBit(nB, block) & S0);
            byte andC = (byte)(S1 & IC_74LS04(Utils.GetByteBit(nB, block)));

            return IC_74LS04((byte)(andA | andB | andC));
        }

        public static byte IC74LS181_B2(byte nA, byte nB, byte S, int block)
        {

            byte S2 = Utils.GetByteBit(S, 2);
            byte S3 = Utils.GetByteBit(S, 3);

            byte andA = (byte)(IC_74LS04(Utils.GetByteBit(nB, block)) & S2 & Utils.GetByteBit(nA, block));
            byte andB = (byte)(Utils.GetByteBit(nA, block) & Utils.GetByteBit(nB, block) & S3);

            return IC_74LS04((byte)(andA | andB));

        }

        public static byte IC74LSL181(byte nA, byte nB, byte S, byte M, byte Cn,
            out byte AeqB, out byte Cn4, out byte nP, out byte nG)
        {

            byte B0_1 = IC74LS181_B1(nA, nB, S, 0);
            byte nB0_1 = IC_74LS04(B0_1);
            byte B0_2 = IC74LS181_B2(nA, nB, S, 0);

            byte B1_1 = IC74LS181_B1(nA, nB, S, 1);
            byte nB1_1 = IC_74LS04(B1_1);
            byte B1_2 = IC74LS181_B2(nA, nB, S, 1);

            byte B2_1 = IC74LS181_B1(nA, nB, S, 2);
            byte nB2_1 = IC_74LS04(B2_1);
            byte B2_2 = IC74LS181_B2(nA, nB, S, 2);

            byte B3_1 = IC74LS181_B1(nA, nB, S, 3);
            byte nB3_1 = IC_74LS04(B3_1);
            byte B3_2 = IC74LS181_B2(nA, nB, S, 3);

            byte nM = IC_74LS04(M);

            byte nF0 = (byte)(IC_74LS04((byte)(Cn & nM)) ^ (nB0_1 & B0_2)); // (B0_1 ^ B0_2); 

            byte nF1 = (byte)(IC_74LS04((byte)((nM & B0_1) | (nM & B0_2 & Cn))) ^ (nB1_1 & B1_2)); //(B1_1 ^ B1_2);

            byte nF2 = (byte)(IC_74LS04((byte)((nM & B1_1) | (nM & B0_1 & B1_2) | (nM & Cn & B0_2 & B1_2))) ^ (nB2_1 & B2_2));  //(B2_1 ^ B2_2); 

            byte nF3 = (byte)(IC_74LS04((byte)((nM & B2_1) | (nM & B1_1 & B2_2) | (nM & B0_1 & B1_2 & B2_2) | (nM & Cn & B0_2 & B1_2 & B2_2))) ^ (nB3_1 & B3_2)); // (B3_1 ^ B3_2)

            AeqB = Utils.GetByteBit((byte)(nF0 & nF1 & nF2 & nF3), 0);
            nP = Utils.GetByteBit(IC_74LS04((byte)(B0_2 & B1_2 & B2_2 & B3_2)), 0);
            nG = Utils.GetByteBit(IC_74LS04((byte)((B0_1 & B1_2 & B2_2 & B3_2) | (B1_1 & B2_2 & B3_2) | (B2_1 & B3_2) | B3_1)), 0);

            Cn4 = Utils.GetByteBit((byte)((Cn & B0_2 & B1_2 & B2_2 & B3_2) | IC_74LS04(nG)), 0);
            byte nF = (byte)((nF3 << 3) | (nF2 << 2) | (nF1 << 1) | (nF0));
            return nF;
        }

        public void Reset()
        {

            //alu._A = 0x00;
            //alu._B = 0x00;
            //alu._C = 0x00;

            //alu.A = 0x00;
            //alu.B = 0x00;

            //alu.C = 0x00;
            alu_output = 0x00;

            //alu.CIN = 0x00; //carry in

            COUT = 0x00; //carry in

            EQ = 0x00;
            P = 0x00;
            G = 0x00;

        }

        //74LS181
        public byte Op(byte A, byte B, byte CIN, byte S, byte M)
        {
            Reset();

            byte _EQ = EQ;
            byte _COUT = COUT;
            byte _P = P;
            byte _G = G;

            alu_output = IC74LSL181(A, B, S, M, CIN, out _EQ, out _COUT, out _P, out _G);

            EQ = _EQ;
            COUT = _COUT;
            P = _P;
            G = _G;

            return alu_output;
        }
    }
}
