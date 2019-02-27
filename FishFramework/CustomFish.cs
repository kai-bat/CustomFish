using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SMLHelper.V2.Handlers;

namespace FishFramework
{
    public class CustomFish
    {
        public string id;
        public string displayName;
        public string tooltip;

        public AssetBundle bundle;
        public string fileName;
        public Atlas.Sprite sprite;

        public bool isPickupable;
        public float scale;

        public float swimSpeed;
        public Vector3 swimRadius;

        public void Register()
        {
            Console.WriteLine("[FishFramework] Creating fish: " + displayName);
            TechType type = TechTypeHandler.AddTechType(id, displayName, tooltip);

            FishSpawner.fishTechTypes.Add(type);

            CustomFishPrefab fish = new CustomFishPrefab(id, fileName, type);
            fish.bundle = bundle;
            fish.scale = scale;
            fish.swimSpeed = swimSpeed;
            fish.swimRadius = swimRadius;
            fish.pickupable = isPickupable;
            try
            {
                SpriteHandler.RegisterSprite(type, sprite);
            }
            catch
            {
                Console.WriteLine("[FishFramework] Could not register sprite");
            }
            PrefabHandler.RegisterPrefab(fish);
        }
    }
}