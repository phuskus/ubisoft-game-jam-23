using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class Movement : SingletonMono<Movement>
{
    private float moveSpeed;
    private bool outOfStamina;
    public bool PlayerHit;
    private Vector3 targetVector;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Player.Settings);
        //Player.Settings.playerStamina = Player.Settings.playerMaxStamina;
    }

    private void Update()
    {
        targetVector = new Vector3(Player.Input.KeyboardInput.x, 0f, Player.Input.KeyboardInput.z).normalized; //get the target to which player should move

        if(targetVector != Vector3.zero && !PlayerHit)
        {
            MoveTowardsTarget(targetVector); //actually move the player
        }

        #region << Useless >>
        //if(Input.GetKey(KeyCode.LeftShift) && !outOfStamina) //while the player has sprint button pressed
        //{
        //    if (Player.Settings.playerStamina > 0f) //if the player is not exhausted
        //    {
        //        Player.Settings.playerStamina = Mathf.MoveTowards(Player.Settings.playerStamina, 0f, Player.Settings.staminaDepletionSpeed * Time.deltaTime); //gradually decrease player's stamina
        //        moveSpeed = Mathf.MoveTowards(moveSpeed, Player.Settings.playerRunSpeed, Player.Settings.Acceleration * Time.deltaTime); //set the player's movement speed to sprint speed value
        //    }
        //    else if (Player.Settings.playerStamina <= 0f) //if player uses up all of his stamina
        //    {
        //        moveSpeed = Player.Settings.playerWalkSpeed; //reset his movement speed back to walk speed value
        //        outOfStamina = true; //turn on the exhausted state
        //    }
        //}
        //else //while the sprint button is released
        //{
        //    Player.Settings.playerStamina = Mathf.MoveTowards(Player.Settings.playerStamina, Player.Settings.playerMaxStamina, Player.Settings.staminaDepletionSpeed * Time.deltaTime); //gradually increase player's stamina
        //    moveSpeed = Player.Settings.playerWalkSpeed; //set the player's movement speed to walk value

        //    if (Player.Settings.playerStamina >= Player.Settings.playerMaxStamina)
        //    {
        //        outOfStamina = false;
        //    }
        //}
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Player.Settings.WallLayer)
        {
            Vector3 direction = transform.position - other.transform.position;
            StartCoroutine(KnockBack(direction.normalized));
        }
    }

    private void MoveTowardsTarget(Vector3 target)
    {
        //float speed = moveSpeed * Time.deltaTime; //calculate the move speed over time
        float speed = Player.Settings.playerWalkSpeed * Time.deltaTime; //calculate the move speed over time
        Vector3 targetPosition = transform.position + target * speed; //calculate the target where to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
    }

    IEnumerator KnockBack(Vector3 direction)
    {
        PlayerHit = true;
        float currentSpeed = Player.Settings.KnockBackPower;

        while(currentSpeed > 0f)
        {
            Vector3 targetPosition = transform.position + direction * currentSpeed; //calculate the target where to move
            targetPosition.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, Player.Settings.KnockBackFade * Time.deltaTime);
            yield return null;
        }
        Debug.Log("kita");
        PlayerHit = false;
    }
}
