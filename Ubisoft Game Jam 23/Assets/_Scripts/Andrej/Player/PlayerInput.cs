using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class PlayerInput : SingletonMono<PlayerInput>
{
    public Vector3 KeyboardInput;
    public Vector3 MousePosition;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        //store the input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        MousePosition = Input.mousePosition.normalized; //store the mouse pointer position

        //calculate the input vector
        KeyboardInput = (Vector3.forward * verticalInput) + (Vector3.right * horizontalInput);

        if(KeyboardInput != Vector3.zero)
        {
            player.Animator.SetInteger("Movement", 1);
        }
        else
        {
            player.Animator.SetInteger("Movement", 0);
        }
    }
}

