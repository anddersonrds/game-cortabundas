using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour {

    public float accuracy = 1.0f;
    public GameObject wayPoint;
    private GameObject stayArea;
    private NpcAi aiScript;
    private DetectPlayer areaScript;
    private UnityEngine.UI.Text text;
    private bool checking;
    private bool warned = false;
    private float timer;
    private int timesWarned = 0;
    private GameMaster gm;
    // Use this for initialization
    void Start () {
        stayArea = GameObject.Find("StayArea");
        aiScript = GetComponent<NpcAi>();
        areaScript = stayArea.GetComponent<DetectPlayer>();
        checking = false;
        text = GameObject.Find("DialogText").GetComponent<UnityEngine.UI.Text>();
        text.text = "";
        text.color = Color.white;
        timer = 0.0f;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    }

    // Update is called once per frame
    void Update () {

        if (Vector3.Distance(wayPoint.transform.position, this.transform.position) < accuracy && timer < 2.0f)
        {
            if (!checking)
            {
                aiScript.Stop();
                transform.Rotate(new Vector3(0, 120, 0));
                checking = true;
            }
        }

        timer -= Time.deltaTime;

        if (checking)
            checkPlayer();
    }

    void checkPlayer()
    {
       if (!areaScript.IsPlayerInside())
        {
            if (!warned)
            {
                timesWarned += 1;
                Warn();
                areaScript.Warn();
            }
        }
       else
        {
            checking = false;
            aiScript.Resume();
            timer = 5.0f;
            ClearWarning();
        }

        if (timesWarned > 2)
        {
            gm.GameOver();
        }
    }

    public void Warn()
    {
        text.text = "Mantenha-se na área de visão";
        text.color = Color.red;
        warned = true;
    }

    public void ClearWarning()
    {
        warned = false;
        text.text = "";
        text.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
}
