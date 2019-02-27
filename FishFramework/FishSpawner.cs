using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Reflection;

namespace FishFramework
{
    [HarmonyPatch(typeof(Creature), "Start")]
    public class FishSpawner
    {
        public static List<TechType> fishTechTypes = new List<TechType>();

        public static void Postfix(Creature __instance)
        {
            if(Random.value < 0.1f)
            {
                int randomIndex = Random.Range(0, fishTechTypes.Count - 1);
                TechType randomFish = fishTechTypes[randomIndex];

                GameObject fish = CraftData.InstantiateFromPrefab(randomFish);
                fish.transform.position = __instance.transform.position;
            }
        }
    }
}
