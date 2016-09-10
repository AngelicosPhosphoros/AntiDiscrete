using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace WindowsFormsApplication1
{
    public class FunctionAnalyzer
    {
        Function A;
        Function B;
        Function C;
        public List<Function[]> Basises;
        public Function[] ElementarFunctions; // For 15th task
        public bool LoadBasisesFromFile(string FileName)
        {
            try
            {
                StreamReader sr = File.OpenText(FileName);
                int num = int.Parse(sr.ReadLine());
                Basises = new List<Function[]>();
                Basises.Capacity = num;
                for (int i = 0; i < num; i++)
                {
                    int k = int.Parse(sr.ReadLine());
                    Basises.Add(new Function[k]);
                    for (int j = 0; j < k; j++)
                    {
                        byte[] TT = new byte[8]; //truth tables
                        string s = sr.ReadLine();
                        for (int m = 0; m < 8; m++)
                        {
                            TT[m] = byte.Parse(s[m] + "");
                        }
                        s = s.Remove(0, 8); //Remove truth table from beginning of string
                        Basises[i][j] = new Function(s, TT);
                    }
                }
                sr.Close();
                return true;
            }
            catch { return false; }
        }//LoadBasisesFromFile
        public bool LoadElementarFunctionsFromFile(string FileName)
        {
            try
            {
                StreamReader sr = File.OpenText(FileName);
                int num = int.Parse(sr.ReadLine());
                ElementarFunctions = new Function[num];
                for (int i = 0; i < num; i++)
                {
                        byte[] TT = new byte[8]; //truth tables
                        string s = sr.ReadLine();
                        for (int m = 0; m < 8; m++)
                        {
                            TT[m] = byte.Parse(s[m] + "");
                        }
                        s = s.Remove(0, 8); //Remove truth table from beginning of string
                        ElementarFunctions[i] = new Function(s, TT);
                }
                sr.Close();
                return true;
            }
            catch { return false; }
        }//LoadElementarFunctionsFromFile
        public FunctionAnalyzer()
        {
            A = new Function("A", new byte[] { 0, 0, 0, 0, 1, 1, 1, 1 });
            B = new Function("B", new byte[] { 0, 0, 1, 1, 0, 0, 1, 1 });
            C = new Function("C", new byte[] { 0, 1, 0, 1, 0, 1, 0, 1 });
            //LoadBasisesFromFile("Basises.res");
            
        }// FunctionAnalyzer
        public string[] AnalyzeInAllBasises(byte[] TruthTable)
        {
            string[] res = new string[Basises.Count];
            
            Parallel.For (0, Basises.Count , j  =>
            {
                res[j] = Analyze(TruthTable, Basises[j]);
            });
      

            return res;
        }
        public string[] AnalyzeElementarFunctionsByThisFunction(byte[] TruthTable)
        {
            string[] res = new string[ElementarFunctions.Length];
            Function F = new Function("F({0}, {1}, {2})",TruthTable);
            Parallel.For(0, ElementarFunctions.Length, j =>
            {
                res[j] =ElementarFunctions[j].Text+ " :  "+ Analyze(ElementarFunctions[j].truthTable, F);
            });
            return res;
        }
        public string Analyze(byte[] TruthTable,params Function[] Basis)
        {
            
            List<Function> F = new List<Function>();
            F.Add(A);
            F.Add(B);
            F.Add(C);
            int LastInt;
            Function func;
            List<Function> NewFunc;
            if (!(this.Search(TruthTable, F, out func, out LastInt) || this.Search(TruthTable, Basis, out func, out LastInt)))
                do
                {
                    NewFunc = new List<Function>();

                    for (int i = 0; i < F.Count; i++) //First argument
                        for (int j = 0; j < F.Count; j++) //Second argument
                            for (int k = 0; k < F.Count; k++) //Third argument
                                for (int op = 0; op < Basis.Length; op++) // Choose function from basis.
                                {
                                    Function nf = Basis[op].Operate(F[i], F[j], F[k]);
                                    if (!(this.Search(nf.truthTable, F, out func, out LastInt) || (this.Search(nf.truthTable, NewFunc, out func, out LastInt))))
                                        NewFunc.Add(nf);
                                    else
                                        if (func.numberOfOperation > nf.numberOfOperation)
                                        {
                                            if (LastInt < F.Count && F[LastInt].Equals(func))
                                            {
                                                F[LastInt] = nf;
                                                op = -1;
                                                i = 0;
                                                j = 0;
                                                k = 0;
                                            }
                                            else
                                            {
                                                NewFunc[LastInt] = nf;
                                            }
                                        }
                                }
                    if (NewFunc.Count == 0) return "Incomplete basis";
                    F.AddRange(NewFunc);
                }
                while (!this.Search(TruthTable, NewFunc, out func, out LastInt));
            
            return func.Text;
        }//Analyze
        private bool Search(byte[] TruthTable, List<Function> F, out Function func, out int index)
        {
            
            for (int i = 0; i < F.Count; i++)

                if (MatrixOperator.Compare(F[i].truthTable, TruthTable))
                {
                    func = F[i];
                    index = i;
                    return true;
                }
            index = -1;
            func = null;
            return false;
        }
        private bool Search(byte[] TruthTable, Function[] F, out Function func, out int index)
        {

            for (int i = 0; i < F.Length; i++)

                if (MatrixOperator.Compare(F[i].truthTable, TruthTable))
                {
                    func = F[i];
                    index = i;
                    return true;
                }
            index = -1;
            func = null;
            return false;
        }

        public Function[] GetDerivatives(byte[] TruthTable)
        {
            Function F = new Function("F", TruthTable);
            Function dA = new Function("F'{0}",new byte[] {0,0,0,0,0,0,0,0});
            Function dB = new Function("F'{1}",new byte[] {0,0,0,0,0,0,0,0});
            Function dC = new Function("F'{2}",new byte[] {0,0,0,0,0,0,0,0});
            Function dAB1 = new Function("F''{0}{1}",new byte[] {0,0,0,0,0,0,0,0});
            Function dAC1 = new Function("F''{0}{2}",new byte[] {0,0,0,0,0,0,0,0});
            Function dBC1 = new Function("F''{1}{2}",new byte[] {0,0,0,0,0,0,0,0});
            Function dABC1 = new Function("F'''{0}{1}{2}",new byte[] {0,0,0,0,0,0,0,0});
            Function dAB2 = new Function("F'({0}{1})",new byte[] {0,0,0,0,0,0,0,0});
            Function dAC2 = new Function("F'({0}{2})",new byte[] {0,0,0,0,0,0,0,0});
            Function dBC2 = new Function("F'({1}{2})",new byte[] {0,0,0,0,0,0,0,0});
            Function dABC2 = new Function("F'({0}{1}{2})",new byte[] {0,0,0,0,0,0,0,0});
            for (byte a=0;a<=1;a++)
            for(byte b=0;b<=1;b++)
                for (byte c = 0; c <= 1; c++)
                {
                    dA.truthTable[(a<<2)+(b<<1)+c] = (byte)(F.operate(a, b, c) ^ F.operate((byte)(a ^ 1), b, c));
                    dB.truthTable[(a << 2) + (b << 1) + c] = (byte)(F.operate(a, b, c) ^ F.operate(a, (byte)(b ^ 1), c));
                    dC.truthTable[(a<<2)+(b<<1)+c] = (byte)(F.operate(a,b,c)^F.operate(a,b,(byte)(c^1)));
                    dAB2.truthTable[(a<<2)+(b<<1)+c] = (byte)(F.operate(a,b,c)^F.operate((byte)(a^1),(byte)(b^1),c));
                    dAC2.truthTable[(a << 2) + (b << 1) + c] = (byte)(F.operate(a, b, c) ^ F.operate((byte)(a^1), b, (byte)(c^1)));
                    dBC2.truthTable[(a << 2) + (b << 1) + c] = (byte)(F.operate(a, b, c) ^ F.operate(a, (byte)(b^1), (byte)(c^1)));
                    dABC2.truthTable[(a << 2) + (b << 1) + c] = (byte)(F.operate(a, b, c) ^ F.operate((byte)(a^1), (byte)(b ^ 1), (byte)(c ^ 1)));
                }
            for (byte a=0;a<=1;a++)
            for(byte b=0;b<=1;b++)
                for (byte c = 0; c <= 1; c++)
                {
                    dAB1.truthTable[(a << 2) + (b << 1) + c] = (byte)(dA.operate(a, b, c) ^ dA.operate(a, (byte)(b ^ 1), c));
                    dAC1.truthTable[(a << 2) + (b << 1) + c] = (byte)(dA.operate(a, b, c) ^ dA.operate(a, b, (byte)(c ^ 1)));
                    dBC1.truthTable[(a << 2) + (b << 1) + c] = (byte)(dB.operate(a, b, c) ^ dB.operate(a, b, (byte)(c ^ 1)));
                }
            for (byte a=0;a<=1;a++)
            for(byte b=0;b<=1;b++)
                for (byte c = 0; c <= 1; c++)
                    dABC1.truthTable[(a << 2) + (b << 1) + c] = (byte)(dAC1.operate(a, b, c) ^ dAC1.operate(a, (byte)(b ^ 1), c));
            return new Function[] { dA, dB, dC, dAB1, dAC1, dBC1, dABC1, dAB2, dAC2, dBC2, dABC2 }; 
        }// GetDerivatives
        static private string FormStr(dynamic a0, dynamic a1, dynamic a2, dynamic a3)
        {
            return string.Format("({0},{1},{2}):{3}|", a0, a1, a2, a3);
        }
        public Function[] GetDerivatives(byte[] TruthTable, out List<string> s)
        {
            Function[] result = GetDerivatives(TruthTable);
            s = new List<string>();
            s.Add(FormStr("A", "B", "C", "F"));
            for (int i = 0; i < result.Length; i++)
                s[0] += "\t" + result[i].Text;
            for (int i = 0; i < 8;i++ )
            {
                s.Add(FormStr(A.truthTable[i], B.truthTable[i], C.truthTable[i], TruthTable[i]));
                for (int j = 0; j < result.Length; j++)
                    s[i+1] += "\t" + result[j].truthTable[i];    
            }
            return result;
        }// GetDerivatives out

        public void TaylorLines(byte[] truthTable, Function[] Derivatives, out string[] XOR, out string[] EQV)
        {
            string[] DerivativesLinesM= { "A", "B", "C", "AB", "AC", "BC", "ABC" };
            string[] DerivativesLinesS = { "A", "B", "C", "(A+B)", "(A+C)", "(B+C)", "(A+B+C)" };
            string[] result = new string[8];
            string[] ends = new string[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = "(" + A.truthTable[i] + B.truthTable[i] + C.truthTable[i] + "):\tF="+truthTable[i].ToString();
                ends[i] = "(" + A.truthTable[i] + B.truthTable[i] + C.truthTable[i] + "):\tF=" + truthTable[i].ToString();
                for (int j = 0; j < 7; j++)
                    if (Derivatives[j].truthTable[i] != 0)
                        result[i] +=" xor " +DerivativesLinesM[j];
                if (A.truthTable[i] != 0) result[i] = result[i].Replace("A", "(!A)");
                if (B.truthTable[i] != 0) result[i] = result[i].Replace("B", "(!B)");
                if (C.truthTable[i] != 0) result[i] = result[i].Replace("C", "(!C)");
                ends[i] += " = "+truthTable[i].ToString();
                for (int j = 0; j < 7; j++)
                    if (Derivatives[j].truthTable[i] != 0)
                        ends[i] += "<=>"+DerivativesLinesS[j];
                if (A.truthTable[i] == 0) ends[i] = ends[i].Replace("A", "(!A)");
                if (B.truthTable[i] == 0) ends[i] = ends[i].Replace("B", "(!B)");
                if (C.truthTable[i] == 0) ends[i] = ends[i].Replace("C", "(!C)");
                
                //result[i] = string.Format("{0,-50}\t{1,-50}", result[i], ends[i]);
            }
          
            XOR = result;
            EQV = ends;
        } //TaylorLines
        public string[] PostCriteriesLines(byte[] truthTable, Function[] Derivatives)
        {
            Function F = new Function("", truthTable);
            string[] res = new string[5];
            if (F.operate(0, 0, 0) == 0) res[0] = "F(0,0,0)=0 <=> F∈T0"; else res[0] = "F(0,0,0)=1 <=> F∉T0";
            if (F.operate(1, 1, 1) == 1) res[1] = "F(1,1,1)=1 <=> F∈T1"; else res[1] = "F(1,1,1)=0 <=> F∉T1";
            
            int i=-1;
            for (int j = 0; j < 8; j++)
            {
                if (Derivatives[10].truthTable[j] == 0)
                {
                    i = j;
                    break;
                }
            }
            
            if (i > -1 && i !=8) res[2] = string.Format("F'(ABC)({0},{1},{2})=0 <=> F∉T*", A.truthTable[i], B.truthTable[i], C.truthTable[i]); else res[2] = "F'(ABC)=1 <=> F∈T*";
            bool fl = false;
            for (i = 3; i < 7 && !fl; i++)
                for (int j = 0; j < 8;j++)
                    if (Derivatives[i].truthTable[j] > 0) { fl = true; break; }
            res[3] = fl ? "F∉TL" : "F∈TL";
            fl=false;
            for (i = 0; i < 7; i++)
                for (int j = i + 1; j < 8; j++)
                    if ((A.truthTable[i] <= A.truthTable[j]) && //If variables of i<=j
                        (B.truthTable[i] <= B.truthTable[j]) &&
                        (C.truthTable[i] <= C.truthTable[j]) &&
                        (truthTable[i] > truthTable[j])) //If function is not monotonic
                    {
                        fl = true;
                        res[4] = string.Format("({0},{1},{2})<({3},{4},{5}) & F({0},{1},{2})=1 >F({3},{4},{5})=0  <=> F∉ T<", A.truthTable[i], B.truthTable[i], C.truthTable[i],
                                                                                                                        A.truthTable[j], B.truthTable[j], C.truthTable[j]);
                    }
            if (!fl) res[4] = "F∈T<";
            
            return res;
        }// PostCriteriesLines

    }//FunctionAnalyzer

}
public class MatrixOperator
{
    static public bool Compare(byte[] X, byte[] Y)
    {
        return (X[0] == Y[0]) && (X[1] == Y[1]) && (X[2] == Y[2]) && (X[3] == Y[3]) && (X[4] == Y[4]) &&
                (X[5] == Y[5]) && (X[6] == Y[6]) && (X[7] == Y[7]);
    }
    static public byte[] StringToTruthTable(string s)
    {
        byte[] TT = new byte[8];
        for (int i = 0; i < 8; i++)
        {
            if (s[i] == '1' || s[i] == '0')
                TT[i] = byte.Parse(s[i] + "");
            else
                throw new Exception("Invalid truth table string.");
        }
        return TT;
    }
    static public int SearchMaxLengthOfString(string[] Arr)
    {
        if (Arr.Length==0) throw new Exception("Empty array.");
        int res = Arr[0].Length;
        for (int i = 1; i < Arr.Length; i++)
            if (Arr[i].Length > res) res = Arr[i].Length;
        return res;
    }
}