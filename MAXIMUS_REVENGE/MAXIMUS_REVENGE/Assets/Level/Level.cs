using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Level : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    public GameObject referenceClickHolderPrefab;
    private GameObject clickHolderPrefab = null;

    public TextMeshProUGUI levelText;



    private int teamCount;
    private int teamPlayerCount;
    private int enemyHealth;
    [SerializeField] private Levels level;

    public int TeamCount { get => teamCount; set => teamCount = value; }
    public int TeamPlayerCount { get => teamPlayerCount; set => teamPlayerCount = value; }
    public int EnemyHealth { get => enemyHealth; set => enemyHealth = value; }
    public Levels Leveln { get => level; set => level = value; }

    public enum Levels
    {
        Level1,
        Level2,
        Level3
    }


    private void Awake()
    {
        createLevel(this.level);
    }

    private void createLevel(Levels level)
    {
        switch (level)
        {
            case Levels.Level1:
                this.teamCount = 1;
                this.teamPlayerCount = 1;
                this.enemyHealth = 100;
                levelText.text = "Level 1";
                //this.level = level;
                break;
            case Levels.Level2:
                this.teamCount = 1;
                this.teamPlayerCount = 3;
                this.enemyHealth = 100;
                levelText.text = "Level 2";
                //this.level = level;
                break;
            case Levels.Level3:
                this.teamCount = 2;
                this.teamPlayerCount = 2;
                this.enemyHealth = 150;
                levelText.text = "Level 3";
                //this.level = level;
                break;
            default:
                break;
        }
    }






    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 pos = transform.position;
        pos.x = pos.x + 200;
        pos.y = pos.y + -200;

        clickHolderPrefab = Instantiate(referenceClickHolderPrefab, pos, Quaternion.identity, transform);
        clickHolderPrefab.GetComponent<ClickHolderLevel>().setItem(this);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (clickHolderPrefab != null)
        {
            Destroy(clickHolderPrefab);
            clickHolderPrefab = null;
        }
    }
}
