using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class Bandit : MonoBehaviour {

    public bool active;
    public bool right;  
    private int row = 0;
    public GameObject attackTarget;
    public GameObject jumpTarget;
    public bool satirdaKal = true;
    public bool move = true;
    Enviorment env;
    Scene scene;
    private float ustSatirTimer = 1.1f;
    private bool isAlive = true;
    private GameObject attackCollider;
    private AttackSensor attackSensor;

    public GameObject hpObj;
    public GameObject infoText;

    public GameObject healthSprite;


    public int damage;
    public int health;
    public int team = 0;

    private int maxHealth;

    public bool isBoss = false; 


    private AudioSource hit, block, walk, tossing, hit_hero;


    private List<Color> colors = new List<Color>() { new Color(3f / 5f, 1f, 3f / 5f), new Color(1f, 3f / 5f, 3f / 5f), new Color(3f / 5f, 3f / 5f, 1f) };



    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 8.0f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;

    // Use this for initialization


    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        env = GameObject.Find("Enviorment").GetComponent<Enviorment>();
        scene = GameObject.Find("Enviorment").GetComponent<Scene>();
        attackCollider = transform.Find("AttackCollider").gameObject;
        attackSensor = transform.Find("AttackCollider").GetComponent<AttackSensor>();
        hit = GetComponents<AudioSource>()[0];
        hit_hero = GetComponents<AudioSource>()[1];
        block = GetComponents<AudioSource>()[2];
        walk = GetComponents<AudioSource>()[3];
        tossing = GetComponents<AudioSource>()[4];

        maxHealth = health;
    }

    private bool hurt = false;
    private float attackTimer = 1f;
    void Update () {

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (isAlive && active)
        {
            m_animator.SetInteger("AnimState", 0);


            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }
            m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

            

            AIFindTarget();
            AIFindPath();
        }    
    }


    private void setCan(int can)
    {
        //-6,388  == %0
        //0 == %100

        float a = 6.388f / maxHealth;
        float b = maxHealth - health;
        hpObj.transform.localPosition = new Vector2(-(a * b), hpObj.transform.localPosition.y);

    }

    public void canlan(int team, int health, int damage,Level.Levels level)
    {

        if (!(level == Level.Levels.LevelBoss))
        {
            GetComponent<SpriteRenderer>().color = colors[team - 1];
            GetComponent<SpriteRenderer>().DOFade(1, 1);
        }


        



        this.team = team;
        this.health = health;
        this.damage = damage;

    }

    private void attack()
    {
        if (attackTimer <= 0 && !hurt)
        {

            
            attackTimer = 1f;
            m_animator.SetTrigger("Attack");

            float time = 0.5f;
            if (isBoss)
            {
                time = 0.3f;
            }

            this.Wait(time, ()=>{
                if (!hurt)
                {
                    tossing.Play();
                    var clonedList = new List<GameObject>(attackSensor.objects);

                    foreach (GameObject obj in clonedList)
                    {
                        if (obj.GetComponent<Bandit>() != null)
                        {
                            obj.GetComponent<Bandit>().takeDamage(damage, this, null);
                        }
                        else if (obj.GetComponent<Hero>() != null)
                        {
                            obj.GetComponent<Hero>().takeDamage(damage, this);
                        }

                    }
                }
                
            });      
        }          
    }



    public void takeDamage(int damage, Bandit bandit, Hero hero)
    {
        hit_hero.Play();
        hit.Play();


        hurt = true;
        m_animator.SetTrigger("Hurt");
        this.Wait(1f, () => { hurt = false; });

        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 1.2f);
        GameObject aa = Instantiate(infoText, pos, Quaternion.identity, transform);
        aa.GetComponent<InfoText>().run(damage.ToString(),InfoText.Type.red);

        health -= damage;
        setCan(health);

        if (health <= 0)
        {
            die(bandit,hero);
        }
    }

    private void die(Bandit bandit, Hero hero)
    {
        walk.Pause();
        isAlive = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        GetComponent<BoxCollider2D>().isTrigger = true;
        attackCollider.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().DOFade(0f, 1f);
        healthSprite.SetActive(false);

        if (hero != null)
        {
            scene.KillCounter.increaseKill(0); 
        }
        else
        {
            scene.KillCounter.increaseKill(bandit.team);           
        }

            scene.respawn(null, this);  
    }



    public void AIjump(bool right, bool up)
    {
        if (m_grounded)
        {
            move = false;


            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
          

            if (up)
            {             
                if (right)
                {
                    this.Wait(0.6f, () => { sagSol(1f, 0.1f); });
                }
                else
                {

                    this.Wait(0.6f, () => { sagSol(-1f, 0.1f); });
                }
            }
            else
            {
                if (right)
                {
                     sagSol(1f, 0.1f);
                }
                else
                {

                    sagSol(-1f, 0.1f);
                }
            }

            this.Wait(1.3f, () => { move = true; });
        }       

    }
    private void sagSol(float inputX,float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;

            if (inputX > 0) { 
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else if (inputX < 0)
            {
                transform.localScale = new Vector3(+1.0f, 1.0f, 1.0f);
            }
                    
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }     

    }


    private void AIFindTarget()
    {
        float distance = 100000;
        foreach (GameObject gm in env.allCharacters)
        {
            float clc = Vector3.Distance(gm.transform.position, this.transform.position);

            if (clc < distance)
            {
                if (gm.GetComponent<Hero>() != null)
                {
                    attackTarget = gm;
                    distance = clc;
                }
                else
                {
                    if (gm.GetComponent<Bandit>().team != this.team)
                    {
                        attackTarget = gm;
                        distance = clc;
                    }
                }
            }
        }
    }


    bool yukariCik = false;
    private void AIFindPath()
    {
        
        if (attackTarget.transform.position.y > this.transform.position.y+2.3f) //üst satır
        {
            ustSatirTimer -= Time.deltaTime;

            if (ustSatirTimer <= 0)
            {
                ustSatirTimer = 1.1f;
                satirdaKal = true;
                yukariCik = true;
            }     
        }
        else if(attackTarget.transform.position.y > this.transform.position.y - 1f   &&  attackTarget.transform.position.y < this.transform.position.y + 2.3f)  //bulunduğu satir
        {
            satirdaKal = true;
            yukariCik = false;
        }
        else  // alt satir
        {
            satirdaKal = false;
            yukariCik = false;
        }

        row = AIFindKat();

        switch (row)
        {
            case 0:
                selectJumpTarget(env.Up0,env.Down0);
                break;
            case 1:
                selectJumpTarget(env.Up1, env.Down1);
                break;
            case 2:
                selectJumpTarget(env.Up2, env.Down2);
                break;
            case 3:
                selectJumpTarget(env.Up3, env.Down3);
                break;
            case 4:
                selectJumpTarget(env.Up4, env.Down4);
                break;
            default:
                break;
        }


        if (move)
        {
            if (jumpTarget != null)
            {
                if (jumpTarget.transform.position.x + 0.1f < this.transform.position.x)
                {
                    AImoveSagSol(-1);
                }
                else if (jumpTarget.transform.position.x - 0.1f > this.transform.position.x)
                {
                    AImoveSagSol(+1);
                }
                else
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);

                    if (satirdaKal)
                    {
                        AIjump(right, true);
                    }
                    
                }
            }
            else
            {
                if (attackTarget.transform.position.x + 1.2f < this.transform.position.x)
                {
                    AImoveSagSol(-1f);
                }
                else if (attackTarget.transform.position.x - 1.2f > this.transform.position.x)
                {
                    AImoveSagSol(+1f);
                }
                else if(attackTarget.transform.position.y > this.transform.position.y - 1f && attackTarget.transform.position.y < this.transform.position.y + 1f)
                {
                    attack();
                    walk.Pause();

                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }


        if (attackTarget.transform.position.x< this.transform.position.x)
        {
            transform.localScale = new Vector3(+1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

    }

    private int AIFindKat()
    {
        if (this.transform.position.y > 8.4f)
        {
            return 4;
        }
        else if (this.transform.position.y > 6f)
        {
            return 3;
        }
        else if (this.transform.position.y > 3.6f)
        {
            return 2;
        }
        else if (this.transform.position.y > 1.2f)
        {
            return 1;
        }
        else
        {
            return 0;
        }

    }
    private void selectJumpTarget(List<GameObject> jump, List<GameObject> down)
    {
        if (yukariCik)
        {
            float distance = 100000;
            foreach (GameObject gm in jump)
            {
                float clc = Vector3.Distance(gm.transform.position, attackTarget.transform.position);

                if (clc < distance)
                {
                    jumpTarget = gm;
                    distance = clc;                  
                }
            }
        }
        else if (!satirdaKal && !yukariCik)
        {
            float distance = 100000;
            foreach (GameObject gm in down)
            {
                float clc = Vector3.Distance(gm.transform.position, this.transform.position);

                if (clc < distance)
                {
                    jumpTarget = gm;
                    distance = clc;
                }
            }
        }
        else
        {
            jumpTarget = null;
        }
    }

    private void AImoveSagSol(float inputX)
    {

        if (!walk.isPlaying)
        {
            walk.Play();
        }
        


        if (inputX > 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }        
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(+1.0f, 1.0f, 1.0f);
        }


        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;



        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);


        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

    }


    private void movement()
    {

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);
        //Idle
        else
            m_animator.SetInteger("AnimState", 0);

    }
}
