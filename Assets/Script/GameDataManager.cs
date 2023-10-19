using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // 该脚本用于游戏运行时保存和传递数据，在切换场景时不销毁

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);   // 场景切换不删除该物体
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
