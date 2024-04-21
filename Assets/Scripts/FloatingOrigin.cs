using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is attached to the camera
[RequireComponent(typeof(Camera))]

public class FloatingOrigin : MonoBehaviour
{
    public float threshold = 100.0f;
    public LevelLayoutGenerator levelLayoutGenerator;

    // This corrects the position of all objects in the scene
    void LateUpdate()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        cameraPosition.y = 0f;


        if (cameraPosition.magnitude > threshold)
        {
            for (int z = 0; z < SceneManager.sceneCount; z++)
            {
                foreach (GameObject go in SceneManager.GetSceneAt(z).GetRootGameObjects())
                {
                    go.transform.position -= cameraPosition;
                }
            }

            Vector3 originDelta = Vector3.zero - cameraPosition;
            levelLayoutGenerator.UpdateSpawnOrigin(originDelta);
            Debug.Log("recentering, origin delta = " + originDelta);
        }
    }
}
