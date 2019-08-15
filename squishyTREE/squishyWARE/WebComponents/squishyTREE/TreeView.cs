namespace squishyWARE.WebComponents.squishyTREE
{
    using squishyWARE.WebComponents.squishyTREE.Design;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    [ToolboxData("<{0}:TreeView runat=server></{0}:TreeView>"), DefaultProperty("Text"), Designer(typeof(TreeDesigner)), DefaultEvent("SelectedNodeChanged")]
    public sealed class TreeView : Control, INamingContainer, IPostBackEventHandler
    {
        internal const string chkAttribute = "c";
        private string collapsedImage = "";
        private string cssClass = "";
        private Color dataBackColor = Color.LightGray;
        private Color dataForeColor = Color.Black;
        private string expandedImage = "";
        private bool forceInheritedChecks = true;
        private Color headerBackColor = Color.DarkGray;
        private Color headerForeColor = Color.White;
        internal ArrayList headers = new ArrayList();
        private Unit height;
        internal const string keyAttribute = "k";
        private squishyWARE.WebComponents.squishyTREE.NodeDisplayStyle nodeDisplayStyle = squishyWARE.WebComponents.squishyTREE.NodeDisplayStyle.Standard;
        private string nonFolderImage = "";
        private squishyWARE.WebComponents.squishyTREE.TreeOutputStyle outputStyle = squishyWARE.WebComponents.squishyTREE.TreeOutputStyle.Standard;
        private squishyWARE.WebComponents.squishyTREE.RenderingAgent renderingAgent;
        private bool scrolling = false;
        internal const string showChkAttribute = "s";
        private Color tableBackColor = Color.Black;
        private int tableBorder = 0;
        private int tableCellpadding = 3;
        private int tableCellspacing = 1;
        private string tableHeaderHorizAlign = "center";
        internal const string textAttribute = "t";
        private Unit width;
        private string windowsLafImageBase = "./";

        public event EventHandler SelectedNodeChanged;

        public event EventHandler TreeNodeChecked;

        public TreeView()
        {
            this.EnableViewState = true;
            this.renderingAgent = new StandardRenderingAgent(this);
        }

        private void addChildNode(XmlNode node, string textAttribute, string keyAttribute, string checkAttribute, string checkDefaultsAttribute)
        {
            this.addChildNode(node, textAttribute, keyAttribute, null, checkAttribute, checkDefaultsAttribute);
        }

        private void addChildNode(XmlNode node, string textAttribute, string keyAttribute, squishyWARE.WebComponents.squishyTREE.TreeNode tNode, string checkAttribute, string checkDefaultsAttribute)
        {
            squishyWARE.WebComponents.squishyTREE.TreeNode node2;
            string key = "";
            bool showCheckbox = false;
            bool isChecked = false;
            string text = node.Attributes[textAttribute].Value;
            if (node.Attributes[keyAttribute] != null)
            {
                key = node.Attributes[keyAttribute].Value;
            }
            if (node.Attributes[checkAttribute] != null)
            {
                showCheckbox = Convert.ToBoolean(node.Attributes[checkAttribute].Value);
            }
            if (node.Attributes[checkDefaultsAttribute] != null)
            {
                isChecked = Convert.ToBoolean(node.Attributes[checkDefaultsAttribute].Value);
            }
            if (tNode == null)
            {
                node2 = this.AddNode(text, key, showCheckbox);
            }
            else
            {
                node2 = tNode.AddNode(text, key, showCheckbox);
            }
            XmlNodeList list = node.SelectNodes("taggedValue");
            int num = 0;
            foreach (XmlNode node3 in list)
            {
                num++;
                string tagName = node3.Attributes["tagName"].Value;
                string text4 = node3.Attributes["tagValue"].Value;
                node2.AddTaggedValue(tagName, text4);
            }
            if ((node.ChildNodes.Count - num) > 0)
            {
                foreach (XmlNode node4 in node.ChildNodes)
                {
                    if (node4.Name != "taggedValue")
                    {
                        this.addChildNode(node4, textAttribute, keyAttribute, node2, checkAttribute, checkDefaultsAttribute);
                    }
                }
            }
            if ((((checkDefaultsAttribute != "") && (node.Attributes[checkDefaultsAttribute] != null)) && (node.Attributes[checkDefaultsAttribute].Value.Trim() != "")) && !this.Page.IsPostBack)
            {
                node2.Check(isChecked);
            }
        }

        private void AddChildNode(DataRow row, string textColumn, string keyColumn, string checkColumn, string checkDefaultsColumn, squishyWARE.WebComponents.squishyTREE.TreeNode node)
        {
            squishyWARE.WebComponents.squishyTREE.TreeNode node2;
            bool showCheckbox = false;
            bool isChecked = false;
            string text = row[textColumn].ToString();
            string key = row[keyColumn].ToString();
            if (checkColumn != "")
            {
                if (row[checkColumn] != DBNull.Value)
                {
                    showCheckbox = Convert.ToBoolean(row[checkColumn]);
                }
                if (row[checkDefaultsColumn] != DBNull.Value)
                {
                    isChecked = Convert.ToBoolean(row[checkDefaultsColumn]);
                }
            }
            if (node == null)
            {
                node2 = this.AddNode(text, key, showCheckbox);
            }
            else
            {
                node2 = node.AddNode(text, key, showCheckbox);
            }
            if (this.headers.Count != 0)
            {
                foreach (object[] objArray in this.headers)
                {
                    if (row.Table.Columns.Contains(objArray[0].ToString()))
                    {
                        node2.AddTaggedValue(objArray[0].ToString(), row[objArray[0].ToString()].ToString());
                    }
                }
            }
            if (row.Table.ChildRelations.Count > 0)
            {
                foreach (DataRelation relation in row.Table.ChildRelations)
                {
                    DataRow[] childRows = row.GetChildRows(relation);
                    foreach (DataRow row2 in childRows)
                    {
                        this.AddChildNode(row2, textColumn, keyColumn, checkColumn, checkDefaultsColumn, node2);
                    }
                }
            }
            if (showCheckbox && !this.Page.IsPostBack)
            {
                node2.Check(isChecked);
            }
        }

        public void AddHeader(string displayName, string formatString, Type valueType, string tagValueName, string dataHorizAlign)
        {
            object[] objArray = new object[] { displayName, formatString, valueType, tagValueName, dataHorizAlign };
            this.headers.Add(objArray);
        }

        public squishyWARE.WebComponents.squishyTREE.TreeNode AddNode(string text, string key)
        {
            return this.AddNode(text, key, false);
        }

        public squishyWARE.WebComponents.squishyTREE.TreeNode AddNode(string text, string key, bool showCheckbox)
        {
            squishyWARE.WebComponents.squishyTREE.TreeNode child = new squishyWARE.WebComponents.squishyTREE.TreeNode(text, 1, key, showCheckbox);
            this.Controls.Add(child);
            return child;
        }

        public void ClearHeaders()
        {
            this.headers.Clear();
        }

        public void CollapseAll()
        {
            foreach (Control control in this.Controls)
            {
                squishyWARE.WebComponents.squishyTREE.TreeNode node = control as squishyWARE.WebComponents.squishyTREE.TreeNode;
                if (node != null)
                {
                    node.CollapseAll();
                }
            }
        }

        public void DataSetBind(DataSet data, string dataTable, string textColumn, string keyColumn)
        {
            this.DataSetBind(data, dataTable, textColumn, keyColumn, "", "");
        }

        public void DataSetBind(DataSet data, string dataTable, string textColumn, string keyColumn, string checkColumn, string checkDefaultsColumn)
        {
            foreach (DataRow row in data.Tables[dataTable].Rows)
            {
                this.AddChildNode(row, textColumn, keyColumn, checkColumn, checkDefaultsColumn, null);
            }
        }

        public void ExpandAll()
        {
            foreach (Control control in this.Controls)
            {
                squishyWARE.WebComponents.squishyTREE.TreeNode node = control as squishyWARE.WebComponents.squishyTREE.TreeNode;
                if (node != null)
                {
                    node.ExpandAll();
                }
            }
        }

        public squishyWARE.WebComponents.squishyTREE.TreeNode FindTreeNode(string key)
        {
            foreach (squishyWARE.WebComponents.squishyTREE.TreeNode node in this.Controls)
            {
                if (node.Key == key)
                {
                    return node;
                }
            }
            foreach (squishyWARE.WebComponents.squishyTREE.TreeNode node in this.Controls)
            {
                squishyWARE.WebComponents.squishyTREE.TreeNode node2 = node.FindTreeNode(key);
                if (node2 != null)
                {
                    return node2;
                }
            }
            return null;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                string xml = savedState.ToString();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                this.XmlBind(doc, "t", "k", "s", "c");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.Page.GetPostBackEventReference(this);
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            bool flag = false;
            if (eventArgument.IndexOf("checkbox") > -1)
            {
                eventArgument = eventArgument.Replace(":checkbox", "");
                flag = true;
            }
            squishyWARE.WebComponents.squishyTREE.TreeNode n = (squishyWARE.WebComponents.squishyTREE.TreeNode) this.Page.FindControl(eventArgument);
            this.ViewState["selectednode"] = n;
            if (!flag)
            {
                n.IsExpanded = !n.IsExpanded;
                if (this.SelectedNodeChanged != null)
                {
                    n.IsSelected = true;
                    this.SelectedNodeChanged(this, new TreeViewNodeClickEventArgs(n));
                }
            }
            else
            {
                n.Check();
                if (n.Parent is squishyWARE.WebComponents.squishyTREE.TreeNode)
                {
                    ((squishyWARE.WebComponents.squishyTREE.TreeNode) n.Parent).trackCheckedChildren();
                }
                if (this.TreeNodeChecked != null)
                {
                    this.TreeNodeChecked(this, new TreeViewNodeClickEventArgs(n));
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderingAgent.RenderTreeStart(writer);
            base.Render(writer);
            this.RenderingAgent.RenderTreeEnd(writer);
        }

        protected override object SaveViewState()
        {
            MemoryStream w = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(w, Encoding.UTF8);
            writer.WriteStartElement("treeview", "");
            foreach (squishyWARE.WebComponents.squishyTREE.TreeNode node in this.Controls)
            {
                node.WriteXml(writer);
            }
            writer.WriteEndElement();
            writer.Flush();
            w.Position = 0;
            string text = new StreamReader(w).ReadToEnd();
            writer.Close();
            w.Close();
            return text;
        }

        public squishyWARE.WebComponents.squishyTREE.TreeNode SelectedNode()
        {
            return (squishyWARE.WebComponents.squishyTREE.TreeNode) this.ViewState["selectednode"];
        }

        public void SetDataModel(ITreeViewModel m)
        {
            m.FillData(this);
        }

        public void XmlBind(XmlDocument doc, string textAttribute, string keyAttribute)
        {
            this.XmlBind(doc, textAttribute, keyAttribute, "", "");
        }

        public void XmlBind(XmlDocument doc, string textAttribute, string keyAttribute, string checkAttribute, string checkDefaultsAttribute)
        {
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                this.addChildNode(node, textAttribute, keyAttribute, checkAttribute, checkDefaultsAttribute);
            }
        }

        [Description("The image that appears on collapsed nodes that contain children"), Category("Node Appearance")]
        public string CollapsedImage
        {
            get
            {
                return this.collapsedImage;
            }
            set
            {
                this.collapsedImage = value;
            }
        }

        [Description("The CssClass for the TreeNodes"), Category("Appearance")]
        public string CssClass
        {
            get
            {
                return this.cssClass;
            }
            set
            {
                this.cssClass = value;
            }
        }

        [Category("Tabular Appearance"), Description("The back color for data value text")]
        public Color DataBackColor
        {
            get
            {
                return this.dataBackColor;
            }
            set
            {
                this.dataBackColor = value;
            }
        }

        [Description("The fore color for data value text"), Category("Tabular Appearance")]
        public Color DataForeColor
        {
            get
            {
                return this.dataForeColor;
            }
            set
            {
                this.dataForeColor = value;
            }
        }

        [Description("The image that appears on expanded nodes that contain children"), Category("Node Appearance")]
        public string ExpandedImage
        {
            get
            {
                return this.expandedImage;
            }
            set
            {
                this.expandedImage = value;
            }
        }

        [Description("When set to true, checking a parent causes all its children to check, and checkng a child causes all of its parents to check"), Category("Behavior")]
        public bool ForceInheritedChecks
        {
            get
            {
                return this.forceInheritedChecks;
            }
            set
            {
                this.forceInheritedChecks = value;
            }
        }

        [Description("The back color for header text"), Category("Tabular Appearance")]
        public Color HeaderBackColor
        {
            get
            {
                return this.headerBackColor;
            }
            set
            {
                this.headerBackColor = value;
            }
        }

        [Category("Tabular Appearance"), Description("The fore color for header text")]
        public Color HeaderForeColor
        {
            get
            {
                return this.headerForeColor;
            }
            set
            {
                this.headerForeColor = value;
            }
        }

        [Category("Appearance")]
        public Unit Height
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

        [Category("Display"), Description("How nodes display themselves as links or not links")]
        public squishyWARE.WebComponents.squishyTREE.NodeDisplayStyle NodeDisplayStyle
        {
            get
            {
                return this.nodeDisplayStyle;
            }
            set
            {
                this.nodeDisplayStyle = value;
            }
        }

        [Description("The image that appears on nodes that contain no children"), Category("Node Appearance")]
        public string NonFolderImage
        {
            get
            {
                return this.nonFolderImage;
            }
            set
            {
                this.nonFolderImage = value;
            }
        }

        [Browsable(false)]
        public squishyWARE.WebComponents.squishyTREE.RenderingAgent RenderingAgent
        {
            get
            {
                return this.renderingAgent;
            }
        }

        [Category("Tree Appearance"), Description("Adds an overflow:auto style to the div tag. This only works with Standard style")]
        public bool Scrolling
        {
            get
            {
                return this.scrolling;
            }
            set
            {
                this.scrolling = value;
            }
        }

        [Category("Tabular Appearance"), Description("The back color for the table")]
        public Color TableBackColor
        {
            get
            {
                return this.tableBackColor;
            }
            set
            {
                this.tableBackColor = value;
            }
        }

        [Description("The border size of the table"), Category("Tabular Appearance")]
        public int TableBorder
        {
            get
            {
                return this.tableBorder;
            }
            set
            {
                this.tableBorder = value;
            }
        }

        [Description("The cellpadding of the table"), Category("Tabular Appearance")]
        public int TableCellpadding
        {
            get
            {
                return this.tableCellpadding;
            }
            set
            {
                this.tableCellpadding = value;
            }
        }

        [Description("The cellspacing of the table"), Category("Tabular Appearance")]
        public int TableCellspacing
        {
            get
            {
                return this.tableCellspacing;
            }
            set
            {
                this.tableCellspacing = value;
            }
        }

        [Category("Tabular Appearance"), Description("The horizontal align of the header table")]
        public string TableHeaderHorizAlign
        {
            get
            {
                return this.tableHeaderHorizAlign;
            }
            set
            {
                this.tableHeaderHorizAlign = value;
            }
        }

        [Description("The output style of the TreeView"), Category("Display")]
        public squishyWARE.WebComponents.squishyTREE.TreeOutputStyle TreeOutputStyle
        {
            get
            {
                return this.outputStyle;
            }
            set
            {
                this.outputStyle = value;
                switch (this.outputStyle)
                {
                    case squishyWARE.WebComponents.squishyTREE.TreeOutputStyle.Standard:
                        this.renderingAgent = new StandardRenderingAgent(this);
                        break;

                    case squishyWARE.WebComponents.squishyTREE.TreeOutputStyle.Tabular:
                        this.renderingAgent = new TabularRenderingAgent(this);
                        break;

                    case squishyWARE.WebComponents.squishyTREE.TreeOutputStyle.WindowsLookAndFeel:
                        this.renderingAgent = new WindowsLookAndFeelRenderingAgent(this);
                        break;

                    default:
                        throw new ArgumentException("Invalid TreeOutputStyle");
                }
            }
        }

        [Category("Appearance")]
        public Unit Width
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

        [Category("Windows Look And Feel Appearance"), Description("The root, resolved URL where the images for the TreeView will reside. You may use ~ in this to indicate your VRoot.")]
        public string WindowsLafImageBase
        {
            get
            {
                string text = base.ResolveUrl(this.windowsLafImageBase);
                if (!text.EndsWith("/"))
                {
                    text = text + "/";
                }
                return text;
            }
            set
            {
                this.windowsLafImageBase = value;
            }
        }
    }
}

