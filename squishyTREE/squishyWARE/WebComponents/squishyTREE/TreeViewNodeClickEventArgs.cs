namespace squishyWARE.WebComponents.squishyTREE
{
    using System;

    public class TreeViewNodeClickEventArgs : EventArgs
    {
        private TreeNode node;

        public TreeViewNodeClickEventArgs(TreeNode n)
        {
            this.node = n;
        }

        public TreeNode Node
        {
            get
            {
                return this.node;
            }
        }
    }
}

