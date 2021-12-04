using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Character movement (running, jumping, animation),
/// Shooting (dealing/receiving damage), Lives panel.
/// </summary>
public class MovementCharacter : Unit
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 17f;
    [SerializeField] private int maxLives = 5;
    [SerializeField] private Joystick joystick;
    [Header("Sound Name")]
    [SerializeField] private string shoot = "shooting";
    [SerializeField] private string receivedDamage = "received damage";
    [SerializeField] private string death = "zvuk smerti";

    private Bullet bullet;
    private int lives = 5;
    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= maxLives)
            {
                lives = value;
                livesBar.Refresh();
            }
        }
    }
    private int sumCoins;
    public int SumCoins
    {
        get { return sumCoins; }
        set { sumCoins = value; }
    }

    private bool isGrounded = false;
    private string nameTagPlatform = "MovePlatform";
    private LivesBar livesBar;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private UnitCollider unitCollider;
    private AudioManager audioManager;

    private void Start()
    {
        unitCollider = FindObjectOfType<UnitCollider>();
        livesBar = FindObjectOfType<LivesBar>();
        bullet = Resources.Load<Bullet>("Bullet/Bullet");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Freak out! No audio manager found in the scene.");
        }
    }

    void Update()
    {
        CheckGround();
        animator.SetFloat("Speed", 0);

        if (Lives <= 0)
        {
            StartCoroutine(Death());
            return;
        }
        else
        {
            if (Input.GetButton("Horizontal") && transform.position.y > -5f) // joystick.Horizontal != 0
            {
                Run();
            }

            if (transform.position.y <= -5f)
            {
                StartCoroutine(Fall());
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Shot();
            }
        }

    }

    /// <summary>
    /// Character movement
    /// </summary>
    void Run()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        //float moveInput = joystick.Horizontal;
        float velocity = Mathf.MoveTowards(transform.position.x, transform.position.x + moveInput, speed * Time.deltaTime);
        transform.position = new Vector2(velocity, transform.position.y);
        sprite.flipX = moveInput < 0f;

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    /// <summary>
    /// Character jumping
    /// </summary>
    void Jump()
    {
        rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Shooting (creating a new bullet)
    /// </summary>
    void Shot()
    {
        Vector3 position = transform.position;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        audioManager.Play(shoot);
        //We assign bullets to the parent (we say that your parent is the one who released you).
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }

    //public void OnShotButoonDown()
    //{
    //    if (Lives > 0)
    //    {
    //        Shot();
    //        Vector3 position = transform.position;
    //        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

    //        //We assign bullets to the parent (we say that your parent is the one who released you).
    //        newBullet.Parent = gameObject;
    //        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    //        shotSound.Play();
    //    }
    //}

    //public void OnJumpButtonDown()
    //{
    //    if (isGrounded && Lives > 0)
    //    {
    //        Jump();
    //        //rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
    //    }
    //}

    /// <summary>
    /// Overriding the method for ReceivDamage the character
    /// </summary>
    public override void ReceivDamage()
    {
        if (Lives > 0)
        {
            Lives--;
            rb.velocity = Vector3.zero;
            audioManager.Play(receivedDamage);
        }


        //Get hit to the side depending on the direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 5f);
        if (hit.collider.tag == "Monster")
        {
            rb.AddForce(-transform.right * 4f, ForceMode2D.Impulse);

        }
        else
        {
            rb.AddForce(transform.right * 4f, ForceMode2D.Impulse);
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        //audioManager.Stop(receivedDamage);
        audioManager.Play(death);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Fall()
    {
        Lives--;
        audioManager.Play(receivedDamage);
        Vector3 pos = new Vector3(transform.position.x + (sprite.flipX ? 1.0f : -1.0f) * 2f, -5f, 0f);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.up);
        Vector3 position;
        if (hit.collider != null)
        {
            position = new Vector3(hit.transform.position.x, hit.transform.position.y + 2.5f, 0f);
            transform.position = position;
        }
        else
        {
            position = new Vector3(transform.position.x + (sprite.flipX ? 1.0f : -1.0f) * 5f, 1f, 0f);
            transform.position = position;
        }
        sprite.enabled = false;
        GetComponent<MovementCharacter>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        GetComponent<MovementCharacter>().enabled = true;
        sprite.enabled = true;
    }

    /// <summary>
    /// Checking the ground to create a jump animation
    /// </summary>
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        isGrounded = colliders.Length > 1;

        if (!isGrounded)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    /// <summary>
    /// Take Bullet Damage
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();

        if (bullet && bullet.Parent != gameObject)
        {
            Lives--;
            audioManager.Play(receivedDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == nameTagPlatform)
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == nameTagPlatform)
        {
            this.transform.parent = null;
        }
    }
}
