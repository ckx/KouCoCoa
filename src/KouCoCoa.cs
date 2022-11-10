using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace KouCoCoa {
    class KouCoCoa {
        #region Properties
        public static string ProgramTitle { get { return "KouCoCoa"; } }
        #endregion

        #region Private member variables
        private static Sdl2Window _window;
        private static GraphicsDevice _gd;
        private static CommandList _cl;
        private static ImGuiController _controller;
        // UI state
        private static Vector3 _clearColor = new Vector3(0.45f, 0.55f, 0.6f);
        #endregion

        static async Task Main(string[] args) {
            Logger.CreateLogFile();
            Globals.RunConfig = await ConfigManager.GetConfig();
            await SetupVeldridWindow();

            Dictionary<RAthenaDbType, List<IDatabase>> _allDatabases = await DatabaseLoader.LoadConfigDatabases();

            // Main application loop
            while (_window.Exists) {
                InputSnapshot snapshot = _window.PumpEvents();
                if (!_window.Exists) { break; }
                _controller.Update(1f / 60f, snapshot); // Feed the input events to our ImGui controller, which passes them through to ImGui.

                /* -------- Imgui UI below this line ----- */
                Sample.SubmitUI();
                /* -------- Imgui UI above this line ----- */

                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(_clearColor.X, _clearColor.Y, _clearColor.Z, 1f));
                _controller.Render(_gd, _cl);
                _cl.End();
                _gd.SubmitCommands(_cl);
                _gd.SwapBuffers(_gd.MainSwapchain);
            }

            await ShutDown();
        }

        #region Private Methods
        private static async Task SetupVeldridWindow() {
            await Logger.WriteLine($"Creating Veldrid graphics " +
                $"window at screen position {Globals.RunConfig.WindowPositionXY[0]},{Globals.RunConfig.WindowPositionXY[1]} " +
                $"with resolution {Globals.RunConfig.WindowResolutionXY[0]}x{Globals.RunConfig.WindowResolutionXY[1]}.");
            if (Globals.RunConfig.WindowPositionXY[0] > 1920) {
                await Logger.WriteLine($"すごーい！お兄ちゃんデカすぎる…");
            }
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(Globals.RunConfig.WindowPositionXY[0], Globals.RunConfig.WindowPositionXY[1],
                Globals.RunConfig.WindowResolutionXY[0], Globals.RunConfig.WindowResolutionXY[1],
                WindowState.Normal, ProgramTitle + " ~ " + GetVersionTagline()),
                new GraphicsDeviceOptions(true, null, true, ResourceBindingModel.Improved, true, true),
                out _window,
                out _gd);
            _window.Resized += () => {
                _gd.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
                _controller.WindowResized(_window.Width, _window.Height);
            };
            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);

        }

        private static async Task ShutDown() {
            await Logger.WriteLine("Shutdown signal received.");

            Globals.RunConfig.WindowPositionXY = new int[] { _window.Bounds.X, _window.Bounds.Y };
            Globals.RunConfig.WindowResolutionXY = new int[] { _window.Width, _window.Height };

            await ConfigManager.StoreConfig(Globals.RunConfig);
            await Logger.WriteLine("[KouKou] え？お兄ちゃん、待ってー！もっと遊びたーい！");
            // Clean up Veldrid resources
            _gd.WaitForIdle();
            _controller.Dispose();
            _cl.Dispose();
            _gd.Dispose();
        }

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
