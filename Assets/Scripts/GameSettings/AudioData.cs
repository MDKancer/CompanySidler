using System;

namespace GameSettings
{
    [Serializable]
    public class AudioData
    {
        public bool stereo = true;
        public float music = 1f;
        public float soundEffects = 1f;
        public float ambientSounds = 1f;
    }
}