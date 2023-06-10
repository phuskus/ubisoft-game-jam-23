using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSmoothFactor;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator staminaBarAnimator;

    [SerializeField] private Transform cursorPosition;

    [SerializeField] private float weightChangeSpeed;

    private float moveSpeed;
    //private float stamina;

    private bool outOfStamina;

    private void Start()
    {
        moveSpeed = GameData.PlayerData.playerWalkSpeed;
        GameData.PlayerData.playerStamina = GameData.PlayerData.playerMaxStamina;
    }

    private void Update()
    {
        Vector3 targetVector = new Vector3(playerInput.KeyboardInput.x, 0f, playerInput.KeyboardInput.z); //get the target to which player should move

        if(targetVector != Vector3.zero)
        {
            MoveTowardsTarget(targetVector); //actually move the player

            //change the animation layer weight according to the movement state
            playerAnimator.SetLayerWeight(1, Mathf.MoveTowards(playerAnimator.GetLayerWeight(1), 1f, weightChangeSpeed * Time.deltaTime));
        }
        else if(targetVector == Vector3.zero)
        {
            //change the animation layer weight according to the movement state
            playerAnimator.SetLayerWeight(1, Mathf.MoveTowards(playerAnimator.GetLayerWeight(1), 0f, weightChangeSpeed * Time.deltaTime));
        }

        if(Input.GetKey(KeyCode.LeftShift) && !outOfStamina) //while the player has sprint button pressed
        {
            if (GameData.PlayerData.playerStamina > 0f) //if the player is not exhausted
            {
                GameData.PlayerData.playerStamina = Mathf.MoveTowards(GameData.PlayerData.playerStamina, 0f, GameData.PlayerData.staminaDepletionSpeed * Time.deltaTime); //gradually decrease player's stamina
                moveSpeed = Mathf.MoveTowards(moveSpeed, GameData.PlayerData.playerRunSpeed, 10f * weightChangeSpeed * Time.deltaTime); //set the player's movement speed to sprint speed value
                playerAnimator.SetLayerWeight(2, Mathf.Lerp(playerAnimator.GetLayerWeight(2), 1f, weightChangeSpeed * Time.deltaTime)); //smoothly transition the player into the running animations
            }
            else if (GameData.PlayerData.playerStamina <= 0f) //if player uses up all of his stamina
            {
                moveSpeed = GameData.PlayerData.playerWalkSpeed; //reset his movement speed back to walk speed value
                playerAnimator.SetLayerWeight(2, Mathf.Lerp(playerAnimator.GetLayerWeight(2), 0f, weightChangeSpeed * Time.deltaTime)); //smootly transition the player back to walking animations
                outOfStamina = true; //turn on the exhausted state
                staminaBarAnimator.SetBool("PlayerExhausted", true);
            }
        }
        else //while the sprint button is released
        {
            GameData.PlayerData.playerStamina = Mathf.MoveTowards(GameData.PlayerData.playerStamina, GameData.PlayerData.playerMaxStamina, GameData.PlayerData.staminaDepletionSpeed * Time.deltaTime); //gradually increase player's stamina
            moveSpeed = GameData.PlayerData.playerWalkSpeed; //set the player's movement speed to walk value

            playerAnimator.SetLayerWeight(2, Mathf.Lerp(playerAnimator.GetLayerWeight(2), 0f, weightChangeSpeed * Time.deltaTime)); //smootly transition the player back to walking animations if needed

            if (GameData.PlayerData.playerStamina >= GameData.PlayerData.playerMaxStamina)
            {
                staminaBarAnimator.SetBool("PlayerExhausted", false);
                outOfStamina = false;
            }
        }
    }

    private void MoveTowardsTarget(Vector3 target)
    {
        float distanceToCursor = (cursorPosition.position - transform.position).magnitude;
        float speed = moveSpeed * Time.deltaTime; //calculate the move speed over time
        Vector3 targetPosition = transform.position + target * speed; //calculate the target where to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
        float distanceToCursor2 = (cursorPosition.position - transform.position).magnitude;
    }
}
