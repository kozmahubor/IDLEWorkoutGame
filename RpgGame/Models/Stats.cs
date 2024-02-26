using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RpgGame.Models;

public class Stats : ObservableObject
{
    private int power;
    
    [JsonProperty("power")]

    public int Power
    {
        get { return power; }
        set { SetProperty(ref power, value); }
    }
    
    private int stamina;
    
    [JsonProperty("stamina")]

    public int Stamina
    {
        get { return stamina; }
        set { SetProperty(ref stamina, value); }
    }
    
    private int energy;
    
    [JsonProperty("energy")]

    public int Energy
    {
        get { return energy; }
        set { SetProperty(ref energy, value); }
    }
    private int priceDiscount;

    [JsonProperty("priceDiscount")]

    public int PriceDiscount
    {
        get { return priceDiscount; }
        set { SetProperty(ref priceDiscount, value); }
    }
    private int doubleLiftChance;

    [JsonProperty("doubleLiftChance")]

    public int DoubleLiftChance
    {
        get { return doubleLiftChance; }
        set { SetProperty(ref doubleLiftChance, value); }
    }

    private double currentEnergyLvl;

    [JsonProperty("currentEnergyLvl")]
    public double CurrentEnergyLvl
    {
        get { return currentEnergyLvl; }
        set { SetProperty(ref currentEnergyLvl, value); }
    }
    //public ICommand UpgradeStatCommand { get; }

    //public Stats()
    //{
    //    UpgradeStatCommand = new RelayCommand<string>(UpgradeStat);
    //}
    //public void UpgradeStat(string statName)
    //{
    //    switch (statName.ToLower())
    //    {
    //        case "power":
    //            Power++;
    //            break;
    //        case "stamina":
    //            Stamina++;
    //            break;
    //        case "energy":
    //            Energy++;
    //            break;
    //        case "pricediscount":
    //            PriceDiscount++;
    //            break;
    //        case "doubleliftchance":
    //            DoubleLiftChance++;
    //            break;
    //    }
    //}
    public Stats(int power, int stamina, int energy, int priceDiscount, int doubleLiftChance, double currentEnergyLvl)
    {
        this.Power = power;
        this.Stamina = stamina;
        this.Energy = energy;
        this.PriceDiscount = priceDiscount;
        this.DoubleLiftChance = doubleLiftChance;
        this.CurrentEnergyLvl = currentEnergyLvl;
    }
    public Stats()
    {

    }
    public Stats GetCopy()
    {
        return new Stats()
        {
            Power = this.Power,
            Stamina = this.Stamina,
            Energy = this.Energy,
            PriceDiscount = this.PriceDiscount,
            DoubleLiftChance = this.DoubleLiftChance,
        };
    }
}