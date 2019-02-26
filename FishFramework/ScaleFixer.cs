using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FishFramework
{
    public class ScaleFixer : MonoBehaviour
    {
        public float scale;

        void Update()
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
