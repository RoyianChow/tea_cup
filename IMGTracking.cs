using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageTracking : MonoBehaviour
{
    // Array of objects that can be placed on tracked images
    [SerializeField]
    private GameObject[] placeableObjects;

    // Dictionary that keeps track of spawned objects by name
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    // Reference to ARTrackedImageManager for managing tracked images
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        // Find ARTrackedImageManager component in the scene
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        // Instantiate and add all placeable objects to the scene
        foreach (GameObject placeableObject in placeableObjects)
        {
            GameObject newObject = Instantiate(placeableObject, Vector3.zero, Quaternion.identity);
            newObject.name = placeableObject.name;
            spawnedObjects.Add(placeableObject.name, newObject);
        }
    }

    private void OnEnable()
    {
        // Register event handler for when tracked images are changed
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        // Unregister event handler for when tracked images are changed
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Handle added, updated, and removed tracked images
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedObjects[trackedImage.name].SetActive(false);
        }
    }

    private async void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        // Get the name and position of the tracked image
        string objectName = trackedImage.referenceImage.name;
        Vector3 objectPosition = trackedImage.transform.position;

        // Get the object prefab and update its position
        GameObject objectPrefab = spawnedObjects[objectName];
        objectPrefab.transform.position = objectPosition;
        objectPrefab.SetActive(true);

        // Deactivate all other spawned objects
        foreach (GameObject obj in spawnedObjects.Values)
        {
            if (obj.name != objectName)
            {
                obj.SetActive(false);
            }
        }
    }
}
