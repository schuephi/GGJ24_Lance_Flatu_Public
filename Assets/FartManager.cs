using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartManager : MonoBehaviour
{
    public AudioSource FartSource;

    public List<AudioClip> ShortFarts;
    public List<AudioClip> LongFarts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fart()
    {
        var randomIndex = Mathf.Clamp(Mathf.RoundToInt(Random.Range(0, ShortFarts.Count)), 0, ShortFarts.Count -1);
        FartSource.clip = this.ShortFarts[randomIndex];
        FartSource.Play();
    }
}
