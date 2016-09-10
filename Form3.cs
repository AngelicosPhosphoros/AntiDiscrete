using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class AddBasisForm : Form
    {
        public AddBasisForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> S = new List<string>();
            S.AddRange(BasisBox.Lines);
            S.Add(TableBox.Text + MaskBox.Text);
            BasisBox.Lines = S.ToArray();
        }
        private void SaveFunction(StreamWriter sw, Function F)
        {
            for (int i = 0; i < 8; i++)
                sw.Write(F.truthTable[i]);
            sw.WriteLine(F.mask);
        }
        private void btnAddBasis_Click(object sender, EventArgs e)
        {
            FunctionAnalyzer Loader = new FunctionAnalyzer();
            Function[] New = new Function[BasisBox.Lines.Length];
            for (int j =0;j<New.Length;j++)
            try
            {
                New[j]= new Function(BasisBox.Lines[j].Substring(8),MatrixOperator.StringToTruthTable(BasisBox.Lines[j]));
            }
            catch
            {
                MessageBox.Show(this, "Проверьте поле ввода таблицы истинности функции!");
                return;
            }
            try
            {
                Loader.LoadBasisesFromFile("Basises.res");
                Loader.Basises.Add(New);
                StreamWriter F = File.CreateText("Basises.res");
                F.WriteLine(Loader.Basises.Count);
                for (int i = 0; i < Loader.Basises.Count; i++)
                {
                    F.WriteLine(Loader.Basises[i].Length);
                    for (int j = 0; j < Loader.Basises[i].Length; j++)
                        SaveFunction(F, Loader.Basises[i][j]);
                }
                F.Flush();
                F.Close();
            }
            catch
            {
                StreamWriter F = File.CreateText("Basises.res");
                F.WriteLine(1);
                F.WriteLine(New.Length);
                for (int i = 0; i < New.Length; i++)
                    SaveFunction(F, New[i]);
                F.Flush();
                F.Close();
            }
        }
    }
}
