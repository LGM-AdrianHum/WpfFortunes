// Module   : WpfFortunes/WpfFortunes/MainWindow.xaml.cs
// Name     : Adrian Hum - adrianhum 
// Created  : 2017-11-23-1:39 PM
// Modified : 2017-11-23-4:38 PM

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Media.Animation;

namespace WpfFortunes
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var curdir =
                Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            AllSayings = File.ReadAllText(Path.Combine(curdir, "fortune.txt")).Split('%');

            St = new Storyboard();

            var a1 = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(3)),
                BeginTime = TimeSpan.FromSeconds(0)
            };

            var a2 = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(2)),
                BeginTime = TimeSpan.FromSeconds(13)
            };

            Storyboard.SetTargetName(a1, "Message");
            Storyboard.SetTargetName(a2, "Message");
            Storyboard.SetTargetProperty(a1, new PropertyPath("Opacity"));
            Storyboard.SetTargetProperty(a2, new PropertyPath("Opacity"));

            St.Children.Clear();
            St.Children.Add(a1);
            St.Children.Add(a2);

            St.Completed += St_Completed;
            Rand = new Random(DateTime.Now.Millisecond);

            St_Completed(null, null); //Call it once to get in the event loop.
        }

        public Random Rand { get; set; }


        public static Storyboard St { get; set; }


        public string[] AllSayings { get; set; }

        private void St_Completed(object sender, EventArgs e)
        {
            var r = Rand.Next(AllSayings.Length);
            Message.Text = AllSayings[r];
            St.Begin(Message);
        }
    }
}