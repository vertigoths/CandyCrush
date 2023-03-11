using System;
using UnityEngine;

namespace BlockRelated
{
    public class BlockInteraction : MonoBehaviour
    {
        private Block _lastInteractedBlock;
        public static BlockInteraction Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        public void Select(Block interactedBlock)
        {
            _lastInteractedBlock = interactedBlock;
        }

        public void DeSelect()
        {
            _lastInteractedBlock = null;
        }

        public void Interact(Block interactedBlock)
        {
            if (_lastInteractedBlock)
            {
                var firstCell = _lastInteractedBlock.GetCell();
                var secondCell = interactedBlock.GetCell();
                
                interactedBlock.ChangeCellTo(firstCell);
                _lastInteractedBlock.ChangeCellTo(secondCell);
                
                _lastInteractedBlock = null;
            }
        }
    }
}
