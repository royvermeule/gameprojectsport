using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trofeeScore : MonoBehaviour
{
    public Text myScoretext;
    private int ScoreNum;



    // Start is called before the first frame update
    void Start()
    {
        ScoreNum = 0;
        myScoretext.text = "Score : " + ScoreNum;


    }
    private void OnTriggerEnter2D(Collider2D trofee)
    {
        
        if(trofee.tag == "Mytrofee")
        {
            ScoreNum += 1;
            Destroy(trofee.gameObject);
            myScoretext.text = "Score" + ScoreNum;
        }

    }
}

   
