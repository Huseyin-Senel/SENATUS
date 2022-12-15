using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Level : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static Level selectedLevel;
    public string levelSceneName;


    public GameObject referenceClickHolderPrefab;
    public Image shadow;
    public GameObject lockObj;
    private GameObject clickHolderPrefab = null;

    public TextMeshProUGUI levelText;

    private Image sceneShadow;


    private int teamCount;
    private int teamPlayerCount;
    private int enemyHealth;
    private int enemyDamage;
    private int killCount;
    [SerializeField] private Levels level;
    [SerializeField] public bool avaliable = false;

    public int TeamCount { get => teamCount; set => teamCount = value; }
    public int TeamPlayerCount { get => teamPlayerCount; set => teamPlayerCount = value; }
    public int EnemyHealth { get => enemyHealth; set => enemyHealth = value; }
    public Levels Leveln { get => level; set => level = value; }
    public int EnemyDamage { get => enemyDamage; set => enemyDamage = value; }
    public int KillCount { get => killCount; set => killCount = value; }

    public enum Levels
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        LevelBoss
    }


    private void Awake()
    {
        createLevel(this.level);
        
    }


    public void setAvaliable(bool avaliable)
    {
        if (avaliable)
        {
            shadow.color = new Color(0, 0, 0, 0);
            lockObj.SetActive(false);
        }
        else
        {
            shadow.color = new Color(0, 0, 0, 0.5f);
            lockObj.SetActive(true);
        }
        this.avaliable = avaliable; 
    }


    private void createLevel(Levels level)
    {
        switch (level)
        {
            case Levels.Level1:
                this.teamCount = 2;
                this.teamPlayerCount = 1;
                this.enemyHealth = 100;
                this.enemyDamage = 36;
                this.killCount = 5;
                levelText.text = "Level 1";
                //this.level = level;
                break;
            case Levels.Level2:
                this.teamCount = 2;
                this.teamPlayerCount = 3;
                this.enemyHealth = 100;
                this.enemyDamage = 35;
                this.killCount = 7;
                levelText.text = "Level 2";
                //this.level = level;
                break;
            case Levels.Level3:
                this.teamCount = 3;
                this.teamPlayerCount = 2;
                this.enemyHealth = 150;
                this.enemyDamage = 60;
                this.killCount = 10;
                levelText.text = "Level 3";
                //this.level = level;
                break;
            case Levels.Level4:
                this.teamCount = 3;
                this.teamPlayerCount = 2;
                this.enemyHealth = 150;
                this.enemyDamage = 60;
                this.killCount = 10;
                levelText.text = "Level 4";
                //this.level = level;
                break;
            case Levels.Level5:
                this.teamCount = 3;
                this.teamPlayerCount = 2;
                this.enemyHealth = 150;
                this.enemyDamage = 60;
                this.killCount = 10;
                levelText.text = "Level 5";
                //this.level = level;
                break;
            case Levels.Level6:
                this.teamCount = 3;
                this.teamPlayerCount = 2;
                this.enemyHealth = 150;
                this.enemyDamage = 60;
                this.killCount = 10;
                levelText.text = "Level 6";
                //this.level = level;
                break;
            case Levels.LevelBoss:
                this.teamCount = 2;
                this.teamPlayerCount = 1;
                this.enemyHealth = 500;
                this.enemyDamage = 60;
                this.killCount = 1;
                levelText.text = "Boss";
                //this.level = level;
                break;
            default:
                break;
        }

        if (avaliable)
        {
            shadow.color = new Color(0,0,0,0);
            lockObj.SetActive(false);
        }
        else
        {
            shadow.color = new Color(0, 0, 0, 0.5f);
            lockObj.SetActive(true);
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {

        if (avaliable)
        {
            Vector3 pos = transform.position;
            pos.x = pos.x + 200;
            pos.y = pos.y + -200;

            clickHolderPrefab = Instantiate(referenceClickHolderPrefab, pos, Quaternion.identity, transform);
            clickHolderPrefab.GetComponent<ClickHolderLevel>().setItem(this);
        }
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (clickHolderPrefab != null)
        {
            Destroy(clickHolderPrefab);
            clickHolderPrefab = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Scane2Events events = GameObject.Find("ScaneEvents").GetComponent<Scane2Events>();
        events.setPlayerPrefs();


        sceneShadow = GameObject.Find("SceneShadow").GetComponent<Image>();
        sceneShadow.enabled = true;
        sceneShadow.DOFade(1, 2);
        selectedLevel = this;


        this.Wait(2.1f,()=>{ SceneManager.LoadScene(levelSceneName); });
        
    }
}
