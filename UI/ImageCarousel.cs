using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SombraStudios.Shared.UI
{
    /// <summary>
    /// Image Carousel Class behaviour. 
    /// Instantiate a list of Image GameObjects prefabs side to side
    /// and cicles between them until the end, then restarts.
    /// Require: 
    /// -Prefab with a Image component and Strech x Strech Anchors Preset.
    /// </summary>
    public class ImageCarousel : MonoBehaviour
    {
        [SerializeField] private RectTransform _imageContainerObject;
        [SerializeField] private GameObject _imagePrefab;
        [SerializeField] private List<Sprite> _imageList;

        [SerializeField] private float _imageTransitionDuration = 1;
        [SerializeField] private float _delayBetweenImages = 2;

        private float _imageWidth;

        // Start is called before the first frame update
        void Start()
        {
            _imageWidth = GetComponent<RectTransform>().rect.width;
            InstantiateCarouselImages();
            StartCoroutine(StartCarousel());
        }

        private void InstantiateCarouselImages()
        {
            for (int i = 0; i < _imageList.Count; i++)
            {
                // Instantiate Image Prefab
                GameObject _go = Instantiate(_imagePrefab, _imageContainerObject.transform);
                _go.GetComponent<Image>().sprite = _imageList[i];
                // Place it next to the previous one
                RectTransform _rt = _go.GetComponent<RectTransform>();
                _rt.localPosition += new Vector3(_imageWidth * i, 0f, 0f);
            }
        }

        IEnumerator StartCarousel()
        {
            int _imageQuantity = _imageContainerObject.childCount;
            do
            {
                // Cycle each image
                for (int i = 0; i < _imageQuantity - 1; i++)
                {
                    yield return new WaitForSeconds(_delayBetweenImages);
                    yield return MoveContainerToX(_imageContainerObject.anchoredPosition.x - _imageWidth);
                }
                // Return to inital point
                yield return new WaitForSeconds(_delayBetweenImages);
                yield return MoveContainerToX(0f);
            } while (true);
        }

        /// <summary>
        /// Interpolates the container's anchored position to the given X over the transition duration.
        /// </summary>
        private IEnumerator MoveContainerToX(float targetX)
        {
            Vector2 start = _imageContainerObject.anchoredPosition;
            Vector2 target = new Vector2(targetX, start.y);

            if (_imageTransitionDuration <= 0f)
            {
                _imageContainerObject.anchoredPosition = target;
                yield break;
            }

            float elapsed = 0f;
            while (elapsed < _imageTransitionDuration)
            {
                elapsed += Time.deltaTime;
                _imageContainerObject.anchoredPosition =
                    Vector2.Lerp(start, target, elapsed / _imageTransitionDuration);
                yield return null;
            }

            _imageContainerObject.anchoredPosition = target;
        }
    }
}