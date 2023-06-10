using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandleDeath
{
    public void ReactToDamage();
    public abstract void HandlePain();
    public abstract void HandleDeath();
}
