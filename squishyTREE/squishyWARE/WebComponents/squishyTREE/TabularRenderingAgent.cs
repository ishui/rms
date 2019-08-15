namespace squishyWARE.WebComponents.squishyTREE
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Web.UI;

    public class TabularRenderingAgent : StandardRenderingAgent
    {
        public TabularRenderingAgent(TreeView tvw) : base(tvw)
        {
        }

        public override void RenderNodeEnd(TreeNode node, HtmlTextWriter output)
        {
            output.WriteEndTag("div");
            output.WriteEndTag("td");
            foreach (object[] objArray in base.TreeView.headers)
            {
                string text = (string) objArray[0];
                string text2 = (string) objArray[1];
                Type type = (Type) objArray[2];
                string text3 = (string) objArray[3];
                output.WriteBeginTag("td");
                output.WriteAttribute("bgcolor", ColorTranslator.ToHtml(base.TreeView.DataBackColor));
                output.WriteAttribute("style", "color:" + ColorTranslator.ToHtml(base.TreeView.DataForeColor) + ";");
                output.WriteAttribute("align", objArray[4].ToString());
                output.Write('>');
                if (node.TaggedValues[text3] != null)
                {
                    string text4 = node.TaggedValues[text3];
                    if ((text2.Trim() != "") && (type.FullName != "System.String"))
                    {
                        object target = type.InvokeMember("Parse", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new object[] { text4 });
                        text4 = (string) type.InvokeMember("ToString", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, target, new object[] { text2 });
                    }
                    output.Write(text4);
                }
                else
                {
                    output.Write("&nbsp;");
                }
                output.WriteEndTag("td");
            }
            output.WriteEndTag("tr");
        }

        public override void RenderNodeStart(TreeNode node, HtmlTextWriter output)
        {
            output.WriteFullBeginTag("tr");
            output.WriteBeginTag("td");
            output.WriteAttribute("bgcolor", ColorTranslator.ToHtml(base.TreeView.DataBackColor));
            output.WriteAttribute("style", "color:" + ColorTranslator.ToHtml(base.TreeView.DataForeColor) + ";");
            output.Write('>');
            output.WriteBeginTag("div");
            output.WriteAttribute("style", "margin-left:" + (node.Indent * 10) + "px;");
            output.Write('>');
        }

        public override void RenderTreeEnd(HtmlTextWriter output)
        {
            output.Write("</table>");
        }

        public override void RenderTreeStart(HtmlTextWriter output)
        {
            output.Write("<table cellpadding='" + base.TreeView.TableCellpadding.ToString() + "' cellspacing='" + base.TreeView.TableCellspacing.ToString() + "' border='" + base.TreeView.TableBorder.ToString() + "' bgcolor='" + base.TreeView.TableBackColor.Name + "'");
            output.WriteAttribute("width", base.TreeView.Width.ToString());
            if (base.TreeView.headers.Count > 0)
            {
                output.WriteFullBeginTag("tr");
                output.WriteBeginTag("td");
                output.WriteAttribute("bgcolor", ColorTranslator.ToHtml(base.TreeView.HeaderBackColor));
                output.WriteAttribute("style", "color:" + ColorTranslator.ToHtml(base.TreeView.HeaderForeColor) + ";");
                output.WriteAttribute("align", base.TreeView.TableHeaderHorizAlign);
                output.Write('>');
                output.Write("&nbsp;");
                output.WriteEndTag("td");
                foreach (object[] objArray in base.TreeView.headers)
                {
                    output.WriteBeginTag("td");
                    output.WriteAttribute("bgcolor", ColorTranslator.ToHtml(base.TreeView.HeaderBackColor));
                    output.WriteAttribute("style", "color:" + ColorTranslator.ToHtml(base.TreeView.HeaderForeColor) + ";");
                    output.WriteAttribute("align", base.TreeView.TableHeaderHorizAlign);
                    output.Write('>');
                    output.Write(objArray[0]);
                    output.WriteEndTag("td");
                }
                output.WriteEndTag("tr");
            }
        }
    }
}

