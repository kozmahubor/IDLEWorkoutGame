using RpgGame.Models;
using System.Collections.Generic;

namespace RpgGame.ViewModel
{
    internal class  SerializedGameData
    {
        public IList<Game> Game { get; set; }

        public SerializedGameData(IList<Game> game)
        {
            this.Game = game;
        }
    }
}