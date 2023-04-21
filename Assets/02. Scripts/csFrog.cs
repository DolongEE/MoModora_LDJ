using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csFrog : MonoBehaviour
{
    public BoxCollider2D Map2Col;

    private Animator anim;  

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        Map2Col.enabled = false;
    }

    private void Update()
    {
        if(csQuestManager.instance.questId == 40)
        {
            FrogDie();
        }
    }

    public void FrogDie()
    {
        anim.SetTrigger("Death");
    }

    public void Frog_End()
    {
        Map2Col.enabled = true;
        Destroy(this.gameObject);
    }
}
