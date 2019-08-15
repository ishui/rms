namespace squishyWARE.WebComponents.squishyTREE
{
    using System;
    using System.Web.UI;

    public class StandardRenderingAgent : RenderingAgent
    {
        public StandardRenderingAgent(TreeView tvw) : base(tvw)
        {
        }

        public override void RenderCheckbox(TreeNode node, HtmlTextWriter output)
        {
            if (node.ShowCheckBox)
            {
                string postBackClientEvent = base.TreeView.Page.GetPostBackClientEvent(base.TreeView, node.UniqueID + ":checkbox");
                output.Write("<input type='checkbox' onclick=\"this.blur();");
                output.Write(postBackClientEvent);
                output.Write("\"");
                if (node.IsChecked)
                {
                    output.Write(" checked");
                }
                output.Write(">");
            }
        }

        public override void RenderImageLink(TreeNode node, HtmlTextWriter output)
        {
            if (node.IsFolder)
            {
                string postBackClientHyperlink = base.TreeView.Page.GetPostBackClientHyperlink(base.TreeView, node.UniqueID);
                if ((base.TreeView.ExpandedImage.Trim().Length != 0) && (base.TreeView.ExpandedImage.Trim().Length != 0))
                {
                    output.WriteBeginTag("a");
                    output.WriteAttribute("href", postBackClientHyperlink, false);
                    output.Write('>');
                    if (node.IsExpanded)
                    {
                        output.Write("<img src='");
                        output.Write(base.TreeView.ExpandedImage);
                        output.Write("' border='0'>");
                    }
                    else
                    {
                        output.Write("<img src='");
                        output.Write(base.TreeView.CollapsedImage);
                        output.Write("' border='0'>");
                    }
                    output.WriteEndTag("a");
                }
            }
            else if (base.TreeView.NonFolderImage.Trim().Length != 0)
            {
                output.Write("<img src='");
                output.Write(base.TreeView.NonFolderImage);
                output.Write("'>");
            }
            else
            {
                output.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
            }
        }

        public override void RenderNodeEnd(TreeNode node, HtmlTextWriter output)
        {
            output.WriteEndTag("div");
        }

        public override void RenderNodeStart(TreeNode node, HtmlTextWriter output)
        {
            output.WriteBeginTag("div");
            output.WriteAttribute("style", "margin-left:" + (node.Indent * 10) + "px;");
            output.Write('>');
        }

        public override void RenderNodeText(TreeNode node, HtmlTextWriter output)
        {
            bool flag = (node.Controls.Count == 0) && (base.TreeView.NodeDisplayStyle == NodeDisplayStyle.LeafNodesNoLink);
            if (!flag)
            {
                output.Write("<a name='" + node.Key + "'>&nbsp;</a>");
                output.WriteBeginTag("a");
                output.WriteAttribute("href", base.TreeView.Page.GetPostBackClientHyperlink(base.TreeView, node.UniqueID), false);
                output.WriteAttribute("class", base.TreeView.CssClass);
                output.Write('>');
            }
            output.Write(node.Text);
            if (!flag)
            {
                output.WriteEndTag("a");
            }
        }

        public override void RenderTreeEnd(HtmlTextWriter output)
        {
            output.WriteEndTag("div");
        }

        public override void RenderTreeStart(HtmlTextWriter output)
        {
            output.WriteBeginTag("div");
            string text = "overflow:";
            if (base.TreeView.Scrolling)
            {
                text = text + "auto";
            }
            else
            {
                text = text + "none";
            }
            if (!base.TreeView.Width.IsEmpty)
            {
                text = text + ";width:" + base.TreeView.Width.ToString();
            }
            if (!base.TreeView.Height.IsEmpty)
            {
                text = text + ";height:" + base.TreeView.Height.ToString();
            }
            output.WriteAttribute("style", text);
            output.Write('>');
        }
    }
}

