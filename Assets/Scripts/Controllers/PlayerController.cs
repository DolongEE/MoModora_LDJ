using System.Collections;
using UnityEngine;
using static Define;

public class PlayerController : Creature
{
    private Coroutine currentCoroutine;
    private CapsuleCollider2D _myCollider;

    private string _enumeratorName;
    private bool _isFlip;
    [SerializeField]
    private bool _isGround = true;
    private bool _isRoll;
    private bool _isAttack;

    #region Input
    private bool _rollKey;
    private bool _attackLeaf;
    private bool _attackBow;
    private bool _jumpKey;
    private bool _crouchKey;
    private float _horizontalAxis; 
    #endregion

    private float _currentY;
    private float _speed;
    private int _attackNum;
    private int _direction;
        
    [SerializeField]
    public float _moveSpeed;
    [SerializeField]
    public float _jumpForce;
    private Vector2 movement;

    [SerializeField]
    private PlayerStates _playerState;
    private PlayerStates _lastState;

    // 플레이어 FSM
    private PlayerStates PlayerState 
    { 
        get {  return _playerState; }
        set
        {
            _playerState = value;
            
            if (_lastState == _playerState)
                return;
            switch (_playerState)
            {
                case PlayerStates.Death:
                    AnimationChange(nameof(AnimationDie));
                    break;
                case PlayerStates.Idle:                    
                    AnimationChange(nameof(AnimationIdle));
                    break;
                case PlayerStates.Move:
                    AnimationChange(nameof(AnimationMove));
                    break;
                case PlayerStates.Jump:
                    AnimationChange(nameof(AnimationJump));
                    break;
                case PlayerStates.Crouch:
                    AnimationChange(nameof(AnimationCrouch));
                    break;
                case PlayerStates.Attack:
                    AnimationChange(nameof(AnimationAttack));
                    break;
                case PlayerStates.Bow:
                    AnimationChange(nameof(AnimationBow));
                    break;
                case PlayerStates.Roll:
                    AnimationChange(nameof(AnimationRoll));
                    break;
            }
        }
    }
    
    private void Start()
    {
        _myCollider = GetComponent<CapsuleCollider2D>();
        PlayerState = PlayerStates.Idle;
    }

    private void Update()
    {
        if (_dead)
            return;

        if(Input.GetKeyDown(KeyCode.P)) { _dead = true; }

        SetInputKey();
        UpdateState();
        FlipSprite();             
    }

    private void FixedUpdate()
    {
        if (_dead)
            return;

        if (_isRoll)
        {
            Roll();
            return;
        }

        Move();

        if (!_isGround)
        {
            GroundCheck();
        }
    }

    private void SetInputKey()
    {
        _rollKey = Input.GetKeyDown(KeyCode.Q);
        _horizontalAxis = Input.GetAxis("Horizontal");
        _attackLeaf = Input.GetKeyDown(KeyCode.S);
        _attackBow = Input.GetKeyDown(KeyCode.D);
        _jumpKey = Input.GetKeyDown(KeyCode.A);
        _crouchKey = Input.GetKey(KeyCode.DownArrow);
    }

    private void UpdateState()
    {
        DamagedState();

        RollState();
        if (_isRoll)
            return;

        AttackState();
        if (_isAttack)
            return;

        if (_jumpKey && _isGround)
        {
            _isGround = false;
            Jump();
            PlayerState = PlayerStates.Jump;
        }

        if (_crouchKey && _isGround)
        {
            _speed = 0;
            PlayerState = PlayerStates.Crouch;
            return;
        }

        if (Absolute(_horizontalAxis) > 0)
        {
            _speed = _moveSpeed;
            if (_isGround)
                PlayerState = PlayerStates.Move;
        }
    }

    private void RollState()
    {
        if (_rollKey && _isGround && _isAttack == false)
        {
            _isRoll = true;
            _speed = 0;
            PlayerState = PlayerStates.Roll;
        }
    }
    private void AttackState()
    {
        if(_attackLeaf || _attackBow)
        {
            _speed = 0;
            _isAttack = true;
            if (_attackLeaf)
            {
                _attackNum++;
                PlayerState = PlayerStates.Attack;
            }
            if (_attackBow)
            {
                PlayerState = PlayerStates.Bow;
            }
        }       
    }
    private void DamagedState()
    {
        if (_dead)
            PlayerState = PlayerStates.Death;

    }

    private void Roll()
    {
        if (_direction.Equals(0))
            _direction = (_spriteRenderer.flipX ? -1 : 1);

        gameObject.layer = LayerMask.NameToLayer("Ghost");

        movement.x = 0.08f * _direction;
        movement.y = _rigid.velocity.y;
        transform.Translate(movement);
    }
    private void Move()
    {
        movement.x = _horizontalAxis * _speed;
        movement.y = _rigid.velocity.y;
        _rigid.velocity = movement;
    }
    private void Jump()
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
    }

    #region 애니메이션

    private IEnumerator AnimationIdle()
    {
        ResetValue();
        int rand;
        while (true)
        {
            _animator.CrossFade("Idle", 0.1f);
            yield return Managers.Coroutine.WaitForSeconds(5f);
            rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    _animator.CrossFade("IdleKick", 0.1f);
                    break;
                case 1:
                    _animator.CrossFade("IdleLook", 0.1f);
                    break;
                case 2:
                    _animator.CrossFade("IdleYawn", 0.1f);
                    break;
            }
            yield return Managers.Coroutine.WaitForSeconds(0.1f);
            yield return Managers.Coroutine.WaitForSeconds(_animator);
        }
    }
    private IEnumerator AnimationMove()
    {
        bool isPrev = false;
        bool isRun = false;
        bool check = false;
        float time = 0;
        while (true)
        {
            if (!Input.anyKey && !check)
            {
                check = true;
                time = Time.time;
            }

            if (time + 0.1f < Time.time && check)
            {
                _animator.CrossFade("RunBrake", 0.1f);
                yield return Managers.Coroutine.WaitForSeconds(0.1f);
                yield return Managers.Coroutine.WaitForSeconds(_animator);
                PlayerState = PlayerStates.Idle;
                yield break;
            }

            if (_isFlip != _spriteRenderer.flipX && isPrev)
            {
                _animator.CrossFade("RunTurn", 0.1f);
                yield return Managers.Coroutine.WaitForSeconds(0.1f);
                yield return Managers.Coroutine.WaitForSeconds(_animator);
                _isFlip = _spriteRenderer.flipX;
                isRun = false;
                check = false;
            }

            if (!isPrev) 
            {             
                isPrev = true;                
                _animator.CrossFade("RunPrev", 0.1f);                
                yield return Managers.Coroutine.WaitForSeconds(0.1f);
                yield return Managers.Coroutine.WaitForSeconds(_animator);
            }

            if (!isRun)
            {
                _isFlip = _spriteRenderer.flipX;
                isRun = true;
                _animator.CrossFade("Running", 0.1f);
            }
            yield return null;
        }
    }
    private IEnumerator AnimationJump()
    {
        bool isFallDown = false;
        _animator.CrossFade("Jump", 0.1f);
        Managers.Sound.Play(Sound.Effect, "Jump");
        yield return Managers.Coroutine.WaitForSeconds(0.1f);
        yield return Managers.Coroutine.WaitForSeconds(_animator);
        while (true)
        {
            if (_rigid.velocity.y < 0 && !isFallDown)
            {
                isFallDown = true;
                _animator.CrossFade("FallDown", 0.1f);
                yield return Managers.Coroutine.WaitForSeconds(0.1f);
                yield return Managers.Coroutine.WaitForSeconds(_animator);

                _animator.CrossFade("Falling", 0.1f);
            }

            if(_isGround)
            {
                _animator.CrossFade("LandSoft", 0.1f);
                yield return Managers.Coroutine.WaitForSeconds(0.1f);
                yield return Managers.Coroutine.WaitForSeconds(_animator);
                PlayerState = PlayerStates.Idle;

                yield break;
            }
            yield return null;
        }
    }
    private IEnumerator AnimationCrouch()
    {
        _animator.CrossFade("Crouch", 0.1f);
        while (true)
        {
            if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                _animator.CrossFade("CrouchUp", 0.1f);
                yield return Managers.Coroutine.WaitForSeconds(0.1f);
                yield return Managers.Coroutine.WaitForSeconds(_animator);
                _crouchKey = false;
                PlayerState = PlayerStates.Idle;
                break;
            }
            yield return null;
        }
        
        yield break;
    }
    private IEnumerator AnimationAttack()
    {
        float time = Time.time;
        int lastnum = 0;
        bool check = false;

        while (true)
        {
            if (!_isGround && !check)
            {
                check = true;
                _animator.CrossFade($"AttackAir", 0.1f);
                Managers.Sound.Play(Sound.Effect, "Leaf2");
                yield return Managers.Coroutine.WaitForSeconds(0.1f);
                yield return Managers.Coroutine.WaitForSeconds(_animator);
            }
            else
            {
                if (time + 0.5f < Time.time)
                {
                    _attackNum = 0;
                    PlayerState = PlayerStates.Idle;
                    yield break;
                }
                else if (lastnum == 2 && _attackNum == 3)
                {
                    time = Time.time;
                    _animator.CrossFade($"Attack3", 0.1f);
                    Managers.Sound.Play(Sound.Effect, "Leaf3");
                    yield return Managers.Coroutine.WaitForSeconds(0.1f);
                    yield return Managers.Coroutine.WaitForSeconds(_animator);
                }
                else if (lastnum == 1 && _attackNum == 2)
                {
                    time = Time.time;
                    lastnum++;
                    _animator.CrossFade($"Attack2", 0.1f);
                    Managers.Sound.Play(Sound.Effect, "Leaf2");
                    yield return Managers.Coroutine.WaitForSeconds(0.1f);
                    yield return Managers.Coroutine.WaitForSeconds(_animator);
                }
                else if (lastnum == 0 && _attackNum == 1)
                {
                    _animator.CrossFade($"Attack1", 0.1f);
                    Managers.Sound.Play(Sound.Effect, "Leaf");
                    lastnum++;
                    yield return Managers.Coroutine.WaitForSeconds(0.1f);
                    yield return Managers.Coroutine.WaitForSeconds(_animator);
                }
            }            
            yield return null;
        }
    }
    private IEnumerator AnimationBow()
    {
        Managers.Sound.Play(Sound.Effect, "Arrow");
        if (_crouchKey)
        {
            _animator.CrossFade("BowCrouch", 0.1f);
            yield return Managers.Coroutine.WaitForSeconds(0.1f);
            yield return Managers.Coroutine.WaitForSeconds(_animator);
        }
        else if (!_isGround)
        {
            _animator.CrossFade("BowAir", 0.1f);
            yield return Managers.Coroutine.WaitForSeconds(0.1f);
            yield return Managers.Coroutine.WaitForSeconds(_animator);
        }
        else
        {
            _animator.CrossFade("Bow", 0.1f);
            yield return Managers.Coroutine.WaitForSeconds(0.1f);
            yield return Managers.Coroutine.WaitForSeconds(_animator);
        }
        while(!_isGround)
        {
            yield return null;
        }
        PlayerState = PlayerStates.Idle;
        yield return null;
    }
    private IEnumerator AnimationRoll()
    {
        _animator.CrossFade("Roll", 0.1f);
        Managers.Sound.Play(Sound.Effect, "Roll");
        yield return Managers.Coroutine.WaitForSeconds(0.1f);
        yield return Managers.Coroutine.WaitForSeconds(_animator);

        PlayerState = PlayerStates.Idle;
        yield return null;
    }
    private IEnumerator AnimationDie()
    {
        Managers.Sound.Play(Sound.Effect, "Death");
        _animator.CrossFade("Death", 0.1f);
        yield return Managers.Coroutine.WaitForSeconds(0.1f);
        yield return Managers.Coroutine.WaitForSeconds(_animator);        
        Managers.UI.ShowPopupUI<UI_PlayerDiePopUp>();
        gameObject.SetActive(false);
        StopAllCoroutines();
        yield return null;
    }
    private void AnimationChange(string name)
    {
        if (name.Equals(_enumeratorName))
            return;

        if (currentCoroutine != null)
        {
            StopCoroutine(_enumeratorName);
            currentCoroutine = null;
        }

        _lastState = PlayerState;
        _enumeratorName = name;
        currentCoroutine = StartCoroutine(name);
    }
    private void ResetValue()
    {
        _direction = 0;
        _isRoll = false;
        _isAttack = false;        
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    #endregion

    private void SoundMove()
    {
        Managers.Sound.Play(Sound.Effect, "grass_footstep");
    }
    
    private void FlipSprite()
    {
        if (_isAttack || _isRoll) 
            return;

        float dir = _horizontalAxis;
        if (dir > 0)
            _spriteRenderer.flipX = false;
        else if (dir < 0)
            _spriteRenderer.flipX = true;
    }    
    private void GroundCheck()
    {
        if (_rigid.velocity.y < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rigid.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
                _isGround = true;
        }
    }
    private float Absolute(float value)
    {
        float result;
        if (value < 0)
            result = -value;
        else
            result = value;
        return result;
    }

}
