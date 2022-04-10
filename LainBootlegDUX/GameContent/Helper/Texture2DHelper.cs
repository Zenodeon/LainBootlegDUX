using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Lain_Bootleg_DUX.GameContent
{
    public static class Texture2DHelper
    {
        public static Texture2D LoadTexture2D(this GraphicsDevice graphicsDevice, string assetPath)
        {
            FileStream fileStream = new FileStream(assetPath, FileMode.Open, FileAccess.Read);
            return Texture2D.FromStream(graphicsDevice, fileStream);
        }
    }
}
