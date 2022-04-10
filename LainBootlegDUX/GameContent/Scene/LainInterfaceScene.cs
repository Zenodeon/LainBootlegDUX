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
            
        }

        public override void OnLoadContent()
        {
            texture = GraphicsDevice.LoadTexture2D("Asset/lainSprite/upscaledBootlegSprites/460.png");
        }

        public override void OnUpdate(GameTime gameTime)
        {
           
        }

        public override void OnDraw(GameTime gameTime)
        {
            Rectangle rectangle = graphics.GraphicsDevice.PresentationParameters.Bounds;
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
