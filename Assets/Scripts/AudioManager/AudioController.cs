using Container;
using GameSettings;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace AudioManager
{
    /// <summary>
    /// Tutorial AudioMixer in Runtime
    /// 1. Click on Master
    /// 2. right click on Attenuation in the Inspector, and click Expose (Volume to C#) script
    /// 3. In the Audio Mixer UI-Panel,  right side , click on Exposed Parameters
    /// 4. Rename the Exposed parameter.
    /// 5. In Script change the exposed parameter <example>audiomixer.SetFloat("exposed parameter name", Mathf.Log10(value)*20)</example>
    /// 6. <remarks>value is between {0...1} </remarks>
    /// </summary>
    public class AudioController
    {
        private SignalBus signalBus;
        private Cloud cloud;
        private AudioData audioData;
        private AudioMixer audioMixer;
        [Inject]
        private void Init(SignalBus signalBus,Cloud cloud , AudioMixer audioMixer)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.audioMixer = audioMixer;
        }

        public AudioData AudioData => audioData;

        /// <summary>
        /// Get the sourcedata from the cloud, and set the audio mixer up.
        /// </summary>
        public void SetImportData()
        {
            this.audioData = cloud.AudiosData;
            SetVolume(audioData.volume);
            SetSoundEffects(audioData.soundEffects);
            SetAmbient(audioData.ambientSounds);
            SetSpeakerMode(audioData.stereo);
        }
        /// <summary>
        /// Set the master volume in the AudioMixer
        /// </summary>
        /// <param name="volume"> value need to be between 0..1</param>
        public void SetVolume(float volume)
        {
            audioData.volume = volume;
            audioMixer.SetFloat("masterVol", Mathf.Log10(volume) * 20);
        }
        /// <summary>
        /// Set the sound effect volume in the AudioMixer
        /// </summary>
        /// <param name="volume"> value need to be between 0..1</param>
        public void SetSoundEffects(float volume)
        {
            audioData.soundEffects = volume;
            audioMixer.SetFloat("sfxVol", Mathf.Log10(volume) * 20);
        }
        /// <summary>
        /// Set the ambient volume in the AudioMixer
        /// </summary>
        /// <param name="volume"> value need to be between 0..1</param>
        public void SetAmbient(float volume)
        {
            audioData.ambientSounds = volume;
            audioMixer.SetFloat("ambientVol", Mathf.Log10(volume) * 20);
        }

        /// <summary>
        /// Set the channel count of the speakers.
        /// </summary>
        /// <remarks>AudioSpeakerMode.Raw (Obsolete)</remarks>
        public void SetSpeakerMode(AudioSpeakerMode audioSpeakerMode)
        {
            audioData.stereo = audioSpeakerMode;
            UnityEngine.AudioSettings.speakerMode = AudioSpeakerMode.Stereo;
        }

        /// <summary>
        /// The settings data will be resetting.
        /// </summary>
        public void Reset()
        {
            cloud.SettingsDataReset();
        }
    }
}