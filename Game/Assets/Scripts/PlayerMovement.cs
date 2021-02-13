using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float accleration;

    private float horizontalMove = 0.0f;
    private float verticalMove = 0.0f;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.gameState != GameManager.GameState.playing) return;

        horizontalMove = Mathf.Lerp(horizontalMove, Input.GetAxisRaw("Horizontal") * speed, accleration * Time.deltaTime);
        verticalMove = Mathf.Lerp(verticalMove, Input.GetAxisRaw("Vertical") * speed, accleration * Time.deltaTime);
        transform.position += transform.right * horizontalMove * Time.deltaTime;
        transform.position += transform.up * verticalMove * Time.deltaTime;
    }

    //private void FixedUpdate()
    //{
    //    controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
    //    jump = false;
    //}
}
