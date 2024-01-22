using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class testmove : MonoBehaviour
{
    Vector2 movement = Vector2.zero;
    Rigidbody2D _rigid;
    void Start()
    {
        //_rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {

        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //movement.x = Input.GetAxis("Horizontal") * 4;
        //movement.y = _rigid.velocity.y;
        //_rigid.velocity = movement;
    }
}
