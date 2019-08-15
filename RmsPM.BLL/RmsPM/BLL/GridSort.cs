namespace RmsPM.BLL
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class GridSort
    {
        private static void AddSecondSort(string SortField, string SortDirect, StateBag ViewState)
        {
            string s = "";
            string text2 = SortField + " " + SortDirect;
            if (ViewState["SecondSort"] != null)
            {
                s = ViewState["SecondSort"].ToString().Trim();
            }
            if (s != "")
            {
                s = text2.Trim() + "," + s;
                if ((GetSubStringCount(s, ",") + 1) > 3)
                {
                    s = DeleteLast(s, ",");
                }
            }
            else
            {
                s = text2.Trim();
            }
            ViewState["SecondSort"] = s;
        }

        public static void Clear(StateBag ViewState)
        {
            ViewState["SortIndex"] = "";
            ViewState["SortField"] = "";
            ViewState["SortDirect"] = "";
            ViewState["SecondSort"] = "";
        }

        private static string DeleteDuplicateField(string SortStr, string SortField)
        {
            string text = SortStr.Trim();
            if (text != "")
            {
                text = text + ",";
                string oldValue = SortField + " ASC,";
                text = text.Replace(oldValue, "");
                oldValue = SortField + " DESC,";
                text = text.Replace(oldValue, "");
                if ((text != "") && (text.Substring(text.Length - 1) == ","))
                {
                    text = text.Substring(0, text.Length - 1);
                }
            }
            return text;
        }

        private static string DeleteLast(string str, string val)
        {
            string text = str;
            int length = text.LastIndexOf(val);
            if (length > -1)
            {
                text = text.Substring(0, length);
            }
            return text;
        }

        private static int GetColumnIndex(DataGrid dgMain, string FieldName)
        {
            for (int i = 0; i < dgMain.Columns.Count; i++)
            {
                if ((dgMain.Columns[i].SortExpression == FieldName) && dgMain.Columns[i].Visible)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string GetSortSQL(StateBag ViewState)
        {
            string text = "";
            if ((ViewState["SortField"] != null) && (ViewState["SortField"].ToString() != ""))
            {
                text = ViewState["SortField"].ToString() + " " + ViewState["SortDirect"].ToString();
                if ((ViewState["SecondSort"] != null) && (ViewState["SecondSort"].ToString() != ""))
                {
                    text = text + "," + ViewState["SecondSort"].ToString();
                }
            }
            return text;
        }

        public static string GetSortSQL(StateBag ViewState, string DefaultSort)
        {
            string sortSQL = GetSortSQL(ViewState);
            if (sortSQL == "")
            {
                sortSQL = DefaultSort;
            }
            return sortSQL;
        }

        private static int GetSubStringCount(string s, string val)
        {
            int startIndex = 0;
            int num3 = 0;
            for (int i = s.IndexOf(val, startIndex); i > -1; i = s.IndexOf(val, startIndex))
            {
                num3++;
                startIndex = i + 1;
            }
            return num3;
        }

        public static void ItemCreate(DataGrid dgMain, StateBag ViewState, object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Header) && (ViewState["SortIndex"] != null))
            {
                for (int i = 0; i < e.Item.Cells.Count; i++)
                {
                    if (i.ToString() == ViewState["SortIndex"].ToString())
                    {
                        Image child = new Image();
                        string text = "../images/";
                        if ((ViewState["ImagePath"] != null) && (ViewState["ImagePath"].ToString().Length > 0))
                        {
                            text = ViewState["ImagePath"].ToString();
                        }
                        if (ViewState["SortDirect"].ToString() == "DESC")
                        {
                            child.ImageUrl = text + "SortDesc.gif";
                        }
                        else
                        {
                            child.ImageUrl = text + "SortAsc.gif";
                        }
                        child.Style["Position"] = "relative";
                        child.Style["Left"] = "10px";
                        e.Item.Cells[i].Controls.Add(child);
                        break;
                    }
                }
            }
        }

        public static void SortCommand(DataGrid dgMain, StateBag ViewState, object source, DataGridSortCommandEventArgs e)
        {
            string sortField = "";
            string sortDirect = "";
            if (ViewState["SortField"] != null)
            {
                sortField = ViewState["SortField"].ToString();
                sortDirect = ViewState["SortDirect"].ToString();
            }
            if (sortField != e.SortExpression)
            {
                if (ViewState["SecondSort"] != null)
                {
                    ViewState["SecondSort"] = DeleteDuplicateField(ViewState["SecondSort"].ToString(), e.SortExpression);
                }
                AddSecondSort(sortField, sortDirect, ViewState);
                ViewState["SortField"] = e.SortExpression;
                sortDirect = "ASC";
            }
            else if (ViewState["SortDirect"].ToString() == "DESC")
            {
                sortDirect = "ASC";
            }
            else
            {
                sortDirect = "DESC";
            }
            ViewState["SortDirect"] = sortDirect;
            int columnIndex = GetColumnIndex(dgMain, e.SortExpression);
            ViewState["SortIndex"] = columnIndex;
        }
    }
}

