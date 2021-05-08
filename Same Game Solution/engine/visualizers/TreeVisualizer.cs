using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Same_Game_Solution.algo_lib;

namespace Same_Game_Solution.engine
{
    public class TreeVisualizer : IVisualizer<Tree<Block>>
    {
        private Graph GetGraph(Tree<Block> tree)
        {
            var graph = new Microsoft.Msagl.Drawing.Graph("graph");
            var allLeaves = new List<TreeNode<Block>>();
            var queue = new Queue<TreeNode<Block>>();
            queue.Enqueue(tree.Root);
            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                if (currentNode?.Childs != null)
                    foreach (var child in currentNode?.Childs)
                    {
                        graph.AddEdge(currentNode.ToString(), child.ToString());
                        queue.Enqueue(child);
                    }
            }

            return graph;
        }

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
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
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
    }
}