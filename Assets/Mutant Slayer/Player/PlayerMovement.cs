using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private Transform gfx;


    private void Update()
    {
        if (joystick.Direction != Vector2.zero)
        {
            Movement();
        }
    }

    private void Movement()
    {
        transform.Translate(speed * Time.deltaTime * joystick.Direction);

        gfx.transform.localScale = new Vector3(joystick.Horizontal > 0 ? 1 : -1, 1, 1);
    }
}
