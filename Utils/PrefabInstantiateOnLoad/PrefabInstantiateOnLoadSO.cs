using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Utils
{

    public class PrefabInstantiateOnLoadSO : ScriptableObject
    {
        public bool Active = true;
        public List<GameObject> PrefabList = new List<GameObject>();
    }
}
