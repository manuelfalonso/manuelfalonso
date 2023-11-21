using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.VFX
{
    /// <summary>
    /// Utils script for adding and removing materials from a Renderer
    /// </summary>
    public class MaterialHandler : MonoBehaviour
    {
        [SerializeField] private Renderer _rederer;

        private Material _addedMaterial = null;


        private void Start()
        {
            if (_rederer == null)
            {
                enabled = false;
                throw new MissingReferenceException();
            }
        }


        public void AddMaterial(Material material)
        {
            if (material == null) { return; }

            var materialList = new List<Material>();
            materialList.AddRange(_rederer.materials);
            materialList.Add(material);

            _rederer.materials = materialList.ToArray();
            _addedMaterial = _rederer.materials[_rederer.materials.Length - 1];
        }

        public void RemoveAddedMaterial()
        {
            if (_addedMaterial == null) { return; }

            var materialList = new List<Material>();
            materialList.AddRange(_rederer.materials);
            materialList.Remove(_addedMaterial);
            
            _rederer.materials = materialList.ToArray();
            _addedMaterial = null;
        }
    }
}
