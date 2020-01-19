using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsFirstTriggerController : MonoBehaviour
{
    public GameObject enemy;
    public EnemyController enemyController;
    public GameObject scene = null;
    public SceneController sceneController;
    public GameObject hideText;
    public GameObject textUI;
    public GameObject player;
    public PlayerController playerController;

    private void Start()
    {
        enemyController = enemy.GetComponent<EnemyController>();
        playerController = player.GetComponent<PlayerController>();

        if (SceneManager.GetActiveScene().name == "Cutscene" || SceneManager.GetActiveScene().name == "Press button")
        {
            sceneController = scene.GetComponent<SceneController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyController.TriggerEnemy();

        if (SceneManager.GetActiveScene().name == "Cutscene")
        {
            sceneController.TriggerScene();
        }
        else if (SceneManager.GetActiveScene().name == "Free movement")
        {
            hideText.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Press button")
        {
            textUI.SetActive(true);
            playerController.pressEToHide = true;
        }
    }
}
