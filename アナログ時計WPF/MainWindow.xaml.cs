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
        Line[] clockLines;
        Line[] clockEdgeLines;

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

            DrawClockCircleEdge();

            double centerX = this.Width / 2;        //中心X軸
            double centerY = this.Height / 2;       //中心Y軸

            //円周の線の記述
            clockLines = new Line[60]
            {
                clockLine1,
                clockLine2,
                clockLine3,
                clockLine4,
                clockLine5,
                clockLine6,
                clockLine7,
                clockLine8,
                clockLine9,
                clockLine10,
                clockLine11,
                clockLine12,
                clockLine13,
                clockLine14,
                clockLine15,
                clockLine16,
                clockLine17,
                clockLine18,
                clockLine19,
                clockLine20,
                clockLine21,
                clockLine22,
                clockLine23,
                clockLine24,
                clockLine25,
                clockLine26,
                clockLine27,
                clockLine28,
                clockLine29,
                clockLine30,
                clockLine31,
                clockLine32,
                clockLine33,
                clockLine34,
                clockLine35,
                clockLine36,
                clockLine37,
                clockLine38,
                clockLine39,
                clockLine40,
                clockLine41,
                clockLine42,
                clockLine43,
                clockLine44,
                clockLine45,
                clockLine46,
                clockLine47,
                clockLine48,
                clockLine49,
                clockLine50,
                clockLine51,
                clockLine52,
                clockLine53,
                clockLine54,
                clockLine55,
                clockLine56,
                clockLine57,
                clockLine58,
                clockLine59,
                clockLine60,
            };
            clockEdgeLines = new Line[12]
            {
                clockEdgeLine1,
                clockEdgeLine2,
                clockEdgeLine3,
                clockEdgeLine4,
                clockEdgeLine5,
                clockEdgeLine6,
                clockEdgeLine7,
                clockEdgeLine8,
                clockEdgeLine9,
                clockEdgeLine10,
                clockEdgeLine11,
                clockEdgeLine12,
            };

            //文字盤の線を描く
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                {
                    clockLines[i].StrokeThickness = 8; clockLines[i].Stroke = new SolidColorBrush(Colors.White);
                    clockEdgeLines[i / 5].StrokeThickness = 6; clockEdgeLines[i / 5].Stroke = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    clockLines[i].StrokeThickness = 1; clockLines[i].Stroke = new SolidColorBrush(Colors.Black);
                }
            }

            DrawClokLine();

        }

        //外円の縁
        private void DrawClockCircleEdge()
        {
            clockCircleEdge.Width = this.Width - 2;
            clockCircleEdge.Height = this.Height - 2;
            clockCircleEdge.SetValue(Canvas.TopProperty, 1d);
            clockCircleEdge.SetValue(Canvas.LeftProperty, 1d);
            Color color = new Color();
            color.A = 0; color.B = 0xff; color.G = 0xff; color.R = 0xff;
            clockCircleEdge.Fill = new SolidColorBrush(color);
            clockCircleEdge.Stroke = new SolidColorBrush(Colors.White);
            clockCircleEdge.StrokeThickness = 1;
        }

        public bool touka = false;
        public bool secHandCheck = true;
        public bool minHandCheck = true;
        public bool dateCheck = false;
        /// <summary>
        /// タイマーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            DrawClockCircleEdge();
            DrawClokLine();
            DrawHand(DateTime.Now);
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

        private void DrawHand(DateTime nowTime)
        {
            double centerX = this.Width / 2;        //中心X軸
            double centerY = this.Height / 2;       //中心Y軸

            //時間の12時間表示
            double nowHour = nowTime.Hour;
            if (nowHour > 12)
            {
                nowHour = nowHour - 12;
            }
            //ぬるぬる計算
            double nowSecond = nowTime.Second;                  //秒針
            if (secHandCheck)
            {
                nowSecond = nowSecond + (double)nowTime.Millisecond / 1000;
            }
            double nowMinute = nowTime.Minute;                  //分針
            if (minHandCheck && nowSecond >= 59)
            {
                nowMinute = nowMinute + (double)nowTime.Millisecond / 1000;
            }
            nowHour = nowHour + nowMinute/*(double)nowTime.Minute*/ / 60;    //時針

            //針先端の座標算出
            //時針
            double houHandLength = 0.5;
            double houHandTipX = centerX + (centerX * ((Math.Cos(((nowHour * Math.PI) / 6) - (Math.PI / 2))))) * houHandLength;
            double houHandTipY = centerY + (centerY * ((Math.Sin(((nowHour * Math.PI) / 6) - (Math.PI / 2))))) * houHandLength;
            //分針
            double minHandLength = 0.84;
            double minHandTipX = centerX + (centerX * ((Math.Cos(((nowMinute * Math.PI) / 30) - (Math.PI / 2))))) * minHandLength;
            double minHandTipY = centerY + (centerY * ((Math.Sin(((nowMinute * Math.PI) / 30) - (Math.PI / 2))))) * minHandLength;
            //秒針
            double secHandLength = 0.97;
            double secHandTipX = centerX + (centerX * ((Math.Cos(((nowSecond * Math.PI) / 30) - (Math.PI / 2))))) * secHandLength;
            double secHandTipY = centerY + (centerY * ((Math.Sin(((nowSecond * Math.PI) / 30) - (Math.PI / 2))))) * secHandLength;
            double secHandOppositeX = centerX + (centerX * ((Math.Cos(((nowSecond * Math.PI) / 30) + (Math.PI / 2))))) * (secHandLength * 0.2);    //反対側の座標
            double secHandOppositeY = centerY + (centerY * ((Math.Sin(((nowSecond * Math.PI) / 30) + (Math.PI / 2))))) * (secHandLength * 0.2);    //反対側の座標
            //縁の先端の座標算出
            //時針
            double houEdgeTipX = houHandTipX + (1 * ((Math.Cos(((nowHour * Math.PI) / 6) - (Math.PI / 2)))));
            double houEdgeTipY = houHandTipY + (1 * ((Math.Sin(((nowHour * Math.PI) / 6) - (Math.PI / 2)))));
            //分針
            double minEdgeTipX = minHandTipX + (1 * ((Math.Cos(((nowTime.Minute * Math.PI) / 30) - (Math.PI / 2)))));
            double minEdgeTipY = minHandTipY + (1 * ((Math.Sin(((nowTime.Minute * Math.PI) / 30) - (Math.PI / 2)))));
            //秒針
            double secEdgeTipX = secHandTipX + (1 * ((Math.Cos(((nowTime.Second * Math.PI) / 30) - (Math.PI / 2)))));
            double secEdgeTipY = secHandTipY + (1 * ((Math.Sin(((nowTime.Second * Math.PI) / 30) - (Math.PI / 2)))));
            double secEdgeOppositeX = secHandOppositeX + (1 * ((Math.Cos(((nowTime.Second * Math.PI) / 30) + (Math.PI / 2)))));
            double secEdgeOppositeY = secHandOppositeY + (1 * ((Math.Sin(((nowTime.Second * Math.PI) / 30) + (Math.PI / 2)))));

            //縁の描画
            //時針
            hourHandEdge.X1 = centerX; hourHandEdge.Y1 = centerY; hourHandEdge.X2 = houEdgeTipX; hourHandEdge.Y2 = houEdgeTipY;
            //分針
            minuteHandEdge.X1 = centerX; minuteHandEdge.Y1 = centerY; minuteHandEdge.X2 = minEdgeTipX; minuteHandEdge.Y2 = minEdgeTipY;
            //秒針
            secondHandEdge.X1 = secEdgeOppositeX; secondHandEdge.Y1 = secEdgeOppositeY; secondHandEdge.X2 = secEdgeTipX; secondHandEdge.Y2 = secEdgeTipY;
            DrawClokLine();         //文字盤の線の描画
            //針本体の描画
            //時針
            hourHand.X1 = centerX; hourHand.Y1 = centerY; hourHand.X2 = houHandTipX; hourHand.Y2 = houHandTipY;
            //分針
            minuteHand.X1 = centerX; minuteHand.Y1 = centerY; minuteHand.X2 = minHandTipX; minuteHand.Y2 = minHandTipY;
            //秒針
            secondHand.X1 = secHandOppositeX; secondHand.Y1 = secHandOppositeY; secondHand.X2 = secHandTipX; secondHand.Y2 = secHandTipY;

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
            DrawClockCircleEdge();
            DrawHand(DateTime.Now);
        }
    }
}