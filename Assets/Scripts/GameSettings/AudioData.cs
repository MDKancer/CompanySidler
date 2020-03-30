using System;
using UnityEngine;

namespace GameSettings
{
    [Serializable]
    public class AudioData
    {
        public AudioSpeakerMode stereo = AudioSpeakerMode.Stereo;
        public float volume = 1f;
        public float soundEffects = 1f;
        public float ambientSounds = 1f;
    }
}