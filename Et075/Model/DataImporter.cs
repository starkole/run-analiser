using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
//using System.Windows.Input;

namespace Et075.Model
{
    public static class DataImporter
    {
        public static Zakaz LoadFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "txt";
            dlg.Filter = "текстові файли (*.txt)|*.txt|всі файли (*.*)|*.*";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Zakaz result = new Zakaz();
                StreamReader sr = new StreamReader(dlg.OpenFile(), Encoding.GetEncoding(1251));
                int idCounter = 0;
                while (!sr.EndOfStream)
                {
                    idCounter++;
                    string ts = sr.ReadLine();
                    string rawRun;
                    string etName;
                    if (ts.Contains("\t") || ts.Contains(" "))
                    {
                        int delimeter = ts.LastIndexOf("\t");
                        if (delimeter < ts.LastIndexOf(" "))
                            delimeter = ts.LastIndexOf(" ");
                        rawRun = ts.Substring(delimeter);
                        etName = ts.Substring(0, delimeter);
                    }
                    else
                    {
                        rawRun = ts;
                        etName = "Et_" + ts;
                    }
                    int run = 0;
                    try
                    { run = int.Parse(rawRun); }
                    catch (FormatException)
                    {
                        if (MessageBox.Show("Помилка у наданому текстовому файлі. Не можу прочитати наклад з текстового рядка: \n" +
                            ts + "\nПродовжити читання файлу?", "Помилка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                            == DialogResult.Cancel)
                        {
                            sr.Close();
                            return null;
                        }

                    }
                    Etyketka et = new Etyketka(idCounter, etName, run);
                    result.Add(et);
                }
                sr.Close();
                if (result.Count < 1)
                    return null;
                return result;
            }
            return null;
        }
    }
}
