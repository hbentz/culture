using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvancedProperties : MonoBehaviour
{
    
    [SerializeField]
    public List<string> HostableTags = new List<string>();

    [SerializeField]
    public List<int> TagMaxes = new List<int>();  // Needs to be same size and order as Hostable Tags

    [SerializeField]
    public List<string> GameTags = new List<string>();

    [SerializeField]
    public List<string> UITags = new List<string>();

    // Keeps track of how many of each object this Gamobject is holding
    public Dictionary<string, int> HousingStatus;
    public Dictionary<string, int> HousingMaxes;

    void Start()
    {
        int i = 0;
        // Go through each of the hostable tags
        foreach (string HostableTag in HostableTags)
        {
            HousingStatus.Add(HostableTag, 0);
            HousingMaxes.Add(HostableTag, TagMaxes[i]);

            // Add to the HousingStatus the number of children with that tag
            foreach (Transform _childTransform in this.transform)
            {
                AdvancedProperties _childProp = _childTransform.GetComponent<AdvancedProperties>();
                if (_childProp != null)
                {
                    if (_childProp.HasGameTag(HostableTag))
                    {
                        HousingStatus[HostableTag]++;
                    }
                }
            }
            i++;
        }

    }

    public bool HasUITag(string tag)
    {
        return UITags.Contains(tag);
    }

    public bool HasGameTag(string tag)
    {
        return GameTags.Contains(tag);
    }

    public IEnumerable<string> GetUITags()
    {
        return UITags;
    }

    public IEnumerable<string> GetGameTags()
    {
        return GameTags;
    }

    public bool TryHostObject(GameObject Child, Vector3 Offset, bool IsTest = false)
    {
        IEnumerable<string> SharedTags = Child.GetComponent<AdvancedProperties>().GetGameTags().Intersect(GetGameTags());
        // If there aren't any common entries between the GameTags of the Child and this one 
        if (!SharedTags.Any())
        {
            return false;
        }
        else
        {
            // Otherwise iterate through the shared tags and see if somewhere exceeds
            foreach (string tag in SharedTags)
            {
                if (HousingStatus[tag] >= HousingMaxes[tag])
                {
                    return false;
                }
            }

            // The child object must now share at least one tag and will not overflow any housing capacities
            if (AttachObject)
            {
                // Increment the housing status
                foreach (string tag in SharedTags)
                {
                    HousingStatus[tag]++;
                }

                // Attach the object and snap it to the offset
                Child.transform.parent = this.transform;
                Child.transform.localPosition = Offset;
            }

            // Give the all clear
            return true;
        }
    }
}
