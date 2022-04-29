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
        public Vector2Int dialMiniWindowSize { get; private set; } = new Vector2Int(164, 74); //92
        public Vector2Int dialExtentionWindowSize { get; private set; } = new Vector2Int(200, 200);
        public Vector2Int dialMiniImageOffset { get; private set; } = new Vector2Int(36, 36);

        //Bigger Version
        //public Vector2Int dialMiniWindowSize { get; private set; } = new Vector2Int(246, 138);
        //public Vector2Int dialExtentionWindowSize { get; private set; } = new Vector2Int(300, 300);
        //public Vector2Int dialMiniImageOffset { get; private set; } = new Vector2Int(54, 54);

        public DialMode currentMode { get; private set; } = DialMode.None;
        private DialMode targetMode;
        private Vector2 lastSize;
        private Vector2 targetSize;
        private float transitionSpeed = 5f;
        private float transitionState = 0;
        private bool updateTransition = false;
        public float modeState { get; private set; } = 0;

        Dial dial;

        public event EventHandler<DialMode> dialModeChange;

        public LainDial(string entityName, GameScene scene) : base(entityName, scene)
        {
            dial = AddSubEntity<Dial>(new Dial("Dial", scene));
            dial.lainDial = this;
        }

        public override void OnInitialize()
        {
            parent.Window.AllowUserResizing = true;
            parent.fixedAspectRatio = true;
        }

        public override void OnLoadContent()
        {
            UpdateWindowSize(dialMiniWindowSize);
            currentMode = DialMode.Mini;
            dialModeChange.Invoke(this, DialMode.Mini);
        }

        public override void OnUpdate(GameTime gt)
        {
            UpdateWindowModeTransition(gt);
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
            {
                parent.Window.AllowUserResizing = false;

                updateTransition = true;

                DialMode state = targetMode == DialMode.Mini? DialMode.Miniing : DialMode.Extenting;
                dialModeChange.Invoke(this, state);
            }
            else
            {
                currentMode = targetMode;
                UpdateWindowSize(targetSize);

                dialModeChange.Invoke(this, targetMode);
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

            transitionState += gt.deltaTime * transitionSpeed;
            transitionState = Math.Clamp(transitionState, 0, 1);
            Vector2 newSize = Vector2.Lerp(lastSize, targetSize, transitionState);

            UpdateWindowSize(newSize);

            if (targetMode == DialMode.Mini)
                modeState = MathU.MapClampRanged(transitionState, 0, 1, 1, 0);
            else
                modeState = transitionState;

            if (transitionState == 1)
            {
                updateTransition = false;

                currentMode = targetMode;
                transitionState = 0;

                parent.Window.AllowUserResizing = true;

                dialModeChange.Invoke(this, targetMode);
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
            Extented,
            Miniing,
            Extenting
        }
    }
}
