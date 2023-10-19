using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainScene : MonoBehaviour
{
    
    public Button gameStartButton;  // 开始游戏按钮
    public Button gameHistoryButton;  // 游戏历史按钮
    public Button gameExitButton;  // 游戏退出按钮
    
     
    
    // Start is called before the first frame update
    void Start()
    {
        gameStartButton=GameObject.Find("gameStartButton").GetComponent<Button>();
        gameHistoryButton=GameObject.Find("gameHistoryButton").GetComponent <Button>();
        gameExitButton=GameObject.Find("gameExitButton").GetComponent<Button>();

        gameStartButton.onClick.AddListener(StartButtonClick);
        gameHistoryButton.onClick.AddListener(HistoryButtonClick);
        gameExitButton.onClick.AddListener(ExitButtonClick);

        
    }   

    // Update is called once per frame
    void Update()
    {

    }


    // 开始游戏按钮事件
    public void StartButtonClick()
    {
        print("跳转至战斗测试场景");
        SceneManager.LoadScene("TestGameScenes");
    }

    // 游戏历史按钮事件
    public void HistoryButtonClick()
    {
        print("养成模式测试未制作");
    }

    // 退出按钮事件
    public void ExitButtonClick()
    {
        print("游戏退出功能按钮未制作");
    }

}
