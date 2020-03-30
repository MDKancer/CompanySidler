using System;
using GameSettings;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIDispatcher.GameComponents
{
    [Serializable,GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    public struct UIAudioComponents
    {
        /// <summary>
        /// Global Sound Volume
        /// </summary>
        [SerializeField]
        private Slider sliderVolume;
        /// <summary>
        /// Audio Output Channel
        /// </summary>
        [SerializeField]
        private TMP_Dropdown channelDropdown;
        /// <summary>
        /// Sound Effects Volume
        /// </summary>
        [SerializeField]
        private Slider sfxVolume;
        /// <summary>
        /// Ambient Sounds Volume
        /// </summary>
        [SerializeField]
        private Slider ambientVolume;

        public void SetData(AudioData data)
        {
            sliderVolume.value = data.volume;
            SetChannels();
            channelDropdown.value = ((int) data.stereo)-1;
            sfxVolume.value = data.soundEffects;
            ambientVolume.value = data.ambientSounds;
        }

        private void SetChannels()
        {
            var values = Enum.GetValues(typeof(AudioSpeakerMode));
            for (int i = 1; i < values.Length; i++)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                optionData.text = values.GetValue(i).ToString();
                channelDropdown.options.Add(optionData);
            }
        }
    }
}