using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameProperties : MonoBehaviour
{
    // Audio handling
    public AudioClip PickUpSound;
    public AudioClip PlaceSound;

    // Game Rule info
    public List<string> ResourceTypes = new List<string>();  // The kinds of resource is this object
    public List<string> HostableResources = new List<string>();  // What resources can be hosted on this
    public List<int> ResourceMaxes = new List<int>();  // How of those resources can be hosted (needs to be same size)

    // Keeps track of how many of each object this Gamobject is holding
    // Dictionarizied versions of above
    public Dictionary<string, int> HousingStatus =  new Dictionary<string, int>();
    public Dictionary<string, int> HousingMaxes = new Dictionary<string, int>();

    // Game interaction settings
    public bool IsDragable = false;
    public bool IsDragging = false;

    void Start()
    {
        // Initialize HousingStatus and HousingMaxes dictionaries
        // Go through each of the types of resources that this object can host
        int i = -1;  // Index for the ResourceMaxes and HostableResources list
        foreach (string HostableResource in HostableResources)
        {
            i++;
            // Add those entries to the HousingStatus and HousingMaxes entrie
            HousingStatus.Add(HostableResource, 0);
            HousingMaxes.Add(HostableResource, ResourceMaxes[i]);

            // Go through each of the children of this object
            foreach (Transform _childTransform in this.transform)
            {
                // If that child has a GameProperties tag
                GameProperties _childProp = _childTransform.GetComponent<GameProperties>();
                if (_childProp != null)
                {
                    // And the properties
                    if (_childProp.IsResource(HostableResource))
                    {
                        // Increment the the housing status for resource type from the main loop
                        HousingStatus[HostableResource]++;
                    }
                }
            }
        }
    }

    public bool IsResource(string _resource)
    {
        return ResourceTypes.Contains(_resource);
    }

    public IEnumerable<string> GetResrouceTypeTags()
    {
        return ResourceTypes;
    }

    public IEnumerable<string> GetHostableResources()
    {
        return HostableResources;
    }
    public bool CanHost(string _resource)
    {
        // If it's possible for this to host objects
        if (HousingMaxes.ContainsKey(_resource))
        {
            // And the max hasn't been exceeded
            if (HousingStatus[_resource] < HousingMaxes[_resource])
            {
                // Then this can host that resource
                return true;
            }
        }
        // In any other case it cannot
        return false;
    }
}
