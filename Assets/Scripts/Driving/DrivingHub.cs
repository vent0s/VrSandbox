using Septim.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Septim.Driving
{
    public class DrivingHub : CommandHub
    {
        [SerializeField]
        private Transform seatAnchor;

        protected override void _Awake()
        {
            base._Awake();
            OnDrivingLoaded();
        }

        public void OnDrivingLoaded()
        {
            PlayerManager.instance.transform.position = seatAnchor.position;
            PlayerManager.instance.transform.parent = seatAnchor;
        }
    }
}
