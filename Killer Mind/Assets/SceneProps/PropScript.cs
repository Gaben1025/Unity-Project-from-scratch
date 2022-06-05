using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropScript : MonoBehaviour
{

    private Vector2 direction;
    private Hero player;
    [SerializeField] private float _launchPower = 500;

    //Click
    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<PolygonCollider2D>().enabled = false;

        if (Input.GetMouseButtonDown(0))
        {
            if ((Input.GetMouseButtonDown(0)&&Input.GetMouseButtonDown(1)))
            {

                if (transform.position.x < player.transform.position.x)
                {
                    direction = transform.position - player.transform.position;
                }

                if (transform.position.x > player.transform.position.x)
                {
                    direction = transform.position + player.transform.position;
                }

                GetComponent<Rigidbody2D>().AddForce(direction * _launchPower);
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
