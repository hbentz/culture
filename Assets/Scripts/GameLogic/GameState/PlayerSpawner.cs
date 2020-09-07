using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    // Dict<nPlayers, PlayerPositions>
    public Dictionary<int, List<Vector3>> SpawnLocations = new Dictionary<int, List<Vector3>>()
    {
        { 1, new List<Vector3>() { new Vector3(0, 0, -22) } }, // Single Player1 Position

        { 2, new List<Vector3>()
            {new Vector3(0, 0, -22), // TwoPlayer Player1 Position
             new Vector3(0, 0, 22)} // TwoPlayer Player2 Position
        },

        { 3, new List<Vector3>()
            {new Vector3(0, 0, -24), // ThreePlayer Player1 Position
             new Vector3(-24, 0, 0), // ThreePlayer Player1 Position
             new Vector3(0, 0, 24)} // ThreePlayer Player3 Position
        },

        { 4, new List<Vector3>()
            {new Vector3(0, 0, -24), // FourPlayer Player1 Position
             new Vector3(-24, 0, 0), // FourPlayer Player1 Position
             new Vector3(0, 0, 24), // FourPlayer Player3 Position
             new Vector3(24, 0, 0)} // FourPlayer Player4 Position
        },
    };

    // Dict<nPlayers, PlayerRotations>
    public Dictionary<int, List<Quaternion>> SpawnRoatations = new Dictionary<int, List<Quaternion>>()
    {
        { 1, new List<Quaternion>() { new Quaternion(0, 0, 0, 1) } }, // Single Player1 Rotation

        { 2, new List<Quaternion>()
            {new Quaternion(0, 0, 0, 1), // TwoPlayer Player1 Rotation
             new Quaternion(0, 1, 0, 0)} // TwoPlayer Player2 Rotation
        },

        { 3, new List<Quaternion>()
            {new Quaternion(0, 0, 0, 1), // ThreePlayer Player1 Rotation
             new Quaternion(0, -0.7071068f, 0, 0.7071068f), // ThreePlayer Player2 Rotation
             new Quaternion(0, 1, 0, 0)} // ThreePlayer Player3 Rotation
        },

        { 4, new List<Quaternion>()
            {new Quaternion(0, 0, 0, 1), // FourPlayer Player1 Rotation
             new Quaternion(0, -0.7071068f, 0, 0.7071068f), // FourPlayer Player2 Rotation
             new Quaternion(0, 1, 0, 0), // FourPlayer Player3 Rotation
             new Quaternion(0, 0.7071068f, 0, 0.7071068f)} // FourPlayer Player4 Rotation
        },
    };

    public LinkedList<Player> MakeNPlayers(int nPlayers)
	{
        LinkedList<Player> returnList = new LinkedList<Player>();

        // Loop nPlayers times
        for (int i = 0; i < nPlayers; i ++)
        {
            // Make a new player prefab with location and rotation as defined earlier
            GameObject _newPlayer = Instantiate(PlayerPrefab, SpawnLocations[nPlayers][i], SpawnRoatations[nPlayers][i]);
            
            // Get the player component and add it to the return list
            returnList.AddLast(_newPlayer.GetComponent<Player>());
        }

        return returnList;
    }
}
