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
        private Vector2Int dialMinOffset = new Vector2Int(36, 36);

        private DialMode currentMode = DialMode.Extented;
        private DialMode targetMode;
        private Vector2 currentSize;
        private Vector2 targetSize;
        private float transitionSpeed = 1;
        private float transitionState = 0;
        private bool updateTransition = false;

        public LainDial(GameScene scene) : base(scene)
        {
        }

        public override void OnInitialize()
        {
            parent.fixedAspectRatio = true;

            SwitchMode(DialMode.Mini, instant: true);
        }

        public override void OnLoadContent()
        {
            texture = graphicDevice.LoadTexture2D("Asset/lainSprite/upscaledBootlegSprites/460.png");


        }

        public override void OnUpdate(GameTime gameTime)
        {

        }

        bool release = true;
        public override void OnDraw(GameTime gt)
        {
            if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.E))
                release = true;

            if (release)
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.E))
                {
                    release = false;
                    DialMode dialMode = currentMode == DialMode.Extented ? DialMode.Mini : DialMode.Extented;
                    SwitchMode(dialMode, true);
                }

            UpdateModeTransition(gt);

            Rectangle rectangle = graphicDevice.PresentationParameters.Bounds;
            int scaledXOffset = (int)MathU.MapClampRanged(rectangle.Width, 0, dialDefaultWindowSize.x, 0, dialMinOffset.x);
            int scaledYOffset = (int)MathU.MapClampRanged(rectangle.Height, 0, dialDefaultWindowSize.y, 0, dialMinOffset.y);
            rectangle.Location = new Point(scaledXOffset, scaledYOffset);
            spriteBth.Draw(texture, rectangle, Color.White);
        }

        private void SwitchMode(DialMode mode, bool instant = false)
        {
            if (currentMode == mode)
                return;

            targetMode = mode;
            targetSize = targetMode == DialMode.Mini? dialDefaultWindowSize - dialMinOffset : dialDefaultWindowSize;

            if (!instant)
                updateTransition = true;
            else
            {
                currentMode = targetMode;
                currentSize = targetSize;
                UpdateWindowSize(currentSize);
            }

            //DLog.Log(targetSize + "");

            //switch (mode)
            //{
            //    case DialMode.Mini:
            //        break;

            //    case DialMode.Extented:
            //        break;
            //}
        }

        private void UpdateModeTransition(GameTime gt)
        {
            if (!updateTransition)
                return;

            parent.Window.AllowUserResizing = false;

            transitionState += (float)gt.ElapsedGameTime.TotalSeconds * transitionSpeed;
            currentSize = Vector2.Lerp(currentSize, targetSize, transitionState);

            DLog.Log(currentSize + "");

            //UpdateWindowSize(currentSize);
        }

        private void UpdateWindowSize(Vector2Int newSize)
        {
            parent.Window.SetWindowSize(newSize);
            parent.SetAspectRatioSize(newSize);
        }

        public enum DialMode
        {
            Mini,
            Extented
        }
    }
}
