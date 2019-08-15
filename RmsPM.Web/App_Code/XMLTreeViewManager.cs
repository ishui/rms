using System;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Rms.ORMap;


namespace RmsPM.Web
{
    /// <summary>
    /// 使用xml定义文件建立树节点
    /// </summary>
    public class XMLTreeViewManager
    {

        public string m_XMLFileName = "";

        //构造函数
        public XMLTreeViewManager(string fileName)
        {
            m_XMLFileName = fileName;
        }

        /// <summary>
        /// 分析树，结合用户权限判断是否显示节点
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="corpCode"></param>
        /// <param name="projectCode">工程代码</param>
        public string GetLeftBarString(RmsPM.Web.User user, string projectCode, string corpCode)
        {

            StringBuilder sb = new StringBuilder();

            try
            {

                if (m_XMLFileName.Length == 0)
                    return "";

                XmlDocument doc = new XmlDocument();
                doc.Load(m_XMLFileName);


                XmlNodeList moduleNodeList = doc.DocumentElement.SelectNodes("Module");
                int iCount = moduleNodeList.Count;
                for (int i = 0; i < iCount; i++)
                {
                    XmlNode moduleNode = moduleNodeList[i];
                    Module module = BuildModule(moduleNode, projectCode, corpCode);

                    //  Right == "" 不要求权限； 或者用户具有该权限
                    if ((module.right == "") || user.HasRight(module.right))
                    {
                        XmlNodeList submoduleNodeList = moduleNode.SelectNodes("SubModule");
                        ArrayList subModules = new ArrayList();
                        int iSubCount = submoduleNodeList.Count;
                        for (int j = 0; j < iSubCount; j++)
                        {
                            XmlNode submoduleNode = submoduleNodeList[j];
                            Module subModule = BuildModule(submoduleNode, projectCode, corpCode);
                            if ((subModule.right == "") || user.HasRight(subModule.right))
                                subModules.Add(subModule);
                        }
                        if (module.url.Trim() != string.Empty)//当前节点直接进入URL链接
                        {
                            sb.Append(@"<table width=100% border=0 cellpadding=0 cellspacing=0>" + "\n");
                            sb.Append(@"<tr><td id='tdGroup" + i.ToString() + @"' class=menu  onclick=""gotoUrl('" + module.url + @"');""  onmouseover=""this.className='menuon';"" onmouseout=""this.className='menu'""  name=" + module.name + @" child=" + module.child + @"><img src=""images/menu_li.gif"" width=14 height=10> " + module.text + @"</td></tr>" + "\n");
                            sb.Append(@"</table>" + "\n");
                        }
                        // 如果一个子节点都没有，父节点就一起不要了
                        else if (subModules.Count > 0)
                        {
                            sb.Append(@"<table width=100% border=0 cellpadding=0 cellspacing=0>" + "\n");
                            sb.Append(@"<tr><td id='tdGroup" + i.ToString() + @"' class=menu  onclick=""outliner()""  onmouseover=""this.className='menuon';"" onmouseout=""this.className='menu'""  name=" + module.name + @" child=" + module.child + @"><img src=""images/menu_li.gif"" width=14 height=10> " + module.text + @"</td></tr>" + "\n");
                            sb.Append(@"<tr><td><DIV class=collapsed id=" + module.name + @"><table width=100% border=0 cellpadding=0 cellspacing=0  bgcolor=#FF0000>" + "\n");

                            int iACount = subModules.Count;
                            for (int j = 0; j < iACount; j++)
                            {
                                Module tempM = (Module)subModules[j];

                                // 最后一个子模块的样式不一样
                                string subClass = "menu-i";
                                if (j == iACount - 1)
                                    subClass = "menu-ibn";

                                switch (tempM.name)
                                {
                                    case "@MaterialCostFirstGroup":  //列出材料库一级分类
                                        EntityData entityMaterialCostGroup = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByClassCode("1411");
                                        DataView dvMaterialCostGroup = new DataView(entityMaterialCostGroup.CurrentTable, "ParentCode=''", "SortID", DataViewRowState.CurrentRows);
                                        foreach (DataRowView drv in dvMaterialCostGroup)
                                        {
                                            string GroupCode = BLL.ConvertRule.ToString(drv.Row["GroupCode"]);
                                            string GroupName = BLL.ConvertRule.ToString(drv.Row["GroupName"]);

                                            GroupName = string.Format(tempM.text, GroupName);

                                            //是否有权限（暂缓）

                                            string url = tempM.url;
                                            if (url.IndexOf("?") >= 0)
                                                url += "&";
                                            else
                                                url += "?";
                                            url += "RootGroupCode=" + GroupCode;
                                            sb.Append(@"<tr onclick=""gotoUrl('" + url + @"');""><td onmouseover=""this.className='menu-ibnon';"" onmouseout=""this.className='" + subClass + @"'"" class=" + subClass + @"><img src=""images/menu_li_i.gif"" width=7 height=7>  " + GroupName + @"</td></tr>" + "\n");
                                        }
                                        entityMaterialCostGroup.Dispose();

                                        break;

                                    case "@WorkDeep1":  //列出项目下的第1级工作项
                                        DataTable tbTask = BLL.WBSRule.GetHasRightChildTask(1, projectCode, user.UserCode);

                                        foreach (DataRow dr in tbTask.Rows)
                                        {
                                            string WBSCode = BLL.ConvertRule.ToString(dr["WBSCode"]);
                                            string TaskName = BLL.ConvertRule.ToString(dr["TaskName"]);

                                            string url = tempM.url;
                                            if (url.IndexOf("?") >= 0)
                                                url += "&";
                                            else
                                                url += "?";
                                            url += "ParentCode=" + WBSCode;
                                            sb.Append(@"<tr onclick=""gotoUrl('" + url + @"');""><td onmouseover=""this.className='menu-ibnon';"" onmouseout=""this.className='" + subClass + @"'"" class=" + subClass + @"><img src=""images/menu_li_i.gif"" width=7 height=7>  " + TaskName + @"</td></tr>" + "\n");
                                        }

                                        break;

                                    case "@ProjectList":  //列出用户有权访问的所有项目
                                        EntityData entityProject = user.m_EntityDataAccessProject;

                                        foreach (DataRow dr in entityProject.CurrentTable.Rows)
                                        {
                                            string ProjectCode = BLL.ConvertRule.ToString(dr["ProjectCode"]);
                                            string ProjectName = BLL.ConvertRule.ToString(dr["ProjectShortName"]);
                                            if (ProjectName == "") ProjectName = BLL.ConvertRule.ToString(dr["ProjectShortName"]);

                                            string url = tempM.url;
                                            if (url.IndexOf("?") >= 0)
                                                url += "&";
                                            else
                                                url += "?";
                                            url += "ProjectCode=" + ProjectCode;

                                            sb.Append(@"<tr onclick=""gotoUrl('" + url + @"');""><td onmouseover=""this.className='menu-ibnon';"" onmouseout=""this.className='" + subClass + @"'"" class=" + subClass + @"><img src=""images/menu_li_i.gif"" width=7 height=7>  " + ProjectName + tempM.text + @"</td></tr>" + "\n");
                                        }

                                        break;

                                    default:
                                        sb.Append(@"<tr onclick=""gotoUrl('" + tempM.url + @"');""><td onmouseover=""this.className='menu-ibnon';"" onmouseout=""this.className='" + subClass + @"'"" class=" + subClass + @"><img src=""images/menu_li_i.gif"" width=7 height=7>  " + tempM.text + @"</td></tr>" + "\n");
                                        break;
                                }

                            }


                            sb.Append(@"</table></DIV></td></tr>" + "\n");
                        }


                    }

                }
                sb.Append(@"<tr><td><img src=""images/menu_btm.gif"" width=146 height=20></td></tr></table>");
                return sb.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Module BuildModule(XmlNode node, string projectCode, string corpCode)
        {
            Module module = new Module();
            module.name = node.SelectSingleNode("Name").InnerText;
            module.text = node.SelectSingleNode("Text").InnerText;
            module.child = node.SelectSingleNode("Child").InnerText;
            module.right = node.SelectSingleNode("Right").InnerText;

            string tempUrl = node.SelectSingleNode("URL").InnerText;
            if ((projectCode != "") && (tempUrl != "") && (tempUrl.ToUpper().IndexOf("PROJECTCODE") == -1))
            {
                // 链接中如果有了 ？ 号，就要用 & 号链接参数
                string linkChar = (tempUrl.IndexOf("?") == -1) ? "?" : "&";
                tempUrl += linkChar + "ProjectCode=" + projectCode;
            }
            if ((corpCode != "") && (tempUrl != "") && (tempUrl.ToUpper().IndexOf("CORPCODE") == -1))
            {
                // 链接中如果有了 ？ 号，就要用 & 号链接参数
                string linkChar = (tempUrl.IndexOf("?") == -1) ? "?" : "&";
                tempUrl += linkChar + "CorpCode=" + corpCode;
            }
            module.url = tempUrl;
            return module;
        }


    }

    public class Module
    {
        public string text;
        public string name;
        public string url;
        public string right;
        public string child;

        public Module()
        { }
    }


    public class SystemMenuManager
    {
        private string _ConfigFileName ;
        private string GetConfigText(XmlNode Node, string ConfigName)
        {
            XmlNode Config = Node.SelectSingleNode(ConfigName);
            return (Config != null) ? Config.InnerText : "";
        }
        /// <summary>
        /// 系统菜单管理类
        /// </summary>
        /// <param name="ConfigFileName"></param>
        public SystemMenuManager(string ConfigFileName)
        {
            _ConfigFileName = ConfigFileName;
        }
        /// <summary>
        /// 输出主菜单HTML文本
        /// </summary>
        /// <returns></returns>
        public string SystemMenuHtml(RmsPM.Web.User user)
        {
            HtmlMenu systemMenu = new HtmlMenu();

            XmlDocument doc = new XmlDocument();
            doc.Load(_ConfigFileName);

            XmlNode realmis = doc.DocumentElement.SelectSingleNode("RealMis");
            List<MenuItem> itemlist = new List<MenuItem>();

            if (realmis != null)
            {
                string RealmisName = realmis.Attributes["Name"].Value;
                XmlNodeList SubSystems = realmis.SelectNodes("SubSystem");
                foreach (XmlNode node in SubSystems)
                {
                    string Right = GetConfigText(node, "Right");
                    string RightLevel = GetConfigText(node, "RightLevel");

                    if ( CheckRight(user, Right,RightLevel))
                    {
                        makeItem(user, systemMenu, itemlist, node);
                    }
                }
            }
            if (systemMenu.DefaultIndex > 0)
                systemMenu.DefaultIndex--;
            systemMenu.MenuItems = itemlist;
            return systemMenu.OutPutMainMenuHtml();
        }

        private static bool CheckRight(RmsPM.Web.User user, string Right,string RightLevel)
        {
            bool hasit = true;
            if (Right != string.Empty && (!user.HasRight(Right))) hasit = false;
            if(RightLevel!=string.Empty)
            {
                if (RightLevel == "IsGroupUser")
                {
                    if (!user.m_IsGroupUser)
                    {
                       hasit=false;
                    }
                }             
            }
            return hasit ;
        }

        private void makeItem(RmsPM.Web.User user, HtmlMenu systemMenu, List<MenuItem> itemlist, XmlNode node)
        {
            MenuItem item = new MenuItem();
            item.Name = GetConfigText(node, "Name");
            item.Text = GetConfigText(node, "Text");
            item.ToolTip = GetConfigText(node, "ToolTip");
            item.NavigateURL = GetConfigText(node, "NavigateURL").Replace("@UserName", user.UserID);
            item.TargetFrame = GetConfigText(node, "TargetFrame");
            item.ClientCommand = GetConfigText(node, "Command").Replace("@UserName", user.UserID);
            item.IsDefault = GetConfigText(node, "IsDefault").ToLower() == "true" ? true : false;
            itemlist.Add(item);
            if (true == item.IsDefault && 0 == systemMenu.DefaultIndex)
                systemMenu.DefaultIndex = itemlist.Count;
        }

        /// <summary>
        /// 菜单项数据类型
        /// </summary>
        internal class MenuItem
        {
            /// <summary>
            /// 菜单项名称，暂无用
            /// </summary>
            public string Name;
            /// <summary>
            /// 鼠标悬浮说明
            /// </summary>
            public string ToolTip;
            /// <summary>
            /// 菜单项文字
            /// </summary>
            public string Text;
            /// <summary>
            /// 点击菜单项时，客户端执行的脚本
            /// </summary>
            public string ClientCommand;
            /// <summary>
            /// 点击菜单项，打开URL
            /// </summary>
            public string NavigateURL;
            /// <summary>
            /// 打开URL的目标窗口，和NavigateURL配套
            /// </summary>
            public string TargetFrame;
            /// <summary>
            /// 是缺省菜单
            /// </summary>
            public bool IsDefault=false;

            public MenuItem()
            {
            }
        }
        /// <summary>
        /// 菜单类，具有菜单项集合，可输出菜单HTML
        /// </summary>
        internal class HtmlMenu
        {
            public int DefaultIndex = 0;
            public HtmlMenu()
            {
            }
            /// <summary>
            /// 菜单项
            /// </summary>
            public List<MenuItem> MenuItems;
            /// <summary>
            /// 输出系统主菜单HTML文本
            /// </summary>
            /// <returns></returns>
            public string OutPutMainMenuHtml()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"<table border=""0"" cellspacing=""0"" cellpadding=""0""  id=""SystemMenu"">");
                sb.Append(@"<tr>");
                int iCount=0;
                foreach (MenuItem item in MenuItems)
                {
                    sb.Append(@" <td><a id=""menuitem");
                    sb.Append(iCount.ToString());
                    sb.Append(@""" href=""" );
                    sb.Append(item.NavigateURL);
                    sb.Append(@""" target=""");
                    sb.Append(item.TargetFrame);
                    sb.Append(@"""");
                    sb.Append(@" class=""heardpart"" ");
                    sb.Append(@" onclick=""javascript:");
                    sb.Append(item.ClientCommand);
                    sb.Append(@"""");
                    sb.Append(@" title=""");
                    sb.Append(item.ToolTip);
                    sb.Append(@""">");
                    sb.Append(item.Text);
                    sb.Append(@"</a></td>");
                    iCount++;
                }
                sb.Append(@"</tr></table>");
                sb.Append(@"<script type=""text/javascript"">document.all('menuitem");
                sb.Append(DefaultIndex.ToString());
                sb.Append(@"').click();</script>");
                return sb.ToString();
            }
        }
    }
}
