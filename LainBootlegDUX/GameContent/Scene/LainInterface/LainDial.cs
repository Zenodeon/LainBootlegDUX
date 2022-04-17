using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LainBootlegDUX.GameContent
{
    public class LainDial : GameEntity
    {
        Texture2D texture;

        private Vector2Int dialDefaultWindowSize = new Vector2Int(200, 128);
        private Vector2Int dialMinOffset = new Vector2Int(-36, -36);

        public LainDial(GameScene scene) : base(scene)
        {
        }

        public override void OnInitialize()
        {
            parent.fixedAspectRatio = false;
        }

        public override void OnLoadContent()
        {
            texture = graphicDevice.LoadTexture2D("Asset/lainSprite/upscaledBootlegSprites/460.png");

            parent.Window.SetWindowSize(dialDefaultWindowSize);
            parent.SetAspectRatioSize(dialDefaultWindowSize);
        }

        public override void OnUpdate(GameTime gameTime)
        {

        }

        public override void OnDraw(GameTime gameTime)
        {
            Rectangle rectangle = graphicDevice.PresentationParameters.Bounds;
            int scaledXOffset = (int) MathU.MapClampRanged(rectangle.Width, 0, dialDefaultWindowSize.x, 0, dialMinOffset.x);
            int scaledYOffset = (int) MathU.MapClampRanged(rectangle.Height, 0, dialDefaultWindowSize.y, 0, dialMinOffset.y);
            rectangle.Location = new Point(scaledXOffset, scaledYOffset);
            spriteBth.Draw(texture, rectangle, Color.White);
        }
    }
}
