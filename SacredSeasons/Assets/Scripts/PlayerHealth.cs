using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    public float MaxValue;
    public float CurrentValue;

    void start()
    {
        MaxValue = 100.0f;
        CurrentValue = MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
