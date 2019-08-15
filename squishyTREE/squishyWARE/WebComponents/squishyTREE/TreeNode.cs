namespace squishyWARE.WebComponents.squishyTREE
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Xml;

    [DesignTimeVisible(false)]
    public sealed class TreeNode : Control, INamingContainer
    {
        private int indent;
        private string key;
        private TreeNode parentNode;
        private TreeView parentTree;
        private bool showCheckBox;
        private NameValueCollection taggedValues;
        private string text;

        internal TreeNode(string text, int indent, string key) : this(text, indent, key, false)
        {
        }

        internal TreeNode(string text, int indent, string key, bool showCheckbox)
        {
            this.taggedValues = new NameValueCollection();
            this.text = text;
            this.indent = indent;
            this.key = key;
            this.showCheckBox = showCheckbox;
        }

        public TreeNode AddNode(string text, string key)
        {
            return this.AddNode(text, key, false);
        }

        public TreeNode AddNode(string text, string key, bool showCheckbox)
        {
            TreeNode child = new TreeNode(text, this.indent + 1, key, showCheckbox);
            child.parentNode = this;
            this.Controls.Add(child);
            return child;
        }

        public void AddTaggedValue(string tagName, string value)
        {
            this.taggedValues.Add(tagName, value);
        }

        public void Check()
        {
            this.Check(!this.IsChecked);
        }

        internal void Check(bool isChecked)
        {
            this.IsChecked = isChecked;
            if (this.findTreeView().ForceInheritedChecks)
            {
                foreach (TreeNode node in this.Controls)
                {
                    node.Check(isChecked);
                }
            }
        }

        public void CollapseAll()
        {
            this.IsExpanded = false;
            foreach (TreeNode node in this.Controls)
            {
                node.CollapseAll();
            }
        }

        public void ExpandAll()
        {
            this.IsExpanded = true;
            foreach (TreeNode node in this.Controls)
            {
                node.ExpandAll();
            }
        }

        public TreeNode FindTreeNode(string key)
        {
            foreach (TreeNode node in this.Controls)
            {
                if (node.Key == key)
                {
                    return node;
                }
            }
            foreach (TreeNode node in this.Controls)
            {
                TreeNode node2 = node.FindTreeNode(key);
                if (node2 != null)
                {
                    return node2;
                }
            }
            return null;
        }

        [Obsolete("", false)]
        private TreeView findTreeView()
        {
            if (this.parentTree == null)
            {
                TreeNode parent = this;
                while (parent.Parent.GetType() != typeof(TreeView))
                {
                    parent = (TreeNode) parent.Parent;
                }
                this.parentTree = (TreeView) parent.Parent;
            }
            return this.parentTree;
        }

        private TreeView FindTreeView()
        {
            if (this.parentTree == null)
            {
                TreeNode parent = this;
                while (parent.Parent.GetType() != typeof(TreeView))
                {
                    parent = (TreeNode) parent.Parent;
                }
                this.parentTree = (TreeView) parent.Parent;
            }
            return this.parentTree;
        }

        private bool hasCheckedChildren()
        {
            bool flag = false;
            foreach (TreeNode node in this.Controls)
            {
                if (node.IsChecked)
                {
                    return true;
                }
                flag = node.hasCheckedChildren();
                if (flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public TreeNode NextSibling()
        {
            int index = this.Parent.Controls.IndexOf(this);
            try
            {
                if (!((this.Parent.Controls[index + 1] != null) && (this.Parent.Controls[index + 1] is TreeNode)))
                {
                    return null;
                }
                return (TreeNode) this.Parent.Controls[index + 1];
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            this.FindTreeView().RenderingAgent.RenderNodeStart(this, output);
            this.FindTreeView().RenderingAgent.RenderImageLink(this, output);
            this.FindTreeView().RenderingAgent.RenderCheckbox(this, output);
            this.FindTreeView().RenderingAgent.RenderNodeText(this, output);
            this.FindTreeView().RenderingAgent.RenderNodeEnd(this, output);
            if ((this.IsFolder && this.IsExpanded) && (this.Controls.Count > 0))
            {
                this.RenderChildren(output);
            }
        }

        internal void trackCheckedChildren()
        {
            if (this.findTreeView().ForceInheritedChecks)
            {
                if (this.hasCheckedChildren())
                {
                    this.IsChecked = true;
                }
                else
                {
                    this.IsChecked = false;
                }
                if (this.Parent is TreeNode)
                {
                    ((TreeNode) this.Parent).trackCheckedChildren();
                }
            }
        }

        internal void WriteXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("treenode");
            writer.WriteAttributeString("t", "", this.Text);
            writer.WriteAttributeString("k", "", this.Key);
            if (this.showCheckBox)
            {
                writer.WriteAttributeString("s", "", "True");
                writer.WriteAttributeString("c", "", this.IsChecked.ToString());
            }
            for (int i = 0; i < this.taggedValues.Count; i++)
            {
                writer.WriteStartElement("taggedValue", "");
                writer.WriteAttributeString("", "tagName", "", this.taggedValues.Keys[i]);
                writer.WriteAttributeString("", "tagValue", "", this.taggedValues.GetValues(this.taggedValues.Keys[i])[0]);
                writer.WriteEndElement();
            }
            foreach (TreeNode node in this.Controls)
            {
                node.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        internal int Indent
        {
            get
            {
                return this.indent;
            }
        }

        public bool IsChecked
        {
            get
            {
                if (this.ViewState["IsChecked"] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(this.ViewState["IsChecked"]);
            }
            set
            {
                this.ViewState["IsChecked"] = value;
            }
        }

        public bool IsExpanded
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["IsExpanded"]);
            }
            set
            {
                this.ViewState["IsExpanded"] = value;
            }
        }

        internal bool IsFolder
        {
            get
            {
                return this.HasControls();
            }
        }

        public bool IsSelected
        {
            get
            {
                if (this.ViewState["IsSelected"] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(this.ViewState["IsSelected"]);
            }
            set
            {
                this.ViewState["IsSelected"] = value;
            }
        }

        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }

        public TreeNode ParentNode
        {
            get
            {
                return this.parentNode;
            }
        }

        internal bool ShowCheckBox
        {
            get
            {
                return this.showCheckBox;
            }
        }

        public NameValueCollection TaggedValues
        {
            get
            {
                return this.taggedValues;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

