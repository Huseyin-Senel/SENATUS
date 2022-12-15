using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using DG.Tweening;
using System;

public class KillCounter : MonoBehaviour
{

    public TextMeshProUGUI targetKillTxt;
    public GameObject teamPrefab;

    //-12 0  1.3f ,10 -50,10 -90
    private Scene scene;


    private List<Color> colors = new List<Color>() { new Color(3f / 5f,1f, 3f / 5f), new Color(1f, 3f / 5f, 3f / 5f), new Color(3f / 5f, 3f / 5f, 1f) };
    private List<int> killCounts = new List<int>();
    private List<GameObject> teamsObj = new List<GameObject>();
    private List<TextMeshProUGUI> teamsKillTxt = new List<TextMeshProUGUI>();

    private void Start()
    {
        scene = GameObject.Find("Enviorment").GetComponent<Scene>();


        for (int i =0; i< Level.selectedLevel.TeamCount; i++)
        {
            teamsObj.Add(Instantiate(teamPrefab, transform.position, Quaternion.identity,transform));
            teamsKillTxt.Add(teamsObj[i].transform.Find("KillTxt").GetComponent<TextMeshProUGUI>());

            if (i==0)
            {
                teamsObj[i].transform.Find("Txt").GetComponent<TextMeshProUGUI>().text = "SEN:";
                teamsObj[i].transform.Find("TeamTxt").GetComponent<TextMeshProUGUI>().text = "";
                teamsObj[i].transform.Find("TeamTxt").GetComponent<TextMeshProUGUI>().color = new Color(4f / 5f, 4f / 5f, 4f / 5f);
                teamsObj[i].transform.Find("Txt").GetComponent<TextMeshProUGUI>().color = new Color(4f / 5f, 4f / 5f, 4f / 5f);
            }
            else
            {
                teamsObj[i].transform.Find("TeamTxt").GetComponent<TextMeshProUGUI>().text = i.ToString();
                teamsObj[i].transform.Find("TeamTxt").GetComponent<TextMeshProUGUI>().color = colors[i - 1];
                teamsObj[i].transform.Find("Txt").GetComponent<TextMeshProUGUI>().color = colors[i - 1];
            }

            killCounts.Add(0);
        }

        targetKillTxt.text = Level.selectedLevel.KillCount.ToString();

        sort();
    }


    public void increaseKill(int team)
    {
        killCounts[team] += 1;
        teamsKillTxt[team].text = killCounts[team].ToString();
        sort();

        if (killCounts[team]>=Level.selectedLevel.KillCount)
        {
            if (team==0)
            {
                scene.endFunction(true);
            }
            else
            {
                scene.endFunction(false);
            }      
        }
    }

    private void sort()
    {
        var sorted = killCounts
            .Select((x, i) => new KeyValuePair<int, int>(x, i))
            .OrderBy(x => x.Key)
            .ToList();

        List<int> B = sorted.Select(x => x.Key).ToList();
        List<int> idx = sorted.Select(x => x.Value).ToList();//küçükten büyüðe



        for (int a = Level.selectedLevel.TeamCount-1; a >= 0; a--)
        {
            if (a == Level.selectedLevel.TeamCount - 1)
            {
                first(teamsObj[idx[a]]);
            }
            else
            {
                others(teamsObj[idx[a]],a);
            }
        }
    }

    private void first(GameObject gameObject)
    {
        gameObject.transform.DOLocalMove(new Vector2(-12,0),1f);
        gameObject.transform.DOScale(1.3f,1f);
    }

    private void others(GameObject gameObject,int pos)
    {
        int i = Math.Abs(pos - (Level.selectedLevel.TeamCount - 2));
        int posY = -50 + (i * -40);


        gameObject.transform.DOLocalMove(new Vector2(10, posY), 1f);
        gameObject.transform.DOScale(1f, 1f);
    }

}
