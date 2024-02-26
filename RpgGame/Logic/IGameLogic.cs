using RpgGame.Models;
using System.Collections.Generic;

namespace RpgGame.Logic
{
    internal interface IGameLogic
    {
        double AddGold { get; }

        void AddMoney();
        void AdminGoldAdd();
        string AdjustMoney(int money);
        double CurrentGoldAmount();
        void SaveGame();
        void SetupCollections(IEnumerable<Game> game, IEnumerable<Stats> stat);
        //void TapForGold();
        int UpgradeCost(string statName);
        void UpgradeStat(string statName);
    }
}