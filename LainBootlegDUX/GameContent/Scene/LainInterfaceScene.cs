using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lain_Bootleg_DUX.GameContent
{
    public class LainInterfaceScene : GameScene
    {
        Texture2D texture;

        public LainInterfaceScene()
        {

        }

        public override void OnInitialize()
        {
            fixedAspectRatio = true;
        }

        public override void OnLoadContent()
        {
            texture = GraphicsDevice.LoadTexture2D("Asset/lainSprite/upscaledBootlegSprites/196.png");
            SetAspectRatioSize(new Vector2(500, 300));
        }

        public override void OnUpdate(GameTime gameTime)
        {
           
        }

        public override void OnDraw(GameTime gameTime)
        {
            Rectangle rectangle = graphics.GraphicsDevice.PresentationParameters.Bounds;
            rectangle.Location = new Point(10, 0);   
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
