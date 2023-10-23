using Septim.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Septim.Lobby
{
    public class LobbyHub : CommandHub
    {
        public void SwitchToDriveScene()
        {
            GameManager.instance.LoadScene("DriveScene");
        }
    }
}

