using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvancedProperties : MonoBehaviour
{
    
    [SerializeField]
    public List<string> HostableResources = new List<string>();

    [SerializeField]
    public List<int> TagMaxes = new List<int>();  // Needs to be same size and order as Hostable Tags

    [SerializeField]
    public List<string> ResrouceTypeTags = new List<string>();

    [SerializeField]
    public List<string> PropertyTags = new List<string>();

    // Keeps track of how many of each object this Gamobject is holding
    public Dictionary<string, int> HousingStatus =  new Dictionary<string, int>();
    public Dictionary<string, int> HousingMaxes = new Dictionary<string, int>();

    void Start()
    {
        int i = 0;
        // Go through each of the hostable tags
        foreach (string HostableResource in HostableResources)
        {
            HousingStatus.Add(HostableResource, 0);
            HousingMaxes.Add(HostableResource, TagMaxes[i]);

            // Add to the HousingStatus the number of children with that tag
            foreach (Transform _childTransform in this.transform)
            {
                AdvancedProperties _childProp = _childTransform.GetComponent<AdvancedProperties>();
                if (_childProp != null)
                {
                    if (_childProp.HasGameTag(HostableResource))
                    {
                        HousingStatus[HostableResource]++;
                    }
                }
            }
            i++;
        }

    }

    public bool HasPropertyTag(string tag)
    {
        return PropertyTags.Contains(tag);
    }

    public bool HasGameTag(string tag)
    {
        return ResrouceTypeTags.Contains(tag);
    }

    public IEnumerable<string> GetPropertyTags()
    {
        return PropertyTags;
    }

    public IEnumerable<string> GetResrouceTypeTags()
    {
        return ResrouceTypeTags;
    }

    public IEnumerable<string> GetHostableResources()
    {
        return HostableResources;
    }

    public bool TryHostObject(GameObject Child, Vector3 Offset, bool IsTest = false)
    {
        // TODO: NEED TO UNHOST CHILD FROM ORIGINAL PARENT 
        IEnumerable<string> SharedTags = Child.GetComponent<AdvancedProperties>().GetResrouceTypeTags().Intersect(GetHostableResources());
        // If there aren't any common entries between the ResrouceTypeTags of the Child and this one 
        if (!SharedTags.Any())
        {
            Debug.Log("Can't host due to no shared features");
            return false;
        }
        else
        {
            // Otherwise iterate through the shared tags and see if somewhere exceeds
            foreach (string tag in SharedTags)
            {
                if (HousingStatus[tag] >= HousingMaxes[tag])
                {
                    Debug.Log("Can't host because it would exceed max housing for " + tag);
                    return false;
                }
            }

            // The child object must now share at least one tag and will not overflow any housing capacities
            if (!IsTest)
            {
                // Increment the housing status
                foreach (string tag in SharedTags)
                {
                    HousingStatus[tag]++;
                }

                // Attach the object and snap it to the offset
                Child.transform.parent = this.transform;
                Child.transform.localPosition = Offset;
                Debug.Log("Sucessfully Hosted");
            }

            // Give the all clear
            return true;
        }
    }
}
