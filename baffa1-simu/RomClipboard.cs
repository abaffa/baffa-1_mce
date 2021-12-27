using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baffa1_mce
{
    public class RomClipboard
    {
        byte[][] clipboard = new byte[RomManager.NBR_INSTRUCTIONS * RomManager.CYCLES_PER_INSTR][];
        string[] info_clip = new string[RomManager.NBR_INSTRUCTIONS * RomManager.CYCLES_PER_INSTR];

        int from1, from2, to1;//, to2;
        int from_origin, from_dest;
        String instr_name_clip = "";


        public void Clear()
        {
            for (int i = 0; i < RomManager.NBR_INSTRUCTIONS * RomManager.CYCLES_PER_INSTR; i++)
            {
                clipboard[i] = new byte[RomManager.NBR_ROMS];
            }
        }

        public void CopyInstruction(int index, RomManager rom) 
        {

            from_origin = index * RomManager.CYCLES_PER_INSTR;

            instr_name_clip = rom.instr_names[index];

            int j = 0;
            for (int i = from_origin; i < from_origin + RomManager.CYCLES_PER_INSTR; i++)
            {
                info_clip[j] = rom.info[i];
                clipboard[j][0] = rom.ROMS[0][i];
                clipboard[j][1] = rom.ROMS[1][i];
                clipboard[j][2] = rom.ROMS[2][i];
                clipboard[j][3] = rom.ROMS[3][i];
                clipboard[j][4] = rom.ROMS[4][i];
                clipboard[j][5] = rom.ROMS[5][i];
                clipboard[j][6] = rom.ROMS[6][i];
                clipboard[j][7] = rom.ROMS[7][i];
                clipboard[j][8] = rom.ROMS[8][i];
                clipboard[j][9] = rom.ROMS[9][i];
                clipboard[j][10] = rom.ROMS[10][i];
                clipboard[j][11] = rom.ROMS[11][i];
                clipboard[j][12] = rom.ROMS[12][i];
                clipboard[j][13] = rom.ROMS[13][i];
                j++;
            }
        }

        public void PasteInstruction(int index, RomManager rom)
        {
            from_dest = index * RomManager.CYCLES_PER_INSTR;

            rom.instr_names[from_origin / RomManager.CYCLES_PER_INSTR] = rom.instr_names[index];
            rom.instr_names[index] = instr_name_clip;

            int j = from_origin;
            for (int i = from_dest; i < from_dest + RomManager.CYCLES_PER_INSTR; i++)
            {
                rom.info[j] = rom.info[i];
                rom.ROMS[0][j] = rom.ROMS[0][i];
                rom.ROMS[1][j] = rom.ROMS[1][i];
                rom.ROMS[2][j] = rom.ROMS[2][i];
                rom.ROMS[3][j] = rom.ROMS[3][i];
                rom.ROMS[4][j] = rom.ROMS[4][i];
                rom.ROMS[5][j] = rom.ROMS[5][i];
                rom.ROMS[6][j] = rom.ROMS[6][i];
                rom.ROMS[7][j] = rom.ROMS[7][i];
                rom.ROMS[8][j] = rom.ROMS[8][i];
                rom.ROMS[9][j] = rom.ROMS[9][i];
                rom.ROMS[10][j] = rom.ROMS[10][i];
                rom.ROMS[11][j] = rom.ROMS[11][i];
                rom.ROMS[12][j] = rom.ROMS[12][i];
                rom.ROMS[13][j] = rom.ROMS[13][i];
                j++;
            }

            j = 0;
            for (int i = from_dest; i < from_dest + RomManager.CYCLES_PER_INSTR; i++)
            {
                rom.info[i] = info_clip[j];
                rom.ROMS[0][i] = clipboard[j][0];
                rom.ROMS[1][i] = clipboard[j][1];
                rom.ROMS[2][i] = clipboard[j][2];
                rom.ROMS[3][i] = clipboard[j][3];
                rom.ROMS[4][i] = clipboard[j][4];
                rom.ROMS[5][i] = clipboard[j][5];
                rom.ROMS[6][i] = clipboard[j][6];
                rom.ROMS[7][i] = clipboard[j][7];
                rom.ROMS[8][i] = clipboard[j][8];
                rom.ROMS[9][i] = clipboard[j][9];
                rom.ROMS[10][i] = clipboard[j][10];
                rom.ROMS[11][i] = clipboard[j][11];
                rom.ROMS[12][i] = clipboard[j][12];
                rom.ROMS[13][i] = clipboard[j][13];
                j++;
            }
        }


        public void CopyCycle(int instr_nbr, int index, RomManager rom)
        {
            bool exit = false;

            for (int i = 0; i < RomManager.CYCLES_PER_INSTR; i++)
            {
                if (index == i)
                {
                    from1 = instr_nbr * RomManager.CYCLES_PER_INSTR + i;
                    for (int j = i; j < RomManager.CYCLES_PER_INSTR; j++)
                    {
                        if (index != j)
                        {
                            to1 = instr_nbr * RomManager.CYCLES_PER_INSTR + j - 1;
                            exit = true;
                            break;
                        }
                    }
                }
                if (exit == true) break;
            }

            for (int i = from1; i <= to1; i++)
            {
                info_clip[i] = rom.info[i];
                clipboard[i][0] = rom.ROMS[0][i];
                clipboard[i][1] = rom.ROMS[1][i];
                clipboard[i][2] = rom.ROMS[2][i];
                clipboard[i][3] = rom.ROMS[3][i];
                clipboard[i][4] = rom.ROMS[4][i];
                clipboard[i][5] = rom.ROMS[5][i];
                clipboard[i][6] = rom.ROMS[6][i];
                clipboard[i][7] = rom.ROMS[7][i];
                clipboard[i][8] = rom.ROMS[8][i];
                clipboard[i][9] = rom.ROMS[9][i];
                clipboard[i][10] = rom.ROMS[10][i];
                clipboard[i][11] = rom.ROMS[11][i];
                clipboard[i][12] = rom.ROMS[12][i];
                clipboard[i][13] = rom.ROMS[13][i];
            }
        }

        public void PasteCycle(int instr_nbr, int index, RomManager rom)
        {
            from2 = instr_nbr * RomManager.CYCLES_PER_INSTR + index;

            for (int i = from2; i <= from2 + to1 - from1; i++)
            {
                rom.info[i] = info_clip[i - (from2 - from1)];
                rom.ROMS[0][i] = clipboard[i - (from2 - from1)][0];
                rom.ROMS[1][i] = clipboard[i - (from2 - from1)][1];
                rom.ROMS[2][i] = clipboard[i - (from2 - from1)][2];
                rom.ROMS[3][i] = clipboard[i - (from2 - from1)][3];
                rom.ROMS[4][i] = clipboard[i - (from2 - from1)][4];
                rom.ROMS[5][i] = clipboard[i - (from2 - from1)][5];
                rom.ROMS[6][i] = clipboard[i - (from2 - from1)][6];
                rom.ROMS[7][i] = clipboard[i - (from2 - from1)][7];
                rom.ROMS[8][i] = clipboard[i - (from2 - from1)][8];
                rom.ROMS[9][i] = clipboard[i - (from2 - from1)][9];
                rom.ROMS[10][i] = clipboard[i - (from2 - from1)][10];
                rom.ROMS[11][i] = clipboard[i - (from2 - from1)][11];
                rom.ROMS[12][i] = clipboard[i - (from2 - from1)][12];
                rom.ROMS[13][i] = clipboard[i - (from2 - from1)][13];
            }
        }

        public void Shift(int instr_nbr, int index, int amount, RomManager rom)
        {
            
            bool exit = false;


            for (int i = 0; i < RomManager.CYCLES_PER_INSTR; i++)
            {
                if (index == i)
                {
                    from1 = instr_nbr * RomManager.CYCLES_PER_INSTR + i;
                    for (int j = i; j < RomManager.CYCLES_PER_INSTR; j++)
                    {
                        if (index != j)
                        {
                            to1 = instr_nbr * RomManager.CYCLES_PER_INSTR + j - 1;
                            exit = true;
                            break;
                        }
                    }
                }
                if (exit == true) break;
            }


            for (int i = from1; i <= to1; i++)
            {

                info_clip[i] = rom.info[i];

                clipboard[i][0] = rom.ROMS[0][i];
                clipboard[i][1] = rom.ROMS[1][i];
                clipboard[i][2] = rom.ROMS[2][i];
                clipboard[i][3] = rom.ROMS[3][i];
                clipboard[i][4] = rom.ROMS[4][i];
                clipboard[i][5] = rom.ROMS[5][i];
                clipboard[i][6] = rom.ROMS[6][i];
                clipboard[i][7] = rom.ROMS[7][i];
                clipboard[i][8] = rom.ROMS[8][i];
                clipboard[i][9] = rom.ROMS[9][i];
                clipboard[i][10] = rom.ROMS[10][i];
                clipboard[i][11] = rom.ROMS[11][i];
                clipboard[i][12] = rom.ROMS[12][i];
                clipboard[i][13] = rom.ROMS[13][i];

                rom.ResetLists(i);
            }

            for (int i = from1; i <= to1; i++)
            {

                rom.info[i + amount] = info_clip[i];

                rom.ROMS[0][i + amount] = clipboard[i][0];
                rom.ROMS[1][i + amount] = clipboard[i][1];
                rom.ROMS[2][i + amount] = clipboard[i][2];
                rom.ROMS[3][i + amount] = clipboard[i][3];
                rom.ROMS[4][i + amount] = clipboard[i][4];
                rom.ROMS[5][i + amount] = clipboard[i][5];
                rom.ROMS[6][i + amount] = clipboard[i][6];
                rom.ROMS[7][i + amount] = clipboard[i][7];
                rom.ROMS[8][i + amount] = clipboard[i][8];
                rom.ROMS[9][i + amount] = clipboard[i][9];
                rom.ROMS[10][i + amount] = clipboard[i][10];
                rom.ROMS[11][i + amount] = clipboard[i][11];
                rom.ROMS[12][i + amount] = clipboard[i][12];
                rom.ROMS[13][i + amount] = clipboard[i][13];
            }
        }

        
        public void Reset(int instr_nbr, int index, RomManager rom)
        {

            bool exit = false;
            for (int i = 0; i < RomManager.CYCLES_PER_INSTR; i++)
            {
                if (index == i)
                {
                    from1 = instr_nbr * RomManager.CYCLES_PER_INSTR + i;
                    for (int j = i; j < RomManager.CYCLES_PER_INSTR; j++)
                    {
                        if (index != j)
                        {
                            to1 = instr_nbr * RomManager.CYCLES_PER_INSTR + j - 1;
                            exit = true;
                            break;
                        }
                    }
                }
                if (exit == true) break;
            }

            for (int i = from1; i <= to1; i++)
            {
                info_clip[i] = rom.info[i];
                rom.ResetLists(i);
            }
        }



        public void ShiftLeft(int cycle_nbr, RomManager rom)
        {
            if (cycle_nbr == 0) return;

            clipboard[0][0] = rom.ROMS[0][cycle_nbr];
            clipboard[0][1] = rom.ROMS[1][cycle_nbr];
            clipboard[0][2] = rom.ROMS[2][cycle_nbr];
            clipboard[0][3] = rom.ROMS[3][cycle_nbr];
            clipboard[0][4] = rom.ROMS[4][cycle_nbr];
            clipboard[0][5] = rom.ROMS[5][cycle_nbr];
            clipboard[0][6] = rom.ROMS[6][cycle_nbr];
            clipboard[0][7] = rom.ROMS[7][cycle_nbr];
            clipboard[0][8] = rom.ROMS[8][cycle_nbr];
            clipboard[0][9] = rom.ROMS[9][cycle_nbr];
            clipboard[0][10] = rom.ROMS[10][cycle_nbr];
            clipboard[0][11] = rom.ROMS[11][cycle_nbr];
            clipboard[0][12] = rom.ROMS[12][cycle_nbr];
            clipboard[0][13] = rom.ROMS[13][cycle_nbr];

            rom.ROMS[0][cycle_nbr - 1] = clipboard[0][0];
            rom.ROMS[1][cycle_nbr - 1] = clipboard[0][1];
            rom.ROMS[2][cycle_nbr - 1] = clipboard[0][2];
            rom.ROMS[3][cycle_nbr - 1] = clipboard[0][3];
            rom.ROMS[4][cycle_nbr - 1] = clipboard[0][4];
            rom.ROMS[5][cycle_nbr - 1] = clipboard[0][5];
            rom.ROMS[6][cycle_nbr - 1] = clipboard[0][6];
            rom.ROMS[7][cycle_nbr - 1] = clipboard[0][7];
            rom.ROMS[8][cycle_nbr - 1] = clipboard[0][8];
            rom.ROMS[9][cycle_nbr - 1] = clipboard[0][9];
            rom.ROMS[10][cycle_nbr - 1] = clipboard[0][10];
            rom.ROMS[11][cycle_nbr - 1] = clipboard[0][11];
            rom.ROMS[12][cycle_nbr - 1] = clipboard[0][12];
            rom.ROMS[13][cycle_nbr - 1] = clipboard[0][13];

            rom.info[cycle_nbr - 1] = rom.info[cycle_nbr];
            rom.info[cycle_nbr] = "";

            rom.ResetLists(cycle_nbr);
        }

        public void ShiftRight(int cycle_nbr, RomManager rom)
        {
            if (cycle_nbr == RomManager.NBR_INSTRUCTIONS * RomManager.CYCLES_PER_INSTR - 1) return;

            clipboard[0][0] = rom.ROMS[0][cycle_nbr];
            clipboard[0][1] = rom.ROMS[1][cycle_nbr];
            clipboard[0][2] = rom.ROMS[2][cycle_nbr];
            clipboard[0][3] = rom.ROMS[3][cycle_nbr];
            clipboard[0][4] = rom.ROMS[4][cycle_nbr];
            clipboard[0][5] = rom.ROMS[5][cycle_nbr];
            clipboard[0][6] = rom.ROMS[6][cycle_nbr];
            clipboard[0][7] = rom.ROMS[7][cycle_nbr];
            clipboard[0][8] = rom.ROMS[8][cycle_nbr];
            clipboard[0][9] = rom.ROMS[9][cycle_nbr];
            clipboard[0][10] = rom.ROMS[10][cycle_nbr];
            clipboard[0][11] = rom.ROMS[11][cycle_nbr];
            clipboard[0][12] = rom.ROMS[12][cycle_nbr];
            clipboard[0][13] = rom.ROMS[13][cycle_nbr];

            rom.ROMS[0][cycle_nbr + 1] = clipboard[0][0];
            rom.ROMS[1][cycle_nbr + 1] = clipboard[0][1];
            rom.ROMS[2][cycle_nbr + 1] = clipboard[0][2];
            rom.ROMS[3][cycle_nbr + 1] = clipboard[0][3];
            rom.ROMS[4][cycle_nbr + 1] = clipboard[0][4];
            rom.ROMS[5][cycle_nbr + 1] = clipboard[0][5];
            rom.ROMS[6][cycle_nbr + 1] = clipboard[0][6];
            rom.ROMS[7][cycle_nbr + 1] = clipboard[0][7];
            rom.ROMS[8][cycle_nbr + 1] = clipboard[0][8];
            rom.ROMS[9][cycle_nbr + 1] = clipboard[0][9];
            rom.ROMS[10][cycle_nbr + 1] = clipboard[0][10];
            rom.ROMS[11][cycle_nbr + 1] = clipboard[0][11];
            rom.ROMS[12][cycle_nbr + 1] = clipboard[0][12];
            rom.ROMS[13][cycle_nbr + 1] = clipboard[0][13];

            rom.info[cycle_nbr + 1] = rom.info[cycle_nbr];
            rom.info[cycle_nbr] = "";

            rom.ResetLists(cycle_nbr);
        }
    }
}
