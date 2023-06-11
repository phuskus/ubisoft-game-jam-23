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
    public static PlayerMesh Mesh;
    public static SoundManager Sound;

    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    private void Start()
    {
        Input = PlayerInput.Instance;
        Movement = Movement.Instance;
        CursorObject = CursorObject.Instance;
        Settings = PlayerData.Instance;
        Gun = PlayerGun.Instance;
        Sound = SoundManager.Instance;

        Mesh = GetComponentInChildren<PlayerMesh>();
        //animator = GetComponentInChildren<Animator>();
    }
}
