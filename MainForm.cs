using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication1
{   /// <summary>
    /// The First Form
    /// </summary>
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
            onComputationFinished += MainForm_onComputationFinished;
        }
        private int computedThreadsCount = 0;
        private void MainForm_onComputationFinished(object sender, ComputationEventArgs e)
        {
            e.textBox.Lines = e.rows;
            computedThreadsCount++;
            if (computedThreadsCount >= 2)
            {
                FinishOfWorkerThreads();
            }
        }

        public void ChangeTruthTableBox(string s)
        {
            truthTableBox.Text = s;
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tabControl1.Size = new Size(this.Width - tabControl1.Left - 25, this.Height - tabControl1.Top - 50);
        }

        private void DerivativePage_Resize(object sender, EventArgs e)
        {
            DerivativesBox.Width = DerivativePage.Width - DerivativesBox.Left - 15;
            DerivativesBox.Height = DerivativePage.Height - DerivativesBox.Top - 15;
        }

        

        private void MinimFormsPage_Resize(object sender, EventArgs e)
        {
            MinimFormsBox.Width = MinimFormsPage.Width - MinimFormsBox.Left - 15;
            MinimFormsBox.Height = MinimFormsPage.Height - MinimFormsBox.Top - 15;
        }

        private void PostPage_Resize(object sender, EventArgs e)
        {
            PostBox.Width = PostPage.Width - PostBox.Left - 15;
            PostBox.Height = PostPage.Height - PostBox.Top - 15;
        }

        private void ElemFunc_Resize(object sender, EventArgs e)
        {
            ElemFuncBox.Width = ElemFuncPage.Width - ElemFuncBox.Left - 15;
            ElemFuncBox.Height = ElemFuncPage.Height - ElemFuncBox.Top - 15;
        }

        private class ComputationEventArgs : EventArgs
        {
            public TextBox textBox;
            public string[] rows;
        }
        private event EventHandler<ComputationEventArgs> onComputationFinished;
        
        private void SoluteAll_Click(object sender, EventArgs e)
        {
            SoluteAll.Enabled = false;
            FunctionAnalyzer Analyzer = new FunctionAnalyzer();
            try
            {
                Analyzer.LoadBasisesFromFile("Basises.res");
            }
            catch (FileLoadException exc)
            {
                MessageBox.Show(this, "Нет необходимых файлов программы.");
                SoluteAll.Enabled = true;
                return;
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, "Поймано исключение " + exc + " Обратитесь к автору");
                SoluteAll.Enabled = true;
                return;
            }
            try
            {
                Analyzer.LoadElementarFunctionsFromFile("Functions.res");
            }
            catch (FileLoadException exc)
            {
                MessageBox.Show(this, "Нет необходимых файлов программы.");
                SoluteAll.Enabled = true;
                return;
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, "Поймано исключение " + exc + " Обратитесь к автору");
                SoluteAll.Enabled = true;
                return;
            }
            byte[] TT = new byte[8];
            try
            {
                TT = MatrixOperator.StringToTruthTable(truthTableBox.Text);
            }
            catch
            {
                MessageBox.Show(this, "Проверьте поле ввода таблицы истинности функции!");
                SoluteAll.Enabled = true;
                return;
            }
            labelStatus.Text = "Идет анализ. Пожалуйста, подождите.";
            labelStatus.ForeColor = Color.Black;
            List<string> DerivativeTable;
            Function[] Derivatives = Analyzer.GetDerivatives(TT, out DerivativeTable);
            DerivativesBox.Lines = DerivativeTable.ToArray();
            DerivativeTable = null;
            {
                string[] XORT, EQVT;
                Analyzer.TaylorLines(TT, Derivatives, out XORT,out EQVT);
                TaylorBoxXor.Lines = XORT;
                TaylorBoxEqv.Lines = EQVT;
            }
            PostBox.Lines = Analyzer.PostCriteriesLines(TT, Derivatives);
            MinimFormsBox.Text = "Идет разложение";
            ElemFuncBox.Text = "Идет разложение";
            CheckForIllegalCrossThreadCalls = false;
            computedThreadsCount = 0;
            Thread elemFunc = new Thread(delegate ()
            {
                ComputationEventArgs args = new ComputationEventArgs();
                args.textBox = ElemFuncBox;
                args.rows = Analyzer.AnalyzeElementarFunctionsByThisFunction(TT);
                onComputationFinished(this, args);
            });
            Thread basisesAnalyze = new Thread(delegate ()
            {
                ComputationEventArgs args = new ComputationEventArgs();
                args.textBox = MinimFormsBox;
                args.rows = Analyzer.AnalyzeInAllBasises(TT);
                onComputationFinished(this, args);
            });
            elemFunc.Start();
            basisesAnalyze.Start();
        }
        private void FinishOfWorkerThreads()
        {
            SoluteAll.Enabled = true;
            labelStatus.Text = "Анализ закончен.";
            labelStatus.ForeColor = Color.Green;
            CheckForIllegalCrossThreadCalls = true;
        }

        private void btnSoluteMarix_Click(object sender, EventArgs e)
        {
            Form2 secondform = new Form2();
            secondform.MainForm = this;
            secondform.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox About = new AboutBox();
            About.ShowDialog(this);
        }

        private void truthTableBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SoluteAll_Click(sender, e);
        }

        private void добавитьБазисВБиблиотекуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBasisForm F = new AddBasisForm();
            F.ShowDialog(this);
        }

        private void сохранитьРезультатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = File.CreateText(saveFileDialog1.FileName);
                    sw.WriteLine("Результат работы программы Анализатор Логических функций"); 
                    sw.WriteLine("©Angelicos Phosphoros"); 
                    sw.WriteLine("Все права защищены");
                    sw.WriteLine();
                    byte[] TruthTable = MatrixOperator.StringToTruthTable(truthTableBox.Text);
                    int Num=0;
                    for (int i=0; i<8;i++)
                        Num+=TruthTable[i]<<i;
                    sw.WriteLine("Анализ логической функции от трех переменных под номером " + Num);
                    sw.WriteLine("с таблицей истинности " + truthTableBox.Text);
                    sw.WriteLine("Разложения по базисам:");
                    for (int i = 0; i < MinimFormsBox.Lines.Length; i++)
                        sw.WriteLine(MinimFormsBox.Lines[i]);
                    sw.WriteLine("Производные:");
                    for (int i = 0; i < DerivativesBox.Lines.Length; i++)
                        sw.WriteLine(DerivativesBox.Lines[i]);
                    sw.WriteLine("Разложения в ряды Тейлора в базисе {XOR, &, 1}:\n");
                    for (int i = 0; i < TaylorBoxXor.Lines.Length; i++)
                        sw.WriteLine(TaylorBoxXor.Lines[i]);
                    sw.WriteLine("Разложения в ряды Тейлора в базисе {EQV, +, 0}:\n");
                    for (int i = 0; i < TaylorBoxEqv.Lines.Length; i++)
                        sw.WriteLine(TaylorBoxEqv.Lines[i]);
                    sw.WriteLine("Соответствие функции пяти замкнутым критериям Поста:\n");
                    for (int i = 0; i < PostBox.Lines.Length; i++)
                        sw.WriteLine(PostBox.Lines[i]);
                    sw.WriteLine("Разложения бинарных функций по функции:\n");
                    for (int i = 0; i < ElemFuncBox.Lines.Length; i++)
                        sw.WriteLine(ElemFuncBox.Lines[i]);
                    sw.Flush();
                    sw.Close();
                }
                catch
                {
                    MessageBox.Show(this, "Невозможно сохранить. Закройте все приложения, использующие данный файл, либо выберите другое место для сохранения.");
                    return;
                }

            }
        }


       



    }
}
