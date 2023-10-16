using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace ObjectPool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private List<T> _prefabs;
        private List<T> _objects;

        //Принимает словарь префабов с их количеством
        public Pool(Dictionary<T,int> prefabGroups)
        {
            _prefabs = prefabGroups.Select(x => x.Key).ToList();
            _objects = new List<T>();

            foreach (var prefab in prefabGroups)
            {
                for (int j = 0; j < prefab.Value; j++)
                {
                    var obj = GameObject.Instantiate(prefab.Key);
                    obj.gameObject.SetActive(false);
                    _objects.Add(obj);
                }
            }
        }

        public T Get()
        {
            var obj = _objects.Where(x => x != null).FirstOrDefault(x => !x.isActiveAndEnabled);
            if (obj == null)
            {
                obj = Create();
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        private T Create(){
            Random rand = new Random();
            var obj = GameObject.Instantiate(_prefabs.ElementAt(rand.Next(0,_prefabs.Count())));
            _objects.Add(obj);
            return obj;
        }
        public void Release(T obj){
            obj.gameObject.SetActive(false);
        }
    }
}