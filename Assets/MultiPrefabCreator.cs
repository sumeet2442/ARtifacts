using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultiPrefabCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs; // List of prefabs to be instantiated
    [SerializeField] private Vector3 prefabOffset;

    private ARTrackedImageManager aRTrackedImageManager;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage image in args.added)
        {
            ReplacePrefab(image);
        }

        foreach (ARTrackedImage image in args.updated)
        {
            if (image.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                ReplacePrefab(image);
            }
        }

        foreach (ARTrackedImage image in args.removed)
        {
            if (spawnedPrefabs.ContainsKey(image.referenceImage.name))
            {
                Destroy(spawnedPrefabs[image.referenceImage.name]);
                spawnedPrefabs.Remove(image.referenceImage.name);
            }
        }
    }

    private void ReplacePrefab(ARTrackedImage image)
    {
        // Remove existing prefab if it exists
        if (spawnedPrefabs.ContainsKey(image.referenceImage.name))
        {
            Destroy(spawnedPrefabs[image.referenceImage.name]);
            spawnedPrefabs.Remove(image.referenceImage.name);
        }

        // Get the prefab corresponding to the reference image name
        GameObject prefabToInstantiate = GetPrefabForReferenceImage(image.referenceImage.name);
        if (prefabToInstantiate != null)
        {
            // Instantiate the prefab at the detected image's position and rotation
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, image.transform);
            instantiatedPrefab.transform.position = prefabOffset;
            spawnedPrefabs[image.referenceImage.name] = instantiatedPrefab;
        }
    }

    private GameObject GetPrefabForReferenceImage(string referenceImageName)
    {
        // Map the reference image name to the corresponding prefab
        foreach (var prefab in prefabs)
        {
            if (prefab.name == referenceImageName)
            {
                return prefab;
            }
        }
        return null;
    }
}
