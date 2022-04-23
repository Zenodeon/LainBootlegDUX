using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LainBootlegDUX.GameContent
{
    public class Dial : GameEntity
    {
        public LainDial lainDial;

        Texture2D texture;

        public Dial(string entityName, GameScene scene) : base(entityName, scene)
        {
        }

        public override void OnInitialize()
        {

        }

        public override void OnLoadContent()
        {
            texture = graphicDevice.LoadTexture2D("Asset/lainSprite/upscaledBootlegSprites/460.png");
        }

        public override void OnUpdate(GameTime gameTime)
        {

        }

        public override void OnDraw(GameTime gameTime)
        {
            Rectangle rectangle = graphicDevice.PresentationParameters.Bounds;
            //int scaledXOffset = (int)MathU.MapClampRanged(rectangle.Width, 0, lainDial.dialMiniWindowSize.x, 0, lainDial.dialMiniWindowSize.x);
            //int scaledYOffset = (int)MathU.MapClampRanged(rectangle.Height, 0, lainDial.dialMiniWindowSize.y, 0, lainDial.dialMiniWindowSize.y);
            //rectangle.Location = new Point(scaledXOffset, scaledYOffset);
            spriteBth.Draw(texture, rectangle, Color.White);
        }
    }
}
