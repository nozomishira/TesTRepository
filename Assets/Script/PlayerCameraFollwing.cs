using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollwing : MonoBehaviour
{
    private GameObject player;
    private Vector3 prevPlayerPos;
    private Vector3 posVector;
    public float scale = 3.0f;
    public float cameraSpeed = 1000.0f;

    void Start()
    {
        player = GameObject.Find("SheepPlayer");
        prevPlayerPos = new Vector3(0, 0, -3);
    }

    void Update()
    {
        Vector3 currentPlayerPos = player.transform.position;
        Vector3 backVector = (prevPlayerPos - currentPlayerPos).normalized;
        posVector = (backVector == Vector3.zero) ? posVector : backVector;
        Vector3 targetPos = currentPlayerPos + scale * posVector;
        targetPos.y = targetPos.y+3;
        targetPos.x -= 10;
        this.transform.position = Vector3.Lerp(
            this.transform.position+transform.forward*-0.1f,
            targetPos,
            cameraSpeed * Time.deltaTime
        );
        this.transform.LookAt(player.transform.position);
        prevPlayerPos = player.transform.position;
    }
}
