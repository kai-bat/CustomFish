using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MinecraftFish
{
    public class TropicalFish : MonoBehaviour
    {
        public void Start ()
        {
            TropicalFishGenerator.ApplyColours(gameObject);
        }
    }
}
