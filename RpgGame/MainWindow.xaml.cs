using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using RpgGame.Models;
using RpgGame;
using RpgGame.ViewModel;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using Microsoft.Toolkit.Mvvm.Input;
using System.Data.Common;
using Image = System.Windows.Controls.Image;
using System.Windows.Threading;

namespace RpgGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Timer
        private DispatcherTimer timer;
        public MainWindow()
        {  

        InitializeComponent();

            //Set timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer;

            // Start the timer
            timer.Start();
        }

       
        #region AdminClick
        private bool isActionInProgress = false;
        private DispatcherTimer actionTimer;
        private void ActionTimer_Tick(object sender, EventArgs e)
        {
            if (isActionInProgress)
            {
                var vm = (MainWindowViewModel)this.DataContext;
                vm.AdminGoldAdd();
            }
        }
        private void AddAdminGold(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)this.DataContext;
            vm.AdminGoldAdd();
        }
        private void StartAction(object sender, MouseButtonEventArgs e)
        {
            isActionInProgress = true;
            actionTimer = new DispatcherTimer();
            actionTimer.Interval = TimeSpan.FromMilliseconds(100); // Set the desired interval
            actionTimer.Tick += ActionTimer_Tick;
            actionTimer.Start();
        }
        private void StopAction(object sender, MouseButtonEventArgs e)
        {
            isActionInProgress = false;
            actionTimer?.Stop();
        }
        #endregion
        //Stamina increase - decrease by time (different values by the upgrades)
        private void Timer(object sender, EventArgs e)
        {
            var vm = (MainWindowViewModel)this.DataContext;
            if (vm.Stats.Select(s => s.CurrentEnergyLvl).FirstOrDefault() < 100)
            {
                CalculateEnergyGainOnTap(vm);
            }

        }
        //Gold by tap!
        private void AddGold_ImageClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (MainWindowViewModel)this.DataContext;

            pressForGold.Source = new BitmapImage(new Uri(ChangeButtonDisplay((int)vm.CurrentGold, pressForGold.Source.ToString()), UriKind.Relative));

            if (vm.Stats.Select(s => s.CurrentEnergyLvl).FirstOrDefault() > 2)
            {
                CalculateEnergyLossOnTap(vm);
                vm.TapForGold();
            }
            else
            {
                MessageBox.Show("Not enough stamina, rest a bit buddy you look GOOD!(:.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void CalculateEnergyGainOnTap(MainWindowViewModel vm)
        {

            double currEng = vm.Stats.Select(s => s.CurrentEnergyLvl).FirstOrDefault();
            double power = vm.Stats.Select(s => s.Power).FirstOrDefault();
            double energy = vm.Stats.Select(s => s.Energy).FirstOrDefault();
            currEng = (currEng < 0) ? 0 : currEng;
            currEng = (currEng < 100) ? (currEng + 1 + energy) : 100;
            currEng = (currEng > 100) ? 100 : currEng;
            vm.Stats.Select(s => s.CurrentEnergyLvl = currEng).FirstOrDefault();


        }
        private void CalculateEnergyLossOnTap(MainWindowViewModel vm)
        {

            double currEng = vm.Stats.Select(s => s.CurrentEnergyLvl).FirstOrDefault();
            double power = vm.Stats.Select(s => s.Power).FirstOrDefault();
            double stamina = vm.Stats.Select(s => s.Stamina).FirstOrDefault();
            if (stamina < 191)
            {
                currEng = (currEng > 0) ? currEng - (3 + power / 2) * (1 - (stamina / 200)) : currEng;
            }else
            {
                currEng = (currEng > 0) ? currEng - (3 + power / 2) *  (1 - (190 / 200)) : currEng;
            }
            
            vm.Stats.Select(s => s.CurrentEnergyLvl = currEng).FirstOrDefault();

        }
        private string ChangeButtonDisplay(int gold, string source)
        {
            string basic = "";
            switch (gold)
            {
                case >= 200:
                    basic = "black";
                    break;
                case >= 100:
                    basic = "blue";
                    break;
                case < 100:
                    basic = "basic";
                    break;
            }
            if (source.Contains("2"))
            {
                return $"/images/btn_{basic}.png";
            }else return $"/images/btn_{basic}2.png";
        }
        //Not used but whatever
        private void UpdateStat_ButtonClick(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)this.DataContext;
            string stat = (string)((Button)sender).CommandParameter;

            // Upgrade the specified stat
            vm.Upgrade(stat);
        }
        //Save game
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var vm = (MainWindowViewModel)this.DataContext;
            vm.SaveGame();
        }


        private void staminaProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}