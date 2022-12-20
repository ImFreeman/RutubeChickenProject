using Features.Egg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MyTest : MonoBehaviour
{
    private EggSpawner _spawner;
    [Inject]
    private void Inject(EggSpawner spawner)
    {
        _spawner = spawner;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _spawner.Spawn(0, 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _spawner.Spawn(1, 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _spawner.Spawn(2, 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _spawner.Spawn(3, 0.1f);
        }
    }
}
