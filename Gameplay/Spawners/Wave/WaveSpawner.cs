using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Spawners.Wave
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] List<WaveConfigSO> _waveConfigs;
        [SerializeField] int _startingWave = 0;
        [SerializeField] bool _looping = false;

        IEnumerator Start()
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
            }
            while (_looping);
        }

        private IEnumerator SpawnAllWaves()
        {
            for (int waveIndex = _startingWave; waveIndex < _waveConfigs.Count; waveIndex++)
            {
                var currentWave = _waveConfigs[waveIndex];
                yield return StartCoroutine(SpawnWave(currentWave));
            }
        }

        private IEnumerator SpawnWave(WaveConfigSO waveConfigSo)
        {
            for (int count = 0; count < waveConfigSo.GetQuantity(); count++)
            {
                Instantiate(
                    waveConfigSo.GetPrefab(),
                    transform.position,
                    Quaternion.identity);
                yield return new WaitForSeconds(waveConfigSo.GetTimeBetweenSpawns());
            }
        }
    }
}
