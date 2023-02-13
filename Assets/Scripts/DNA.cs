using UnityEngine;

namespace MachineLearningBasic.Data
{
    public class DNA : MonoBehaviour
    {
        public float r;
        public float g;
        public float b;

        bool dead = false;
        public float timeToDie = 0;
        SpriteRenderer sRenderer;
        Collider2D sCollider;

        private void OnMouseDown()
        {
            dead = true;
            timeToDie = PopulationManager.elapsed;
            Debug.Log("Dead At: " + timeToDie);
            sRenderer.enabled = false;
            sCollider.enabled = false;
        }

        void Start()
        {
            sRenderer = GetComponent<SpriteRenderer>();
            sCollider = GetComponent<Collider2D>();

            sRenderer.color = new Color(r, g, b);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

