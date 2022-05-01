using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace LainBootlegDUX.GameContent
{
    public static class LainSFXManager
    {
        private const string bootlegSFXPath = "Asset/lainSFX/bootlegSFX/";
        private const string upscaledbootlegSFXPath = "Asset/lainSFX/upscaledBootlegSFX/";

        public static LainSFXMode lainSFXMode { get; private set; }
        public static bool upscaledMode => lainSFXMode == LainSFXMode.UpscaledBootleg;

        public static event EventHandler<LainSFXMode> sfxModeChange;

        public static void SetSFXMode(LainSFXMode mode)
        {
            lainSFXMode = mode;

            DLog.Log("LainSFXManager || Lain SFX Mode Set To : " + mode.ToString());

            if (sfxModeChange != null)
                sfxModeChange.Invoke(null, mode);
        }

        public static void ToggleTextureMode()
        {
            if (upscaledMode)
                SetSFXMode(LainSFXMode.Bootleg);
            else
                SetSFXMode(LainSFXMode.UpscaledBootleg);
        }

        public static SoundEffect GetLainSFX(int id)
        {
            string sfxPath = upscaledMode ? upscaledbootlegSFXPath : bootlegSFXPath;
            return LoadUtility.LoadAudio($"{sfxPath}{id}.wav");
        }

        public enum LainSFXMode
        {
            Bootleg,
            UpscaledBootleg
        }
    }
}
