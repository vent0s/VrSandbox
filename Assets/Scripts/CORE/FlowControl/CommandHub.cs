using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Septim.Data
{
    public class CommandHub : MonoBehaviour
    {
        public static CommandHub instance;

        private void Awake()
        {
            _Awake();
        }

        private void OnDestroy()
        {
            _OnDestroy();
        }

        protected virtual void _Awake()
        {
            instance = this;
        }

        protected virtual void _OnDestroy()
        {
            instance = null;
        }
    }
}

