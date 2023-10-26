using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoRect : MonoBehaviour
{

    public ScrollRect battleDescScrollRect;
    public ScrollRect roundNumScrollRect;

    public Scrollbar battleTextScrollbar;

    // Start is called before the first frame update
    void Start()
    {
        battleDescScrollRect=GameObject.Find("BattleDescription").GetComponent<ScrollRect>();
        roundNumScrollRect=GameObject.Find("RoundNum").GetComponent<ScrollRect>();

        battleTextScrollbar=GameObject.Find("BattleTextScrollbar").GetComponent<Scrollbar>();
        battleTextScrollbar.value=0f;
    }

    // Update is called once per frame
    void Update()
    {
        roundNumScrollRect.normalizedPosition=battleDescScrollRect.normalizedPosition;
    }
}
