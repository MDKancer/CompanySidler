using System;
using System.Collections;
using AudioManager;
using Enums;
using InputWrapper;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using VideoManager;
using Zenject;

namespace UIDispatcher.UiComponents
{
    [GUIColor(0.36f,0.41f,0.71f,1f)]
    public class UIController : MonoBehaviour
    {
        public UIAudioComponents uiAudioComponents;
        public UIVideoComponents uiVideoComponents;
        public UIControlComponents uiControlComponents;
        private SignalBus signalBus;
        private AudioController audioController;
        private VideoController videoController;
        private InputBinding inputBinding;
        [Inject]
        private void Init(SignalBus signalBus,AudioController audioController, VideoController videoController, InputBinding inputBinding)
        {
            this.signalBus = signalBus;
            this.audioController = audioController;
            this.videoController = videoController;
            this.inputBinding = inputBinding;
        }

        private void Start()
        {
            //please not in Awake 
            //because in the State simulator are all datas in awake imported in to the game
            //and than audiocontroller and video controller are not yet imported
            uiAudioComponents.SetData(audioController.AudioData);
            uiVideoComponents.SetData(videoController.VideoData);
            uiControlComponents.SetData(inputBinding.KeyBoardData);
        }

        #region AudioSettingEvents
        public void Volume(float value)
        {
            audioController.SetVolume(value);
        }

        public void SetChannel(int value)
        {
            audioController.SetSpeakerMode((AudioSpeakerMode)Enum.GetValues(typeof(AudioSpeakerMode)).GetValue(value+1));
        }

        public void SetSFXVolume(float value)
        {
            audioController.SetSoundEffects(value);
        }

        public void SetAmbientSoundVolume(float value)
        {
            audioController.SetAmbient(value);
        }
        #endregion

        #region VideoSettingEvents

        public void SetDisplayMode(bool value)
        {
            videoController.SetFullScreen(value);
        }

        public void SetResolution(int value)
        {
            videoController.SetResolution((ScreenResolution)Enum.GetValues(typeof(ScreenResolution)).GetValue(value));
        }

        public void SetAntiAliasing(bool value)
        {
            videoController.SetAntiAliasing(value);
        }

        public void SetGamma(float value)
        {
            videoController.SetAmbientIntensity(value);
        }

        public void SetBrightness(float value)
        {
            videoController.SetBrightness(value);
        }

        public void SetTexQuality(int value)
        {
            videoController.SetTextureQuality((Details)Enum.GetValues(typeof(Details)).GetValue(value));
        }

        public void SetShadowQuality(int value)
        {
            videoController.SetShadowQuality((ShadowResolution)Enum.GetValues(typeof(ShadowResolution)).GetValue(value));
        }
        #endregion

        #region ControlSettingEvents

        public void OnClickLabel(TextMeshProUGUI label)
        {
            StartCoroutine(WaitForNewKeyCode(label));
        }

        private KeyCode GetKeyCode(String value)
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (keycode.ToString().Equals(value))
                {
                    return keycode;
                }
            }

            return KeyCode.None;
        }

        private IEnumerator WaitForNewKeyCode(TextMeshProUGUI label)
        {
            var oldKeyCode = GetKeyCode(label.text);

            //Development Version
            do
            {
                //do somthing
                yield return null;
            } while (!Input.anyKey);

            var newKeyCode = GetKeyCode(Input.inputString);
            inputBinding.ChangeBinding(oldKeyCode,newKeyCode);
            label.SetText(newKeyCode.ToString());

        }
        #endregion
    }
}