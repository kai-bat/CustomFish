using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using FishFramework;
using Harmony;

namespace MinecraftFish
{
    public static class MainPatch
    {
        public static AssetBundle bundle;

        public static void Patch ()
        {
            HarmonyInitalizer.HarmonyInit();
            bundle = AssetBundle.LoadFromFile(Path.Combine(Environment.CurrentDirectory, "QMods/MinecraftFish/fishassets"));

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

            Sprite codsprite = bundle.LoadAsset<Sprite>("Sprites/Raw_Cod.png");

            cod.Register();
        }
    }
}
