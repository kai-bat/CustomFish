using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MinecraftFish
{
    public static class TropicalFishGenerator
    {
        public static List<string> tropicalFishPrefabFiles;
        public static List<Color> fishColours;

        public static void Init ()
        {
            tropicalFishPrefabFiles = new List<string>();
            fishColours = new List<Color>();
            GameObject[] fishes = MainPatch.bundle.LoadAllAssets<GameObject>();
            Material[] mats = MainPatch.bundle.LoadAllAssets<Material>();

            foreach(GameObject obj in fishes)
            {
                if(obj.name.Contains("Tropical"))
                {
                    tropicalFishPrefabFiles.Add("Assets/Prefabs/" + obj.name+".prefab");
                }
            }

            foreach(Material mat in mats)
            {
                if(mat.name.Contains("Tropical"))
                {
                    fishColours.Add(mat.color);
                }
            }
        }

        public static string GetRandomFish()
        {
            if(tropicalFishPrefabFiles.Count == 0)
            {
                return null;
            }

            int index = Random.Range(0, tropicalFishPrefabFiles.Count - 1);

            return tropicalFishPrefabFiles[index];
        }

        public static void ApplyColours(GameObject fish)
        {
            Shader shader = Shader.Find("MarmosetUBER");
            Color colour1 = RandomColour();
            Color colour2 = RandomColour();

            while(colour1 == colour2)
            {
                colour2 = RandomColour();
            }

            Material mainMat = new Material(shader);
            mainMat.color = colour1;
            Material detailMat = new Material(shader);
            detailMat.color = colour2;
            Material eyeMat = new Material(shader);
            eyeMat.color = Color.black;

            Renderer[] rends = fish.GetComponentsInChildren<Renderer>();
            foreach(Renderer rend in rends)
            {
                if(rend.name.Contains("Main"))
                {
                    rend.material = mainMat;
                }
                else if(rend.name.Contains("Detail"))
                {
                    rend.material = detailMat;
                }
                else if(rend.name.Contains("Eyes"))
                {
                    rend.material = eyeMat;
                }
            }
        }

        static Color RandomColour()
        {
            int index = Random.Range(0, fishColours.Count - 1);

            return fishColours[index];
        }
    }
}
