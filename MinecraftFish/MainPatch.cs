using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using FishFramework;

namespace MinecraftFish
{
    public static class MainPatch
    {
        public static AssetBundle bundle;

        public static void Patch ()
        {
            bundle = AssetBundle.LoadFromFile(Path.Combine(Environment.CurrentDirectory, "QMods/MinecraftFish/fishassets"));

            TropicalFishGenerator.Init();

            CustomFish cod = new CustomFish();
            cod.id = "MineCod";
            cod.displayName = "Cod";
            cod.tooltip = "The classic, blocky, tasty fish";

            cod.bundle = bundle;
            cod.fileName = "Assets/Prefabs/CodFish.prefab";

            cod.scale = 0.07f;
            cod.isPickupable = true;

            cod.swimRadius = Vector3.one * 10f;
            cod.swimSpeed = 7f;

            cod.Register();

            int index = 0;
            foreach (string tropicalPrefab in TropicalFishGenerator.tropicalFishPrefabFiles)
            {
                Console.WriteLine($"[MinecraftFish] Creating tropical fish with filename: {tropicalPrefab}");
                CustomFish tropical = new CustomFish();
                tropical.id = "Tropical"+index;
                index++;
                tropical.displayName = "Tropical Fish";
                tropical.tooltip = "A lovely colourful tropical fish";

                tropical.bundle = bundle;
                tropical.fileName = tropicalPrefab;

                tropical.scale = 0.07f;
                tropical.isPickupable = true;

                tropical.swimRadius = Vector3.one * 10f;
                tropical.swimSpeed = 7f;
                tropical.components = new List<Type>
                {
                    typeof(TropicalFish)
                };

                tropical.Register();
            }
        }
    }
}
