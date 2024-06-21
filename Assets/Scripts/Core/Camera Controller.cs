
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room Camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        // Room Camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        // Follow player
        float desiredX = player.position.x + lookAhead;
        float clampedX = Mathf.Clamp(desiredX, 0.28f, 16.94f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Update lookAhead
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);



        //0.28, 16.94
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
