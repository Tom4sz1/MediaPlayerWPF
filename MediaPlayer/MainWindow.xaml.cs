using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            ApplicationCommands.Close.InputGestures.Add(
                new KeyGesture(Key.Q, ModifierKeys.Control)
                );
        }

        private void VideoClipPlay(object sender, RoutedEventArgs e)
        {
            VideoClip.Play();
            if(timer != null)
                timer.Start();
        }

        private void VideoClipPause(object sender, RoutedEventArgs e)
        {
            VideoClip.Pause();
            if(timer != null)
                timer.Stop();
        }

        private void VideoClipStop(object sender, RoutedEventArgs e)
        {
            VideoClip.Stop();
            VideoClip.Position = TimeSpan.Zero;
            if (timer != null) {
                timer.Stop();
                TimerSlider.Value = 0;
            }
        }

        private void VideoClip_MediaFaild(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VideoClip.ScrubbingEnabled = true;
            VideoClip.Stop();
        }

        private void VideoClip_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimerSlider.Maximum = VideoClip.NaturalDuration.TimeSpan.TotalSeconds;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            TotalTimeOfVideo.Content = VideoClip.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");

            VideoClip.Play();
            VideoClip.Pause();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TimerSlider.Value = VideoClip.Position.TotalSeconds;
        }

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoClip.Position = TimeSpan.FromSeconds(TimerSlider.Value);
        }

        private void TimerSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            VideoClip.Pause();
            if(timer != null)
                timer.Stop();
        }

        private void TimerSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            VideoClip.Play();
            if (timer != null)
                timer.Start();
        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4)|*.mp4";
            if(openFileDialog.ShowDialog() == true)
                VideoClip.Source = new System.Uri(Path.GetFullPath(openFileDialog.FileName));
        }

        private void CommandBinding_CanExecute_2(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute=true;
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("All rights reserved Tomasz Piotrowski");
        }
    }
}
