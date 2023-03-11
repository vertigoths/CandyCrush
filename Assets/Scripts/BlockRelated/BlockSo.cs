using UnityEngine;

namespace BlockRelated
{
    [CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/BlockSo", order = 1)]
    public class BlockSo : ScriptableObject
    {
        public BlockType blockType;
        public Sprite sprite;
    }
}
