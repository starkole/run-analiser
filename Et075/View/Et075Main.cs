using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Et075.Controller;
using Et075.Model;

namespace Et075.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            int wndWidth = (int)(System.Windows.SystemParameters.FullPrimaryScreenWidth
                - System.Windows.SystemParameters.FullPrimaryScreenWidth / 3);
            int wndHeight = (int)(System.Windows.SystemParameters.FullPrimaryScreenHeight
                - System.Windows.SystemParameters.FullPrimaryScreenHeight / 6);
            this.ClientSize = new Size(wndWidth, wndHeight);

            foreach (var item in PrintingProvider.SystemPrintersNamesList)
                printersComboBox.Items.Add(item);
            printersComboBox.SelectedItem = PrintingProvider.DefaultPrinterName;

            dataGridView.Columns["id"].ValueType = typeof(int);
            dataGridView.Columns["run"].ValueType = typeof(int);
            dataGridView.Columns["name"].ValueType = typeof(string);
            dataGridView.DataError += ((o, s) => ShowErrorMessage("Введить число або натисниіть Esc, щоб скасувати"));
            dataGridView.CellValueChanged += ((o, s) => UpdateStatusStrip());
            dataGridView.RowsAdded += ((o, s) => UpdateStatusStrip());
            dataGridView.RowsRemoved += ((o, s) => UpdateStatusStrip());
            dataGridView.CellBeginEdit += dataGridView_CellBeginEdit;
        }//end:MainForm

        /// <summary>
        /// Sets default id and name values for a new row
        /// </summary>
        void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int maxId = 0;
            //Get max id value from dataGridView
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                if (r.Cells["id"].Value != null && (int)r.Cells["id"].Value > maxId)
                    maxId = (int)r.Cells["id"].Value;
            }
            //Set the "default" values for a new row
            if (dataGridView.Rows[e.RowIndex].Cells["id"].Value == null
                || (int)dataGridView.Rows[e.RowIndex].Cells["id"].Value < 1)
            {
                dataGridView.Rows[e.RowIndex].Cells["id"].Value = maxId + 1;
                dataGridView.Rows[e.RowIndex].Cells["name"].Value = string.Format("Et{0}", maxId + 1);
            }
        }//end:dataGridView_CellBeginEdit

        /// <summary>
        /// Handles the click on the printButton.
        /// If printPreview not empty, prints it to printer selected.
        /// </summary>
        private void printButton_Click(object sender, EventArgs e)
        {
            if (richTextBox.TextLength < 1)
            {
                ShowErrorMessage("Немає результатів для друку");
                return;
            }
            richTextBox.PrintRichTextContents(printersComboBox.Text);

        }//end:printButton_Click

        /// <summary>
        /// Handles the click on the calculateButton.
        /// Calculates results and shows them in printPreview.
        /// </summary>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            //Get input data to analize
            Zakaz z = new Zakaz();
            if (!ParseDataTable(ref z))
            {
                ShowErrorMessage("Немає даних для обчислення");
                return;
            }

            //Analize data
            Dictionary<String, StatsList> results = new Dictionary<String, StatsList>();
            results.Add("SplitFromMinToMax", Analizer.FirstPass(z));
            //results.Add("PackIntoSheet", Analizer.PackIntoSheet(z));
            //results.Add("SplitByGcd", Analizer.SplitByGcd(z));
            //results.Add("FindBestRunWithShifting", Analizer.FindBestRunWithShifting(z));

            //Setup initial values and text styles 
            Font titleFont = new Font("Calibri", 10, FontStyle.Bold);
            Font textFont = new Font("Calibri", 10, FontStyle.Regular);
            Font addedTextFont = new Font("Calibri", 8, FontStyle.Regular);
            richTextBox.ResetText();
            richTextBox.SelectionIndent = 5;
            richTextBox.SelectionRightIndent = 5;
            StringBuilder resultString;

            //Generate title
            richTextBox.SelectionFont = titleFont;
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox.AppendText("Аналіз замовлення\n");
            resultString = new StringBuilder();
            resultString.Append(DateTime.Now.ToLongDateString());
            resultString.Append(" / ");
            resultString.Append(DateTime.Now.ToLongTimeString());
            resultString.Append("\n");
            resultString.Append(string.Format("В замовленні {0} етикеток.", (from et in z select et.Run).Sum()));
            resultString.Append(" Наклади етикеток: ");
            foreach (Etyketka et in z)
                resultString.Append(string.Format("{0} ", et.Run));
            resultString.Append("\n\n");
            richTextBox.SelectionFont = addedTextFont;
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox.AppendText(resultString.ToString());

            //Generate results
            foreach (var item in results)
            {
                richTextBox.SelectionFont = titleFont;
                richTextBox.SelectionAlignment = HorizontalAlignment.Left;
                richTextBox.AppendText(string.Format("Результати за алгоритмом {0} (спусків {1}, передрук {2} тис. етикеток).\n",
                    item.Key, item.Value.Count, ((float)(item.Value.OverprintsSum)) / 1000));
                int spuskCounter = 1;
                foreach (var r in item.Value)
                {
                    richTextBox.SelectionFont = textFont;
                    richTextBox.AppendText(string.Format("Спуск №{0}. Зайнято {1} з {2} позицій на спуску. Наклад {3} аркушів. Передрук {4} етикеток.\n",
                        spuskCounter++, r.EtsOnSheetCount, Constants.ETS_ON_SHEET, r.Run, r.OverprintsSum));
                    resultString = new StringBuilder();
                    resultString.Append(string.Format("\t№       позицій           передрук      (наклад)      назва\n"));
                    foreach (var et in r.Ets)
                    {
                        resultString.Append(string.Format("\t{1} \t{3} \t{4} \t({2}) \t{0}.\n",
                            et.Name, et.Id, et.Run, et.CountOnSheet, et.Overprint));
                    }
                    richTextBox.SelectionFont = addedTextFont;
                    richTextBox.AppendText(resultString.ToString());
                }
                richTextBox.AppendText("\n");
            }
        }//end:calculateButton_Click

        /// <summary>
        /// Handles the click on the clearButton.
        /// Clears data table and printPreview.
        /// </summary>
        private void clearButton_Click(object sender, EventArgs e)
        {
            richTextBox.ResetText();
            dataGridView.Rows.Clear();
        }//end:clearButton_Click

        /// <summary>
        /// Handles the click on the loadButton.
        /// Tries to load data from external text file.
        /// </summary>
        private void loadButton_Click(object sender, EventArgs e)
        {
            Zakaz z = DataImporter.LoadFile();
            if (z == null)
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "Неможливо завантажити дані";
                return;
            }
            dataGridView.Rows.Clear();
            foreach (var et in z)
                dataGridView.Rows.Add(et.Id, et.Run, et.Name);
            UpdateStatusStrip();
        }//end:loadButton_Click

        /// <summary>
        /// Updates text in the status bar at the bottom of the window.
        /// </summary>
        private void UpdateStatusStrip()
        {
            int sumRun = 0;
            int itemsCount = 0;
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                if (r.Cells["run"].Value != null)
                {
                    sumRun += (int)r.Cells["run"].Value;
                    itemsCount++;
                }
            }

            toolStripStatusLabel1.ForeColor = Color.Black;
            toolStripStatusLabel1.Text =
                string.Format("Додано позицій {0}. Загальний наклад {1}.", itemsCount, sumRun);
        }//end:UpdateStatusStrip

        /// <summary>
        /// Shows given message at status bar in red color.
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            toolStripStatusLabel1.ForeColor = Color.Red;
            toolStripStatusLabel1.Text = message;
        }//end:ShowErrorMessage

        /// <summary>
        /// Reads data from dataGridView to z. 
        /// Returns false if z would be empty after parsing.
        /// </summary>
        private bool ParseDataTable(ref Zakaz z)
        {
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                if (r.Cells["id"].Value != null && r.Cells["run"].Value != null)
                {
                    z.Add(new Etyketka(
                        (int)r.Cells["id"].Value,
                        r.Cells["name"].Value.ToString(),
                        (int)r.Cells["run"].Value));
                }
            }
            return z.Count > 0;
        }//end:ParseDataTable
    }
}
