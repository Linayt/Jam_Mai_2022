using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEtats : MonoBehaviour
{
    public bool onGround;

    public bool isHit;

    PlayerController pController;
    internal void Initialize()
    {
        pController = GetComponent<PlayerController>();
    }
}
