using Newtonsoft.Json;
using System.IO;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RpgGame.Models;
using RpgGame.Logic;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using System;

namespace RpgGame.ViewModel;

internal class MainWindowViewModel : ObservableRecipient
{
    //public Game GameData { get; set; }
    IGameLogic logic;

    public ObservableCollection<Game> GameData { get; set; }
    public ObservableCollection<Stats> Stats { get; set; }
    //gets current gold
    public double CurrentGold
    {
        get
        {
            return logic.CurrentGoldAmount();
        }
    }


    public static bool IsInDesignMode
    {
        get
        {
            var prop = DesignerProperties.IsInDesignModeProperty;
            return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
        }
    }
    public MainWindowViewModel()
    :   this(IsInDesignMode ? null : Ioc.Default.GetService<IGameLogic>())
    {

    }
    public MainWindowViewModel(IGameLogic logic)
    {
        //Get data from json --

        this.logic = logic;
        GameData = new ObservableCollection<Game>();
        Stats = new ObservableCollection<Stats>();

        string filePath = "././Logic/data.json";
        string json = File.ReadAllText(filePath);
        SerializedGameDataModel data = JsonConvert.DeserializeObject<SerializedGameDataModel>(json)!;


        GameData.Add(data.Game);
        Stats.Add(data.Game.Stats);
        logic.SetupCollections(GameData, Stats);
    }

    //private void LoadGameData()
    //{
    //    string filePath = "GameData.json";

    //    if (File.Exists(filePath))
    //    {
    //        string json = File.ReadAllText(filePath);
    //        GameData = JsonConvert.DeserializeObject<Game>(json);
    //    }
    //    else
    //    {
    //        GameData = new Game
    //        {
    //            UserLevel = 10,
    //            UserMoney = "1234567",
    //            Stats = new Stats()
    //            {
    //                Power = 10,
    //                Stamina = 2,
    //                Energy = 3,
    //                PriceDiscount = 5,
    //                DoubleLiftChance = 7,
    //            }
    //        };
    //    }
    //}
    /*string json = File.ReadAllText("Logic/data.json");
    SerializedGameData data = JsonConvert.DeserializeObject<SerializedGameData>(json)!;

    foreach (var item in data.Game)
    {
        Game.Add(item.GetCopy());
    }

    logic.SetupCollections(Game);


    Messenger.Register<MainWindowViewModel, string, string>(this, "GameInfo", (recipient, msg) =>
    {
        OnPropertyChanged("AddGold");
    });*/

    //public void GoldAdjust(int gold)
    //{
    //    GameData.AdjustMoney(gold);
    //}
    //public void GoldNeedForUpdate(string statName)
    //{
    //    GameData.UpgradeCost(statName);
    //}
    //public void Upgrade(string stat)
    //{
    //    if (GameData.Stats != null)
    //    {
    //        GameData.UpgradeStat(stat);
    //    }
    //}
    //public double GoldForUpgrade 
    //{
    //    get
    //    {
    //        return GoldForUpgrade;
    //    }

    //    set
    //    {
    //        this.GoldForUpgrade = value;
    //    } 
    //}

    //public void TapForGold()
    //{
    //    if (GameData.UserMoney != null)
    //    {
    //        GameData.AddMoney();
    //    }

    //}

    public void TapForGold()
    {
        if (logic.CurrentGoldAmount() != null)
        {
            logic.AddMoney();
        }

    }
    public void AdminGoldAdd()
    {
        if (logic.CurrentGoldAmount() != null)
        {
            logic.AdminGoldAdd();
        }

    }
    public void GoldAdjust(int gold)
    {
        logic.AdjustMoney(gold);
    }
    public void GoldNeedForUpdate(string statName)
    {
        logic.UpgradeCost(statName);
    }
    public void Upgrade(string stat)
    {
        logic.UpgradeStat(stat);
        
    }
    //public double GoldForUpgrade
    //{
    //    get
    //    {
    //        return GoldForUpgrade;
    //    }

    //    set
    //    {
    //        this.GoldForUpgrade = value;
    //    }
    //}

    public void SaveGame()
    {
        logic.SaveGame();
    }
}