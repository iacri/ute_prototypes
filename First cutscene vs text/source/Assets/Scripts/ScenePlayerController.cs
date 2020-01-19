using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePlayerController : MonoBehaviour
{
    private enum MovementType
    {
        translation = 0,
        rotation = 1
    }

    public float walkSpeed;
    public float rotationSpeed;
    public GameObject playerObject;
    public Transform playerTransform;
    public GameObject cameraObject;
    public Transform cameraTransform;
    public float cameraPlayerDistance;
    public float cameraHeight;
    public float cameraTilt;
    public GameObject scene;
    public SceneController sceneController;
    private Queue<KeyValuePair<MovementType, Vector3>> imposedMovements = new Queue<KeyValuePair<MovementType, Vector3>>();
    private KeyValuePair<MovementType, Vector3> objective;
    private bool reachedObjective = true;
    private bool finishedCutscene = false;

    private void Start()
    {
        Cursor.visible = false;
        playerTransform = playerObject.transform;
        cameraTransform = cameraObject.transform;

        if (scene != null)
        {
            sceneController = scene.GetComponent<SceneController>();
        }

        if (SceneManager.GetActiveScene().name == "Cutscene")
        {
            StartCoroutine(FillCutsceneMovementsCoroutine());
        }
        else
        {
            FillButtonSceneMovements();
        }

    }

    private void Update()
    {
        Rigidbody rb = playerObject.GetComponent<Rigidbody>();

        #region Camera Movement

        cameraTransform.position = playerTransform.position - playerTransform.forward * cameraPlayerDistance + Vector3.up * cameraHeight;

        #endregion

        #region Camera Rotation

        cameraTransform.rotation = playerTransform.rotation;
        cameraTransform.Rotate(Vector3.right * cameraTilt);

        #endregion

        #region Imposed Movement

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (imposedMovements.Count != 0 && reachedObjective)
        {
            objective = imposedMovements.Dequeue();
            reachedObjective = false;
        }

        if (!reachedObjective)
        {
            switch (objective.Key)
            {
                case MovementType.translation:
                    if ((objective.Value - playerTransform.position).magnitude > 0.1)
                    {
                        rb.velocity = (objective.Value - playerTransform.position).normalized * walkSpeed;
                    }
                    else
                    {
                        rb.velocity = Vector3.zero;
                        reachedObjective = true;
                    }

                    break;

                case MovementType.rotation:
                    playerTransform.LookAt(objective.Value);

                    reachedObjective = true;
                    break;

                default:
                    throw new UnityException("Movement type not recognized");
            }
        }

        if (SceneManager.GetActiveScene().name == "Press button")
        {
            if (reachedObjective && imposedMovements.Count == 0)
            {
                sceneController.SceneHappened();
            }
        }

        if (finishedCutscene && imposedMovements.Count == 0 && reachedObjective)
        {
            sceneController.SceneHappened();
            finishedCutscene = false;
        }

        #endregion
    }

    private IEnumerator FillCutsceneMovementsCoroutine()
    {
        imposedMovements.Enqueue(new KeyValuePair<MovementType, Vector3>(MovementType.rotation, EnemyController.finalPosition));

        yield return new WaitForSeconds(2);

        imposedMovements.Enqueue(new KeyValuePair<MovementType, Vector3>(MovementType.translation, new Vector3(1f, 1.05f, 4f)));
        imposedMovements.Enqueue(new KeyValuePair<MovementType, Vector3>(MovementType.translation, new Vector3(5f, 1.05f, 6.5f)));
        imposedMovements.Enqueue(new KeyValuePair<MovementType, Vector3>(MovementType.translation, new Vector3(5f, 1.05f, 12f)));

        finishedCutscene = true;
    }

    private void FillButtonSceneMovements()
    {
        imposedMovements.Enqueue(new KeyValuePair<MovementType, Vector3>(MovementType.translation, new Vector3(5f, 1.05f, 6.5f)));
        imposedMovements.Enqueue(new KeyValuePair<MovementType, Vector3>(MovementType.translation, new Vector3(5f, 1.05f, 12f)));
    }
}
