using System.Collections.Generic;
using UnityEngine;

public class MiniGameSelector
{
    static void Main()
    {
        // Create a list of strings
        List<string> items = new List<string> { "Apple", "Banana", "Cherry", "Date", "Elderberry" };

        // Create a random object
        System.Random random = new System.Random();

        // Choose a random string from the list
        string randomItem = items[random.Next(items.Count)];

        // Display the chosen string
        Debug.Log("Randomly selected item: " + randomItem);
    }
}

