using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace RpgGame.Models;

public class Game : ObservableObject
{
    [JsonProperty("userLevel")]
    public int UserLevel { get; set; }


    private string userMoney;
    [JsonProperty("userMoney")]

    public string UserMoney
    {
        get { return userMoney; }
        set { SetProperty(ref userMoney, value); }
    }

    private Stats stats;

    [JsonProperty("stats")]

    public Stats Stats
    {
        get { return stats; }
        set { SetProperty(ref stats, value); }
    }
    [JsonIgnore]
    public bool IsAutoClickOn
    {
        get { return isAutoClickOn; }
        set { SetProperty(ref isAutoClickOn, value); }
    }
    private bool isAutoClickOn = false;

    [JsonIgnore]
    public ICommand UpgradeStatCommand { get; }
    
    public Game()
    {
        UpgradeStatCommand = new RelayCommand<string>(UpgradeStat);
       
    }

    public Game(int userLevel, string money, Stats stats)
    {
        this.UserLevel = userLevel;
        this.userMoney = money;
        this.stats = stats;
    }
    public Game GetCopy()
    {
        return new Game()
        {
            UserLevel = this.UserLevel,
            userMoney = this.userMoney,
            Stats = this.Stats
        };
    }

    public string AdjustMoney(int money)
    {
        double goldAmount = (double)money;
        string tag = "";
        if (goldAmount > 999999)
        {
            goldAmount = goldAmount / 999999;
            tag = "M";
        }
        else if (goldAmount > 999)
        {
            goldAmount = goldAmount / 999;
            tag = "K";
        }
        return goldAmount.ToString("F1")+tag;
    }
    public int UpgradeCost(string statName)
    {
        int costByCurrentLevel = 0;
        double discount = Stats.PriceDiscount;

        switch (statName.ToLower())
        {
            case "power":
                costByCurrentLevel = Stats.Power;
                break;
            case "stamina":
                costByCurrentLevel = Stats.Stamina;
                break;
            case "energy":
                costByCurrentLevel = Stats.Energy;
                break;
            case "pricediscount":
                costByCurrentLevel = Stats.PriceDiscount;
                break;
            case "doubleliftchance":
                costByCurrentLevel = Stats.DoubleLiftChance;
                break;
        }
        double price = (costByCurrentLevel + costByCurrentLevel * costByCurrentLevel);
        //Discount calculating (max 50% min 1%)
        price = price - (price * (discount / 1000 * 5));

        return (int)price;

    }
    public int CurrentStatCost(string statName)
    {
        int level = 0;
        switch (statName.ToLower())
        {
            case "power":
                level = Stats.Power;
                break;
            case "stamina":
                level = Stats.Stamina;
                break;
            case "energy":
                level = Stats.Energy;
                break;
            case "pricediscount":
                level = Stats.PriceDiscount;
                break;
            case "doubleliftchance":
                level = Stats.DoubleLiftChance;
                break;
        }
        return level;
    }
    public void UpgradeStat(string statName)
    {
        string stat = statName.ToLower();
        double current_money = double.Parse(this.UserMoney);
        int upgradeCost = UpgradeCost(stat);
        if (current_money >= upgradeCost)
        {
            //stat_lvl_up
            switch (stat)
            {
                case "power":
                    this.Stats.Power = this.Stats.Power + 1;
                    break;
                case "stamina":
                    this.Stats.Stamina++;
                    break;
                case "energy":
                    this.Stats.Energy++;
                    break;
                case "pricediscount":
                    this.Stats.PriceDiscount++;
                    break;
                case "doubleliftchance":
                    this.Stats.DoubleLiftChance++;
                    break;
            }
            this.UserMoney = (current_money - upgradeCost).ToString();
        }else MessageBox.Show("Not enough gold for this upgrade.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

    }

    public void AddMoney()
    {
        this.UserMoney = (int.Parse(this.UserMoney) + 1).ToString();
    }
}