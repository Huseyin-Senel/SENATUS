using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{
    private bool shopOn = false;
    private bool collesiumOn = false;

    public Label label1, label2,label3;

    private Hero characterScript;


    private Image sceneShadow;


    private static bool a = false;
    // Start is called before the first frame update
    void Start()
    {
        characterScript = GameObject.Find("Hero").GetComponent<Hero>();
        sceneShadow = GameObject.Find("SceneShadow").GetComponent<Image>();
        sceneShadow.DOFade(0, 2);
        this.Wait(2f,() => { sceneShadow.enabled = false; });



        if (a)
        {
            GameObject menu = GameObject.Find("Menu");
            menu.transform.localPosition = new Vector2(menu.transform.localPosition.x, 1080);

            GameObject background = GameObject.Find("Shadow");
            SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
            sr.color = new Color(0, 0, 0, 0.3f);

            GameObject reset = GameObject.Find("Reset");
            reset.transform.localPosition = new Vector2(reset.transform.localPosition.x, -600);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shopOn)
            {
                GetComponent<AudioSource>().Play();

                GameObject a = GameObject.Find("SellBuy");
                a.transform.DOLocalMove(new Vector3(50,+1000,1),1f);
                shopOn = false;
                characterScript.active = true;
            }

            if (collesiumOn)
            {
                GetComponent<AudioSource>().Play();

                GameObject a = GameObject.Find("Collesium");
                a.transform.DOLocalMove(new Vector3(0, +1000, 1), 1f);
                collesiumOn = false;
                characterScript.active = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (label1.characterIn)
            {
                GetComponent<AudioSource>().Play();

                if (PlayerPrefs.GetInt("levelBoss") != 0)
                {

                    Scane2Events events = GameObject.Find("ScaneEvents").GetComponent<Scane2Events>();
                    events.setPlayerPrefs();


                    sceneShadow = GameObject.Find("SceneShadow").GetComponent<Image>();
                    sceneShadow.enabled = true;
                    sceneShadow.DOFade(1, 2);

                    Level level = new Level();
                    level.TeamCount = 2;
                    level.TeamPlayerCount = 1;
                    level.EnemyHealth = 500;
                    level.EnemyDamage = 60;
                    level.KillCount = 1;

                    Level.selectedLevel = level;
                    


                    this.Wait(2.1f, () => { SceneManager.LoadScene("LevelBoss"); });



                }
                else
                {
                    
                    characterScript.typeText("henuz buna hazir degilim");
                }
            }
            if (label2.characterIn)
            {
                if (!collesiumOn)
                {
                    GetComponent<AudioSource>().Play();


                    GameObject a = GameObject.Find("Collesium");
                    a.transform.DOLocalMove(new Vector3(0, -40, 1), 1f);
                    collesiumOn = true;
                    characterScript.active = false;
                    characterScript.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                }
            }
            if (label3.characterIn)
            {
                if (!shopOn)
                {
                    GetComponent<AudioSource>().Play();

                    GameObject a = GameObject.Find("SellBuy");
                    a.transform.DOLocalMove(new Vector3(50, -40, 1), 1f);
                    shopOn = true;
                    characterScript.active = false;
                    characterScript.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }



            
        }
    }



    public void Play_Click()
    {

        GetComponent<AudioSource>().Play();

        GameObject menu = GameObject.Find("Menu");
        //menu.transform.DOMoveY(1500, 2);
        menu.transform.DOLocalMoveY(1080,2);
        //menu.transform.DOMoveY(1000, 2).SetEase(Ease.OutBounce);


        GameObject background = GameObject.Find("Shadow");
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        sr.DOFade(0.3f, 2);
        //Color color = new Color(160f/255f,160f/255f,160f/255f);
        //sr.DOColor(color, 2);
        //sr.color = color;


        GameObject reset = GameObject.Find("Reset");
        reset.transform.DOLocalMoveY(-600, 2);
        a = true;
    }
}
