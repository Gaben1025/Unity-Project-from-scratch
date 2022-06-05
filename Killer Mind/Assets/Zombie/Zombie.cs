using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private bool facingRight = false;
    private Vector2 currentPosition,newPostion;
    private float positionDifference;
    private Transform target;


    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;

    private float canAttack;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    private void Update()
    {
        currentPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        newPostion = transform.position;

        positionDifference = newPostion.x - currentPosition.x;

        if (newPostion.x < currentPosition.x && facingRight)
        {
            flip();
        }
        else if (newPostion.x > currentPosition.x && !facingRight)
        {
            flip();
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                collision.gameObject.GetComponent<Hero>().UpdateHealth(-attackDamage);
                collision.gameObject.GetComponent<Hero>().getHit();

                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }
}
