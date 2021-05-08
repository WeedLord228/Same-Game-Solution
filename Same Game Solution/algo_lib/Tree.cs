using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Same_Game_Solution.engine;

namespace Same_Game_Solution.algo_lib
{
    public class TreeNode<T>
    {
        public double Score { get; set; }

        public List<TreeNode<T>> Childs { get; set; }

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

        public override string ToString()
        {
            var sb = new StringBuilder();
            // sb.Append(Score);
            sb.Append("TURN :" + Data);
            sb.Append("\n");
            sb.Append("GAME STATE :\n" + GameState);
            sb.Append("\n");
            sb.Append("SCORE :\n" + GameState.Score);
            sb.Append("\n");
            sb.Append("EVALUATED :\n" + Score);
            
            return sb.ToString();
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
            var allLeaves = new List<TreeNode<T>>();
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this.Root);
            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                foreach (var child in currentNode.Childs)
                {
                    if (child.Childs == null)
                    {
                        allLeaves.Add(child);
                    }
                    else
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            var maxScore = allLeaves.Max(x => x.Score);
            return allLeaves.First(x => x.Score == maxScore);
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
                res.Add(currentNode.Data);
                currentNode = currentNode.Batya;
            }

            res.Reverse();
            return res;
        }
    }
}