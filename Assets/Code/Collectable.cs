using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class Collectable : MonoBehaviour, ICollidable
    {
        [SerializeField] private SOCollectableValues values;

        public void OnCollision()
        {
            SetScore();
            SetCarbon();

            // TODO: Change Destroy() to pooling
            Destroy(gameObject);
        }

        private void SetScore()
        {
            ScoreManager.Instance.AddScore(values.score);
        }

        private void SetCarbon()
        {
            CarbonManager.Instance.AddCarbon(values.carbonImpact);
        }
    }
}
