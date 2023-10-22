using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Septim.Data
{
    public class CommandHub : MonoBehaviour
    {
        public void SwitchToDriveScene()
        {
            GameManager.instance.LoadScene("DriveScene");
        }
    }
}

