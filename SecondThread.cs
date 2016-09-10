using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
   
    public class SecondThread
    {
        public delegate void OnFinishArgs(string[] basisesResult, string[] elemFuncResult);
        public event OnFinishArgs OnFinish;
        public void Main(FunctionAnalyzer Analyzer, byte[] TT)
        {
            string[] BasisMin = new string[0], ElemMin=BasisMin;
            Parallel.Invoke(
                delegate() { BasisMin = Analyzer.AnalyzeInAllBasises(TT); },
                delegate() { ElemMin = Analyzer.AnalyzeElementarFunctionsByThisFunction(TT); });
            OnFinish(BasisMin, ElemMin);
        }
    }
}
