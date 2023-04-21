using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csKahoCtrl : MonoBehaviour
{
    public float m_maxSpeed;
    public float m_jumpPower;
    public csChatUI script_CahtUI;
    public csTransferMap script_TransferMap;
    public GameObject scanObject;

    Vector2 direction;
    public bool isJump;

    //this.gameobject 컴포넌트 변수
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer leaf_spriteRenderer;
    private Animator anim;

    private bool isCrouch;
    private int attackCount;
    private int keyCount;


    public float horizon;
    public float vertic;
    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        leaf_spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        isJump = false;
    }

    void Update()
    {
        horizon = Input.GetAxis("Horizontal");
        vertic = Input.GetAxis("Vertical");

        if (!script_CahtUI.isAction)
        {
            if (Mathf.Abs(horizon) > 0)
            {
                anim.SetBool("isRun", true);
                leaf_spriteRenderer.flipX = spriteRenderer.flipX = horizon < 0;
            }
            else
            {
                //rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0, rigid.velocity.y);
                anim.SetBool("isRun", false);
            }

            //Crouch
            isCrouch = (vertic < 0) ? true : false;
            anim.SetBool("isCrouch", isCrouch);

            //Jump
            if (Input.GetButtonDown("Jump") && !isJump)
            {
                isJump = true;
                rigid.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJump", true);                
            }
            if (Input.GetKeyDown(KeyCode.S) && isJump)
            {
                anim.SetTrigger("attack_air");
            }

            //Attack
            if (!isJump)
            {
                if (keyCount > 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Kaho_Attack3") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f)
                {
                    attackCount = 0;
                    anim.SetInteger("attack", attackCount);
                    keyCount = 0;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    keyCount++;
                    attackCount++;
                    anim.SetInteger("attack", attackCount);
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Kaho_Attack" + attackCount.ToString()) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f)
                {
                    attackCount = 0;
                    anim.SetInteger("attack", attackCount);
                    keyCount = 0;
                }
            }
        }


        //Scan Object & Chat
        if (scanObject == null)
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && scanObject.CompareTag("Door"))
        {
            script_TransferMap.TransferMap(this.transform, scanObject);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            script_CahtUI.Action(scanObject);
        }
    }

    private void FixedUpdate()
    {
        anim.SetFloat("fall", rigid.velocity.y);

        horizon = Input.GetAxis("Horizontal");
        vertic = Input.GetAxis("Vertical");
        //UI Open시 움직임 제한
        if (!script_CahtUI.isAction)
        {
            if (!isCrouch)
            {
                //Move
                KahoMove();
            }
            //Scan to ray
            CheckObject();
            //JumpCheck
            JumpCheck();
        }
    }



    private void KahoMove()
    {
        //Move
        rigid.AddForce(Vector2.right * horizon, ForceMode2D.Impulse);

        if (rigid.velocity.x > m_maxSpeed)
        {
            rigid.velocity = new Vector2(m_maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < m_maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(m_maxSpeed * (-1), rigid.velocity.y);
        }
    }

    private void CheckObject()
    {
        //Ray
        RaycastHit2D rayObject;
        if (horizon > 0)
        {
            direction = Vector2.right;
        }
        else if (horizon < 0)
        {
            direction = Vector2.left;
        }
        rayObject = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Object"));
        Debug.DrawRay(transform.position, direction, new Color(1, 0, 0));
        if (rayObject.collider != null)
        {
            scanObject = rayObject.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
    private void JumpCheck()
    {
        //JumpCheck
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(transform.position, Vector3.down * 1.4f, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 1.4f, LayerMask.GetMask("Ground"));

            if (rayHit.collider != null)
            {
                anim.SetBool("isJump", false);
                isJump = false;
            }
        }
    }
}
