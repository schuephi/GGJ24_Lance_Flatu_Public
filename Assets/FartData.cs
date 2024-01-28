using System;
using UnityEngine;

namespace Assets
{
    [Serializable]
    public class FartData
    {
        public float Intensity = 0f;
        public AudioClip StartFart;
        public AudioClip LoopFart;
        public AudioClip EndFart;
    }
}