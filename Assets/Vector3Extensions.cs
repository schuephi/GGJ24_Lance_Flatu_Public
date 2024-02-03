﻿using UnityEditor;
using UnityEngine;

namespace Assets
{
    public static class Vector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 v) {
            return new Vector2(v.x, v.y);
        }
    }
}