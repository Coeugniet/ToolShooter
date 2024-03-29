﻿using Shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
    public abstract void OnPooled();
    public abstract void Initialise();
}
