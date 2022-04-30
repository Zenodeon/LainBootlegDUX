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
        public Vector2Int dialExtentedImageSize { get; private set; } = new Vector2Int(200, 200);

        List<Texture2D> dialTextures = new List<Texture2D>();
        float dialTextureInterval = 0;

        LainDial.DialMode dialMode;

        float dialTransitionState = 0;
        float transitionExtendingStartOffset = 0f;
        float transitionMiniingStartOffset = 0;
        float transitionSpeed = 5;


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
            LoadDialTextures();
            
        }

        public override void OnUpdate(GameTime gt)
        {
            UpdateDialTransitionState(gt);
        }

        public override void OnDraw(GameTime gt)
        {
            DrawDial();
        }

        private void UpdateDialTransitionState(GameTime gt)
        {
            float intervalStep = gt.deltaTime * transitionSpeed;

            switch (dialMode)
            {
                case LainDial.DialMode.Mini:
                case LainDial.DialMode.Miniing:

                    if (lainDial.transitionState >= transitionMiniingStartOffset)
                        dialTransitionState -= intervalStep;

                    break;

                case LainDial.DialMode.Extented:
                case LainDial.DialMode.Extenting:

                    if (lainDial.transitionState >= transitionExtendingStartOffset)
                        dialTransitionState += intervalStep;

                    break;
            }

            dialTransitionState = MathHelper.Clamp(dialTransitionState, 0, 1);

            DLog.Log("Dial Transition State : " + dialTransitionState);
        }

        private void DrawDial()
        {
            Texture2D dialTexture = GetStateTexture(dialTransitionState);

            Rectangle rect = graphicDevice.PresentationParameters.Bounds;

            Vector2 windowRelativeSize = MathU.MapClampRanged(dialTransitionState, 0, 1, lainDial.dialMiniWindowSize, lainDial.dialExtentionWindowSize);
            Vector2 scale = MathU.MapClampRanged(new Vector2(rect.Width, rect.Height), Vector2.Zero, windowRelativeSize, Vector2.Zero, Vector2.One);
            Vector2 imageOffset = MathU.MapClampRanged(dialTransitionState, 0, 1, lainDial.dialMiniImageOffset, Vector2.Zero);

            Vector2 imageRelativeSize = new Vector2(dialTexture.Width, dialTexture.Height);
            //Vector2 scaleVector = new Vector2(scale.x, scale.y);
            Vector2 scaleVector = new Vector2(1, 1);

            Vector2Int dialOffset = imageOffset * -scaleVector;
            Vector2Int dialSize = imageRelativeSize * scaleVector;

            Rectangle dialRect = new Rectangle(dialOffset.x, dialOffset.y, dialSize.x, dialSize.y);

            spriteBth.Draw(dialTexture, dialRect, Color.White);
        }

        private Texture2D GetStateTexture(float state)
        {
            int textureIndex = (state * (dialTextures.Count - 1)).RoundOff();
            return dialTextures[textureIndex];
        }

        private void LoadDialTextures()
        {
            AddDialTexture(460);

            AddDialTexture(461);
            AddDialTexture(462);
            AddDialTexture(463);
            AddDialTexture(464);
            AddDialTexture(465);

            AddDialTexture(485);

            dialTextureInterval = 1f / dialTextures.Count;
        }

        private void AddDialTexture(int imageID)
        {
            dialTextures.Add(graphicDevice.LoadTexture2D($"Asset/lainSprite/bootlegSprites/{imageID}.png"));
        }
    }
}