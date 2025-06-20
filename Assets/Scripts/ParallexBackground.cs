using UnityEngine;

public class ParallexBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;

    [SerializeField] private ParallexLayer[] backgroundLayers;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;

        lastCameraPositionX = currentCameraPositionX;

        foreach (ParallexLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
        }
    }
}
