using System;
using UnityEngine;
using System.Linq;
using CustomEventBus;

namespace Levels{
    public class LevelDataManager : IService
    {
        private const string PrefabsFilePath = "Levels/";
        private static string[] PrefabsLevel = (from x in Enumerable.Range(1,ConstantValues.LEVEL_COUNT) select "Level" + Convert.ToString(x)).ToArray();
        public int LevelsCount => PrefabsLevel.Length;
        public T GetLevel<T>(int levelId) where T: Level
        {
            var prefabsLevelName = PrefabsLevel[levelId];
            var path = PrefabsFilePath + prefabsLevelName;
            var level = Resources.Load<T>(path);
            if (level == null)
            {
                Debug.LogError("Cant find prefab at path " + path);
            }
            return level;
        }
        public T ShowLevel<T>(int levelId) where T: Level
        {
            var go = GetLevel<T>(levelId);
            if (go == null)
            {
                Debug.LogError("Show level - object not found");
                return null;
            }
            return GameObject.Instantiate(go, LevelHolder);
        }
        public static Transform LevelHolder
        {
            get { return ServiceLocator.Current.Get<LevelHolder>().transform; }
        }
}
}