using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 7f;
    public Animator animator;

    void Start()
    {

    }

    void Update()
    {
        float speedX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float speedy = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        animator.SetFloat("movement", speedX * speed);
        
        if (speedX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (speedX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 position = transform.position;
        transform.position = new Vector3(speedX + position.x, speedy + position.y, position.z);
    }
}
