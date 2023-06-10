using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class Player : SingletonMono<Player>
{
    public static PlayerInput Input;

    private void Start()
    {
        Input = PlayerInput.Instance;
    }
}
