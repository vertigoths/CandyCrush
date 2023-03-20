using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject blockEffectPrefab;
    [SerializeField] private GameObject hypeEffectPrefab;

    public static EffectManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayBlockEffect(Vector3 position)
    {
        var effect = Instantiate(blockEffectPrefab);
        effect.transform.position = position;
    }

    public void PlayHypeEffect()
    {
        Instantiate(hypeEffectPrefab);
    }
}
