using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace アナログ時計WPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer;
        List<Line> clockLines = new List<Line>();
        List<Line> clockEdgeLines = new List<Line>();
        Ellipse clockCircleEdge;

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

            clockCircleEdge = new Ellipse();
            clockCircleEdge.Width = this.Width - 2;
            clockCircleEdge.Height = this.Height - 2;
            clockCircleEdge.SetValue(Canvas.TopProperty, 1d);
            clockCircleEdge.SetValue(Canvas.LeftProperty, 1d);
            Color color = new Color();
            color.A = 0; color.B = 0xff; color.G = 0xff; color.R = 0xff;
            clockCircleEdge.Fill = new SolidColorBrush(color);
            clockCircleEdge.Stroke = new SolidColorBrush(Colors.White);
            clockCircleEdge.StrokeThickness = 1;
            canvas.Children.Add(clockCircleEdge);

            double centerX = this.Width / 2;        //中心X軸
            double centerY = this.Height / 2;       //中心Y軸

            //文字盤の線を描く
            for (int i = 0; i < 60; i++)
            {
                //座標をだす
                double lineLength = 1d - 0.1;
                double lineTipX = centerX + (centerX * Math.Cos((Math.PI / 30) * i));
                double lineX = centerX + (centerX * Math.Cos((Math.PI / 30) * i)) * lineLength;
                double lineTipY = centerY + (centerY * Math.Sin((Math.PI / 30) * i));
                double lineY = centerY + (centerY * Math.Sin((Math.PI / 30) * i)) * lineLength;
                double lineEdgeX = lineX - (1 * Math.Cos((Math.PI / 30) * i));
                double lineEdgeY = lineY - (1 * Math.Sin((Math.PI / 30) * i));
                //描画
                if (i % 5 == 0)
                {
                    Line edgeline = new Line();
                    edgeline.X1 = lineEdgeX; edgeline.Y1 = lineEdgeY; edgeline.X2 = lineTipX; edgeline.Y2 = lineTipY;
                    edgeline.StrokeThickness = 8; edgeline.Stroke = new SolidColorBrush(Colors.White);
                    Line fiveline = new Line();
                    fiveline.X1 = lineTipX; fiveline.Y1 = lineTipY; fiveline.X2 = lineX; fiveline.Y2 = lineY;
                    fiveline.StrokeThickness = 6; fiveline.Stroke = new SolidColorBrush(Colors.Black);

                    canvas.Children.Add(edgeline);
                    canvas.Children.Add(fiveline);

                    clockLines.Add(edgeline); clockEdgeLines.Add(fiveline);
                }
                else
                {
                    Line line = new Line();
                    line.X1 = lineTipX; line.Y1 = lineTipY; line.X2 = lineX; line.Y2 = lineY;
                    line.StrokeThickness = 1; line.Stroke = new SolidColorBrush(Colors.Black);

                    canvas.Children.Add(line);

                    clockLines.Add(line);
                }
            }

        }

        DateTime nowTime;
        public bool touka = false;
        public bool secHandCheck = false;
        public bool minHandCheck = false;
        public bool dateCheck = false;
        /// <summary>
        /// タイマーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 時計の円周の線を描く
        /// </summary>
        private void DrawClokLine()
        {
            double centerX = this.Width / 2;        //中心X軸
            double centerY = this.Height / 2;       //中心Y軸

            //文字盤の線を描く
            for (int i = 0; i < 60; i++)
            {
                //座標をだす
                double lineLength = 1d - 0.1;
                double lineTipX = centerX + (centerX * Math.Cos((Math.PI / 30) * i));
                double lineX = centerX + (centerX * Math.Cos((Math.PI / 30) * i)) * lineLength;
                double lineTipY = centerY + (centerY * Math.Sin((Math.PI / 30) * i));
                double lineY = centerY + (centerY * Math.Sin((Math.PI / 30) * i)) * lineLength;
                double lineEdgeX = lineX - (1 * Math.Cos((Math.PI / 30) * i));
                double lineEdgeY = lineY - (1 * Math.Sin((Math.PI / 30) * i));
                //描画
                if (i % 5 == 0)
                {
                    Line edgeline = clockLines[i];
                    edgeline.X1 = lineEdgeX; edgeline.Y1 = lineEdgeY; edgeline.X2 = lineTipX; edgeline.Y2 = lineTipY;
                    Line fiveline = clockEdgeLines[i / 5];
                    fiveline.X1 = lineTipX; fiveline.Y1 = lineTipY; fiveline.X2 = lineX; fiveline.Y2 = lineY;
                }
                else
                {
                    Line line = clockLines[i];
                    line.X1 = lineTipX; line.Y1 = lineTipY; line.X2 = lineX; line.Y2 = lineY;
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TokeiWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawClokLine();
        }
    }
}