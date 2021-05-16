using System.Collections;
using System.Collections.Generic;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class BeamSearchTree<T> : Tree<T>
        where T : Block
    {
        public BeamSearchTree(TreeNode<T> root) : base(root)
        {
        }

        public IEnumerable<T> GetBlockPath(TreeNode<T> node)
        {
            var res = new List<T>();
            var currentNode = node;
            while (currentNode.Batya != null)
            {
                res.Add(currentNode.Data);
                currentNode = currentNode.Batya;
            }

            res.Reverse();
            return res;
        }

        public TreeNode<T> GetNodeByGameState(GameState gameState)
        {
            var queue = new Queue<TreeNode<Block>>();
            queue.Enqueue(Root as TreeNode<Block>);

            while (queue.Count != 0)
            {
                var currentElem = queue.Dequeue();

                if (currentElem.GameState == gameState)
                {
                    return currentElem as TreeNode<T>;
                }

                if (currentElem.Children != null)
                    foreach (var cgild in currentElem.Children)
                    {
                        queue.Enqueue(cgild);
                    }
            }
            return null;
        }
    }
}