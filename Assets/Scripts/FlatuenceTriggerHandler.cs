using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatuenceTriggerHandler : MonoBehaviour
{
    private StoryIntroManager storyIntroManager;

    private void Awake()
    {
        storyIntroManager = FindFirstObjectByType<StoryIntroManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lance"))
        {
            if (!storyIntroManager.FartingPhaseStarted)
            {
                GameManager.Instance.ReportFirstSequenceCompleted();
            }
        }
    }
}
