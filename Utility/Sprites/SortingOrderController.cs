using System;
using UnityEngine;
using SombraStudios.Shared.Extensions;

namespace SombraStudios.Shared.Utility.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SortingOrderController : MonoBehaviour
    {
        private const int ORDER_MULTIPLIER = 100;

        [Header("Settings")]
        [Tooltip("Sorting order mode. YAxis is the default. Custom allows you to set your own sorting order function.")]
        [SerializeField] private SortingMode _sortingMode = SortingMode.YAxis;
        [Tooltip("Offset to add to the sorting order. This is useful for manual overrides.")]
        [SerializeField] private int _offset = 0;

        /// <summary>
        /// Custom sorting function. This allows you to set your own sorting order function.
        /// </summary>
        public Func<Transform, int> CustomSortingFunction { get; set; }

        [Header("References")]
        [Tooltip("Sprite renderer to set the sorting order. This is the default sprite renderer.")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            this.SafeInit(ref _spriteRenderer);
        }

        private void LateUpdate()
        {
            int sortingOrder = CalculateSortingOrder();
            _spriteRenderer.sortingOrder = sortingOrder + _offset;
        }

        /// <summary>
        /// Calculates the sorting order based on the selected sorting mode.
        /// </summary>
        /// <returns>The calculated sorting order.</returns>
        private int CalculateSortingOrder()
        {
            Vector3 pos = transform.position;

            return _sortingMode switch
            {
                SortingMode.YAxis => Mathf.RoundToInt(-pos.y * ORDER_MULTIPLIER),
                SortingMode.XAxis => Mathf.RoundToInt(-pos.x * ORDER_MULTIPLIER),
                SortingMode.XYSum => Mathf.RoundToInt(-(pos.x + pos.y) * ORDER_MULTIPLIER),
                SortingMode.XYDifference => Mathf.RoundToInt(-(pos.x - pos.y) * ORDER_MULTIPLIER),
                SortingMode.Custom => CustomSortingFunction?.Invoke(transform) ?? 0,
                _ => 0,
            };
        }

        public enum SortingMode
        {
            YAxis,
            XAxis,
            XYSum,
            XYDifference,
            Custom
        }
    }
}
