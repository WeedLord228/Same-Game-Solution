using System.Collections.Generic;
using System.Linq;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class TreeNode<T>
    {
        public double Score { get; }

        public List<TreeNode<T>> Childs { get; }

        public TreeNode<T> Batya { get; }

        public T Data { get; }

        public GameState GameState { get; }

        public TreeNode(double score, List<TreeNode<T>> childs, TreeNode<T> batya, T data, GameState gameState)
        {
            this.Score = score;
            this.Childs = childs;
            this.Batya = batya;
            this.Data = data;
            this.GameState = gameState;
        }
    }

    public class Tree<T>
    {
        public TreeNode<T> Root { get; }

        public TreeNode<T> BestLeaf
        {
            get => this.GetBestLeaf();
        }

        private TreeNode<T> GetBestLeaf()
        {
            var allChilds = new List<TreeNode<T>>();
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this.Root);
            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                foreach (var child in currentNode.Childs)
                {
                    if (child.Childs == null)
                    {
                        allChilds.Add(child);
                    }
                    else
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            var maxScore = allChilds.Max(x => x.Score);
            return allChilds.First(x => x.Score == maxScore);
        }

        public Tree(TreeNode<T> root)
        {
            this.Root = root;
        }


        public IEnumerable<T> GetBestPath()
        {
            var res = new List<T>();
            var currentNode = BestLeaf;
            while (currentNode.Batya != null)
            {
                res.Add(currentNode.Batya.Data);
                currentNode = currentNode.Batya;
            }

            res.Reverse();
            return res;
        }
    }
}