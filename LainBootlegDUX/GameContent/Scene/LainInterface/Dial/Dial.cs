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
        public LainDial lainDial { get; set; }

        public Vector2Int dialMiniImageSize { get; private set; } = new Vector2Int(200, 128);


        Texture2D texture;

        LainDial.DialMode dialMode;

        public Dial(string entityName, GameScene scene) : base(entityName, scene)
        {
        }

        public override void OnInitialize()
        {
            lainDial.dialModeChange += OnDialModeChange;
        }

        private void OnDialModeChange(object sender, LainDial.DialMode dialModeState)
        {
            dialMode = dialModeState;
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
            switch (dialMode)
            {
                case LainDial.DialMode.Mini:
                    DrawMiniDial();
                    break;
            }
        }

        private void DrawMiniDial()
        {
            Rectangle rectangle = graphicDevice.PresentationParameters.Bounds;
            int scaledXOffset = (int)MathU.MapClampRanged(rectangle.Width, 0, lainDial.dialMiniWindowSize.x, 0, lainDial.dialMiniWindowSize.x);
            int scaledYOffset = (int)MathU.MapClampRanged(rectangle.Height, 0, lainDial.dialMiniWindowSize.y, 0, lainDial.dialMiniWindowSize.y);
            rectangle.Location = new Point(scaledXOffset, scaledYOffset);

            Vector2Int dialSize = dialMiniImageSize;

            Rectangle dialRect = new Rectangle(-lainDial.dialMiniImageOffset.x, -lainDial.dialMiniImageOffset.y, dialSize.x, dialSize.y);

            spriteBth.Draw(texture, dialRect, Color.White);
        }
    }
}