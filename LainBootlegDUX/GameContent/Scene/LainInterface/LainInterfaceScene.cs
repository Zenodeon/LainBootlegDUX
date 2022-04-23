using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LainBootlegDUX.GameContent
{
    public class LainInterfaceScene : GameScene
    {
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

        }
    }
}
