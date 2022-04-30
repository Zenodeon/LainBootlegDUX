using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LainBootlegDUX.GameContent
{
    public class LainInterfaceScene : GameScene
    {
        private bool release;

        public LainInterfaceScene()
        {
            AddEntity(new LainDial("LainDial", this));
        }

        public override void OnInitialize()
        {

        }

        public override void OnLoadContent()
        {

        }

        public override void OnUpdate(GameTime gameTime)
        {
           
        }

        public override void OnDraw(GameTime gameTime)
        {
            if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.H))
                release = true;

            if (release)
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.H))
                {
                    release = false;
                    LainTextureManager.ToggleTextureMode();
                }
        }
    }
}
