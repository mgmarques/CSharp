using System.ComponentModel;

namespace DALECommerceApp.Models.Enums
{
    
    public enum Categories
	{
        [Description("Computer Components")]
        Components,
        [Description("Monitor")]
        Monitor,
        [Description("Smart TV")]
        SamrtTv,
        [Description("Audio")]
        Audio,
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
        Games,
        [Description("Services")]
        Services,
        [Description("Trinkets")]
        Trinkets
    }
}

