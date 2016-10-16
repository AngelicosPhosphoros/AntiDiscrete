using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace WindowsFormsApplication1
{
    public class FunctionAnalyzer
    {
        private int all = 0;
        private int finished = 0;
        public const string INCOMPLETE_BASIS_STRING = "Incomplete basis";
        private Function A;
        private Function B;
        private Function C;
        public List<Function[]> Basises;
        public Function[] ElementaryFunctions; // For 15th task
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
                        byte[] TT = new byte[Function.TRUTH_TABLE_LENGTH]; //truth tables
                        string s = sr.ReadLine();
                        for (int m = 0; m < Function.TRUTH_TABLE_LENGTH; m++)
                        {
                            TT[m] = byte.Parse(s[m] + "");
                        }
                        s = s.Remove(0, Function.TRUTH_TABLE_LENGTH); //Remove truth table from beginning of string
                        Basises[i][j] = new Function(s, TT);
                    }
                }
                sr.Close();
                return true;
            }
            catch (IOException exc)
            {
                throw new FileLoadException("File not found");
            }
        }//LoadBasisesFromFile
        public bool LoadElementarFunctionsFromFile(string FileName)
        {
            try
            {
                StreamReader sr = File.OpenText(FileName);
                int num = int.Parse(sr.ReadLine());
                ElementaryFunctions = new Function[num];
                for (int i = 0; i < num; i++)
                {
                    byte[] TT = new byte[Function.TRUTH_TABLE_LENGTH]; //truth tables
                    string s = sr.ReadLine();
                    for (int m = 0; m < Function.TRUTH_TABLE_LENGTH; m++)
                    {
                        TT[m] = byte.Parse(s[m] + "");
                    }
                    s = s.Remove(0, Function.TRUTH_TABLE_LENGTH); //Remove truth table from beginning of string
                    ElementaryFunctions[i] = new Function(s, TT);
                }
                sr.Close();
                return true;
            }
            catch (IOException exc)
            {
                throw new FileLoadException("File not found");
            }
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
            all += Basises.Count;
            Parallel.For(0, Basises.Count, j =>
          {
              res[j] = Analyze(TruthTable, Basises[j]);
          });

            //for (int j = 0; j < Basises.Count; j++)
            //{
            //    res[j] = Analyze(TruthTable, Basises[j]);
            //}

            return res;
        }
        public string[] AnalyzeElementarFunctionsByThisFunction(byte[] TruthTable)
        {
            all += Basises.Count;
            string[] res = new string[ElementaryFunctions.Length];
            Function F = new Function("F({0}, {1}, {2})", TruthTable);
            Parallel.For(0, ElementaryFunctions.Length, j =>
            {
                res[j] = ElementaryFunctions[j].Text + " :  " + Analyze(ElementaryFunctions[j], new Function[] { F });
            });
            return res;
        }

        public string Analyze(byte[] TruthTable, ICollection<Function> Basis)
        {
            return Analyze(new Function("", TruthTable), Basis);
        }

        public string Analyze(Function searched, ICollection<Function> Basis)
        {
            int searchedCode = searched.GetCode();
            Function founded = (new List<Function>(Basis)).Find(x => x.GetCode() == searchedCode);
            if (founded != null)
            {
                return founded.Text;
            }

            IDictionary<int, Function> avaibleFunctions = new LinkedDictionary<int, Function>(false);
            foreach (var f in new Function[] { A, B, C })
            {
                avaibleFunctions.Add(f.GetCode(), f);
            }

            List<Function> newFunctions = new List<Function>();
            IDictionary<int, Function> yetGenerated = new Dictionary<int, Function>();
            bool isChanged;
            #region MainSearchCycle
            do
            {
                isChanged = false;
                yetGenerated.Clear();
                newFunctions.Clear();
                // We jump from 5 cycles
                //RESET_GOTO:
                #region Generation of new combinations
                foreach (var f1 in avaibleFunctions)
                {
                    if (yetGenerated.ContainsKey(f1.Key))
                    {
                        continue;
                    }
                    foreach (var f2 in avaibleFunctions)
                    {
                        if (yetGenerated.ContainsKey(f2.Key))
                        {
                            continue;
                        }
                        foreach (var f3 in avaibleFunctions)
                        {
                            if (yetGenerated.ContainsKey(f3.Key))
                            {
                                continue;
                            }
                            foreach (Function operation in Basis) // Choose function from basis.
                            {
                                Function generated = operation.combine(f1.Value, f2.Value, f3.Value);
                                int key = generated.GetCode();
                                if (!avaibleFunctions.TryGetValue(key, out founded)
                                    || founded.NumberOfOperations > generated.NumberOfOperations)
                                {
                                    if (yetGenerated.ContainsKey(key))
                                    {
                                        if (yetGenerated[key].NumberOfOperations > generated.NumberOfOperations)
                                            yetGenerated[key] = generated;
                                    }
                                    else
                                    {
                                        yetGenerated.Add(key, generated);
                                    }
                                    isChanged = true;
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Saving of new functions
                newFunctions.AddRange(yetGenerated.Values);
                newFunctions.Sort((x, y) =>
                {
                    return x.NumberOfOperations.CompareTo(y.NumberOfOperations);
                });

                // We join new set to old set
                foreach (var item in newFunctions)
                {
                    CheckFunctionArgs(item, avaibleFunctions);
                    if(avaibleFunctions.ContainsKey(item.GetCode()))
                    {
                        Function old = avaibleFunctions[item.GetCode()];
                        CheckFunctionArgs(old, avaibleFunctions);
                        if (old.NumberOfOperations > item.NumberOfOperations)
                            avaibleFunctions[item.GetCode()] = item;
                    }
                    else
                    {
                        avaibleFunctions.Add(item.GetCode(),item);
                    }
                    isChanged = true;
                }
                #endregion

                // Если все новые функции более длинные, чем та, которую мы уже нашли
                if (isChanged && avaibleFunctions.TryGetValue(searchedCode, out founded))
                {
                    bool fl = true;
                    foreach (var f in newFunctions)
                    {
                        if (f.NumberOfOperations <= founded.NumberOfOperations)
                        {
                            fl = false;
                            break;
                        }
                    }
                    if (fl)
                    {
                        break;
                    }
                }

            }
            while (isChanged);
            #endregion MainSearchCycle
            finished++;
            if (avaibleFunctions.TryGetValue(searchedCode, out founded))
            {
                return founded.Text;
            }
            else
            {
                return INCOMPLETE_BASIS_STRING;
            }
        }

        private static void CheckFunctionArgs(Function f, IDictionary<int,Function> functions)
        {
            if (functions[f.A.GetCode()] != f.A)
            {
                f.ReplaceArgument(functions[f.A.GetCode()]);
            }
            if (functions[f.B.GetCode()] != f.B)
            {
                f.ReplaceArgument(functions[f.B.GetCode()]);
            }
            if (functions[f.C.GetCode()] != f.C)
            {
                f.ReplaceArgument(functions[f.C.GetCode()]);
            }
        }

        public Function[] GetDerivatives(byte[] TruthTable)
        {
            Function F = new Function("F", TruthTable);
            Function dA = new Function("F'{0}", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dB = new Function("F'{1}", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dC = new Function("F'{2}", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dAB1 = new Function("F''{0}{1}", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dAC1 = new Function("F''{0}{2}", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dBC1 = new Function("F''{1}{2}", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dABC1 = new Function("F'''{0}{1}{2}", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dAB2 = new Function("F'({0}{1})", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dAC2 = new Function("F'({0}{2})", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dBC2 = new Function("F'({1}{2})", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            Function dABC2 = new Function("F'({0}{1}{2})", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            for (byte a = 0; a <= 1; a++)
                for (byte b = 0; b <= 1; b++)
                    for (byte c = 0; c <= 1; c++)
                    {
                        dA.setBit((a << 2) + (b << 1) + c, (F.operate(a, b, c) ^ F.operate((byte)(a ^ 1), b, c)));
                        dB.setBit((a << 2) + (b << 1) + c, (F.operate(a, b, c) ^ F.operate(a, (byte)(b ^ 1), c)));
                        dC.setBit((a << 2) + (b << 1) + c, (F.operate(a, b, c) ^ F.operate(a, b, (byte)(c ^ 1))));
                        dAB2.setBit((a << 2) + (b << 1) + c, (F.operate(a, b, c) ^ F.operate((byte)(a ^ 1), (byte)(b ^ 1), c)));
                        dAC2.setBit((a << 2) + (b << 1) + c, (F.operate(a, b, c) ^ F.operate((byte)(a ^ 1), b, (byte)(c ^ 1))));
                        dBC2.setBit((a << 2) + (b << 1) + c, (F.operate(a, b, c) ^ F.operate(a, (byte)(b ^ 1), (byte)(c ^ 1))));
                        dABC2.setBit((a << 2) + (b << 1) + c, (F.operate(a, b, c) ^ F.operate((byte)(a ^ 1), (byte)(b ^ 1), (byte)(c ^ 1))));
                    }
            for (byte a = 0; a <= 1; a++)
                for (byte b = 0; b <= 1; b++)
                    for (byte c = 0; c <= 1; c++)
                    {
                        dAB1.setBit((a << 2) + (b << 1) + c, (dA.operate(a, b, c) ^ dA.operate(a, (byte)(b ^ 1), c)));
                        dAC1.setBit((a << 2) + (b << 1) + c, (dA.operate(a, b, c) ^ dA.operate(a, b, (byte)(c ^ 1))));
                        dBC1.setBit((a << 2) + (b << 1) + c, (dB.operate(a, b, c) ^ dB.operate(a, b, (byte)(c ^ 1))));
                    }
            for (byte a = 0; a <= 1; a++)
                for (byte b = 0; b <= 1; b++)
                    for (byte c = 0; c <= 1; c++)
                        dABC1.setBit((a << 2) + (b << 1) + c, (dAC1.operate(a, b, c) ^ dAC1.operate(a, (byte)(b ^ 1), c)));
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
            for (int i = 0; i < Function.TRUTH_TABLE_LENGTH; i++)
            {
                s.Add(FormStr(A.getBit(i), B.getBit(i), C.getBit(i), TruthTable[i]));
                for (int j = 0; j < result.Length; j++)
                    s[i + 1] += "\t" + result[j].getBit(i);
            }
            return result;
        }// GetDerivatives out

        public void TaylorLines(byte[] truthTable, Function[] Derivatives, out string[] XOR, out string[] EQV)
        {
            string[] DerivativesLinesM = { "A", "B", "C", "AB", "AC", "BC", "ABC" };
            string[] DerivativesLinesS = { "A", "B", "C", "(A+B)", "(A+C)", "(B+C)", "(A+B+C)" };
            string[] result = new string[Function.TRUTH_TABLE_LENGTH];
            string[] ends = new string[Function.TRUTH_TABLE_LENGTH];
            for (int i = 0; i < Function.TRUTH_TABLE_LENGTH; i++)
            {
                result[i] = "(" + A.getBit(i) + B.getBit(i) + C.getBit(i) + "):\tF=" + truthTable[i].ToString();
                ends[i] = "(" + A.getBit(i) + B.getBit(i) + C.getBit(i) + "):\tF=" + truthTable[i].ToString();
                for (int j = 0; j < 7; j++)
                    if (Derivatives[j].getBit(i) != 0)
                        result[i] += " xor " + DerivativesLinesM[j];
                if (A.getBit(i) != 0) result[i] = result[i].Replace("A", "(!A)");
                if (B.getBit(i) != 0) result[i] = result[i].Replace("B", "(!B)");
                if (C.getBit(i) != 0) result[i] = result[i].Replace("C", "(!C)");
                ends[i] += " = " + truthTable[i].ToString();
                for (int j = 0; j < 7; j++)
                    if (Derivatives[j].getBit(i) != 0)
                        ends[i] += "<=>" + DerivativesLinesS[j];
                if (A.getBit(i) == 0) ends[i] = ends[i].Replace("A", "(!A)");
                if (B.getBit(i) == 0) ends[i] = ends[i].Replace("B", "(!B)");
                if (C.getBit(i) == 0) ends[i] = ends[i].Replace("C", "(!C)");

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

            int i = -1;
            for (int j = 0; j < Function.TRUTH_TABLE_LENGTH; j++)
            {
                if (Derivatives[10].getBit(j) == 0)
                {
                    i = j;
                    break;
                }
            }

            if (i > -1 && i != Function.TRUTH_TABLE_LENGTH) res[2] = string.Format("F'(ABC)({0},{1},{2})=0 <=> F∉T*", A.getBit(i), B.getBit(i), C.getBit(i)); else res[2] = "F'(ABC)=1 <=> F∈T*";
            bool fl = false;
            for (i = 3; i < 7 && !fl; i++)
                for (int j = 0; j < Function.TRUTH_TABLE_LENGTH; j++)
                    if (Derivatives[i].getBit(j) > 0) { fl = true; break; }
            res[3] = fl ? "F∉TL" : "F∈TL";
            fl = false;
            for (i = 0; i < 7; i++)
                for (int j = i + 1; j < Function.TRUTH_TABLE_LENGTH; j++)
                    if ((A.getBit(i) <= A.getBit(j)) && //If variables of i<=j
                        (B.getBit(i) <= B.getBit(j)) &&
                        (C.getBit(i) <= C.getBit(j)) &&
                        (truthTable[i] > truthTable[j])) //If function is not monotonic
                    {
                        fl = true;
                        res[4] = string.Format("({0},{1},{2})<({3},{4},{5}) & F({0},{1},{2})=1 >F({3},{4},{5})=0  <=> F∉ T<", A.getBit(i), B.getBit(i), C.getBit(i),
                                                                                                                        A.getBit(j), B.getBit(j), C.getBit(j));
                    }
            if (!fl) res[4] = "F∈T<";

            return res;
        }// PostCriteriesLines

    }//FunctionAnalyzer

}
public class MatrixOperator
{
    const int TRUTH_TABLE_LENGTH = 8;

    static public byte[] StringToTruthTable(string s)
    {
        byte[] TT = new byte[TRUTH_TABLE_LENGTH];
        for (int i = 0; i < TRUTH_TABLE_LENGTH; i++)
        {
            if (s[i] == '1' || s[i] == '0')
                TT[i] = byte.Parse(s[i] + "");
            else
                throw new Exception("Invalid truth table string.");
        }
        return TT;
    }

}