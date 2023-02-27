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
    private Dictionary<string, List<GameObject>> spawnedObjects = new Dictionary<string, List<GameObject>>();

    // Reference to ARTrackedImageManager for managing tracked images
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        // Find ARTrackedImageManager component in the scene
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        // Instantiate and add all placeable objects to the scene
        foreach (GameObject placeableObject in placeableObjects)
        {
            spawnedObjects.Add(placeableObject.name, new List<GameObject>());
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
            if (spawnedObjects.ContainsKey(trackedImage.name))
            {
                foreach (GameObject obj in spawnedObjects[trackedImage.name])
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private async void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        // Get the name and position of the tracked image
        string objectName = trackedImage.referenceImage.name;
        Vector3 objectPosition = trackedImage.transform.position;

        // Get the object prefab and add it to the tracked image
        GameObject objectPrefab = GetObjectPrefab(objectName);
        GameObject spawnedObject = Instantiate(objectPrefab, objectPosition, Quaternion.identity);
        spawnedObject.SetActive(true);

        // Add the spawned object to the list of spawned objects for this tracked image
        if (!spawnedObjects.ContainsKey(objectName))
        {
            spawnedObjects.Add(objectName, new List<GameObject>());
        }
        spawnedObjects[objectName].Add(spawnedObject);

        // Deactivate all other spawned objects for this tracked image
        foreach (GameObject obj in spawnedObjects[objectName])
        {
            if (obj != spawnedObject)
            {
                obj.SetActive(false);
            }
        }
    }

    private GameObject GetObjectPrefab(string objectName)
    {
        // Find the object prefab with the specified name
        foreach (GameObject placeableObject in placeableObjects)
        {
            if (placeableObject.name == objectName)
            {
                return placeableObject;
            }
        }

        // Return the first object prefab if no matching prefab was found
        return placeableObjects[0];
    }
}
