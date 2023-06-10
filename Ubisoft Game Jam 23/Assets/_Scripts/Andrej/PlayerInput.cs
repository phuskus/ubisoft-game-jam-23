using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 input;
    public Vector3 mousePosition;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float blendSpeed = 0.5f;

    private void Update()
    {
        //store the input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        mousePosition = Input.mousePosition.normalized; //store the mouse pointer position

        //calculate the input vector
        input = (Vector3.forward * verticalInput) + (Vector3.right * horizontalInput);

        Vector3 animationVector = transform.InverseTransformDirection(input);

        playerAnimator.SetFloat("MoveX", Mathf.Lerp(playerAnimator.GetFloat("MoveX"), animationVector.x, blendSpeed * Time.deltaTime));
        playerAnimator.SetFloat("MoveZ", Mathf.Lerp(playerAnimator.GetFloat("MoveZ"), animationVector.z, blendSpeed * Time.deltaTime));
    }
}

