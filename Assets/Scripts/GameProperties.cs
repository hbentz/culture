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

    /// <summary>
    /// Checks to see if this Gamobject can hypothetically host _child
    /// </summary>
    /// <param name="_child">The GameObject that is being checked for compatibility</param>
    /// <returns>true if it can be hosted, false if it cannot</returns>
    public bool CanHost(GameObject _child)
    {
        if (_child.TryGetComponent<GameProperties>(out GameProperties _childProperties))
        {
            // Go through each of the _resources the provided child has
            foreach (string _resource in _childProperties.GetResrouceTypeTags())
            {
                // If it's generally possible for this to host objects
                if (HousingMaxes.ContainsKey(_resource))
                {
                    // But the max has been exceeded
                    if (HousingStatus[_resource] >= HousingMaxes[_resource])
                    {
                        // Then this cannot host that resource
                        return false;
                    }
                }
                else
                {
                    // Otherwise it's not a valid host
                    return false;
                }
            }
            // Then they must share a type AND have space
            return true;
        }
        else
        {
            // If the child object has no GameProperties then it can be hosted
            return false;
        }
    }
    
    /// <summary>
    /// Checks if the child can be moved using .IsDragable
    /// Then decrements the appropriate HousingStatus entry in the parent
    /// </summary>
    /// <param name="_child">A child of this object that is to be unhosted</param>
    /// <returns>true if the child was unhosted</returns>
    public bool UnHost(GameObject _child)
    {
        // Check that the child is indeed a child
        if (HostedChildren.Contains(_child))
        {
            // If the child is dragable proceed, otherwise fail and return false
            if (_child.GetComponent<GameProperties>().IsDragable)
            {
                // Go through each of the resource types of the child and decement the housing status
                foreach (string _resource in _child.GetComponent<GameProperties>().GetResrouceTypeTags())
                {
                    HousingStatus[_resource]--;
                }
                HostedChildren.Remove(_child);
                return true;
            }
        }
        // If it didn't return true there was no UnHost
        return false;
    }

    /// <summary>
    /// If valid, will make _child a child of this object and update the housing status
    /// </summary>
    /// <param name="_child">The GameObject hosted on this object</param>
    /// <returns>true if the nesting operation was a success</returns>
    public bool Host(GameObject _child)
    {
        // If this object cannot host _child
        if (!CanHost(_child))
        {
            return false;
        }

        // Try to unhost it from that previous parent
        if (!_child.transform.parent.gameObject.GetComponent<GameProperties>().UnHost(_child))
        {
            // If that didn't work return false
            return false;
        }

        // Increment the housing status of this board
        foreach (string _resource in _child.GetComponent<GameProperties>().GetResrouceTypeTags())
        {
            HousingStatus[_resource]++;
        }

        // Attach the child to this object and snap it to the original locaiton offset
        _child.transform.parent = this.transform;
        HostedChildren.Add(_child);
        GetComponent<AudioSource>().PlayOneShot(PlaceSound);
        return true;
    }
}
