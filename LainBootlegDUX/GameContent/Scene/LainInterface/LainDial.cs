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
        public Vector2Int dialMiniWindowSize { get; private set; } = new Vector2Int(164, 92);
        public Vector2Int dialExtentionWindowSize { get; private set; } = new Vector2Int(200, 200);
        public Vector2Int dialMiniImageOffset { get; private set; } = new Vector2Int(36, 36);

        public DialMode currentMode { get; private set; } = DialMode.None;
        private DialMode targetMode;
        private Vector2 lastSize;
        private Vector2 targetSize;
        private float transitionSpeed = 10;
        private float transitionState = 0;
        private bool updateTransition = false;

        Dial dial;

        public LainDial(string entityName, GameScene scene) : base(entityName, scene)
        {
            dial = AddSubEntity<Dial>(new Dial("Dial", scene));
            dial.lainDial = this;
        }

        public override void OnInitialize()
        {
            parent.fixedAspectRatio = true;

            UpdateWindowSize(dialMiniWindowSize);
            currentMode = DialMode.Mini;
        }

        public override void OnLoadContent()
        {

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
                    if (!updateTransition)
                    {
                        DialMode dialMode = currentMode == DialMode.Extented ? DialMode.Mini : DialMode.Extented;
                        SwitchMode(dialMode);
                    }
                }

            UpdateWindowModeTransition(gt);
        }

        private void SwitchMode(DialMode mode, bool instant = false)
        {
            if (currentMode == mode)
                return;

            targetMode = mode;
            targetSize = targetMode == DialMode.Mini? dialMiniWindowSize : dialExtentionWindowSize;

            lastSize = parent.Window.GetWindowSize();

            Vector2 relativeScale = GetRelativeScale();
            targetSize *= relativeScale;

            if (!instant)
                updateTransition = true;
            else
            {
                currentMode = targetMode;
                UpdateWindowSize(targetSize);
            }
        }

        private Vector2 GetRelativeScale()
        {
            Vector2 defaultModeSize = currentMode == DialMode.Mini ? dialMiniWindowSize : dialExtentionWindowSize;

            float relativeX = MathU.MapClampRanged(lastSize.x, 0, defaultModeSize.x, 0, 1);
            float relativeY = MathU.MapClampRanged(lastSize.y, 0, defaultModeSize.y, 0, 1);

            return new Vector2(relativeX, relativeY);
        }

        private void UpdateWindowModeTransition(GameTime gt)
        {
            if (!updateTransition)
                return;

            parent.Window.AllowUserResizing = false;

            transitionState += gt.deltaTime * transitionSpeed;
            transitionState = Math.Clamp(transitionState, 0, 1);
            Vector2 newSize = Vector2.Lerp(lastSize, targetSize, transitionState);

            UpdateWindowSize(newSize);

            if (transitionState == 1)
            {
                updateTransition = false;

                currentMode = targetMode;
                transitionState = 0;

                parent.Window.AllowUserResizing = true;
            }
        }

        private void UpdateWindowSize(Vector2Int relativeSize)
        {
            parent.Window.SetWindowSize(relativeSize);
            parent.SetAspectRatioSize(relativeSize);
        }

        public enum DialMode
        {
            None,
            Mini,
            Extented
        }
    }
}
