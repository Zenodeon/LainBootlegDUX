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
            texture = GraphicsDevice.LoadTexture2D("Asset/lainSprite/upscaledBootlegSprites/460.png");
            SetAspectRatioSize(new Vector2(200, 128));
        }

        public override void OnUpdate(GameTime gameTime)
        {
           
        }

        public override void OnDraw(GameTime gameTime)
        {
            Rectangle rectangle = GraphicsDevice.PresentationParameters.Bounds;
            rectangle.Location = new Point(0, 0);   
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
