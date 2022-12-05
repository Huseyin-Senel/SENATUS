using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;

public class Bandit : MonoBehaviour {


    public bool right;
    public int team = 0;
    private int kat = 0;
    public GameObject attackTarget;
    public GameObject jumpTarget;
    public bool satirdaKal = true;
    public bool move = true;
    Enviorment env;
    private float ustSatirTimer = 1.1f;


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
    }
	

	void Update () {
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
            
            
                // Swap direction of sprite depending on walk direction
                if (inputX > 0)
                    transform.localScale = new Vector3(+1.0f, 1.0f, 1.0f);
                else if (inputX < 0)
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                // Move
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
                if (gm.GetComponent<HeroKnight>() != null)
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

        kat = AIFindKat();

        switch (kat)
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
                    AImoveSagSol(1);
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
                    AImoveSagSol(-0.5f);
                }
                else if (attackTarget.transform.position.x - 1.2f > this.transform.position.x)
                {
                    AImoveSagSol(0.5f);
                }
            }
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
        // -- Handle input and movement --

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
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
