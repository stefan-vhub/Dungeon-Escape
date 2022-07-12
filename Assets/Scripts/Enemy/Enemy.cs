using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;

    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool isDead = false;

    protected bool isHit = false;

    protected Player player;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Waiting for the animation to finish
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false || isDead == true)
        {
            return;
        }
        
        if(isDead == false)
            Movement();
    }

    // Enemy movement, change the face and animation
    public virtual void Movement()
    {
        // Change the face
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        // Movement
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle"); // Start idle animation
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle"); // Start idle animation
        }

        if(isHit == false)
        {
            // The enemy goes to the currentTarget
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        // Check for distance between palyer and enemy
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if(distance > 2.0f)
        {
           isHit = false;
           anim.SetBool("InCombat", false);
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = false; // face right
        }
        else if (direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = true; // face laft
        }
    }
}
