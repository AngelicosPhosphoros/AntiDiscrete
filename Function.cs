using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Function // Standart function from three arguments
    {
        public byte[] truthTable; // Таблица истинности данной функции
        public string Text // Text interpretation of finished function
        {
            get 
            {
                if ((A == null) && (B == null) && (C == null))
                    return string.Format(this.mask, "A","B","C");
                else
                    return string.Format(this.mask, A.Text, B.Text, C.Text);
            }
        }
        public string mask; // Mask to create new function based on this function
        public int numberOfOperation; // number of operations what had been used to create this
        Function A; // This is three arguments;
        Function B;
        Function C;
        public Function(string Mask, byte[] TruthTable)//Create a function with this truth table
        {
            if (TruthTable.Length ==8)
            truthTable = TruthTable;
            else return;
            mask = Mask;
            A = null;
            B = null;
            C = null;
            numberOfOperation = 0;
        }
        
        public Function Operate(Function a, Function b, Function c) //Operate with this operands
        {
            
            Function NF=new Function(this.mask, new byte[] {0,0,0,0,0,0,0,0});
            for (int i = 0; i < 8;i++ )
            {
                int j= ((a.truthTable[i]) <<2) + ((b.truthTable[i])<<1)+c.truthTable[i];
                NF.truthTable[i] = this.truthTable[j];
            }
            NF.numberOfOperation = a.numberOfOperation + b.numberOfOperation + c.numberOfOperation + 1;
            NF.A = a;
            NF.B = b;
            NF.C = c;
            return NF;
        }
     
        public byte operate(byte a, byte b, byte c) //Get result of function
        {
            return this.truthTable[(a << 2) + (b << 1) + c];
        }
        
}
    
}