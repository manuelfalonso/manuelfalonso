using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Utility
{

    public class PrefabInstantiateOnLoadSO : ScriptableObject
    {
        public bool Active = true;
        public List<GameObject> PrefabList = new List<GameObject>();
    }
}
