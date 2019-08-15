namespace ZL.WebControls.DateTimeBox
{
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.Design;
    using System.Web.UI.WebControls;

    public class DateTimeBoxDesigner : ControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            ZL.WebControls.DateTimeBox.DateTimeBox component = (ZL.WebControls.DateTimeBox.DateTimeBox) base.Component;
            component.Style.Add("position", "statis");
            Table table = new Table();
            table.BorderWidth = Unit.Pixel(0);
            table.CellPadding = 0;
            table.CellSpacing = 0;
            table.Width = component.Width;
            table.Height = component.Height;
            TableRow child = new TableRow();
            TableCell cell = new TableCell();
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.VerticalAlign = VerticalAlign.Middle;
            TableCell cell2 = new TableCell();
            cell2.Width = Unit.Pixel(1);
            cell2.HorizontalAlign = HorizontalAlign.Center;
            cell2.VerticalAlign = VerticalAlign.Middle;
            TextBox box2 = new TextBox();
            box2.ID = component.ID;
            box2.CssClass = component.CssClass;
            box2.Enabled = component.Enabled;
            box2.ToolTip = component.ToolTip;
            box2.Width = Unit.Percentage(100);
            box2.Height = Unit.Percentage(100);
            box2.Text = component.Text;
            box2.Attributes.Add("defaultValue", component.Text);
            if (component.ReadOnly)
            {
                box2.ReadOnly = base.ReadOnly;
            }
            box2.Style.Add("cursor", "hand");
            if (component.DateTextAlign == ZL.WebControls.DateTimeBox.DateTimeBox.TextAlign.Left)
            {
                box2.Style.Add("text-align", "left");
            }
            else if (component.DateTextAlign == ZL.WebControls.DateTimeBox.DateTimeBox.TextAlign.Right)
            {
                box2.Style.Add("text-align", "right");
            }
            else if (component.DateTextAlign == ZL.WebControls.DateTimeBox.DateTimeBox.TextAlign.Center)
            {
                box2.Style.Add("text-align", "center");
            }
            Image image = new Image();
            if (component.ImageUrl == "")
            {
                image.ImageUrl = "/ZL_Client/Web/UI/DateTimeBox.gif";
            }
            else
            {
                image.ImageUrl = component.ImageUrl;
            }
            image.BorderWidth = Unit.Pixel(0);
            image.Style.Add("cursor", "hand");
            if (!component.Enabled)
            {
                image.Attributes.Add("disabled", "true");
            }
            cell.Controls.Add(box2);
            cell2.Controls.Add(image);
            child.Controls.Add(cell);
            child.Controls.Add(cell2);
            table.Controls.Add(child);
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            table.RenderControl(writer2);
            return writer.ToString();
        }
    }
}

