using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SudokuEngine;

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
            txtInput.Text = @"Path for sudoku input file...";
            cmbDifficulty.DataSource = Enum.GetValues(typeof(Common.Difficulty));
        }

        private void txtInput_Enter(object sender, EventArgs e)
        {
            txtInput.Text = string.Empty;
        }

        private void txtInput_Leave(object sender, EventArgs e)
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
        private void btnSolve_Click(object sender, EventArgs e)
        {
            try
            {
                btnSolve.Enabled = false;
                var sudokuSolver = SudokuSolver.GetSolver(ReadInputs(txtInput.Text.Trim()));
                var solvedBoard = sudokuSolver.Solve();

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
                    txtOutput.AppendText($"Recursion depth: {sudokuSolver.RecursionDepth.ToString()} : {sudokuSolver.Difficulty.ToString()}");
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.Message;
            }
            finally
            {
                btnSolve.Enabled = true;
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
                btnGenerate.Enabled = false;
                var generator = SudokuGenerator.GetGenerator(9, (Common.Difficulty)cmbDifficulty.SelectedItem);
                var generatedBoard = generator.Generate();

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
            finally
            {
                btnGenerate.Enabled = true;
            }
        }

        /// <summary>
        /// input file should contain 0 for empty cells
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private int[,] ReadInputs(string filePath)
        {
            var rowCount = 0;
            var lines = File.ReadAllLines(filePath);
            var board = new int[lines.Length, lines.Length];

            foreach (var line in lines)
            {
                var columnCount = 0;
                var numbers = line.ToCharArray();

                foreach (var number in numbers)
                {
                    board[rowCount, columnCount] = int.Parse(number.ToString());
                    columnCount++;
                }
                rowCount++;
            }

            return board;
        }
    }
}
