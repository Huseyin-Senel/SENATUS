using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private GroundSensor m_groundSensor;
    private AttackSensor m_attackSensor;
    private GameObject attackCollider;
    private Scene scene;
    private TextMeshProUGUI maxHpTxt;
    private TextMeshProUGUI hpTxt;
    private UnityEngine.UI.Slider hpSlider;
    private TextMeshProUGUI potTxt;

    public GameObject infoText;

    private TextMeshPro tmp;
    private GameObject child0;


    private AudioSource hit, block, walk, tossing, hit_hero, defence, punch, block2;


    public SpriteRenderer shield;
    public SpriteRenderer sword;
    


    private int health;
    private int armor;
    private int damage;
    private int potCount;
    private const int damage2 = 20;



    [SerializeField] int maxHp = 100;
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 8f;

    public bool active = true;
    public bool inWar = true;
    public bool directionLeft = false;
    private bool isDefence = false;  

    public int Health { get => health; set => health = value; }
    public int Armor { get => armor; set => armor = value; }
    public int Damage { get => damage; set => damage = value; }
    public int PotCount { get => potCount; set => potCount = value; }

    void Start()
    {
        if (GameObject.Find("Enviorment") != null)
        {
            scene = GameObject.Find("Enviorment").GetComponent<Scene>();
        }
        if (GameObject.Find("Health") != null)
        {
            hpSlider = GameObject.Find("Health").GetComponent<UnityEngine.UI.Slider>();
            hpSlider.value = maxHp;
            hpSlider.maxValue = maxHp;
        }
        if (GameObject.Find("HpMaxTxt") != null)
        {
            maxHpTxt = GameObject.Find("HpMaxTxt").GetComponent<TextMeshProUGUI>();
            maxHpTxt.text = maxHp.ToString();
        }
        if (GameObject.Find("HpTxt") != null)
        {
            hpTxt = GameObject.Find("HpTxt").GetComponent<TextMeshProUGUI>();
            hpTxt.text = maxHp.ToString();
        }
        if (GameObject.Find("PotTxt") != null)
        {
            potTxt = GameObject.Find("PotTxt").GetComponent<TextMeshProUGUI>();
        }

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("Ground").GetComponent<GroundSensor>();
        m_attackSensor = transform.Find("AttackCollider").GetComponent<AttackSensor>();
        attackCollider = transform.Find("AttackCollider").gameObject;
        health = maxHp;
        hit = GetComponents<AudioSource>()[0];
        hit_hero = GetComponents<AudioSource>()[1];
        block = GetComponents<AudioSource>()[2];
        walk = GetComponents<AudioSource>()[3];
        tossing = GetComponents<AudioSource>()[4];
        defence = GetComponents<AudioSource>()[5];
        punch = GetComponents<AudioSource>()[6];
        block2 = GetComponents<AudioSource>()[7];

        child0 = transform.Find("back").gameObject;
        tmp = child0.transform.Find("Text").gameObject.GetComponent<TextMeshPro>();




        canlan();
    }


    private bool hurt = false;
    private float attackTimer = 0.7f;
    void Update()
    {

        if (m_body2d.velocity.x != 0 && m_groundSensor.grounded && !walk.isPlaying) //if (inputX != 0 && m_groundSensor.grounded && !walk.isPlaying)
        {
            walk.Play();
        }
        else if (walk.isPlaying)
        {
            walk.Pause();
        }


        if (active)
        {
            movement();

            if (Input.GetKeyDown("e") && inWar && health<maxHp)
            {
                usePot();
            }
        }


        if (armor > 0 && !shield.enabled)
        {
            shield.enabled = true;
        }
        if (armor <= 0 && shield.enabled)
        {
            shield.enabled = false;
        }


        if (damage > 0 && !sword.enabled)
        {
            sword.enabled = true;
        }
        if (damage <= 0 && sword.enabled)
        {
            sword.enabled = false;
        }


        if (attackTimer>0)
        {
            attackTimer -= Time.deltaTime;
        }
        
    }



    private void setCan(int can)
    {
        if (can<0)
        {
            can = 0;
        }

        hpTxt.text = can.ToString();
        hpSlider.value = can;
    }





    private int calculeteDefence(int damage, Bandit bandit)
    {
        int defence = 0;

        if (bandit.gameObject.transform.position.x > transform.position.x)
        {
            if (!directionLeft)
            {
                if (isDefence)
                {
                    if (armor>0)
                    {
                        block.Play();
                    }
                    else
                    {
                        block2.Play();
                    }
          

                    defence = ((damage / 3) * armor);
                }
                else
                {
                    defence = ((damage / 10) * armor);
                }
            }
        }
        else
        {
            if (directionLeft)
            {
                if (isDefence)
                {
                    if (armor > 0)
                    {
                        block.Play();
                    }
                    else
                    {
                        block2.Play();
                    }

                    defence = ((damage / 3) * armor);
                }
                else
                {
                    defence = ((damage / 10) * armor);
                }
            }
        }

        if (defence>damage)
        {
            defence = damage;
        }

        return defence;

    }


    public void takeDamage(int damage,Bandit bandit)
    {
        hurt = true;
        m_animator.SetTrigger("Hurt");
        this.Wait(1f, () => { hurt = false; });

        int defence = calculeteDefence(damage, bandit);


        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.6f);
        GameObject aa;

        if ((damage - defence)  > 0)
        {
            hit_hero.Play();
            hit.Play();
            aa = Instantiate(infoText, pos, Quaternion.identity, transform);
            aa.GetComponent<InfoText>().run((damage - defence).ToString(), InfoText.Type.red);
        }
        

        if (defence>0)
        {
            aa = Instantiate(infoText, pos, Quaternion.identity, transform);
            aa.GetComponent<InfoText>().run(defence.ToString(), InfoText.Type.yellow);
        }
        


        health -= (damage-defence);
        setCan(health);


        if (health <=0)
        {
            die(bandit);
        }
    }

    private void canlan()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        GetComponent<SpriteRenderer>().DOFade(1, 1);
    }



    private void die(Bandit bandit)
    {
        active = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        GetComponent<BoxCollider2D>().enabled = false;
        //attackCollider.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().DOFade(0f, 1f);

        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                child.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
            }
        }


        scene.KillCounter.increaseKill(bandit.team);
        scene.respawn(this, null);
    }

    
    private void attack()
    {
        if (attackTimer <= 0 && !hurt)
        {
            attackTimer = 0.7f;



            if (damage>0)
            {
                tossing.Play();
            }
            else
            {
                punch.Play();
            }

            

            var clonedList = new List<GameObject>(m_attackSensor.objects);

            foreach (GameObject obj in clonedList)
            {
                if (obj.GetComponent<Bandit>() != null)
                {
                    obj.GetComponent<Bandit>().takeDamage((Damage * (damage2/5))+damage2, null, this);
                }
            }

            if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                m_animator.SetTrigger("Attack");
            }
        }
    }

    private void usePot()
    {
        if (potCount>0)
        {
            potCount--;
            health += 50;    

            if (health > maxHp)
            {
                health = maxHp;
            }

            Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.6f);
            GameObject aa = Instantiate(infoText, pos, Quaternion.identity, transform);
            aa.GetComponent<InfoText>().run(50.ToString(), InfoText.Type.green);

            potTxt.text = potCount.ToString();


            setCan(health);
        }
    }

    private void movement()
    {


        m_animator.SetBool("JumpBool", !m_groundSensor.grounded);


        float inputX = Input.GetAxis("Horizontal");
        if (!isDefence)
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }



        //walk.play


        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    child.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            directionLeft = false;
            attackCollider.transform.localPosition = new Vector2(0.6f, attackCollider.transform.localPosition.y);

        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    child.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            directionLeft = true;
            attackCollider.transform.localPosition = new Vector2(-0.6f, attackCollider.transform.localPosition.y);
        }



        if (Input.GetKeyDown("w") && m_groundSensor.grounded && !isDefence)
        {
            m_animator.SetTrigger("Jump");
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        }

        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }

        // Block
        if (Input.GetMouseButtonDown(1))
        {
            //if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Defence"))
            //{
            //    m_animator.SetTrigger("Defence");
            //}
            defence.Play();
            m_animator.SetBool("DefenceBool", true);
            isDefence = true;

        }
        if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("DefenceBool", false);
            isDefence = false;
        }


        if (inputX == 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }



        //Run
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            
            m_animator.SetBool("Run", true);
        }
        //Idle
        else
        {
            m_animator.SetBool("Run", false);
        }

    }

    public void scriptRun(float inputX)
    {
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    child.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            directionLeft = false;
            attackCollider.transform.localPosition = new Vector2(0.6f, attackCollider.transform.localPosition.y);

        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    child.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            directionLeft = true;
            attackCollider.transform.localPosition = new Vector2(-0.6f, attackCollider.transform.localPosition.y);
        }


        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_animator.SetBool("Run", true);
        }
        //Idle
        else
        {
            m_animator.SetBool("Run", false);
        }


        

    }


    public void typeText(string str, float typeSpeed = 15f)
    {
        child0.SetActive(true);

        string text = "";
        DOTween.To(() => text, x => text = x, str, str.Length / typeSpeed).OnUpdate(() =>
        {
            tmp.text = text;
        });

        this.Wait((str.Length / typeSpeed)+1f, ()=>{ child0.SetActive(false); ; });

    }
}
