using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.IO.Compression;

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

            Dictionary<string, Image> images = LoadImages();

            ApplicationConfiguration.Initialize();            

            // Run the winforms logic on an STAThread
            Thread uiThread = new(() => Application.Run(new MainContainer(startupDatabases, images))); 
            uiThread.SetApartmentState(ApartmentState.STA); 
            uiThread.Start();
        }

        #region Private Methods
        private static Dictionary<string, Image> LoadImages()
        {
            Dictionary<string, Image> retDict = new();
#if !DEBUG
            using (ZipArchive zip = ZipFile.Open("data/spritedata.zip", ZipArchiveMode.Read)) {
                foreach (ZipArchiveEntry entry in zip.Entries) {
                    Stream stream = entry.Open();
                    Image img = Image.FromStream(stream);
                    retDict.Add(entry.FullName, img);
                }
            }
#endif
            return retDict;
        }
        #endregion
    }
}
