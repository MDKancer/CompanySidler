using System;
using Enums;
using GameSettings;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIDispatcher.GameComponents
{
    [Serializable,GUIColor(1f,1f,0f,1f)]
    public struct UIVideoComponents
    {
        /// <summary>
        /// Display Mode
        /// </summary>
        public Toggle displayModeToggle;

        public TMP_Dropdown resolutionDropdown;
        public Toggle antiAliasingToggle;
        public Slider gammaSlider;
        public Slider brightnessSlider;
        public TMP_Dropdown texQualityDropdown;
        public TMP_Dropdown shadowQualityDropdown;

        public void SetData(VideoData data)
        {
            displayModeToggle.isOn = data.fullScreen;
            SetResolutions();
            resolutionDropdown.value = (int) data.screenResolution;
            antiAliasingToggle.isOn = data.antiAliasing;
            gammaSlider.value = data.gamma;
            brightnessSlider.value = data.brightness;
            SetTexQualities();
            texQualityDropdown.value = (int) data.textureQuality;
            SetShadowQualities();
            shadowQualityDropdown.value = (int) data.shadowQuality;


        }
        private void SetResolutions()
        {
            var values = Enum.GetValues(typeof(ScreenResolution));
            for (int i = 1; i < values.Length; i++)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                optionData.text = values.GetValue(i).ToString();
                resolutionDropdown.options.Add(optionData);
            }
        }
        private void SetTexQualities()
        {
            var values = Enum.GetValues(typeof(Details));
            for (int i = 1; i < values.Length; i++)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                optionData.text = values.GetValue(i).ToString();
                resolutionDropdown.options.Add(optionData);
            }
        }
        private void SetShadowQualities()
        {
            var values = Enum.GetValues(typeof(ShadowResolution));
            for (int i = 1; i < values.Length; i++)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                optionData.text = values.GetValue(i).ToString();
                resolutionDropdown.options.Add(optionData);
            }
        }
    }
}