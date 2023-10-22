using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Septim.Map
{
    public class MapManager : MonoBehaviour
    {
        /*
              ____       __                                   
             |  _ \ ___ / _| ___ _ __ ___ _ __   ___ ___  ___ 
             | |_) / _ \ |_ / _ \ '__/ _ \ '_ \ / __/ _ \/ __|
             |  _ <  __/  _|  __/ | |  __/ | | | (_|  __/\__ \
             |_| \_\___|_|  \___|_|  \___|_| |_|\___\___||___/
         */
        private static MapManager _instance;
        public static MapManager instance => _instance;

        public event Action OnStartLoadScene;
        private event Action OnLoadingScene;
        public event Action OnStartLoadedScene;
        private event Action OnLoadedScene;

        private string curDestinationScene;

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
            OnLoadingScene += LoadScene;
            OnLoadedScene += LoadedScene;

        }

        private void Start()
        {
            SceneManager.sceneLoaded += StartLoadedScene;
        }

        public string GetCurSceneName()
        {
            return SceneManager.GetActiveScene().name;
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
         * TODO
         * 
         * THE PLAN IS, WE INVOKE LOAD SCENE METHOD, LOAD SCENE INVOKE FADE OUT
         * WHEN FADED OUT, UIMANAGER INVOKE SECOND LOAD SCENE METHOD, 
         * WHICH ACTUALLY LOAD SCENE AND PERFORM GC
         * 
         * THEN WHEN SCENE LOADED, CALL BACK ATTACHED TO SCENE MANAGER INVOKE TO 
         * CALL FADE IN AND OTHER ISSUES
         */
        public void StartLoadScene(string destinationScene)
        {
            curDestinationScene = destinationScene;

            OnStartLoadScene?.Invoke();

            //clear ui state
            //UiManager.instance.InteractionClear();

            //Fade screen
            UiManager.instance.FadeScreen(true, true, null, OnLoadingScene);
        }

        private void LoadScene()
        {
            System.GC.Collect();
            SceneManager.LoadScene(curDestinationScene);
        }

        public void StartLoadedScene(Scene scene, LoadSceneMode mode)
        {
            //Fade out screen
            UiManager.instance.FadeScreen(false, true, null, OnLoadedScene);



            //invoke delegation
            OnLoadedScene?.Invoke();
        }

        private void LoadedScene()
        {
            //when screen faded out

            //set player state
            //PlayerManager.instance.SetCurrentState(eCharacterState.locomotion);

            //set player position
        }
    }
}

