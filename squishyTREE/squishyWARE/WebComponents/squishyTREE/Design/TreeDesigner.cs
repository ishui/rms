namespace squishyWARE.WebComponents.squishyTREE.Design
{
    using squishyWARE.WebComponents.squishyTREE;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.Design;

    public class TreeDesigner : ControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            TreeView component = (TreeView) base.Component;
            component.Controls.Clear();
            component.ClearHeaders();
            component.AddHeader("DateTime Header", "yyyy-MM-dd", typeof(DateTime), "val1", "center");
            component.AddHeader("Double Header (currency)", "c", typeof(double), "val2", "right");
            component.AddHeader("Double Header", "n", typeof(double), "val3", "right");
            component.AddHeader("String Header", "", typeof(string), "val4", "left");
            TreeNode node = component.AddNode("Test Node 1", "n1", true);
            node.AddTaggedValue("val1", "1/1/2001");
            node.AddTaggedValue("val2", "90873");
            node.AddTaggedValue("val3", "90873");
            node.AddTaggedValue("val4", "Hello!");
            TreeNode node2 = component.AddNode("Test Node 2", "n2", true);
            node2.AddTaggedValue("val1", "1/1/2001");
            node2.AddTaggedValue("val2", "90873");
            node2.AddTaggedValue("val3", "90873");
            node2.AddTaggedValue("val4", "Hello!");
            node.AddNode("Sub-node", "s1", false);
            node.AddNode("Sub-node", "s2", false);
            node2.AddNode("Sub-node", "s3", true);
            node2.AddNode("Sub-node", "s4", true);
            component.ExpandAll();
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            component.RenderControl(writer2);
            return writer.ToString();
        }
    }
}

