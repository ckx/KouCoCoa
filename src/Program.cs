using System.Numerics;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace KouCoCoa {
    class Program {
        public static string ProgramTitle { get { return "KouCoCoa"; } }

        private static Sdl2Window _window;
        private static GraphicsDevice _gd;
        private static CommandList _cl;
        private static ImGuiController _controller;

        // UI state
        private static Vector3 _clearColor = new Vector3(0.45f, 0.55f, 0.6f);

        static async Task Main(string[] args) {
            // Setup config
            Globals.RunConfig = await ConfigManager.GetConfig();

            // Create window, GraphicsDevice, and resources.
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(Globals.RunConfig.WindowXY[0], Globals.RunConfig.WindowXY[1], 
                Globals.RunConfig.ResolutionXY[0], Globals.RunConfig.ResolutionXY[1], 
                WindowState.Normal, ProgramTitle),
                new GraphicsDeviceOptions(true, null, true, ResourceBindingModel.Improved, true, true),
                out _window,
                out _gd);
            _window.Resized += () => {
                _gd.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
                _controller.WindowResized(_window.Width, _window.Height);
            };
            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);

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
            Globals.RunConfig.WindowXY = new int[] { _window.Bounds.X, _window.Bounds.Y };
            Globals.RunConfig.ResolutionXY = new int[] { _window.Width, _window.Height };

            ConfigManager.StoreConfig(Globals.RunConfig);

            // Clean up Veldrid resources
            _gd.WaitForIdle();
            _controller.Dispose();
            _cl.Dispose();
            _gd.Dispose();
        }
    }
}
