#if REQUIRES_EXTERNAL_PACKAGE
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace SombraStudios.UI
{
    /// <summary>
    /// Image Carousel Class behaviour. 
    /// Instantiate a list of Image GameObjects prefabs side to side
    /// and cicles between them until the end, then restarts.
    /// Require: 
    /// -Prefab with a Image component and Strech x Strech Anchors Preset.
    /// -DOTween package installed.
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
            DOTween.Init();
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
                    Tween nextImage = _imageContainerObject.DOAnchorPosX(_imageWidth * -1, _imageTransitionDuration)
                            .SetDelay(_delayBetweenImages)
                            .SetRelative();
                    yield return nextImage.WaitForCompletion();
                }
                // Return to inital point
                Tween restart = _imageContainerObject.DOAnchorPosX(0, _imageTransitionDuration).SetDelay(_delayBetweenImages);
                yield return restart.WaitForCompletion();
            } while (true);
        }
    }
}
#endif