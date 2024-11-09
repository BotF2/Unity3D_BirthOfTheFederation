using UnityEngine;

namespace Assets.Core
{

    public class E_Animator3 : MonoBehaviour
    {
        // public bool _warpingInOver = false;
        public Animator anim;
        public AudioSource warpAudioSource_0;
        //private SetShipLayerByAnimaStat shipLayerSetup;
        //private CameraMultiTarget cameraMultiTarget;
        int once = 0;

        void Start()
        {
            anim = GetComponent<Animator>();
            // cameraMultiTarget = GetComponent<CameraMultiTarget>();
        }

        // Update is called once per frame  
        void Update()
        {
            if (GameManager.Instance._statePassedCombatInit)
            {
                anim.SetBool("EnemyWarp3", true);
                PlayWarp();
            }
            // lets warp animation run
        }

        public void PlayWarp() // called in animation - warp
        {
            if (GameManager.Instance._statePassedCombatInit)
            {
                warpAudioSource_0.volume = 1f;
                warpAudioSource_0.Play();
            }
        }
        public void SetShipLayers()
        {
            GameManager.Instance.SetShipLayer();
            GameManager.Instance.WarpingInCompleted();
        }
    }
}
