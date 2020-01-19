using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject spotLight;
    public Transform lightTransform;
    public GameObject enemy;
    public Transform enemyTransform;
    public float lightEnemyDistance;
    public float lightHeight;
    public float lightTilt;
    public Vector3 objective;
    public float speed;
    private Queue<Vector3> requestedRotations = new Queue<Vector3>();
    private bool triggered;
    public int rotationSpeed = 2;
    private bool rotated;
    public static readonly Vector3 finalPosition = new Vector3(1, 2, 21);

    private void Start()
    {
        lightTransform = spotLight.transform;
        enemyTransform = enemy.transform;
        objective = enemyTransform.position;
    }
    void Update()
    {
        Rigidbody rb = enemy.GetComponent<Rigidbody>();

        #region Light control

        lightTransform.position = enemyTransform.position + enemyTransform.forward * lightEnemyDistance + Vector3.up * lightHeight - Vector3.up * enemyTransform.position.y;

        lightTransform.rotation = enemyTransform.rotation;
        lightTransform.Rotate(Vector3.right * lightTilt);

        #endregion

        #region Enemy movement

        if ((objective - enemyTransform.position).magnitude > 0.2)
        {
            rb.velocity = (objective - enemyTransform.position).normalized * speed;

        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (requestedRotations.Count != 0)
        {
            enemyTransform.Rotate(requestedRotations.Dequeue());
        }

        #endregion

        #region Triggered behavior

        if (triggered)
        {
            if ((objective - enemyTransform.position).magnitude < 0.2 && !rotated)
            {
                int i;
                for (i = Mathf.Abs(Mathf.RoundToInt(enemyTransform.rotation.x)); i < Mathf.Abs(Mathf.RoundToInt(enemyTransform.rotation.x + 90)); i += rotationSpeed)
                {
                    Rotate(Vector3.down * rotationSpeed);
                }

                rotated = true;
            }
        }

        #endregion
    }

    public void Rotate(Vector3 angles)
    {
        requestedRotations.Enqueue(angles);
    }

    public void TriggerEnemy()
    {
        triggered = true;

        objective = finalPosition;
    }
}
