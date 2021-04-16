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
                txtTab2Search.Text = txtTab3Search.Text = txtTab4Search.Text = txtTab1Search.Text;
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
                    myTable.Add(new ConcordanceHits() { Id = p, myWords = S, myFile = _XXX });
                    p++;
                }
                DG.ItemsSource = myTable;

                
                FindListViewItem(this.DG);

            }
            catch(Exception ex)
            {
                MessageBox.Show("File not selected!");
            }

        }

        public void FindListViewItem(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                ListViewItem lv = obj as ListViewItem;
                if (lv != null)
                {
                    HighlightText(lv);
                }
                FindListViewItem(VisualTreeHelper.GetChild(obj as DependencyObject, i));
            }
        }

        private void txtTab11Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindListViewItem(this.DG);
        }

        Regex regex;
        private void HighlightText(Object itx)
        {
            if (itx != null)
            {
                if (itx is TextBlock)
                {
                    regex = new Regex("(" + txtTab11Search.Text + ")", RegexOptions.IgnoreCase);
                    TextBlock tb = itx as TextBlock;
                    if (txtTab11Search.Text.Length == 0)
                    {
                        string str = tb.Text;
                        tb.Inlines.Clear();
                        tb.Inlines.Add(str);
                        return;
                    }
                    string[] substrings = regex.Split(tb.Text);
                    tb.Inlines.Clear();
                    foreach (var item in substrings)
                    {
                        if (regex.Match(item).Success)
                        {
                            Run runx = new Run(item);
                            runx.Background = Brushes.Red;
                            tb.Inlines.Add(runx);
                        }
                        else
                        {
                            tb.Inlines.Add(item);
                        }
                    }
                    return;
                }
                else
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(itx as DependencyObject); i++)
                    {
                        HighlightText(VisualTreeHelper.GetChild(itx as DependencyObject, i));
                    }
                }
            }
        }
        

        private void btnTab2Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtTab1Search.Text = txtTab3Search.Text = txtTab4Search.Text = txtTab2Search.Text;
                var myFile1 = File.ReadAllText(myOpenedFile);
                int v = (from a in myFile1
                         select a).Count();

                labTab2CharsCount.Text = v.ToString();
                labTab2FileName.Text = _XXX;
                var myFile = (File.ReadAllText(myOpenedFile)).Split();
                labNumberHits2.Text = myFile.Where(u => u.Contains(txtTab2Search.Text)).Count().ToString();
                labTab2HitsCount.Text = myFile.Where(u => u.Contains(txtTab2Search.Text)).Count().ToString();

                //int i = 0;
                //List<int> indexOfFind = new List<int>();
                //foreach (var q in myFile)
                //{
                //    if (q.Contains(txtTab1Search.Text))
                //    {
                //        indexOfFind.Add(i);
                //    }

                //    i++;
                //}
                //MessageBox.Show("1");
                //holders = new List<Holder>();
                //values = new List<Value>();
                //for(int kk=1;kk<10;kk++)
                //{
                //    if(indexOfFind.Contains(kk))
                //    {
                //        values.Add(new Value(kk*100, 0));
                //        values.Add(new Value(kk*100, 100));
                //        values.Add(new Value(kk*100, 0));
                //    }
                //    else
                //    {
                //        values.Add(new Value(kk, 0));
                //    }
                //}

                //Paint();

                //this.StateChanged += (sender, e) => Paint();
                //this.SizeChanged += (sender, e) => Paint();
                //< Canvas Grid.Row = "2" x: Name = "chartCanvas" ScrollViewer.HorizontalScrollBarVisibility = "Visible" ScrollViewer.VerticalScrollBarVisibility = "Visible" Margin = "10" Background = "White" />

                            List <ConcordanceHits> myTable = new List<ConcordanceHits>();
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
        //private Line xAxisLine, yAxisLine;
        //private double xAxisStart = 100, yAxisStart = 100, interval = 100;
        //private Polyline chartPolyline;

        //private Point origin;
        //private List<Holder> holders;
        //private List<Value> values;

        //public class Holder
        //{
        //    public double X { get; set; }
        //    public double Y { get; set; }
        //    public Point Point { get; set; }

        //    public Holder()
        //    {
        //    }
        //}

        //public class Value
        //{
        //    public double X { get; set; }
        //    public double Y { get; set; }

        //    public Value(double x, double y)
        //    {
        //        X = x;
        //        Y = y;
        //    }
        //}

        //public void Paint()
        //{
        //    try
        //    {
        //        if (this.ActualWidth > 0 && this.ActualHeight > 0)
        //        {
        //            chartCanvas.Children.Clear();
        //            holders.Clear();

        //            // axis lines
        //            xAxisLine = new Line()
        //            {
        //                X1 = xAxisStart,
        //                Y1 = this.ActualHeight - yAxisStart,
        //                X2 = this.ActualWidth - xAxisStart,
        //                Y2 = this.ActualHeight - yAxisStart,
        //                Stroke = Brushes.LightGray,
        //                StrokeThickness = 1,
        //            };
        //            yAxisLine = new Line()
        //            {
        //                X1 = xAxisStart,
        //                Y1 = yAxisStart - 50,
        //                X2 = xAxisStart,
        //                Y2 = this.ActualHeight - yAxisStart,
        //                Stroke = Brushes.LightGray,
        //                StrokeThickness = 1,
        //            };

        //            chartCanvas.Children.Add(xAxisLine);
        //            chartCanvas.Children.Add(yAxisLine);

        //            origin = new Point(xAxisLine.X1, yAxisLine.Y2);

        //            var xTextBlock0 = new TextBlock() { Text = $"{0}" };
        //            chartCanvas.Children.Add(xTextBlock0);
        //            Canvas.SetLeft(xTextBlock0, origin.X);
        //            Canvas.SetTop(xTextBlock0, origin.Y + 5);

        //            // y axis lines
        //            var xValue = xAxisStart;
        //            double xPoint = origin.X + interval;
        //            while (xPoint < xAxisLine.X2)
        //            {
        //                var line = new Line()
        //                {
        //                    X1 = xPoint,
        //                    Y1 = yAxisStart - 50,
        //                    X2 = xPoint,
        //                    Y2 = this.ActualHeight - yAxisStart,
        //                    Stroke = Brushes.LightGray,
        //                    StrokeThickness = 10,
        //                    Opacity = 1,
        //                };

        //                chartCanvas.Children.Add(line);

        //                var textBlock = new TextBlock { Text = $"{xValue}", };

        //                chartCanvas.Children.Add(textBlock);
        //                Canvas.SetLeft(textBlock, xPoint - 12.5);
        //                Canvas.SetTop(textBlock, line.Y2 + 5);

        //                xPoint += interval;
        //                xValue += interval;
        //            }


        //            var yTextBlock0 = new TextBlock() { Text = $"{0}" };
        //            chartCanvas.Children.Add(yTextBlock0);
        //            Canvas.SetLeft(yTextBlock0, origin.X - 20);
        //            Canvas.SetTop(yTextBlock0, origin.Y - 10);

        //            // x axis lines
        //            var yValue = yAxisStart;
        //            double yPoint = origin.Y - interval;
        //            while (yPoint > yAxisLine.Y1)
        //            {
        //                var line = new Line()
        //                {
        //                    X1 = xAxisStart,
        //                    Y1 = yPoint,
        //                    X2 = this.ActualWidth - xAxisStart,
        //                    Y2 = yPoint,
        //                    Stroke = Brushes.LightGray,
        //                    StrokeThickness = 10,
        //                    Opacity = 1,
        //                };

        //                chartCanvas.Children.Add(line);

        //                var textBlock = new TextBlock() { Text = $"{yValue}" };
        //                chartCanvas.Children.Add(textBlock);
        //                Canvas.SetLeft(textBlock, line.X1 - 30);
        //                Canvas.SetTop(textBlock, yPoint - 10);

        //                yPoint -= interval;
        //                yValue += interval;
        //            }

        //            // connections
        //            double x = 0, y = 0;
        //            xPoint = origin.X;
        //            yPoint = origin.Y;
        //            while (xPoint < xAxisLine.X2)
        //            {
        //                while (yPoint > yAxisLine.Y1)
        //                {
        //                    var holder = new Holder()
        //                    {
        //                        X = x,
        //                        Y = y,
        //                        Point = new Point(xPoint, yPoint),
        //                    };

        //                    holders.Add(holder);

        //                    yPoint -= interval;
        //                    y += interval;
        //                }

        //                xPoint += interval;
        //                yPoint = origin.Y;
        //                x += 100;
        //                y = 0;
        //            }

        //            // polyline
        //            chartPolyline = new Polyline()
        //            {
        //                Stroke = new SolidColorBrush(Color.FromRgb(68, 114, 196)),
        //                StrokeThickness = 10,
        //            };
        //            chartCanvas.Children.Add(chartPolyline);

        //            // showing where are the connections points
        //            foreach (var holder in holders)
        //            {
        //                Ellipse oEllipse = new Ellipse()
        //                {
        //                    Fill = Brushes.Red,
        //                    Width = 10,
        //                    Height = 10,
        //                    Opacity = 0,
        //                };

        //                chartCanvas.Children.Add(oEllipse);
        //                Canvas.SetLeft(oEllipse, holder.Point.X - 5);
        //                Canvas.SetTop(oEllipse, holder.Point.Y - 5);
        //            }

        //            // add connection points to polyline
        //            foreach (var value in values)
        //            {
        //                var holder = holders.FirstOrDefault(h => h.X == value.X && h.Y == value.Y);
        //                if (holder != null)
        //                    chartPolyline.Points.Add(holder.Point);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        private void btnTab3Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                var myFile = File.ReadAllText(myOpenedFile);
                txtTab2Search.Text = txtTab1Search.Text = txtTab4Search.Text = txtTab3Search.Text;
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
                txtTab2Search.Text = txtTab3Search.Text = txtTab1Search.Text = txtTab4Search.Text;
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
