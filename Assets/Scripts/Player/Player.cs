using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;

    private Rigidbody2D _rigid;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _speed = 3.5f;
    private bool _resetJump = false;
    private bool _grounded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        // If the player presses the left click, animation attacks start
         if(CrossPlatformInputManager.GetButtonDown("A Button") && IsGrounded() == true)
          {
            _playerAnim.Attack(); // Start attack animation
        }

    }

    void Movement()
    {
        //float move = Input.GetAxisRaw("Horizontal"); // Save the player's movement and the direction it is going
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); // Mobile Move

        _grounded = IsGrounded(); // For jump animation, to return to normal

        // Changing direction depending on the player's movement left or right
        if (move > 0) Flip(true);
        else if (move < 0) Flip(false);

        // If player press the space and Is Grounded is true, the player jump // Input.GetKeyDown(KeyCode.Space) For Pc
        if (CrossPlatformInputManager.GetButtonDown("B Button") && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce); // New position for player
            StartCoroutine(ResetJumpRoutine()); // Start a IEnumerator
            _playerAnim.Jump(true); // Start jump animation
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y); // New position for player
        _playerAnim.Move(move);// Start move animation
    }

    // Check if the player is on ground
    bool IsGrounded()
    {
        // Make a line and check if it collides with layerMask
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 6);
        
        // Check status of collider
        if (hitInfo.collider != null)
        {
            if(_resetJump == false)
            {
                _playerAnim.Jump(false); // Stop jump Animation
                return true; 
            }
        }

        return false;
    }

    // Change face direction of the player
    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            // Change the position of sword effects
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            // Change the postion of sword effects
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    // Suspend the execution of the code for the jump
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }

        Debug.Log("Player: Attack()");
        Health--;
        UIManager.Instance.UpdateLives(Health);

        if(Health < 1)
        {
            _playerAnim.Death();
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}