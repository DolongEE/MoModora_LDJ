using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class Creature : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigid;
    protected Animator _animator;
    protected Collider2D _collider;

    public int _hp;
    public int _maxHp;
    public int _attackDamage;

    public bool _dead;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _hp = 10;
        _maxHp = _hp;
    }

    public void Damaged(int? damage)
    {
        if (_hp <= 0)
        {
            _dead = true;
            return;
        }

    }

    protected void FlipSprite(float move)
    {
        if (move > 0)
            _spriteRenderer.flipX = false;
        else if (move < 0)
            _spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int? damage = collision.transform.GetComponent<Creature>()?._attackDamage ?? 0;
        if (collision.transform.CompareTag("Monster"))
        {
            Damaged(damage);
            Debug.Log($"{damage} Ãæµ¹!");
        }
        if (collision.transform.CompareTag("PlayerWeapon"))
        {
            Damaged(damage);
        }
    }
}
