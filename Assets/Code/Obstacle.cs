using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Polar
{
    public class Obstacle : MonoBehaviour, ICollidable
    {
        private UnityEvent endGameEvent;

        public void OnCollision()
        {
            Debug.LogWarning(this.name + " collided with a bear.");
            // TODO: GameManager gameSpeed
            // TODO: Obstacle call SetGameSpeed 0 OnCollision
            // TODO: End game
            //GameManager.Instance.EndGame();
        }

        // TODO: Get rid off SameSpeed SO
        // TODO: Game speed setting here?
    }
}