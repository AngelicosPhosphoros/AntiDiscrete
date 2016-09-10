using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        List<byte[,]> Matrix;
        List<List<byte[]>> Operations;
        public MainForm MainForm;
        public Form2()
        {
            InitializeComponent();
        }
        private bool ParseTextBox(TextBox Box, int Pillar, byte[,] matrix)
        {
            try
            {
                int coef= int.Parse(Box.Text);
                for (int i = 0; i < 8; i++)
                {
                    byte m =(byte)((coef>>i) & 1);
                    matrix[7 - i, Pillar] = m;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void SwapLines(int a, int b, byte[,] matrix)
        {
            byte temp;
            for (int i = 0; i < 9; i++)
            {
                temp = matrix[a, i];
                matrix[a, i] = matrix[b, i];
                matrix[b, i] = temp;
            }
        }
        private void SortLines( byte[,] matrix)
        {
            for (int i = 0; i < 7; i++)
                for (int k = i; k < 8; k++)
                    if (matrix[k, i] == 1 && EmptyLineOnLowerPill(matrix, i, k))
                        SwapLines(k, i, matrix);
        }
        private bool CheckMatrix(byte[,] matrix)
        {
            for(int i=0;i<8;i++)
            {
                int sum=0;
                for(int j=0;j<8;j++)
                    sum+=matrix[i,j];
                if (sum!=1) return false;
            }
            return true;
        }
        private byte[,] NewMatrixFromOld(byte[,] Old) // To clone matrix
        {
            byte[,] res = new byte[8, 9];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 9; j++)
                    res[i, j] = Old[i, j];
            return res;
        }
        private bool EmptyLineOnLowerPill(byte[,] M, int Pill,int index)
        {
            for (int j = 0; j < Pill; j++)
                if (M[index, j] == 1) return false;
            return true;
        }
        private void OperateLines(byte[,] M, int A, int B) //(A)=(A) xor (B)
        {
            for (int i = 0; i < 9; i++)
                M[A, i] = (byte)(M[B, i] ^ M[A,i]);
        }
        private bool SoluteMatrix()
        {
            byte[,] M = new byte[8,9];
            Operations= new List<List<byte[]>>();
            int Pill = 0;
            while (!CheckMatrix(Matrix[Matrix.Count - 1]) && Pill<8)
            {
                List<byte[]> Oper = new List<byte[]>();
                M = NewMatrixFromOld(Matrix[Matrix.Count - 1]);
                byte i = 0;
                while (i<8 && (M[i, Pill] != 1 || !EmptyLineOnLowerPill(M, Pill, i))) i++;
                if (i == 8) return false;
                for (byte j =0; j < 8; j++)
                    if ((i!=j) && (M[j, Pill] == 1))
                    {
                        OperateLines(M, j, i);
                        Oper.Add(new byte[] { (byte)(j+1), (byte)(i+1) });
                    }
                Matrix.Add(M);
                Operations.Add(Oper);
                Pill++;
                
            }

            M = NewMatrixFromOld(Matrix[Matrix.Count - 1]);
            SortLines(M);
            Matrix.Add(M);
            return true;
        }
        private List<string> MatrixToStringList(byte[,] M)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                string s = "|";
                for (int j = 0; j < 8; j++)
                    s += M[i, j];
                s += "|" + M[i, 8] + "|";
                res.Add(s);
            }
            return res;
        }
        private List<string> OperationsToStringList(List<byte[]> Oper)
        {
            List<string> res = new List<string>();
            res.Add("");
            for (int i = 0; i < Oper.Count; i++)
            {
                string s = string.Format("({0})+=({1})",Oper[i][0],Oper[i][1]);
                res.Add(s);
            }
            res.Add("");
            res.Add("");
            return res;
        }
        private string[] MatrixesToLineArray()
        {
            List<string> res= new List<string>();
            for (int i = 0; i < Operations.Count; i++)
            {
                res.AddRange(MatrixToStringList(Matrix[i]));
                res.AddRange(OperationsToStringList(Operations[i]));
            }
            res.AddRange(MatrixToStringList(Matrix[Matrix.Count - 2]));
            res.AddRange(new string[] { "", "", "" });
            res.AddRange(MatrixToStringList(Matrix[Matrix.Count - 1]));
            return res.ToArray();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Matrix = new List<byte[,]>();
            Matrix.Add(new byte[8, 9]);
            if (!(ParseTextBox(textBox1, 0, Matrix[0]) &&
                ParseTextBox(textBox2, 1, Matrix[0]) &&
                ParseTextBox(textBox3, 2, Matrix[0]) &&
                ParseTextBox(textBox4, 3, Matrix[0]) &&
                ParseTextBox(textBox5, 4, Matrix[0]) &&
                ParseTextBox(textBox6, 5, Matrix[0]) &&
                ParseTextBox(textBox7, 6, Matrix[0]) &&
                ParseTextBox(textBox8, 7, Matrix[0]) &&
                ParseTextBox(textBox9, 8, Matrix[0])))
                {
                    MessageBox.Show(this,"Перепроверь значения полей ввода!");
                    return;
                }
            if (SoluteMatrix())
            {
                textBox10.Lines = MatrixesToLineArray();
                if (MainForm != null)
                {
                    string s = "";
                    for (int i = 0; i < 8; i++)
                        s += Matrix[Matrix.Count - 1][i, 8];
                    MainForm.ChangeTruthTableBox(s);
                }
            }
            else MessageBox.Show("Извините, но матрица нерешаема. Проверьте ввод");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
                try
                {
                    StreamWriter sw = File.CreateText(saveFileDialog1.FileName);
                    for (int i = 0; i < textBox10.Lines.Length; i++)
                        sw.WriteLine(textBox10.Lines[i]);
                    sw.Flush();
                    sw.Close();
                }
                catch
                {
                    MessageBox.Show(this, "Сохранить не удалось. Попробуйте сохранить в другое место.", "Файл недоступен.");
                }
        }

       
    }
}
