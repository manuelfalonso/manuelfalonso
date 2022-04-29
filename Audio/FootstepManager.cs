using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script Manager that uses FootstepAudioClipListSO to run foot step clips
/// depending of the Tag of the floor by its collision.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class FootstepManager : MonoBehaviour
{
    public List<FootstepAudioClipListSO> stepsList;

    private List<AudioClip> _currentList;

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        for (int i = 0; i < stepsList.Count; i++)
        {
            if (hit.transform.tag == stepsList[i].Tag)
            {
                _currentList = stepsList[i].Steps;
                break;
            }
			else
                currentList = null;
        }
    }

    public void PlayStep()
    {
        if (currentList != null)
        {
            AudioClip clip = currentList[Random.Range(0, currentList.Count)];
            source.PlayOneShot(clip);
        }
    }
}
