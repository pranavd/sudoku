using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitialSteps();
        }

        public void InitialSteps()
        {
            txtInput.Text = "Path for sudoku input file...";
            cmbDifficulty.DataSource = Enum.GetValues(typeof(SudokuGenerator.Difficulty));
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            txtInput.Text = string.Empty;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text))
            {
                txtInput.Text = "Path for sudoku input file...";
            }
        }

        /// <summary>
        /// solves sudoku board 9X9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var sudokuSolver = new SudokuSolver();
                var solvedBoard = sudokuSolver.SolveSudoku(txtInput.Text.Trim());

                //displaying solved sudoku
                if (solvedBoard != null)
                {
                    txtOutput.Text = string.Empty;
                    for (var row = 0; row < solvedBoard.GetLength(0); row++)
                    {
                        for (var column = 0; column < solvedBoard.GetLength(1); column++)
                        {
                            txtOutput.AppendText(solvedBoard[row, column].ToString());
                        }
                        txtOutput.AppendText("\r\n");
                    }
                    txtOutput.AppendText("\r\n\r\n");

                    //displaying recursion depth, using recursion depth for judging difficulty
                    //samurai level
                    if (sudokuSolver.RecursionDepth > 1000000)
                    {
                        txtOutput.AppendText($"Recursion depth: {sudokuSolver.RecursionDepth.ToString()} : Samurai");
                    }
                    else
                    {
                        //hard level
                        if (sudokuSolver.RecursionDepth > 500000)
                        {
                            txtOutput.AppendText($"Recursion depth: {sudokuSolver.RecursionDepth.ToString()} : Hard");
                        }
                        else
                        {
                            //medium level
                            if (sudokuSolver.RecursionDepth > 250000)
                            {
                                txtOutput.AppendText($"Recursion depth: {sudokuSolver.RecursionDepth.ToString()} : Medium");
                            }
                            //easy level
                            txtOutput.AppendText($"Recursion depth: {sudokuSolver.RecursionDepth.ToString()} : Easy");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.Message;
            }
        }

        /// <summary>
        /// generates sudoku board of 9X9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                var generator = new SudokuGenerator();
                var generatedBoard = generator.Generate((SudokuGenerator.Difficulty)cmbDifficulty.SelectedItem);

                txtOutput.Text = string.Empty;
                for (var row = 0; row < generatedBoard.GetLength(0); row++)
                {
                    for (var column = 0; column < generatedBoard.GetLength(1); column++)
                    {
                        txtOutput.AppendText(generatedBoard[row, column].ToString());
                    }
                    txtOutput.AppendText("\r\n");
                }
                txtOutput.AppendText("\r\n\r\n");
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.Message;
            }
        }
    }
}
