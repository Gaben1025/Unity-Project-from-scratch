using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    private Rigidbody2D body;
    public  Animator anim;
    private float movement, jumps = 0f, health = 0f, lastYPos;
    private bool facingRight = true, death = false;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float movementModifier;
    [SerializeField] private float jumpModifier;
    [SerializeField] private float maxJumpAmount = 1f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        health = maxHealth;
        death = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(movement * movementModifier, body.velocity.y);
        if (death == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.Play("running");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.Play("running");
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.F))
            {
                anim.Play("Idle");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.Play("hitting");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumps++;
                if (jumps <= maxJumpAmount)
                {
                    body.velocity = new Vector2(movement * movementModifier, jumpModifier);
                    anim.Play("jumping");
                }
            }


            if (movement < 0 && facingRight)
            {
                flip();
            }
            else if (movement > 0 && !facingRight)
            {
                flip();
            }


            if (transform.position.y == lastYPos)
            {
                jumps = 0f;
            }
            lastYPos = transform.position.y;
        }
    }

    public void UpdateHealth(float mod)
    {
        health+=mod;

        if (health > maxHealth)//Makes sure the heath doesnt go over the max
        {
            health = maxHealth;
        }
        else if (health <= 0f)
        {
            health=0f;
            playerDie();
        }
    }

    private void playerDie()
    {
        if (death == false)
        {
            anim.Play("dying");
            anim.Play("dead");
            body.bodyType = RigidbodyType2D.Static;
            death = true;
        }
    }


    public void retry()
    {
        string currecntSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currecntSceneName);
        Debug.Log("Player Respawn");
    }

    public void getHit()
    {
        if (body.bodyType == RigidbodyType2D.Dynamic)
        {
            anim.Play("taking_damage");
            anim.Play("Idle");
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
