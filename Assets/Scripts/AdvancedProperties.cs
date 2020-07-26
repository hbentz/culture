using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedProperties : MonoBehaviour
{
    
    [SerializeField]
    public List<string> HostableTags = new List<string>();

    [SerializeField]
    public List<int> TagMaxes = new List<int>();  // Needs to be same size as Hostable Tags

    [SerializeField]
    public List<string> GameTags = new List<string>();

    [SerializeField]
    public List<string> UITags = new List<string>();

    public int[] HousingStatus;

    void Start()
    {
        // Keep track of how many of each object this Gamobject is holding
        HousingStatus = new int[HostableTags.Count];
        int i = 0;  // HousingSatus Counter

        // Go through each of the hostable tags
        foreach (string HostableTag in HostableTags)
        {
            // Add to the HousingStatus the number of children with that tag
            foreach (Transform _childTransform in this.transform)
            {
                AdvancedProperties _childProp = _childTransform.GetComponent<AdvancedProperties>();
                if (_childProp != null)
                {
                    if (_childProp.HasGameTag(HostableTag))
                    {
                        HousingStatus[i]++;
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

    public bool HostObject(GameObject Child)
    {
        if 
    }
}
