using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCards.Config
{
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public static class Constants
    {
        //------- MODEL  ----------------------
        public static readonly string CARDS_PATH = @"DB\cards.json";
        public static readonly string TECHS_PATH = @"DB\techs.json";
        public static readonly string EQUIPS_PATH = @"DB\equips.json";
        public static readonly string ENEMIES_PAH = @"DB\enemies.json";
        //-- CHARS
        public static readonly string CHARS_PATH = @"DB\chars.json";
        public static readonly byte DEFAULT_MANA_CAPACITY = 3;
        public static readonly byte DEFAULT_DECK_CAPACITY = 10;

        //------- MAP PROPERTIES  -------------
        public static readonly string BACKGROUND_IMG = "Background";
        public static readonly string BACKGROUND_COLOR = "BackgroundColor";

        //------- TILED CUSTOM OBJECTS  -------------
        public static readonly string PORTAL_TYPE = "portal";
        
    }
}
