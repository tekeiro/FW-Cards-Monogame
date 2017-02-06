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
        //----- CARDS  ---------
        public static readonly string CARDS_PATH = @"DB\cards.json";

        //------- MAP PROPERTIES  -------------
        public static readonly string BACKGROUND_IMG = "Background";
        public static readonly string BACKGROUND_COLOR = "BackgroundColor";

        //------- TILED CUSTOM OBJECTS  -------------
        public static readonly string PORTAL_TYPE = "portal";
        
    }
}
