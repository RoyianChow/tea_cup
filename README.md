# tea_cup


**The script defines a class called "ImageTracking" that inherits from MonoBehaviour.
It contains a private array of GameObjects called "placeableObjects" that can be placed on tracked images.
It also contains a private dictionary of GameObjects called "spawnedObjects" that keeps track of spawned objects by name.
The script uses ARFoundation to track images and get their positions.
In the Awake method, the script finds the ARTrackedImageManager component in the scene and instantiates all placeable objects.
In the OnEnable method, the script registers an event handler for when tracked images are changed.
In the OnDisable method, the script unregisters the event handler for when tracked images are changed.
In the OnTrackedImagesChanged method, the script handles added, updated, and removed tracked images.
In the UpdateTrackedImage method, the script gets the name and position of the tracked image and updates the position of the corresponding spawned object.
It also deactivates all other spawned objects that are not associated with the tracked image.**
