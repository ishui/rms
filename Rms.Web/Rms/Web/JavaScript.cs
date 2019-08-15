namespace Rms.Web
{
    using System;

    public class JavaScript
    {
        public static string ScriptEnd = "</Script>";
        public static string ScriptStart = "<Script Language=\"JavaScript\">";

        public static string Alert(bool m_bIsScript, string m_strString)
        {
            string text = CheckString.ClearJavaScriptString(m_strString).Replace("\n", " ");
            if (m_bIsScript)
            {
                return WriteJS("alert(\"" + text + "\");");
            }
            return ("alert(\"" + text + "\");");
        }

        public static string DialogMax(bool m_bIsScript, string m_strUrl, string m_strWinName)
        {
            string text = "var m_iWinOpenWidth=window.screen.availWidth-10;m_iWinOpenHeight=window.screen.availHeight-30;window.open('" + m_strUrl + "','" + m_strWinName + "','width='+m_iWinOpenWidth+',height='+m_iWinOpenHeight+',fullscreen=3,top=0,left=0,scrollbars=no');";
            if (m_bIsScript)
            {
                return WriteJS(text);
            }
            return text;
        }

        public static string HistoryTo(bool m_bIsScript, int m_iHistoryNumber)
        {
            if (m_bIsScript)
            {
                return WriteJS("history.go(" + m_iHistoryNumber.ToString() + ");");
            }
            return ("history.go(" + m_iHistoryNumber.ToString() + ");");
        }

        public static string IncludeJS(string m_strString)
        {
            return ("<Script Language='JavaScript' src='" + m_strString + "'></Script>");
        }

        public static string ItemFocus(bool m_bIsScript, string m_strString)
        {
            if (m_bIsScript)
            {
                return WriteJS(m_strString + ".focus();");
            }
            return (m_strString + ".focus();");
        }

        public static string OpenerReload(bool m_bIsScript)
        {
            string text = "if (window.opener) window.opener.navigate(window.opener.location.href  );";
            if (m_bIsScript)
            {
                return WriteJS(text);
            }
            return text;
        }

        public static string PageTo(bool m_bIsScript, string m_strString)
        {
            if (m_bIsScript)
            {
                return WriteJS("window.location.href='" + m_strString + "';");
            }
            return ("window.location.href='" + m_strString + "';");
        }

        public static string Reload(bool m_bIsScript)
        {
            if (m_bIsScript)
            {
                return WriteJS("window.navigate(window.location.href  );");
            }
            return "window.navigate(window.location.href);";
        }

        public static string WinClose(bool m_bIsScript)
        {
            if (m_bIsScript)
            {
                return WriteJS("window.opener=null;window.close();");
            }
            return "window.opener=null;window.close();";
        }

        public static string WinOpen(bool m_bIsScript, string m_strUrl, string m_strWinName, string m_strWidth, string m_strHeight, string m_strLeft, string m_strTop, bool m_bMenuBar, bool m_bToolBar, bool m_bStatus, bool m_bScrollBar, bool m_bTitleBar, bool m_bResizable, bool m_bLocation, bool m_bFullScreen)
        {
            string text = "";
            if (m_strWidth != "")
            {
                text = text + "width=" + m_strWidth + ",";
            }
            if (m_strHeight != "")
            {
                text = text + "height=" + m_strHeight + ",";
            }
            if (m_strTop != "")
            {
                text = text + "top=" + m_strTop + ",";
            }
            if (m_strLeft != "")
            {
                text = text + "left=" + m_strLeft + ",";
            }
            if (m_bMenuBar)
            {
                text = text + "menubar=yes,";
            }
            else
            {
                text = text + "menubar=no,";
            }
            if (m_bToolBar)
            {
                text = text + "toolbar=yes,";
            }
            else
            {
                text = text + "toolbar=no,";
            }
            if (m_bScrollBar)
            {
                text = text + "scrollbars=yes,";
            }
            else
            {
                text = text + "scrollbars=no,";
            }
            if (m_bStatus)
            {
                text = text + "status=yes,";
            }
            else
            {
                text = text + "status=no,";
            }
            if (m_bTitleBar)
            {
                text = text + "titlebar=yes,";
            }
            else
            {
                text = text + "titlebar=no,";
            }
            if (m_bResizable)
            {
                text = text + "resizable=yes,";
            }
            else
            {
                text = text + "resizable=no,";
            }
            if (m_bLocation)
            {
                text = text + "location=yes,";
            }
            else
            {
                text = text + "location=no,";
            }
            if (m_bFullScreen)
            {
                text = text + "fullscreen=yes,";
            }
            else
            {
                text = text + "fullscreen=no,";
            }
            if (m_bIsScript)
            {
                return WriteJS("window.open('" + m_strUrl + "','" + m_strWinName + "','" + text + "');");
            }
            return ("window.open('" + m_strUrl + "','" + m_strWinName + "','" + text + "');");
        }

        public static string WinOpenMax(bool m_bIsScript, string m_strUrl, string m_strWinName)
        {
            string text = "var m_iWinOpenWidth=window.screen.availWidth-10;m_iWinOpenHeight=window.screen.availHeight-30;window.open('" + m_strUrl + "','" + m_strWinName + "','width='+m_iWinOpenWidth+',height='+m_iWinOpenHeight+',top=0,left=0,menubar=no,toolbar=no,scrollbars=no,status=no,titlebar=no,resizable=no,location=no');";
            if (m_bIsScript)
            {
                return WriteJS(text);
            }
            return text;
        }

        public static string WriteJS(string m_strString)
        {
            return ("<Script Language='JavaScript'>" + m_strString + "</Script>");
        }
    }
}

