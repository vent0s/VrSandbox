using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Septim.Data
{
    public class DataManager : MonoBehaviour
    {
        /*
              ____       __                                   
             |  _ \ ___ / _| ___ _ __ ___ _ __   ___ ___  ___ 
             | |_) / _ \ |_ / _ \ '__/ _ \ '_ \ / __/ _ \/ __|
             |  _ <  __/  _|  __/ | |  __/ | | | (_|  __/\__ \
             |_| \_\___|_|  \___|_|  \___|_| |_|\___\___||___/
         */

        private static DataManager _instance;
        public static DataManager instance => _instance;

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

        /*
              __  __                       _     _  __           ____ _          _      
             |  \/  | ___  _ __   ___     | |   (_)/ _| ___     / ___(_)_ __ ___| | ___ 
             | |\/| |/ _ \| '_ \ / _ \    | |   | | |_ / _ \   | |   | | '__/ __| |/ _ \
             | |  | | (_) | | | | (_) |   | |___| |  _|  __/   | |___| | | | (__| |  __/
             |_|  |_|\___/|_| |_|\___/    |_____|_|_|  \___|    \____|_|_|  \___|_|\___|
         */

        /*
             _____                           _ 
            |  __ \                         | |
            | |  \/ ___ _ __   ___ _ __ __ _| |
            | | __ / _ \ '_ \ / _ \ '__/ _` | |
            | |_\ \  __/ | | |  __/ | | (_| | |
             \____/\___|_| |_|\___|_|  \__,_|_|
         */
    }
}

