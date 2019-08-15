namespace RmsPM.WebControls.MainMenu
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:Menu runat=server></{0}:Menu>")]
    public class Menu : WebControl
    {
        private int g_iZIndex = 2;
        private RmsPM.WebControls.MainMenu.MenuInfo g_Menu = new RmsPM.WebControls.MainMenu.MenuInfo();
        private string g_strCssFile;

        public Menu()
        {
            base.EnableViewState = false;
            this.EnableViewState = false;
        }

        private string GetChildScript(ArrayList m_Items, string m_strIndex)
        {
            string text = "";
            string text2 = "";
            string text3 = "_MenuIFrame" + m_strIndex;
            text = ((((((((text + "<Script Language=\"JavaScript\">") + "MenuList[MenuList.length]=\"" + m_strIndex + "\";") + text3 + ".document.clear();") + text3 + ".document.open();") + text3 + ".document.write(\"") + "<html>") + "<LINK href=\\\"" + this.CssFile + "\\\" type=\\\"text/css\\\" rel=\\\"stylesheet\\\">") + "<body bottomMargin=\\\"0\\\" leftMargin=\\\"0\\\" topMargin=\\\"0\\\" rightMargin=\\\"0\\\">") + "<table class=\\\"MenuPanel\\\" id=\\\"Table" + m_strIndex + "\\\" cellSpacing=\\\"0\\\" cellPadding=\\\"3\\\">";
            for (int i = 0; i < m_Items.Count; i++)
            {
                RmsPM.WebControls.MainMenu.ItemInfo info = (RmsPM.WebControls.MainMenu.ItemInfo) m_Items[i];
                string text5 = text + "<tr>";
                text = ((text5 + "<td onmouseover=\\\"DisplayMenuItem('" + m_strIndex + "_" + i.ToString() + "',this);\\\" onmouseup=\\\"" + info.Event + ";ClearMenu();\\\">") + info.Text) + "</td>" + "</tr>";
            }
            return ((((((((text + "</table>") + "</body></html>" + "<Script Language=\\\"JavaScript\\\">") + "function DisplayMenuItem(sIndex,obj){" + "window.parent.frames.DisplayMenuItem(sIndex,obj);") + "}" + "function ClearMenu(){") + "window.parent.frames.ClearMenu();" + "}") + @"<\/Script>" + "\");") + text3 + ".document.close();") + "</Script>" + text2);
        }

        private string GetLayerHtml(ArrayList m_Items, string m_strIndex)
        {
            string text = "";
            string text2 = "";
            for (int i = 0; i < m_Items.Count; i++)
            {
                string text3 = (m_strIndex == "") ? i.ToString() : (m_strIndex + "_" + i.ToString());
                RmsPM.WebControls.MainMenu.ItemInfo info = (RmsPM.WebControls.MainMenu.ItemInfo) m_Items[i];
                string text5 = text2;
                text2 = ((text5 + "<div id=\"_MenuDiv" + text3 + "\" style=\"display:none;top:0px;left:0px;position:absolute;overflow: visible;z-index:" + this.g_iZIndex.ToString() + "\">") + "<iframe id=\"_MenuIFrame" + text3 + "\" scrolling=\"no\" frameborder=\"0\" src=\"about:blank\"></iframe>") + "</div>" + this.GetChildScript(info, text3);
                if (info.Count > 0)
                {
                    text = text + this.GetLayerHtml(info, text3);
                }
            }
            return (text2 + text);
        }

        protected override void Render(HtmlTextWriter output)
        {
            string text = "";
            text = (((((((((((((((((((((((((((((((((((((((((((((((((((text + "<table class=\"MainMenuPanel\" width=\"" + base.Width.ToString() + "\" border=\"0\" cellspacing=\"3\" cellpadding=\"0\"><tr>") + "<Script Language=\"JavaScript\">" + "var MenuList=new Array();") + "var MenuNowItem=null;" + "function ClearMenu(){") + "for(var i=0;i<MenuList.length;i++){" + "var _div=document.all(\"_MenuDiv\"+MenuList[i].toString());") + "_div.style.display=\"none\";" + "}") + "}" + "function DisplayMenuItem(sIndex,obj){") + "var iZIndex=" + this.g_iZIndex.ToString() + ";") + "var sIndexs=sIndex.split(\"_\");") + "var sIndexList=new Array();" + "for(var s=sIndexs.length-1;s>-1;s--){") + "var sIndexName=\"\";" + "for(var m=s;m>-1;m--){") + "sIndexName=(m==0?\"\":\"_\")+sIndexs[m]+sIndexName;" + "}") + "sIndexList[sIndexList.length]=sIndexName;" + "var x_div=document.all(\"_MenuDiv\"+sIndexName.toString());") + "var t_obj=document.all(\"MainMenuItem\"+sIndex.split(\"_\")[0].toString());" + "var top=GetOffsetTop(t_obj)+t_obj.offsetHeight+1;") + "var left=GetOffsetLeft(t_obj);" + "var sIndexss=sIndexName.split(\"_\");") + "for(var n=sIndexss.length-1;n>0;n--){" + "var sIndexssName=\"\";") + "for(var o=n-1;o>-1;o--){" + "sIndexssName=(o==0?\"\":\"_\")+sIndexss[o]+sIndexssName;") + "}" + "var objs=eval(\"_MenuIFrame\"+sIndexssName.toString());") + "if(objs.document.body.childNodes[0].childNodes[0].childNodes[0]){" + "objs=objs.document.body.childNodes[0].childNodes[0].childNodes[parseInt(sIndexss[sIndexss.length-1])].childNodes[0];") + "top+=GetOffsetTop(objs)+2;" + "left+=GetOffsetLeft(objs)+objs.offsetWidth-2;") + "}" + "}") + "x_div.style.zIndex=iZIndex+sIndexName.split(\"_\").length;" + "x_div.style.pixelTop=top;") + "x_div.style.pixelLeft=(left<0?0:left);" + "}") + "for(var i=0;i<MenuList.length;i++){" + "var _div=document.all(\"_MenuDiv\"+MenuList[i].toString());") + "var _iframe=eval(\"_MenuIFrame\"+MenuList[i].toString());" + "var _iframes=document.all(\"_MenuIFrame\"+MenuList[i].toString());") + "if(_div&&_iframe&&_iframes){" + "var bShow=false;") + "for(var j=0;j<sIndexList.length;j++){" + "if(MenuList[i].toString()==sIndexList[j].toString()){") + "bShow=true;" + "}") + "}" + "if(bShow){") + "_div.style.display=\"\";" + "_iframes.style.pixelWidth=_iframe.document.body.childNodes[0].offsetWidth;") + "_iframes.style.pixelHeight=_iframe.document.body.childNodes[0].offsetHeight;" + "}else{") + "_div.style.display=\"none\";" + "}") + "}" + "}") + "}" + "function GetOffsetTop(obj){") + "var top=obj.offsetTop;" + "if(obj.parentNode.tagName.toLowerCase()!=\"body\"){") + "top+=GetParentOffsetTop(obj);" + "}") + "return top;" + "}") + "function GetParentOffsetTop(obj){" + "var top=0;") + "if(obj.parentNode.tagName.toLowerCase()!=\"body\"){" + "if(obj.parentNode.tagName.toLowerCase()==\"table\"&&obj.parentNode.parentNode.tagName.toLowerCase()!=\"body\"){") + "top+=obj.parentNode.offsetTop;" + "top+=obj.parentNode.parentNode.offsetTop;") + "}" + "top+=GetParentOffsetTop(obj.parentNode);") + "}" + "return top;") + "}" + "function GetOffsetLeft(obj){") + "var top=obj.offsetLeft;" + "if(obj.parentNode.tagName.toLowerCase()!=\"body\"){") + "top+=GetParentOffsetLeft(obj);" + "}") + "return top;" + "}") + "function GetParentOffsetLeft(obj){" + "var top=0;") + "if(obj.parentNode.tagName.toLowerCase()!=\"body\"){" + "if(obj.parentNode.tagName.toLowerCase()==\"table\"&&obj.parentNode.parentNode.tagName.toLowerCase()!=\"body\"){") + "top+=obj.parentNode.parentNode.offsetLeft;" + "}") + "top+=GetParentOffsetLeft(obj.parentNode);" + "}") + "return top;" + "}") + "</Script>" + "<Script Language=\"JavaScript\">") + "document.body.onclick=ClearMenu;" + "</Script>";
            for (int i = 0; i < this.g_Menu.Count; i++)
            {
                text = (((text + "<td id=\"MainMenuItem" + i.ToString() + "\" align=\"center\"") + " onmousedown=\"DisplayMenuItem('" + i.ToString() + "',this);\"") + " onmouseout=\"document.body.onclick=ClearMenu;\"" + " onmouseover=\"document.body.onclick=null;\">") + this.g_Menu[i].Text + "</td>";
            }
            text = text + "</tr></table>";
            base.ClearChildViewState();
            output.Write(text);
            base.ClearChildViewState();
        }

        public string CssFile
        {
            get
            {
                return this.g_strCssFile;
            }
            set
            {
                this.g_strCssFile = value;
            }
        }

        public string LayerHtml
        {
            get
            {
                return this.GetLayerHtml(this.g_Menu, "");
            }
        }

        public RmsPM.WebControls.MainMenu.MenuInfo MenuInfo
        {
            set
            {
                this.g_Menu = value;
            }
        }

        public int ZIndex
        {
            get
            {
                return this.g_iZIndex;
            }
            set
            {
                this.g_iZIndex = value;
            }
        }
    }
}

