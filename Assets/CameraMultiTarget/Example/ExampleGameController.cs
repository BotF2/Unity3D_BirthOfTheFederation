using UnityEngine;

public class ExampleGameController : MonoBehaviour
{
    ////public CameraMultiTarget cameraMultiTarget;
    //public GameObject targetPrefab;

    //// private IEnumerator Start() {
    ////    var numberOfTargets = 3;
    ////    var targets = new List<GameObject>(numberOfTargets);
    ////    targets.Add(CreateTarget());
    ////    cameraMultiTarget.SetTargets(targets.ToArray());
    ////    foreach (var _ in Enumerable.Range(0, numberOfTargets - targets.Count)) {
    ////        yield return new WaitForSeconds(5.0f);
    ////        targets.Add(CreateTarget());
    ////        cameraMultiTarget.SetTargets(targets.ToArray());
    ////    }
    ////    yield return null;
    ////}

    //private GameObject CreateTarget() {
    //    GameObject _destination = GameObject.Instantiate(targetPrefab);
    //    _destination.AddComponent<ExampleTargetBehaviour>();
    //    return _destination;
    //}

}