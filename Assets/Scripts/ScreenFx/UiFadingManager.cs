using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace Septim.UI
{
    public class UiFadingManager : MonoBehaviour
    {
        /*
              ____       __                                   
             |  _ \ ___ / _| ___ _ __ ___ _ __   ___ ___  ___ 
             | |_) / _ \ |_ / _ \ '__/ _ \ '_ \ / __/ _ \/ __|
             |  _ <  __/  _|  __/ | |  __/ | | | (_|  __/\__ \
             |_| \_\___|_|  \___|_|  \___|_| |_|\___\___||___/
         */

        private static UiFadingManager _instance;
        public static UiFadingManager instance => _instance;

        private Volume volume;
        private ColorAdjustments colorAdjustments;

        public float faddingSpeed = 1f;

        /*
         * NON INSPECTOR
         */

        public event Action OnFaddingStartAction;

        public event Action OnFaddingEndAction;

        private bool onFadding = false;

        private Coroutine curFadding;

        /*
              __  __                       _     _  __           ____ _          _      
             |  \/  | ___  _ __   ___     | |   (_)/ _| ___     / ___(_)_ __ ___| | ___ 
             | |\/| |/ _ \| '_ \ / _ \    | |   | | |_ / _ \   | |   | | '__/ __| |/ _ \
             | |  | | (_) | | | | (_) |   | |___| |  _|  __/   | |___| | | | (__| |  __/
             |_|  |_|\___/|_| |_|\___/    |_____|_|_|  \___|    \____|_|_|  \___|_|\___|
         */

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            GetVolume();
        }

        /*
             _____                           _ 
            |  __ \                         | |
            | |  \/ ___ _ __   ___ _ __ __ _| |
            | | __ / _ \ '_ \ / _ \ '__/ _` | |
            | |_\ \  __/ | | |  __/ | | (_| | |
             \____/\___|_| |_|\___|_|  \__,_|_|
         */

        public void FadeScreen(bool isFadeIn, bool isBlackOut)
        {
            FaddingStart();
            if (volume == null)
            {
                FaddingEnd();
                return;
            }
            if (curFadding != null)
            {
                StopCoroutine(curFadding);
                curFadding = null;
            }
            if (isFadeIn)
            {
                curFadding = StartCoroutine(FadeInScreen(isBlackOut));
            }
            else
            {
                curFadding = StartCoroutine(FadeOutScreen(isBlackOut));
            }
        }

        private IEnumerator FadeOutScreen(bool isBlackOut)
        {
            float time = 0f;
            if (colorAdjustments == null)
            {
                Debug.Log("NULL");
            }
            float fadeInitValue = colorAdjustments.postExposure.GetValue<float>();
            VolumeParameter<float> param = new VolumeParameter<float>();
            param.value = fadeInitValue;
            if (isBlackOut)
            {
                while (colorAdjustments.postExposure.GetValue<float>() < 0f)
                {
                    param.value = Mathf.Lerp(fadeInitValue, 0f, time * faddingSpeed);
                    //Debug.Log(param.value);
                    colorAdjustments.postExposure.SetValue(param);
                    time += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                while (colorAdjustments.postExposure.GetValue<float>() > 0f)
                {
                    param.value = Mathf.Lerp(fadeInitValue, 0f, time * faddingSpeed);
                    //Debug.Log(param.value);
                    colorAdjustments.postExposure.SetValue(param);
                    time += Time.deltaTime;
                    yield return null;
                }

            }
            FaddingEnd();
        }

        private IEnumerator FadeInScreen(bool isBlackOut)
        {
            float time = 0f;
            float fadeInitValue = colorAdjustments.postExposure.GetValue<float>();
            VolumeParameter<float> param = new VolumeParameter<float>();
            param.value = fadeInitValue;
            if (isBlackOut)
            {
                while (colorAdjustments.postExposure.GetValue<float>() > -10f)
                {
                    param.value = Mathf.Lerp(fadeInitValue, -10f, time * faddingSpeed);
                    //Debug.Log(param.value);
                    colorAdjustments.postExposure.SetValue(param);
                    time += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {

                while (colorAdjustments.postExposure.GetValue<float>() < 10f)
                {
                    param.value = Mathf.Lerp(fadeInitValue, 10f, time * faddingSpeed);
                    //Debug.Log(param.value);
                    colorAdjustments.postExposure.SetValue(param);
                    time += Time.deltaTime;
                    yield return null;
                }
            }
            FaddingEnd();
        }

        //we will process all ui rendering or initiation related issue on following two methods, aswell as invoking delegations
        private void FaddingStart()
        {
            if (OnFaddingStartAction != null)
            {
                OnFaddingStartAction?.Invoke();
                OnFaddingStartAction = null;
            }
        }

        private void FaddingEnd()
        {
            if (OnFaddingEndAction != null)
            {
                OnFaddingEndAction?.Invoke();
                OnFaddingEndAction = null;
            }
            if (curFadding != null)
            {
                StopCoroutine(curFadding);
                curFadding = null;
            }
        }


        public void GetVolume()
        {
            if (volume == null)
            {
                GameObject volumeObj = GameObject.FindGameObjectWithTag("PostProcessing");
                if (volumeObj != null)
                {
                    volume = volumeObj.GetComponent<Volume>();
                }
                else
                {
                    volume = null;
                    colorAdjustments = null;
                }
                if (volume != null)
                {
                    //Debug.Log("Volume found");
                    volume.profile.TryGet(out colorAdjustments);
                }
            }

        }
    }
}
