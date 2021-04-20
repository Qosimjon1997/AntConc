using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Documents;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using UzbekConc;
using System.Text;
using ClosedXML.Excel;
using System.Diagnostics;

namespace AntConc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string myOpenedFile = "";
        string _XXX = "";
        private void btnMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if(openFileDialog.ShowDialog() == true)
            {
                myOpenedFile = openFileDialog.FileName;
                var xxx = openFileDialog.FileName.Split("\\");
                string _xxx = xxx[xxx.Length - 1];
                txtNameOfFile.Text = _xxx;
                _XXX = _xxx;
                
            }

        }

        private void btnTab1Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtTab3Search.Text = txtTab4Search.Text = txtTab1Search.Text;
                var myFile = (File.ReadAllText(myOpenedFile)).Split();
                labNumberHits.Text = myFile.Where(u => u.Contains(txtTab1Search.Text)).Count().ToString();

                int i = 0;
                List<int> indexOfFind = new List<int>();
                foreach (var q in myFile)
                {
                    if (q.Contains(txtTab1Search.Text))
                    {
                        indexOfFind.Add(i);
                    }

                    i++;
                }
                txtRichText1.Document.Blocks.Clear();
                foreach (var t in indexOfFind)
                {
                    string S = "";
                    if (t >= 7)
                    {
                        for (int u = 7; u >= 0; u--)
                        {
                            S += " " + myFile[t - u];
                        }
                    }
                    else
                    {
                        for (int u = t % 7; u >= 0; u--)
                        {
                            S += " " + myFile[t - u];
                        }
                    }

                    if (t + 7 <= myFile.Count())
                    {
                        for (int u = 1; u <= 7; u++)
                        {
                            S += " " + myFile[t + u];
                        }
                    }
                    else
                    {
                        for (int u = 1; u <= u - myFile.Count(); u++)
                        {
                            S += " " + myFile[t + u];
                        }
                    }
                    txtRichText1.Document.Blocks.Add(new Paragraph(new Run(S)));
                }

                txtRichText1.SelectAll();
                txtRichText1.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                txtRichText1.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);


                Regex reg = new Regex(txtTab1Search.Text.Trim(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                TextPointer position = txtRichText1.Document.ContentStart;
                List<TextRange> ranges = new List<TextRange>();
                while (position != null)
                {
                    if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                    {
                        string text = position.GetTextInRun(LogicalDirection.Forward);
                        var matchs = reg.Matches(text);

                        foreach (Match match in matchs)
                        {

                            TextPointer start = position.GetPositionAtOffset(match.Index);
                            TextPointer end = start.GetPositionAtOffset(txtTab1Search.Text.Trim().Length);

                            TextRange textrange = new TextRange(start, end);
                            ranges.Add(textrange);
                        }
                    }
                    position = position.GetNextContextPosition(LogicalDirection.Forward);
                }


                foreach (TextRange range in ranges)
                {
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                    range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("File not selected!");
            }

        }

        private void btnTab3Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                var myFile = File.ReadAllText(myOpenedFile);
                txtTab1Search.Text = txtTab4Search.Text = txtTab3Search.Text;
                txtRichText.Document.Blocks.Clear();
                txtRichText.Document.Blocks.Add(new Paragraph(new Run(myFile)));
                var myFile2 = (File.ReadAllText(myOpenedFile)).Split();
                labNumberHits3.Text = myFile2.Where(u => u.Contains(txtTab3Search.Text)).Count().ToString();
                labFileName3.Text = _XXX;


                txtRichText.SelectAll();
                txtRichText.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                txtRichText.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);


                Regex reg = new Regex(txtTab3Search.Text.Trim(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                TextPointer position = txtRichText.Document.ContentStart;
                List<TextRange> ranges = new List<TextRange>();
                while (position != null)
                {
                    if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                    {
                        string text = position.GetTextInRun(LogicalDirection.Forward);
                        var matchs = reg.Matches(text);

                        foreach (Match match in matchs)
                        {

                            TextPointer start = position.GetPositionAtOffset(match.Index);
                            TextPointer end = start.GetPositionAtOffset(txtTab3Search.Text.Trim().Length);

                            TextRange textrange = new TextRange(start, end);
                            ranges.Add(textrange);
                        }
                    }
                    position = position.GetNextContextPosition(LogicalDirection.Forward);
                }


                foreach (TextRange range in ranges)
                {
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                    range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File not selected!");
            }
        }

        private void btnTab4Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtTab3Search.Text = txtTab1Search.Text = txtTab4Search.Text;
                var myFile = (File.ReadAllText(myOpenedFile)).Split();
                int ttt = myFile.Count();
                int i = 0;
                List<int> indexOfFind = new List<int>();
                foreach (var q in myFile)
                {
                    if (q.Contains(txtTab4Search.Text))
                    {
                        indexOfFind.Add(i);
                    }

                    i++;
                }
                List<ConcordanceHits> myTable = new List<ConcordanceHits>();
                int p = 1;
                foreach (var t in indexOfFind)
                {
                    string S = "";
                    for(int k=0; k<Convert.ToInt32(myUpDown1.Value)-1;k++)
                    {
                        try
                        {
                            S += myFile[t + k]+" ";
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                    for(int k=Convert.ToInt32(myUpDown1.Value)-1; k<Convert.ToInt32(myUpDown2.Value);k++)
                    {
                        try
                        {
                            S += myFile[t + k] + " ";
                            myTable.Add(new ConcordanceHits() { Id = p, myWords = S});
                            p++;
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                    
                }
                labNumberHits4.Text = (p - 1).ToString();
                DG4.ItemsSource = myTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("File not selected!");
            }

        }

        private void btnTab5Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                char[] separator = { ',', '\n', '\t', ' ', '\b', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', '\"', '[', ']', '(', ')' };
                var myFile = (File.ReadAllText(myOpenedFile)).Split(separator);
                var hashSet = new HashSet<string>(myFile);
                labWordType4.Text = (hashSet.Count-1).ToString();

                var v = myFile.GroupBy(s => s).OrderByDescending(s=>s.Count()).ThenBy(n=>n.Key).ToList();

                List<WordTypes> www = new List<WordTypes>();
                int m = 1;
                for (int i=1;i<v.Count;i++)
                {
                    www.Add(new WordTypes() { Id = m, Word = v[i].Key, Count = v[i].Count() });
                    m++;
                }
                DG5.ItemsSource = www;



            }
            catch (Exception ex)
            {
                MessageBox.Show("File not selected!");
            }
        }

        private void btnTab5Convert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                char[] separator = { ',', '\n', '\t', ' ', '\b', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', '\"', '[', ']', '(', ')' };
                var myFile = (File.ReadAllText(myOpenedFile)).Split(separator);
                var hashSet = new HashSet<string>(myFile);
                labWordType4.Text = (hashSet.Count - 1).ToString();

                var v = myFile.GroupBy(s => s).OrderByDescending(s => s.Count()).ThenBy(n => n.Key).ToList();

                List<WordTypes> www = new List<WordTypes>();
                int m = 1;
                for (int i = 1; i < v.Count; i++)
                {
                    www.Add(new WordTypes() { Id = m, Word = v[i].Key, Count = v[i].Count() });
                    m++;
                }

                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "EXCELL FILE (*.xlsx)|*.xlsx";
                saveFileDialog1.RestoreDirectory = true;
                string filename;
                if (saveFileDialog1.ShowDialog() == true)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        // Code to write the stream goes here.
                        filename = saveFileDialog1.FileName;
                        myStream.Close();

                        var workbook = new XLWorkbook();
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");
                        worksheet.Columns().AdjustToContents();
                        worksheet.Cell(1, 1).Value = "Fayl : " + _XXX;
                        worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 3)).Merge();
                        worksheet.Cell(2, 1).Value = "Darajasi";
                        worksheet.Cell(2, 2).Value = "Soni";
                        worksheet.Cell(2, 3).Value = "So'z";

                        int kk = 3;
                        foreach (WordTypes rv in www)
                        {
                            worksheet.Cell(kk, 1).Value = rv.Id;
                            worksheet.Cell(kk, 2).Value = rv.Count;
                            worksheet.Cell(kk, 3).Value = rv.Word;
                            kk++;
                        }

                        worksheet.Columns("A", "Z").AdjustToContents();
                        //  string sql = "SELECT Dorilars.Id as id,Partiyas.Id as t_id,  TovarNomi as Nomi, IshlabChiqaruvchi as Ishlab_chiqaruvchi,Nomi as Turi,Doza,(BazaviyNarx) as Olish_Narxi,SotiladiganNarx as Sotiladigan_Narx,NechtaKeldiDona/PachkadaNechta as Necha_Pachka,NechtaKeldiDona%PachkadaNechta as Necha_Dona,PachkadaNechta as Pachkada_soni, Shtrix as Shtrix_Kod, CONVERT(varchar, KelganSana, 3) as kelgan_sana From Dorilars,Partiyas,DorilarTuris where Dorilars.DorilarTuriId=DorilarTuris.Id and Dorilars.id=Partiyas.DorilarId;";

                        workbook.SaveAs(filename);
                        Process cmd = new Process();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.Start();

                        cmd.StandardInput.WriteLine(filename);
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.Close();
                        cmd.WaitForExit();
                        //  workbook.
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("File not selected!");
            }
        }
    }
}
