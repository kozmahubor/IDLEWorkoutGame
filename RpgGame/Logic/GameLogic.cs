using Microsoft.Toolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using RpgGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RpgGame.Logic
{
    class GameLogic : IGameLogic
    {
        IEnumerable<Game> game;
        IEnumerable<Stats> stat;
        Game gameData;
        IMessenger messenger;


        public GameLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public double AddGold
        {
            get
            {
                return Convert.ToInt32(game.Select(g => g.UserLevel)) + 1;
            }
        }
        //public void TapForGold()
        //{
        //    if (game.Select(g => g.UserMoney) != null)
        //    {
        //        (game as Game).AddMoney();
        //    }
        //    messenger.Send("Gold added", "GameInfo");

        //}
        public void SetupCollections(IEnumerable<Game> game, IEnumerable<Stats> stat)
        {
            this.game = game;
            this.stat = stat;
        }
        //---------------------------------------------------------LOGIC METHODS-----------------------------------------------------------------------
        //---------------------------------------------------------LOGIC METHODS-----------------------------------------------------------------------

        public double CurrentGoldAmount()
        {
            return double.Parse(game.Select(g => g.UserMoney).FirstOrDefault());
        }
        public void AddMoney()
        {
            //Calculating chance of getting x2 gold (max 100% min 1% )
            int doubleGoldChance = stat.Select(s => s.DoubleLiftChance).FirstOrDefault();
            Random random = new Random();
            int randomNumber = random.Next(1, 102-doubleGoldChance);

            if (randomNumber == 1)
            {
                
                game = game.Select(g =>
                {

                    g.UserMoney = (double.Parse(g.UserMoney) + 2 * (0.1 * g.Stats.Power + 1)).ToString();
                    return g;
                }).ToList();
            }
            else
            {
                game = game.Select(g =>
                {

                    g.UserMoney = (double.Parse(g.UserMoney) + (0.1 * g.Stats.Power) + 1).ToString();
                    return g;

                }).ToList();
            }
            
        }
        
        public void AdminGoldAdd()
        {


            game = game.Select(g =>
            {
                g.UserMoney = (double.Parse(g.UserMoney) + 1000000000).ToString();
                return g;
            }).ToList();
            
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
            return goldAmount.ToString("F1") + tag;
        }


        public int UpgradeCost(string statName)
        {
            int costByCurrentLevel = 0;
            double discount = stat.Select(s => s.PriceDiscount).FirstOrDefault();
            switch (statName.ToLower())
            {
                case "power":
                    costByCurrentLevel = stat.Select(s => s.Power).FirstOrDefault();
                    break;
                case "stamina":
                    costByCurrentLevel = stat.Select(s => s.Stamina).FirstOrDefault();
                    break;
                case "energy":
                    costByCurrentLevel = stat.Select(s => s.Energy).FirstOrDefault();
                    break;
                case "pricediscount":
                    costByCurrentLevel = stat.Select(s => s.PriceDiscount).FirstOrDefault();
                    break;
                case "doubleliftchance":
                    costByCurrentLevel = stat.Select(s => s.DoubleLiftChance).FirstOrDefault();
                    break;
            }
            double price = (costByCurrentLevel + costByCurrentLevel * costByCurrentLevel);
            //Discount calculating (max 50% min 1%)
            price = price - (price * (discount / 1000 * 5));
            return (int)price;
        }

        //1.Game Logic
        public void UpgradeStat(string statName)
        {
            string statToUpgrade = statName.ToLower();
            int current_money = int.Parse(game.Select(g => g.UserMoney).FirstOrDefault());
            int upgradeCost = UpgradeCost(statToUpgrade);
            if (current_money >= upgradeCost)
            {
                //stat_lvl_up
                switch (statToUpgrade)
                {
                    case "power":
                        stat.Select(s => s.Power + 1).FirstOrDefault();
                        break;
                    case "stamina":
                        stat.Select(s => s.Stamina + 1).FirstOrDefault();
                        break;
                    case "energy":
                        stat.Select(s => s.Energy + 1).FirstOrDefault();
                        break;
                    case "pricediscount":
                        stat.Select(s => s.PriceDiscount + 1).FirstOrDefault();
                        break;
                    case "doubleliftchance":
                        stat.Select(s => s.DoubleLiftChance + 1).FirstOrDefault();
                        break;

                }
                game.Select(g => g.UserMoney = (current_money - upgradeCost).ToString());
            }
            else
            {
                MessageBox.Show("Not enough gold for this upgrade.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void SaveGame()
        {
            string json = JsonConvert.SerializeObject(new
            {
                game = game.FirstOrDefault(),
            });
            File.WriteAllText("Logic/data.json", json);
        }
    }
}
