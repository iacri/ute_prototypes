using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject spotLight;
    public Transform lightTransform;
    public GameObject enemy;
    public Transform enemyTransform;
    public GameObject enemyLight;
    public GameObject player;
    public float lightEnemyDistance;
    public float lightHeight;
    public float lightTilt;

    private void Start()
    {
        lightTransform = spotLight.transform;
        enemyTransform = enemy.transform;
    }
    void Update()
    {
        Rigidbody rb = enemy.GetComponent<Rigidbody>();

        #region Light control

        lightTransform.position = enemyTransform.position + enemyTransform.forward * lightEnemyDistance + Vector3.up * lightHeight - Vector3.up * enemyTransform.position.y;

        lightTransform.rotation = enemyTransform.rotation;
        lightTransform.Rotate(Vector3.right * lightTilt);

        enemyLight.transform.position = enemyTransform.position + enemyTransform.up * 1.8f;

        enemyLight.transform.rotation = enemyTransform.rotation;
        enemyLight.transform.Rotate(Vector3.forward * 90);

        #endregion

        #region Enemy rotation control

        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        #endregion
    }
}
