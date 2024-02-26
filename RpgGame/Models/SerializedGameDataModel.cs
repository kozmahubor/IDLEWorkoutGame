using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Models
{
    internal class SerializedGameDataModel
    {
        public Game Game { get; set; }
        public SerializedGameDataModel(Game game) 
        {
            this.Game = game;    
        }
    }
}
