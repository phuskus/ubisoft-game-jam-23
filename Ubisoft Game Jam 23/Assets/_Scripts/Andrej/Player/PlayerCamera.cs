using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = Player.Instance.transform;
    }

    private void LateUpdate()
    {
        if (player == null)
            return;
        
        Vector3 targetPosition = (player.position + Player.CursorObject.transform.position) / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -Player.Settings.CameraHeadDistance + player.position.x, player.position.x + Player.Settings.CameraHeadDistance);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -Player.Settings.CameraHeadDistance + player.position.y, player.position.y + Player.Settings.CameraHeadDistance);
        targetPosition -= transform.forward * Player.Settings.CameraHeadDistance;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Player.Settings.CameraFollowSpeed * Time.deltaTime);
        //transform.position = targetPosition;
    }
}
