using UnityEngine;
using UnityEngine.SceneManagement;

//this class allows some objects to not being destroyed in order to perform transitions between gameplay scenes.
public class DontDestroyOnGamePlay : MonoBehaviour
{
    private DontDestroyOnGamePlay[] notDestructibleObjects;
    private void Awake()
    {

       notDestructibleObjects = GameObject.FindObjectsOfType(typeof(DontDestroyOnGamePlay)) as DontDestroyOnGamePlay[];
        
       DontDestroyOnLoad(gameObject);
        
    }

    private void Update()
    {
        //destroy those objects if there´s not a gameplay scene loaded
        if (!(SceneManager.GetActiveScene().name.Equals("MainScene") || SceneManager.GetActiveScene().name.Equals("Zone2")))
            Destroy(gameObject);

        //destroy repeated objects
        foreach (DontDestroyOnGamePlay item in notDestructibleObjects)
        {
            if (this.gameObject.name == item.gameObject.name && !gameObject.Equals(item.gameObject))
            {
                Destroy(gameObject);
            }
            
        }

    }
}
