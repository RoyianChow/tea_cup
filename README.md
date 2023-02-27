#AR Image Tracking with Unity and AR Foundation

This Unity project uses AR Foundation and AR Tracked Image Manager to place prefabs on detected images. The script allows for multiple prefabs to be placed on each image.

#How to Use
Clone or download this repository
Open the project in Unity
Create or import prefabs that you want to place on detected images
Add the prefabs to the placeableObjects array in the ImageTracking script
Build and run the project on an AR-enabled device
Point the device's camera at a tracked image and the prefabs will appear on top of it

#Details

The ImageTracking script is responsible for managing the tracked images and placing prefabs on them. It uses a dictionary to keep track of spawned objects by name, and the trackedImageManager to manage tracked images.

When a tracked image is added or updated, the UpdateTrackedImage function is called to get the name and position of the image, get the object prefab, and add it to the image. The function also adds the spawned object to the list of spawned objects for this tracked image, and deactivates all other spawned objects for the same image.

When a tracked image is removed, the OnTrackedImagesChanged function is called to deactivate all spawned objects for the removed image.

#Notes

Make sure to set up image tracking in the AR Tracked Image Manager before running the project
This project was created using Unity 2019.4.18f1 and AR Foundation 3.1.3


