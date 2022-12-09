using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KouCoCoa {
    class Program {
        #region Properties
        public static string ProgramName { get { return "KouCoCoa"; } }
        #endregion

        static async Task Main(string[] args) {
            await Logger.CreateLogFile();
            Globals.RunConfig = await ConfigIO.GetConfig();
#if DEBUG
            Globals.RunConfig.LoggingLevel = LogLevel.Debug;
#endif
            Dictionary<RAthenaDbType, List<IDatabase>> startupDatabases = 
                await DatabaseLoader.LoadDatabasesFromConfig(Globals.RunConfig);

            ApplicationConfiguration.Initialize();

            // Run the winforms logic on an STAThread
            Thread uiThread = new(() => Application.Run(new MainContainer(startupDatabases))); 
            uiThread.SetApartmentState(ApartmentState.STA); 
            uiThread.Start();
        }

        #region Private Methods
        private static string GetVersionTagline() {
            List<string> taglines = new() {
                "Girls need Tao cards, too!",
                "Onii-chan, look! Another Iron Cain!",
                "The way to a girl's heart is Grilled Peco!",
                "Slow Poison !!",
                "heal plz",
                "zeny plz",
                "pa baps po",
                "Son of Great Bitch !!",
                "THEN WHO WAS STRINGS!?",
                "Remember, all you need for level 99 is a Cotton Shirts!",
                "If only ASSASSIN OF THE DARK could save us!!",
                "Don't forget to remove all your ekipz!!",
                "Don't you have anything better to do? Go outside.",
                "Mou, onii-chan!! That's not where you stick a Tao card!!",
                "Ohh, time to wang some DBs?",
                "ONE... HUNDRED... ICE PICKS!?!?!?",
                "Where did I leave my Megingjard...",
                "WE ARE THE STAAAAAARS",
                "Boys are... Arrow Shower.",
                "Oh, my Drooping Cat? Yeah, it has more INT than you..."
            };

            Random rand = new();
            int index = rand.Next(taglines.Count);
            return taglines[index];
        }
        #endregion
    }
}
