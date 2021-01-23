using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerScript : MonoBehaviour
{
    [SerializeField] Image menu;
    [SerializeField] Button startButton;
    [SerializeField] Text scoreLabel;
    [SerializeField] Image endGame;
    [SerializeField] Button endGameButton;
    public static int score; //����
    GameObject player;
    float timeLeft = 4;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        endGame.gameObject.SetActive(false);
        Time.timeScale = 0; //������������� �����
        //�� ������� �� ������ ���� ����������, ����� ������������
        startButton.onClick.AddListener(delegate
        {
            menu.gameObject.SetActive(false);
            Time.timeScale = 1;
        });
        player = GameObject.FindGameObjectWithTag("Player"); //�������� ������ � ����� "Player"
    }
    
    // Update is called once per frame
    void Update()
    {   //���� ������ �� ������� ����� 4 ������� ����� ���������������
        if (!player) 
        {
            endGame.gameObject.SetActive(true);
            endGameButton.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Time.timeScale = 0;
            }
        }
        scoreLabel.text = "Score: " + score; //���������� �����
    }
}
