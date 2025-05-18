using Unity.Netcode;
using UnityEngine;
using Assets.Core;

/// <summary>
/// Represents a player in the HelloWorldManager system.
/// Component of Player prefab.
/// </summary>
public class HelloWorldPlayer : MonoBehaviour
{
    /// <summary>
    /// Moves the player. Add your movement logic here.
    /// </summary>
    public void Move()
    {
        Debug.Log("Player moved.");
    }
}
