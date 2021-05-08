using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Same_Game_Solution.algo_lib;

namespace Same_Game_Solution.engine.visualizers
{
    public class TreeVisualizer : IVisualizer<Tree<Block>>
    {
        public void render(Tree<Block> toRender)
        {
            //     //create a form 
            //     Form form = new Form();
            //     //create a viewer object 
            //     GViewer viewer = new GViewer();
            //     
            //     //create a graph object
            //     var graph = GetGraph(toRender);
            //     
            //     //bind the graph to the viewer 
            // viewer.Graph = graph;
            //     //associate the viewer with the form 
            //     form.SuspendLayout();
            //     viewer.Dock = DockStyle.Fill;
            //     form.Controls.Add(viewer);
            //     form.ResumeLayout();
            //     //show the form 
            //     form.ShowDialog();
            // }

            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            var viewer = new GViewer();
            //create a graph object 
            var graph = GetGraph(toRender);
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
        }

        private Graph GetGraph(Tree<Block> tree)
        {
            var graph = new Graph("graph");
            var queue = new Queue<TreeNode<Block>>();
            queue.Enqueue(tree.Root);
            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                if (currentNode?.Childs == null) continue;
                foreach (var child in currentNode?.Childs)
                {
                    graph.AddEdge(currentNode.ToString(), child.ToString());
                    queue.Enqueue(child);
                }
            }

            return graph;
        }
    }
}