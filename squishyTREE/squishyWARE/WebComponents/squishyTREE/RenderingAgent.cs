namespace squishyWARE.WebComponents.squishyTREE
{
    using System;
    using System.Web.UI;

    public abstract class RenderingAgent
    {
        private squishyWARE.WebComponents.squishyTREE.TreeView tvw;

        protected RenderingAgent(squishyWARE.WebComponents.squishyTREE.TreeView tvw)
        {
            this.tvw = tvw;
        }

        public abstract void RenderCheckbox(TreeNode node, HtmlTextWriter output);
        public abstract void RenderImageLink(TreeNode node, HtmlTextWriter output);
        public abstract void RenderNodeEnd(TreeNode node, HtmlTextWriter output);
        public abstract void RenderNodeStart(TreeNode node, HtmlTextWriter output);
        public abstract void RenderNodeText(TreeNode node, HtmlTextWriter output);
        public abstract void RenderTreeEnd(HtmlTextWriter output);
        public abstract void RenderTreeStart(HtmlTextWriter output);

        protected squishyWARE.WebComponents.squishyTREE.TreeView TreeView
        {
            get
            {
                return this.tvw;
            }
        }
    }
}

