<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectmaterialin.aspx.cs" Inherits="RmsPM.Web.SelectBox.selectmaterialin" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>材料领用</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet">
	<LINK href="../Images/infra.css" type="text/css" rel="stylesheet">
	<LINK href="../Images/TreeView.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Rms.js"></script>
    <script language="javascript">
	
	//高级查询
function ShowAdvSearch()
{
	var display = form1.txtAdvSearch.value;
	if ( display == "none" )
	{
		display = "block";
	}
	else
	{
		display = "none";
	}
	
	form1.txtAdvSearch.value = display;
	
	SetAdvSearch();
}

function SetAdvSearch()
{
	document.all("divAdvSearch").style.display = form1.txtAdvSearch.value;

	if ( form1.txtAdvSearch.value == "none" )
	{
//		Form1.imgAdvSearch.src = "../images/ArrowDown.gif";
		form1.imgAdvSearch.title = "高级查询";
	}
	else
	{
//		Form1.imgAdvSearch.src = "../images/ArrowUp.gif";
		form1.imgAdvSearch.title = "隐藏高级查询";
	}
}
</script>
</head>
<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            </tr>
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">选择领用材料</td>
				</tr>
            <tr height="100%">
                <td class="table" vAlign="top">
                    <table height="100%" width="100%">
                        <tr>
                            <td>
                                <table width="100%" class="search-area" cellspacing="0" cellpadding="0" border="0" onkeydown="SearchKeyDown();">
                                    <tr>
                                         <td>
                                              <table>
                                                 <tr>
                                                    <td nowrap>
                                                        材料名称：</td>
                                                    <td>
                                                        <input ID="MaterialCodeTextBox" size="12" name="MaterialCodeTextBox" runat="server" class="input"></td>
                                                    <td nowrap>
                                                        规 格：</td>
                                                    <td><input ID="Spec" name="Spec" size="12" runat="server" class="input"></td>
                                                    <td><input id="chkSearch" type="checkbox" value="0" runat="server" checked> 在分类中查找</td>
                                                    <td><INPUT class="submit" id="btnSearch" type="button" value="搜 索" runat="server" onclick="doSearch();">
										            &nbsp;<img src="../images/search_more.gif" title="高级查询" style="CURSOR:hand" id="imgAdvSearch" onclick="javascript:ShowAdvSearch();"></td>
                                                </tr>
                                             </table>
                                             <table style="DISPLAY:none" id="divAdvSearch">  
                                                <tr>  
                                                    <td nowrap>
                                                        单 位：</td>
                                                    <td>
                                                    <input ID="Unit" name="Unit" size="12" runat="server" class="input">
                                                        </td>
                                                    <td nowrap>
                                                        入库日期：</td>
                                                    <td><cc3:calendar id="InDateStart" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                                                 --&gt;
                                                        <cc3:calendar id="InDateEnd" runat="server" CalendarResource="../Images/CalendarResource/" ReadOnly="False" Display="True" Value=""></cc3:calendar>
                                                                </td>    
                                                </tr>
                                            </table>
                                       </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td>
                                <table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
                                    <tr>
				                        <td vAlign="top" width="25%">
					                        <iframe id="frameLeft" src=""
						                        frameBorder="no" width="100%" scrolling="auto" height="100%"></iframe>
				                        </td>
				                        <td vAlign="top">
					                        <iframe id="frameMain" src="" frameBorder="no" width="100%" height="100%" scrolling="no">
					                        </iframe>
				                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table> 
        <input id="txtAdvSearch" type="hidden" value="none" name="txtAdvSearch" runat="server"><input id="txtRootGroupCode" type="hidden" name="txtRootGroupCode" runat="server">
        <input id="txtselectmaterialinCode" type="hidden" runat="server">
        <input id="txtRootUnitCode" type="hidden" name="txtRootUnitCode" runat="server">
        <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
    </form>
<script language="javascript">
//var MaterialTypeCode = '<%=Request["MaterialTypeCode"]%>';
var IsEmpty = "1";
document.all("frameLeft").src = '../Systems/ShowSystemGroupTree.aspx?ClassCode=1501&MainFunc=GotoList&ShowSortID=0&RootGroupCode=' + form1.txtRootGroupCode.value;
document.all("frameMain").src = "../selectbox/selectmaterialinlist.aspx?flag=<%=Request["flag"]%>&IsEmpty=" + IsEmpty+"&ReturnFunc=<%=ViewState["ReturnFunc"]%>&ProjectCode=<%=Request["ProjectCode"]%>" ;
function doSearch()
{
	var MaterialTypeCode = form1.txtselectmaterialinCode.value  ;

	var chk = form1.chkSearch.checked ;
	var sChk = "";
	if ( chk )
	sChk = "1";
		
	var href = "../selectbox/selectmaterialinlist.aspx?flag=<%=Request["flag"]%>&ReturnFunc=<%=ViewState["ReturnFunc"]%>&ProjectCode=<%=Request["ProjectCode"]%>"
		
		+ '&MaterialName=' +escape(form1.MaterialCodeTextBox.value)
		+ '&Unit=' +escape(form1.Unit.value)
		+ '&Spec=' +escape(form1.Spec.value)

    	+ '&chkSearch=' +sChk
//		+ '&RootGroupCode=' + escape(Form1.txtRootGroupCode.value)
		;
    if(chk)
    href = href + '&MaterialTypeCode='+MaterialTypeCode;
	if (form1.txtAdvSearch.value != "none") //高级查询
	{
	    href = href + '&InDateStart=' +escape(form1.InDateStart.value)
                    + '&InDateEnd=' +escape(form1.InDateEnd.value)
	        ;

	}
	
	document.all("frameMain").src = href;
}
function GotoList(typeCode)
{
	form1.txtselectmaterialinCode.value = typeCode;
	MaterialTypeCode = typeCode;

	if (form1.chkSearch.checked) //按分类＋查询条件查
	{
		doSearch();
	}
	else //只按分类查
	{
		document.all("frameMain").src = "../selectbox/selectmaterialinlist.aspx?flag=<%=Request["flag"]%>&MaterialTypeCode="+MaterialTypeCode + "&chkSearch=1&ReturnFunc=<%=ViewState["ReturnFunc"]%>&ProjectCode=<%=Request["ProjectCode"]%>" ;
	}
}
//搜索条件按回车
function SearchKeyDown()
{
	if(event.keyCode==13)
	{
		event.keyCode = 9;
		form1.btnSearch.click();
	}
}
</script>
</body>
</html>


