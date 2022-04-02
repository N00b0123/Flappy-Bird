using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawRate = 1f;
    [SerializeField] private float minHeigth = -1f;
    [SerializeField] private float maxHeigth = 1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawRate, spawRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipe = Instantiate(prefab, transform.position, Quaternion.identity);
        pipe.transform.position += Vector3.up * Random.Range(minHeigth, maxHeigth);
    }
}
