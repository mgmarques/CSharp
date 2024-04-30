using Microsoft.EntityFrameworkCore;

namespace GameShopping.ProductAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() {}
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "PlayStation 5 Digital Edition",
                Price = new decimal(569.9),
                Description = "<li>Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.</li>\r\n<li>Stunning Games - Marvel at incredible graphics and experience new PS5 features.</li>\r\n<li>Breathtaking Immersion - Discover a deeper gaming experience with support for haptic feedback, adaptive triggers, and 3D Audio technology.</li>\r\n<li>Model Number CFI-1102B</li>",
                ImageURL = "https://m.media-amazon.com/images/I/61loOpDhuML._SX522_.jpg",
                CategoryName = "Console"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Xbox Series X",
                Price = new decimal(559.99),
                Description = "Xbox Series X, the fastest, most powerful Xbox ever. Explore rich new worlds with 12 teraflops of raw graphic processing power, DirectX ray tracing, a custom SSD, and 4K gaming. Make the most of every gaming minute with Quick Resume, lightning-fast load times, and gameplay of up to 120 FPS—all powered by Xbox Velocity Architecture. Enjoy thousands of games from four generations of Xbox, with hundreds of optimized titles that look and play better than ever. And when you add Xbox Game Pass Ultimate (membership sold separately), you get online multiplayer to play with friends and an instant library of 100+ high-quality games, including day one releases from Xbox Game Studios.*",
                ImageURL = "https://m.media-amazon.com/images/I/61c937dHIvL._SX522_.jpg",
                CategoryName = "Console"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Darth Vader Helmet Star Wars Black Series",
                Price = new decimal(359.99),
                Description = "Fans can imagine the biggest battles and missions in the Star Wars saga with helmets from The Black Series! With exquisite features and decoration, this series embodies the quality and realism that Star Wars devotees love. Imagine donning the signature helmet of Darth Vader with the Darth Vader Premium Electronic Helmet, featuring collar, mask, and hood pieces for multi-piece, adjustable fit and assembly. When the collar is put on, wearers can activate breathing sound effects. When the mask is fitted to the collar with the secondary button and magnetic holds, helmet sealing sound effects will be activated. Using the primary button, wearers can activate breathing sound effects, and when removing the mask, activate helmet removal sounds. With movie-accurate sound effects and premium interior and exterior design, this helmet delivers on the iconic presentation and detail of roleplay items from Star Wars The Black Series. Additional products each sold separately. Star Wars products are produced by Hasbro under license from Lucasfilm Ltd. Hasbro and all related terms are trademarks of Hasbro.",
                ImageURL = "https://m.media-amazon.com/images/I/7100QE31gZL._AC_SY879_.jpge",
                CategoryName = "Replicase"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "PDP Victrix Pro BFG Wireless Gaming Controller",
                Price = new decimal(99.99),
                Description = "PDP Victrix Pro BFG Wireless Gaming Controller for Xbox Series X|Series S, Xbox One, and Windows 10/11, Dolby Atmos Audio, Remappable Buttons, Customizable Triggers/Paddles/D-Pad, PC App, White",
                ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
                CategoryName = "Control"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "ASUS ROG Strix G16",
                Price = new decimal(1649.99),
                Description = "ASUS ROG Strix G16 (2023) Gaming Laptop, 16” 16:10 FHD 165Hz, GeForce RTX 4070, Intel Core i9-13980HX, 16GB DDR5, 1TB PCIe SSD, Wi-Fi 6E, Windows 11, G614JI-AS94, Eclipse Gray",
                ImageURL = "https://m.media-amazon.com/images/I/71v0BQo8T8L._AC_SX679_.jpg",
                CategoryName = "Gaming Laptop"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Name = "Super Mario Bros.™ Wonder - Nintendo Switch (US Version)",
                Price = new decimal(49.99),
                Description = "<li>Find wonder in the Flower Kingdom in the next side-scrolling Super Mario adventure</li>\r\n<li>Collect Wonder Flowers for surprising, game-changing effects like pipes coming alive, an enemy stampede, and much, much more</li>\r\n<li>Choose from the largest cast of characters in a side-scrolling Mario game, including Mario, Luigi, Peach, Daisy and other favorites</li>\r\n<li>Ease into the action with four different-colored Yoshis and Nabbit who can’t take damage</li>\r\n<li>Discover new power-ups like Elephant Fruit, which transforms Mario and friends into an elephant that can swing its trunk and spray water</li>",
                ImageURL = "https://m.media-amazon.com/images/I/81KYcp48kgL._SX522_.jpg",
                CategoryName = "Game"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 7,
                Name = "Fallout 3 - PlayStation 3 Game of the Year Edition",
                Price = new decimal(19.9),
                Description = "Vault-Tec engineers have worked around the clock on an interactive reproduction of Wasteland life for you to enjoy from the comfort of your own vault. Included is an expansive world, unique combat, shockingly realistic visuals, tons of player choice, and an incredible cast of dynamic characters. Every minute is a fight for survival against the terrors of the outside world – radiation, Super Mutants, and hostile mutated creatures. From Vault-Tec, America’s First Choice in Post Nuclear Simulation.",
                ImageURL = "https://github.com/leandrocgsi/erudio-microservices-dotnet6/blob/main/ShoppingImages/7_coffee.jpg?raw=true",
                CategoryName = "Game"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 8,
                Name = "Red Dead Redemption 2 (PS4)",
                Price = new decimal(59.9),
                Description = "<li>From the creators of Grand Theft Auto V and Red Dead Redemption</li>\r\n<li><li>The deepest and most complex world Rockstar Games has ever created</li>\r\n<li>Covers a huge range of 19th century American landscapes</li>\r\n<li>Play as Arthur Morgan, lead enforcer in the notorious Van der Linde gang</li>\r\n<li>Interact with every character in the world with more than just your gun;Engage in conversation with people you meet. Your actions influence Arthur's honour;Horses are a cowboy's best friend, both transport and personal companion;Bond with your horse as you ride together to unlock new abilities</li>",
                ImageURL = "https://m.media-amazon.com/images/I/71VlXi52jBL._SX522_.jpg",
                CategoryName = "Game"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 9,
                Name = "The Art of Fallout 4 Hardcover – Illustrated",
                Price = new decimal(49.9),
                Description = "Bethesda Game Studios, the award-winning creators of Fallout® 3 and The Elder Scrolls V: Skyrim®, welcome you to the world of Fallout® 4 - their most ambitious game ever, and the next generation of open-world gaming. \r\n<br/><br/>\r\nThe Art of Fallout 4 is a must-have collectible for fans and an ultimate resource for every Wasteland wanderer. Featuring 368 oversize pages, never-before-seen designs, and concept art from the game's dynamic environments, iconic characters, detailed weapons, and more -- along with commentary from the developers themselves.",
                ImageURL = "https://m.media-amazon.com/images/I/61L+OwTjNeL._SY522_.jpg",
                CategoryName = "Book"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 10,
                Name = "Red Dead Redemption 2 (PS4)",
                Price = new decimal(59.99),
                Description = "<li>From the creators of Grand Theft Auto V and Red Dead Redemption</li>\r\n<li><li>The deepest and most complex world Rockstar Games has ever created</li>\r\n<li>Covers a huge range of 19th century American landscapes</li>\r\n<li>Play as Arthur Morgan, lead enforcer in the notorious Van der Linde gang</li>\r\n<li>Interact with every character in the world with more than just your gun;Engage in conversation with people you meet. Your actions influence Arthur's honour;Horses are a cowboy's best friend, both transport and personal companion;Bond with your horse as you ride together to unlock new abilities</li>",
                ImageURL = "https://m.media-amazon.com/images/I/71VlXi52jBL._SX522_.jpg",
                CategoryName = "Game"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 11,
                Name = "BENGOO Stereo Pro Gaming Headset",
                Price = new decimal(31.99),
                Description = "BENGOO Stereo Pro Gaming Headset for PS4, PC, Xbox One Controller, Noise Cancelling Over Ear Headphones with Mic, LED Light, Bass Surround, Soft Memory Earmuffs for Laptop Mac Wii Accessory Kits",
                ImageURL = "https://m.media-amazon.com/images/I/71uA3QaBCyL._AC_SX679_.jpg",
                CategoryName = "Headset"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 12,
                Name = "Z-Edge 32-inch Curved Gaming Monitor",
                Price = new decimal(269.99),
                Description = "Z-Edge 32-inch Curved Gaming Monitor 16:9 1920x1080 240Hz 1ms Frameless LED Gaming Monitor, UG32P AMD Freesync Premium Display Port HDMI Built-in Speakers",
                ImageURL = "https://m.media-amazon.com/images/I/71fcV1bMz6L._AC_SX679_.jpg",
                CategoryName = "TV/Monitor"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 13,
                Name = "Dragon Ball Super Goku Character T-Shirt",
                Price = new decimal(59.99),
                Description = "Go super saiyan with this Dragon Ball Super tee. The shirt features a hovering Goku. A set of colored panels containing the characters of Vegeta, Piccolo, Trunks, Goten, and Krillen appear next to Goku. The tee comes in a black short sleeve crew neck. Show your support for the legendary saiyans with this graphic tee today.",
                ImageURL = "https://github.com/leandrocgsi/erudio-microservices-dotnet6/blob/main/ShoppingImages/13_dragon_ball.jpg",
                CategoryName = "Clothing"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 14,
                Name = "Riders Republic | Complete - PC [Online Game Code]",
                Price = new decimal(99.99),
                Description = "With the Complete Edition, face no restrictions and shred like there is no tomorrow! It includes the base game, the Year 1 Pass (BMX add-on, eight toys, winter cosmetic pack, and BMX cosmetic pack) and the Skate Plus Pack (Skate add-on, hoverboard, skate and hoverboard cosmetics, and Ridge Ultimate Pack).",
                ImageURL = "https://m.media-amazon.com/images/I/81Jn7fWFj4L._SY679_.jpg",
                CategoryName = "Game"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 15,
                Name = "Star Wars The Black Series Boba Fett Premium Electronic Helmet",
                Price = new decimal(239.99),
                Description = "Once regarded as one of the most fearsome and capable bounty hunters in the galaxy, Boba Fett seemingly met his demise in the Sarlacc pit on Tatooine. Fett has survived the beast and has now reclaimed his distinctive Mandalorian armor.\r\n<br/>\r\n<br/>\r\nCommemorate the live-action return of fan-favorite character Boba Fett with The Black Series Boba Fett (Re-Armored) Premium Electronic Helmet, inspired by the streaming series The Mandalorian on Disney+! This roleplay item with premium deco, realistic detail, and series-inspired design is a great addition to any Star Wars fan’s collection. Featuring a flip-down rangefinder with flashing LED lights and an illuminated heads-up display (HUD), fans can imagine what it was like for the famous bounty hunter to reclaim his iconic armor and suit up for galactic action alongside The Mandalorian!",
                ImageURL = "https://m.media-amazon.com/images/I/61fIP-1a+lL._AC_SY879_.jpg",
                CategoryName = "Replicas"
            });
        }
    }
}
