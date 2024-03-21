using System.ComponentModel;

namespace FilteringSorting.Models.Enums
{
    
    public enum Categories
	{
        [Description("None")]
        None,
        [Description("Monitor")]
        Monitor,
        [Description("Smart TV")]
        SamrtTv,
        [Description("Game Console")]
        GameConsole,
        [Description("Game Controler")]
        GameControler,
        [Description("Handset")]
        Handset,
        [Description("HD")]
        HD,
        [Description("Gaming Furniture")]
        GamingFurniture,
        [Description("Games")]
        Games
    }
}

