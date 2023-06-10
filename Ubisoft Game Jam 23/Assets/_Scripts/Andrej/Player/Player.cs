using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class Player : SingletonMono<Player>
{
    public static PlayerInput Input;
    public static Movement Movement;
    public static CursorObject CursorObject;
    public static PlayerData Settings;
    public static PlayerGun Gun;

    private void Start()
    {
        Input = PlayerInput.Instance;
        Movement = Movement.Instance;
        CursorObject = CursorObject.Instance;
        Settings = PlayerData.Instance;
        Gun = PlayerGun.Instance;
    }
}
