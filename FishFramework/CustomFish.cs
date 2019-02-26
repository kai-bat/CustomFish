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
        public Sprite sprite;

        public bool isPickupable;
        public float scale;

        public void Register()
        {
            TechType type = TechTypeHandler.AddTechType(id, displayName, tooltip);

            CustomFishPrefab fish = new CustomFishPrefab(bundle, id, fileName, scale, isPickupable, type);
            SpriteHandler.RegisterSprite(type, "Assets/Raw_Cod.png");
            PrefabHandler.RegisterPrefab(fish);
        }
    }
}