using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace LainBootlegDUX.GameContent
{
    public static class LoadUtility
    {
        public static Texture2D LoadTexture2D(this GraphicsDevice graphicsDevice, string assetPath)
        {
            FileStream fileStream = new FileStream(assetPath, FileMode.Open, FileAccess.Read);
            return Texture2D.FromStream(graphicsDevice, fileStream);
        }

        public static SoundEffect LoadAudio(string assetPath)
        {
            FileStream fileStream = new FileStream(assetPath, FileMode.Open, FileAccess.Read);
            return SoundEffect.FromStream(fileStream);
        }
    }
}
