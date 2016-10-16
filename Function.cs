using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Function // Standart function from three arguments
    {
        public const int TRUTH_TABLE_LENGTH = 8;
        public string Text // Text interpretation of finished function
        {
            get
            {
                if ((A == null) && (B == null) && (C == null))
                    return string.Format(this.mask, "A", "B", "C");
                else
                    return string.Format(this.mask, A.Text, B.Text, C.Text);
            }
        }
        private string _mask;
        // Mask to create new function calls based on this function
        public string mask
        {
            get { return _mask; }
            set
            {
                if (value == null) throw new ArgumentException("Mask cannot be null");
                _mask = value;
            }
        }
        public int NumberOfOperations { get {
                if ((A == null) && (B == null) && (C == null))
                    return 0;
                else
                    return 1 + A.NumberOfOperations + B.NumberOfOperations + C.NumberOfOperations;
            } } // number of operations what had been used to create this
        // This is three arguments, which form the Function
        public Function A{get;private set;}
        public Function B{get;private set;}
        public Function C { get; private set; }
        private byte byteTable; // Таблица истинности

        private Function(string Mask, byte code)
        {
            byteTable = code;
            mask = Mask;
            //numberOfOperations = 0;
        }

        public Function(string Mask, byte[] TruthTable)//Create a function with this truth table
        {
            byteTable = CalcByteCode(TruthTable);
            mask = Mask;
            A = null;
            B = null;
            C = null;
            //numberOfOperations = 0;
        }


        /// <summary>
        /// Create new function FN, which result is same as F(a(A,B,C),b(A,B,C),c(A,B,C))
        /// </summary>
        /// <returns>New function</returns>
        public Function combine(Function a, Function b, Function c) //operate with this operands
        {
            byte generatedCode = 0;
            for (int i = 0; i < TRUTH_TABLE_LENGTH; i++)
            {
                generatedCode |= (byte)(this.operate(
                    (byte)((a.GetCode() >> i) & 1),
                    (byte)((b.GetCode() >> i) & 1),
                    (byte)((c.GetCode() >> i) & 1))
                    << i);
            }
            Function NF = new Function(this.mask, generatedCode);
            //NF.numberOfOperations = a.numberOfOperations + b.numberOfOperations + c.numberOfOperations + 1;
            NF.A = a;
            NF.B = b;
            NF.C = c;
            return NF;
        }

        /// <summary>
        /// Create new function FN, which result is same as F(a(A,B,C),b(A,B,C),c(A,B,C))
        /// </summary>
        /// <returns>New function</returns>
        public byte operate(byte a, byte b, byte c) //Get result of function
        {
            if (a > 1 || b > 1 || c > 1)
            {
                throw new ArgumentException("Bit values must be 0 or 1");
            }
            return (byte)((GetCode() >> ((a << 2) | (b << 1) | c)) & 1);
        }

        private static bool checkTruthTableCorrect(byte[] table)
        {
            return (table != null && table.Length == TRUTH_TABLE_LENGTH)
                && !Array.Exists(table, x => { return x != 0 && x != 1; });
        }
        private static byte CalcByteCode(byte[] Table)
        {
            int code = 0;
            for (int i = 0; i < Table.Length; i++)
            {
                code ^= Table[i] << i;
            }
            return (byte)code;
        }
        public int GetCode()
        {
            return byteTable;
        }

        private static byte[] builtTable(byte code)
        {
            byte[] res = new byte[TRUTH_TABLE_LENGTH];
            for (int i = 0; i < TRUTH_TABLE_LENGTH; i++)
            {
                res[i] = (byte)((code >> i) & 1);
            }
            return res;
        }

        public void setBit(int position, int value)
        {
            if (value != 0 && value != 1)
            {
                throw new ArgumentOutOfRangeException("Bit can be only 0 or 1");
            }
            if (position < 0 || position >= TRUTH_TABLE_LENGTH)
            {
                throw new ArgumentOutOfRangeException("Position out of range. Must be nonnegative and less than " + TRUTH_TABLE_LENGTH);
            }
            int selector = 1 << position;
            // xxxxAxxxx => 0000A0000 => xxxx0xxxx=>xxxxVxxxx
            byteTable = (byte)((byteTable & selector) ^ byteTable | (value << position));
        }

        public int getBit(int position)
        {
            if (position < 0 || position >= TRUTH_TABLE_LENGTH)
            {
                throw new ArgumentOutOfRangeException("Position out of range. Must be nonnegative and less than " + TRUTH_TABLE_LENGTH);
            }
            return 1 & (GetCode() >> position);
        }

        public override string ToString()
        {
            int k = 0;
            for (int i = 0; i < TRUTH_TABLE_LENGTH / 2; i++)
            {
                k |= (1 & (byteTable >> i)) << TRUTH_TABLE_LENGTH - i - 1;
                k |= (1 & (byteTable >> TRUTH_TABLE_LENGTH - i - 1)) << i;
            }
            return String.Format("{0} {1}: {2}", Text,k, Convert.ToString(k,2).PadLeft(8,'0'));
        }

        public void ReplaceArgument(Function f)
        {
            A = bestArgument(A, f);
            B = bestArgument(B, f);
            C = bestArgument(C, f);
        }

        private static Function bestArgument(Function arg, Function f)
        {
            if (arg!=null && arg.GetCode() == f.GetCode() && arg.NumberOfOperations > f.NumberOfOperations)
            {
                return f;
            }
            else return arg;
        }
    }

}