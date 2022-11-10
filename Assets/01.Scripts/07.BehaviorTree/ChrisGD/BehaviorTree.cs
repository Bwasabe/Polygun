using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChrisGD
{
    public class BehaviorTree : MonoBehaviour
    {
        private BTNode _mRoot;
        private bool _startBehavior;
        private Coroutine _behavior;

        public Dictionary<string,object> Blackboard { get; set; }
        public BTNode Root => _mRoot;

        private void Start() {
            Blackboard = new Dictionary<string, object>();
            Blackboard.Add("WorldBounds", new Rect(0, 0, 5, 5));

            // _mRoot = new BTNode(this);
            _mRoot = new BTRepeater(this, new BTSequencer(this, new BTNode[] { new BTRandomWalk(this) }));


            _startBehavior = false;
        }

        private void Update() {
            if(!_startBehavior)
            {
                _behavior = StartCoroutine(RunBehavior());
                _startBehavior = true;
            }
        }

        private IEnumerator RunBehavior()
        {
            BTNode.Result result = Root.Execute();
            while(result == BTNode.Result.Running)
            {
                Debug.Log("Root result : " + result);
                yield return null;
                result = Root.Execute();
            }

            Debug.Log("Behavior has finished with: " + result);
        }

    }
}

