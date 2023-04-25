using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPlayerCtrl : MonoBehaviour
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
    private Animator anim;
   
    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        isJump = false;
    }

    void Update()
    {
        if (!script_CahtUI.isAction)
        {
            //flip
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }
            if (Input.GetButton("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            //Jump
            if (Input.GetButtonDown("Jump") && !isJump)
            {
                isJump = true;
                rigid.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJump", true);
            }
        }
        //Scan Object & Chat
        if(scanObject == null)
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
        float h = Input.GetAxis("Horizontal");
        //UI Open시 움직임 제한
        if (!script_CahtUI.isAction)
        {
            //Move
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            anim.SetFloat("run", Mathf.Abs(h));

            if (rigid.velocity.x > m_maxSpeed)
            {
                rigid.velocity = new Vector2(m_maxSpeed, rigid.velocity.y);
            }
            else if (rigid.velocity.x < m_maxSpeed * (-1))
            {
                rigid.velocity = new Vector2(m_maxSpeed * (-1), rigid.velocity.y);
            }

            //Ray
            RaycastHit2D rayObject;
            if (h > 0)
            {
                direction = Vector2.right;
            }
            else if (h < 0)
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

            //JumpCheck
            if (rigid.velocity.y < 0)
            {
                Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));

                RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

                if (rayHit.collider != null)
                {
                    anim.SetBool("isJump", false);
                    isJump = false;
                }
            }
        }
    }
}
