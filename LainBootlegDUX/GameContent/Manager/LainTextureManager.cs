using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace LainBootlegDUX.GameContent
{
    public static class LainTextureManager
    {
        private const string bootlegTexturePath = "Asset/lainTextures/bootlegTextures/";
        private const string upscaledbootlegTexturePath = "Asset/lainTextures/upscaledBootlegTextures/";

        public static LainTextureMode lainTextureMode { get; private set; }
        public static bool upscaledMode => lainTextureMode == LainTextureMode.UpscaledBootleg;
        public static int scaleFactor => upscaledMode ? 4 : 1;

        private static GraphicsDevice currentGraphicsDevice;

        public static event EventHandler<LainTextureMode> textureModeChange;

        public static void SetTextureMode(LainTextureMode mode)
        {
            lainTextureMode = mode;

            DLog.Log("LainTextureManager || Lain Texture Mode Set To : " + mode.ToString());

            if (textureModeChange != null)
                textureModeChange.Invoke(null, mode);
        }

        public static void ToggleTextureMode()
        {
            if (upscaledMode)
                SetTextureMode(LainTextureMode.Bootleg);
            else
                SetTextureMode(LainTextureMode.UpscaledBootleg);
        }

        public static void StartGraphicsDevice(GraphicsDevice graphicsDevice)
            => currentGraphicsDevice = graphicsDevice;

        public static void EndGraphicsDevice()
            => currentGraphicsDevice = null;

        public static Texture2D GetLainTexture(int id)
        {
            if (currentGraphicsDevice == null)
                return null;

            string texturePath = upscaledMode? upscaledbootlegTexturePath : bootlegTexturePath;
            return currentGraphicsDevice.LoadTexture2D($"{texturePath}{id}.png");
        }

        public enum LainTextureMode
        {
            Bootleg,
            UpscaledBootleg
        }
    }
}
