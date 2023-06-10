using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] private LayerMask layer;

    private Vector3 targetMousePosition;

    Ray ray;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Player.Settings.GroundLayer))
        {
            transform.LookAt(Player.CursorObject.transform.position, Vector3.up);
            mousePosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            float mouseDistance = Vector3.Distance(transform.position, mousePosition);

            if (mouseDistance <= Player.Settings.cameraExtendRadius)
            {
                targetMousePosition = mousePosition;
            }
            else
            {
                targetMousePosition = transform.position + (mousePosition - transform.position).normalized * Player.Settings.cameraExtendRadius;
            }

            Player.CursorObject.transform.position = Vector3.MoveTowards(Player.CursorObject.transform.position, targetMousePosition, Player.Settings.TurnRate * Time.deltaTime);
        }
    }
}
