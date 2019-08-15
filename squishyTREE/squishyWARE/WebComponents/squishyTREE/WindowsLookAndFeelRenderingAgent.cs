namespace squishyWARE.WebComponents.squishyTREE
{
    using System;
    using System.Text;
    using System.Web.UI;

    public class WindowsLookAndFeelRenderingAgent : StandardRenderingAgent
    {
        private bool first;

        public WindowsLookAndFeelRenderingAgent(TreeView tvw) : base(tvw)
        {
            this.first = true;
        }

        private bool IsFirst()
        {
            if (this.first)
            {
                this.first = false;
                return true;
            }
            return false;
        }

        private bool ParentHasSibling(TreeNode source, int indentDiff)
        {
            Control parent = source;
            for (int i = 0; i < indentDiff; i++)
            {
                parent = parent.Parent;
            }
            if (parent is TreeNode)
            {
                return (((TreeNode) parent).NextSibling() != null);
            }
            return false;
        }

        public override void RenderImageLink(TreeNode node, HtmlTextWriter output)
        {
            int indent = node.Indent;
            StringBuilder builder = new StringBuilder();
            while (indent > 0)
            {
                bool flag2;
                bool flag = node.NextSibling() != null;
                bool flag3 = node.Parent is TreeView;
                if (node.Parent is TreeNode)
                {
                    flag2 = this.ParentHasSibling(node, node.Indent - indent);
                }
                else
                {
                    flag2 = false;
                }
                if (node.Indent == indent)
                {
                    if (node.HasControls())
                    {
                        string text = "<a href=\"" + base.TreeView.Page.GetPostBackClientHyperlink(base.TreeView, node.UniqueID) + "\">";
                        string text2 = "</a>";
                        if (flag)
                        {
                            if (node.IsExpanded)
                            {
                                if (this.IsFirst())
                                {
                                    builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "topexpandedsibling.gif' border='0'>" + text2);
                                }
                                else
                                {
                                    builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "middleexpandedsibling.gif' border='0'>" + text2);
                                }
                            }
                            else if (this.IsFirst())
                            {
                                builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "topcollapsedsibling.gif' border='0'>" + text2);
                            }
                            else
                            {
                                builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "middlecollapsedsibling.gif' border='0'>" + text2);
                            }
                        }
                        else if (node.IsExpanded)
                        {
                            if (this.IsFirst())
                            {
                                builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "topexpandednosibling.gif' border='0'>" + text2);
                            }
                            else
                            {
                                builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "middleexpandednosibling.gif' border='0'>" + text2);
                            }
                        }
                        else if (this.IsFirst())
                        {
                            builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "topcollapsednosibling.gif' border='0'>" + text2);
                        }
                        else
                        {
                            builder.Insert(0, text + "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "middlecollapsednosibling.gif' border='0'>" + text2);
                        }
                    }
                    else if (flag)
                    {
                        builder.Insert(0, "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "middlesiblingnochildren.gif' border='0'>");
                    }
                    else
                    {
                        builder.Insert(0, "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "bottomnosiblingnochildren.gif' border='0'>");
                    }
                }
                else if (flag2)
                {
                    builder.Insert(0, "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "vertbardots.gif' border='0'>");
                }
                else
                {
                    builder.Insert(0, "<img align='top' src='" + base.TreeView.WindowsLafImageBase + "clear.gif' border='0'>");
                }
                indent--;
            }
            output.Write(builder.ToString());
        }

        public override void RenderNodeEnd(TreeNode node, HtmlTextWriter output)
        {
            output.Write("</nobr></td></tr>");
        }

        public override void RenderNodeStart(TreeNode node, HtmlTextWriter output)
        {
            output.Write("<tr><td><nobr>");
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
                if (node.IsExpanded)
                {
                    output.Write("<img src='" + base.TreeView.WindowsLafImageBase + "openfolder.gif' border='0'>");
                }
                else
                {
                    output.Write("<img src='" + base.TreeView.WindowsLafImageBase + "closedfolder.gif' border='0'>");
                }
            }
            output.Write("&nbsp;");
            if ((base.TreeView.SelectedNode() != null) && (base.TreeView.SelectedNode().Key == node.Key))
            {
                output.Write("<b>");
                output.Write(node.Text);
                output.Write("</b>");
            }
            else
            {
                output.Write(node.Text);
            }
            if (!flag)
            {
                output.WriteEndTag("a");
            }
        }

        public override void RenderTreeEnd(HtmlTextWriter output)
        {
            output.WriteEndTag("table");
        }

        public override void RenderTreeStart(HtmlTextWriter output)
        {
            output.WriteBeginTag("table");
            output.WriteAttribute("cellpadding", "0");
            output.WriteAttribute("cellspacing", "0");
            output.WriteAttribute("border", "0");
            output.Write('>');
        }
    }
}

