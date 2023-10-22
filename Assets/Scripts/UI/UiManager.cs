using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Septim.UI;

namespace Septim
{
    [RequireComponent(typeof(UiFadingManager))]
    public class UiManager : MonoBehaviour
    {
        /*
              ____       __                                   
             |  _ \ ___ / _| ___ _ __ ___ _ __   ___ ___  ___ 
             | |_) / _ \ |_ / _ \ '__/ _ \ '_ \ / __/ _ \/ __|
             |  _ <  __/  _|  __/ | |  __/ | | | (_|  __/\__ \
             |_| \_\___|_|  \___|_|  \___|_| |_|\___\___||___/
         */

        private static UiManager _instance;
        public static UiManager instance => _instance;

        /*
              __  __                       _     _  __           ____ _          _      
             |  \/  | ___  _ __   ___     | |   (_)/ _| ___     / ___(_)_ __ ___| | ___ 
             | |\/| |/ _ \| '_ \ / _ \    | |   | | |_ / _ \   | |   | | '__/ __| |/ _ \
             | |  | | (_) | | | | (_) |   | |___| |  _|  __/   | |___| | | | (__| |  __/
             |_|  |_|\___/|_| |_|\___/    |_____|_|_|  \___|    \____|_|_|  \___|_|\___|
         */

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        /*
             _____                           _ 
            |  __ \                         | |
            | |  \/ ___ _ __   ___ _ __ __ _| |
            | | __ / _ \ '_ \ / _ \ '__/ _` | |
            | |_\ \  __/ | | |  __/ | | (_| | |
             \____/\___|_| |_|\___|_|  \__,_|_|
         */


        /*
             _____                         ______    
            /  ___|                        |  ___|   
            \ `--.  ___ _ __ ___  ___ _ __ | |___  __
             `--. \/ __| '__/ _ \/ _ \ '_ \|  _\ \/ /
            /\__/ / (__| | |  __/  __/ | | | |  >  < 
            \____/ \___|_|  \___|\___|_| |_\_| /_/\_\
         */

        public void FadeScreen(bool isFadeIn, bool isBlackOut, Action eventOnStart, Action eventOnEnd)
        {
            Debug.Log("Fade start");
            if (eventOnStart != null)
            {
                UiFadingManager.instance.OnFaddingStartAction += eventOnStart;
            }
            if (eventOnEnd != null)
            {
                UiFadingManager.instance.OnFaddingEndAction += eventOnEnd;
            }

            FadeScreen(isFadeIn, isBlackOut);
        }

        public void FadeScreen(bool isFadeIn, bool isBlackOut)
        {
            UiFadingManager.instance.FadeScreen(isFadeIn, isBlackOut);
        }
    }
}

