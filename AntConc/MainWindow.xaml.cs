using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System;

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
                txtNameOfFile.Text = myOpenedFile;

            }

        }

        private void btnTab1Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                List<ConcordanceHits> myTable = new List<ConcordanceHits>();
                int p = 1;
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
                    myTable.Add(new ConcordanceHits() { Id = p, myWords = S, myFile = myOpenedFile });
                    p++;
                }
                DG.ItemsSource = myTable;
            }
            catch(Exception ex)
            {
                MessageBox.Show("File not selected!");
            }

        }

        private void btnTab2Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myFile1 = File.ReadAllText(myOpenedFile);
                int v = (from a in myFile1
                         select a).Count();

                labTab2CharsCount.Text = v.ToString();
                labTab2FileName.Text = myOpenedFile;
                var myFile = (File.ReadAllText(myOpenedFile)).Split();
                labNumberHits2.Text = myFile.Where(u => u.Contains(txtTab1Search.Text)).Count().ToString();
                labTab2HitsCount.Text = myFile.Where(u => u.Contains(txtTab1Search.Text)).Count().ToString();

                List<ConcordanceHits> myTable = new List<ConcordanceHits>();
                for (int i = 1; i <= Convert.ToInt32(labTab2HitsCount.Text); i++)
                {
                    myTable.Add(new ConcordanceHits() { Id = i });
                }
                DG2.ItemsSource = myTable;
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
                txtRichText.Text = myFile;
                var myFile2 = (File.ReadAllText(myOpenedFile)).Split();
                labNumberHits3.Text = myFile2.Where(u => u.Contains(txtTab3Search.Text)).Count().ToString();
                labFileName3.Text = myOpenedFile;

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
                labTotalNo4.Text = (p - 1).ToString();
                DG4.ItemsSource = myTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("File not selected!");
            }

        }
    }
}
