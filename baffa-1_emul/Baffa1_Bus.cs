using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa_1_emul
{
    public class Baffa1_Bus
    {
        public byte data_bus { get; set; }

        public byte k_bus { get; set; } // input pra alu k -> y
        public byte w_bus { get; set; } // input pra alu w -> x

        public Baffa1_Alu_Bus alu_bus = new Baffa1_Alu_Bus();

        /////
        public byte bus_tristate(Baffa1_Registers baffa1_registers)
        {
            return (byte)(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_DMA_ACK) | Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_HALT)); //IC151
        }

        public byte bus_rd(Baffa1_Registers baffa1_registers, byte rd, byte panel_rd)
        {

            byte ret = 0x00;

            if (bus_tristate(baffa1_registers) != 0x00)
                ret = panel_rd;
            else
                ret = rd;

            return (byte)((~ret) & 0b00000001);
        }

        public byte bus_wr(Baffa1_Registers baffa1_registers, byte wr, byte panel_wr)
        {

            byte ret = 0x00;

            if (bus_tristate(baffa1_registers) != 0x00)
                ret = panel_wr;
            else
                ret = wr;

            return (byte)((~ret) & 0b00000001);
        }


        public byte bus_mem_io(Baffa1_Registers baffa1_registers, byte mem_io, byte panel_mem_io)
        {

            byte ret = 0x00;

            if (bus_tristate(baffa1_registers) != 0x00)
                ret = panel_mem_io;
            else
                ret = mem_io;

            return ret;
        }


        public void Init()
        {
            this.data_bus = 0b00000000;
            this.k_bus = 0b00000000; // input pra alu x e y
            this.w_bus = 0b00000000; // input pra alu x e y

            this.alu_bus.x_bus = 0b00000000; //alu entrada
            this.alu_bus.y_bus = 0b00000000; //alu entrada
            this.alu_bus.z_bus = 0b00000000; //alu saida


            // flags do alu
            this.alu_bus.alu_zf = 0x00; // ZeroFlag
            this.alu_bus.alu_cf = 0x00; // Carry Flag
            this.alu_bus.alu_of = 0x00; // Overflow Flag

            this.alu_bus.alu_final_cf = 0x00;
            this.alu_bus.alu_output = 0x00;
        }

        public void reset()
        {
            this.data_bus = 0b00000000;
            this.k_bus = 0b00000000; // input pra alu x e y
            this.w_bus = 0b00000000; // input pra alu x e y

            this.alu_bus.x_bus = 0b00000000; //alu entrada
            this.alu_bus.y_bus = 0b00000000; //alu entrada
            this.alu_bus.z_bus = 0b00000000; //alu saida
        }


        public byte k_bus_refresh(Baffa1_Registers baffa1_registers, byte alu_b_src)
        {

            //IC92 //I103 //IC116

            byte k_bus = 0x00;
            switch (alu_b_src & 0b00000011)
            {
                case 0x00: k_bus = baffa1_registers.MDRl.Value(); break;
                case 0x01: k_bus = baffa1_registers.MDRh.Value(); break;
                case 0x02: k_bus = baffa1_registers.TDRl.Value(); break;
                case 0x03: k_bus = baffa1_registers.TDRh.Value(); break;
            }

            return k_bus;
        }

        public byte w_bus_refresh(
            Baffa1_Registers baffa1_registers,
            byte panel_regsel,
            byte alu_a_src,
            byte display_reg_load,
            byte int_vector,
            byte int_masks,
            byte int_status,
            TextWriter fa, bool DEBUG_RDREG, HW_TTY hw_tty)
        {

            //IC125 //IC118 //IC3   //IC2  //IC9  //IC42  //IC6   //IC20 //IC5  //IC80  //IC41  //IC44
            //IC30  //IC130 //IC56  //IC62 //IC53 //IC133 //IC68  //IC69 //IC67 //IC141 //IC81
            //IC82  //IC71  //IC144 //IC85 //IC86 //IC84  //IC152 //IC88 //IC89 //IC86  //IC160

            byte w_bus = 0x00;

            byte inABC = 0x00;
            byte inAB = 0x00;

            if (bus_tristate(baffa1_registers) == 0x00 & display_reg_load == 0x00)
            {
                inABC = (byte)(alu_a_src & (byte)0b00000111);
                inAB = (byte)(Utils.GetByteBit(alu_a_src, 3) | Utils.SetByteBit(Utils.GetByteBit(alu_a_src, 4), 1));
            }
            else
            {
                inABC = (byte)(panel_regsel & 0b00000111);
                inAB = (byte)(Utils.GetByteBit(panel_regsel, 3) | Utils.SetByteBit(Utils.GetByteBit(panel_regsel, 4), 1));
            }

            if (inAB == 0x00)
            {
                switch (inABC)
                {
                    case 0x00:
                        w_bus = baffa1_registers.Al.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Al", w_bus); }
                        break;
                    case 0x01:
                        w_bus = baffa1_registers.Ah.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Ah", w_bus); }
                        break;
                    case 0x02:
                        w_bus = baffa1_registers.Bl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Bl", w_bus); }
                        break;
                    case 0x03:
                        w_bus = baffa1_registers.Bh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Bh", w_bus); }
                        break;
                    case 0x04:
                        w_bus = baffa1_registers.Cl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Cl", w_bus); }
                        break;
                    case 0x05:
                        w_bus = baffa1_registers.Ch.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Ch", w_bus); }
                        break;
                    case 0x06:
                        w_bus = baffa1_registers.Dl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Dl", w_bus); }
                        break;
                    case 0x07:
                        w_bus = baffa1_registers.Dh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "Dh", w_bus); }
                        break;
                }

            }
            else if (inAB == 0x01)
            {
                switch (inABC)
                {
                    case 0x00:
                        w_bus = baffa1_registers.SPl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "SPl", w_bus); }
                        break;
                    case 0x01:
                        w_bus = baffa1_registers.SPh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "SPh", w_bus); }
                        break;
                    case 0x02:
                        w_bus = baffa1_registers.BPl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "BPl", w_bus); }
                        break;
                    case 0x03:
                        w_bus = baffa1_registers.BPh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "BPh", w_bus); }
                        break;
                    case 0x04:
                        w_bus = baffa1_registers.SIl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "SIl", w_bus); }
                        break;
                    case 0x05:
                        w_bus = baffa1_registers.SIh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "SIh", w_bus); }
                        break;
                    case 0x06:
                        w_bus = baffa1_registers.DIl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "DIl", w_bus); }
                        break;
                    case 0x07:
                        w_bus = baffa1_registers.DIh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "DIh", w_bus); }
                        break;
                }
            }
            else if (inAB == 0x02)
            {
                switch (inABC)
                {
                    case 0x00:
                        w_bus = baffa1_registers.PCl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "PCl", w_bus); }
                        break;
                    case 0x01:
                        w_bus = baffa1_registers.PCh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "PCh", w_bus); }
                        break;
                    case 0x02:
                        w_bus = baffa1_registers.MARl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "MARl", w_bus); }
                        break;
                    case 0x03:
                        w_bus = baffa1_registers.MARh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "MARh", w_bus); }
                        break;
                    case 0x04:
                        w_bus = baffa1_registers.MDRl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "MDRl", w_bus); }
                        break;
                    case 0x05:
                        w_bus = baffa1_registers.MDRh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "MDRh", w_bus); }
                        break;
                    case 0x06:
                        w_bus = baffa1_registers.TDRl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "TDRl", w_bus); }
                        break;
                    case 0x07:
                        w_bus = baffa1_registers.TDRh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "TDRh", w_bus); }
                        break;
                }
            }
            else if (inAB == 0x03)
            {
                switch (inABC)
                {
                    case 0x00:
                        w_bus = baffa1_registers.SSPl.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "SSPl", w_bus); }
                        break;
                    case 0x01:
                        w_bus = baffa1_registers.SSPh.Value(); if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "SSPh", w_bus); }
                        break;
                    case 0x02:
                        w_bus = int_vector; if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "INT_VECTOR", w_bus); }
                        break;
                    case 0x03:
                        w_bus = int_masks; if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "INT_MASKS", w_bus); }
                        break;
                    case 0x04:
                        w_bus = int_status; if (DEBUG_RDREG && fa != null) { Utils.Reg8BitPrint(fa, "READ ", "INT_STATUS", w_bus); }
                        break;
                }
            }
            return w_bus;
        }


        public byte x_bus_refresh(Baffa1_Registers baffa1_registers, byte alu_a_src, byte w_bus)
        {

            byte x_bus = 0x00;

            if (!Utils.CheckByteBit(alu_a_src, 5))
                x_bus = w_bus;

            else
            {

                switch ((alu_a_src & 0b00000011))
                {
                    case 0x00:

                        x_bus = (byte)(
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_ZF), 0) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_CF), 1) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_SF), 2) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_OF), 3) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_12), 4) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_13), 5) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_14), 6) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWh.Value(), Baffa1_Registers.MSWh_15), 7)
                            );
                        break;

                    case 0x01:

                        x_bus = (byte)(
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_DMA_ACK), 0) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_INTERRUPT_ENABLE), 1) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_CPU_MODE), 2) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_PAGING_EN), 3) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_HALT), 4) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_DISPLAY_REG_LOAD), 5) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_14), 6) |
                            Utils.SetByteBit(Utils.GetByteBit(baffa1_registers.MSWl.Value(), Baffa1_Registers.MSWl_DIR), 7)
                            );
                        break;

                    case 0x02:
                        x_bus = baffa1_registers.Gl.Value();
                        break;

                    case 0x03:
                        x_bus = baffa1_registers.Gh.Value();
                        break;
                }
            }

            return x_bus;
        }
    }
}
