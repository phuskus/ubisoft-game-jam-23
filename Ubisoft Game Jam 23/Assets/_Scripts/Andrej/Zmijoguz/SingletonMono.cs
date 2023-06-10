using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zmijoguz
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        public static T Instance;

        private void Awake() => Instance = (T)this;
    } 
}

