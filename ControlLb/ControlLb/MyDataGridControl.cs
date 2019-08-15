namespace ControlLb
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyDataGridControl : WebControl
    {
        private string bgColor;
        private int border;
        private string connectionString;
        private string headerColor;
        private int height;
        private string sqlQuery;
        private int width;

        public MyDataGridControl() : base(HtmlTextWriterTag.Table)
        {
        }

        protected override void AddAttributesToRender(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Border, this.border.ToString());
            output.AddAttribute(HtmlTextWriterAttribute.Bgcolor, this.bgColor);
            output.AddAttribute(HtmlTextWriterAttribute.Height, this.height.ToString());
            output.AddAttribute(HtmlTextWriterAttribute.Width, this.width.ToString());
            base.AddAttributesToRender(output);
        }

        protected override void OnInit(EventArgs e)
        {
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            string text = null;
            SqlConnection connection = new SqlConnection(this.connectionString);
            connection.Open();
            SqlDataReader reader = new SqlCommand(this.sqlQuery, connection).ExecuteReader(CommandBehavior.CloseConnection);
            int fieldCount = reader.FieldCount;
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddAttribute(HtmlTextWriterAttribute.Colspan, fieldCount.ToString());
            output.AddAttribute(HtmlTextWriterAttribute.Align, "Center");
            output.AddAttribute(HtmlTextWriterAttribute.Bgcolor, "Red");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.RenderBeginTag(HtmlTextWriterTag.B);
            output.Write("Wrox Data Display Table Control");
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.AddAttribute(HtmlTextWriterAttribute.Bgcolor, this.headerColor);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            for (int i = 0; i < fieldCount; i++)
            {
                output.RenderBeginTag(HtmlTextWriterTag.Td);
                output.Write(reader.GetName(i).ToUpper());
                output.RenderEndTag();
            }
            output.RenderEndTag();
            while (reader.Read())
            {
                output.RenderBeginTag(HtmlTextWriterTag.Tr);
                for (int j = 0; j < fieldCount; j++)
                {
                    output.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (reader.IsDBNull(j))
                    {
                        text = "";
                    }
                    else
                    {
                        switch (reader.GetFieldType(j).ToString())
                        {
                            case "System.Int16":
                                text = reader.GetInt16(j).ToString();
                                goto Label_0308;

                            case "System.Int32":
                                text = reader.GetInt32(j).ToString();
                                goto Label_0308;

                            case "System.Int64":
                                text = reader.GetInt64(j).ToString();
                                goto Label_0308;

                            case "System.Decimal":
                                text = reader.GetDecimal(j).ToString();
                                goto Label_0308;

                            case "System.DateTime":
                                text = reader.GetDateTime(j).ToString();
                                goto Label_0308;

                            case "System.String":
                                text = reader.GetString(j).ToString();
                                goto Label_0308;

                            case "System.Boolean":
                                text = reader.GetBoolean(j).ToString();
                                goto Label_0308;

                            case "System.Guid":
                                text = reader.GetGuid(j).ToString();
                                goto Label_0308;

                            case "System.Double":
                                text = reader.GetDouble(j).ToString();
                                goto Label_0308;

                            case "System.Byte":
                                text = reader.GetByte(j).ToString();
                                goto Label_0308;
                        }
                    }
                Label_0308:
                    output.Write(text);
                    output.RenderEndTag();
                }
                output.RenderEndTag();
            }
            base.RenderContents(output);
        }

        public string BGColor
        {
            get
            {
                return this.bgColor;
            }
            set
            {
                this.bgColor = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            set
            {
                this.connectionString = value;
            }
        }

        public string HeaderColor
        {
            get
            {
                return this.headerColor;
            }
            set
            {
                this.headerColor = value;
            }
        }

        public string SqlQuery
        {
            get
            {
                return this.sqlQuery;
            }
            set
            {
                this.sqlQuery = value;
            }
        }

        public int TableBorder
        {
            get
            {
                return this.border;
            }
            set
            {
                this.border = value;
            }
        }

        public int TableHeight
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public int TableWidth
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }
    }
}

