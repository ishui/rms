<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>

<%@ Page Language="c#" Inherits="RmsPM.Web.Document.DocumentInfo" CodeFile="DocumentInfo.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>文档信息</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../Rms.js"></script>

</head>

<script language="javascript">
	
	//修改
	function Modify()
	{
		var code = Form1.txtDocumentCode.value;
        var GroupCodeReadonly = '<%=Request.QueryString["GroupCodeReadonly"]%>';
		var w = 780;
		var h = 560;
		window.navigate("DocumentModify.aspx?Action=Modify&DocumentCode="+code + "&GroupCodeReadonly=" + GroupCodeReadonly, "文档修改" );
	}

    //审核
    function DoCheck()
    {
		var code = Form1.txtDocumentCode.value;
	    OpenCustomWindow("DocumentCheck.aspx?DocumentCode=" + code,"文档审核", 300, 180);
    }
    
	function ViewAttach(AttachmentCode)
	{
		var w = screen.width;
		var h = screen.height;
		window.open("AttachView.aspx?AttachmentCode=" + AttachmentCode, "" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=1,resizable=1,status:no;");
	}
		
</script>

<body scroll="no">
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#ffffff">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    文档信息</td>
            </tr>
            <tr>
                <td class="tools-area" valign="top">
                    <img src="../images/btn_li.gif" align="absMiddle">
                    <input class="button" id="btnModify" onclick="Modify()" type="button" value="修 改"
                        name="btnModify" runat="server">
                    <input class="button" id="btnModifyEx" style="display: none" onclick="Modify();"
                        type="button" value="审核后修改" name="btnModifyEx" runat="server">
                    <input class="button" id="btnDelete" onclick="javascript:if(!window.confirm('确实要删除这条记录吗？')) return false;"
                        type="button" value="删 除" name="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
                    <input class="button" id="btnCheck" onclick="DoCheck();" type="button" value="审 核"
                        name="btnCheck" runat="server">
                    <input class="button" id="btnClose" onclick="window.close()" type="button" value="关 闭"
                        name="btnClose">
                </td>
            </tr>
            <tr>
                <td class="table" valign="top">
                    <table class="form" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="form-item" width="100">
                                标题：</td>
                            <td>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                            <td class="form-item" width="100">
                                编号：</td>
                            <td>
                                <asp:Label ID="lblDocumentID" runat="server"></asp:Label></td>
                            <td class="form-item" width="100">
                                状态：</td>
                            <td>
                                <asp:Label ID="lblStatusName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                文档类型：</td>
                            <td colspan="3">
                                <asp:Label ID="lblGroupName" runat="server"></asp:Label></td>
                            <td class="form-item">
                                交档人：</td>
                            <td>
                                <asp:Label ID="lblAuthor" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                创建人：</td>
                            <td>
                                <asp:Label ID="lblCreatePersonName" runat="server"></asp:Label></td>
                            <td class="form-item">
                                修改人：</td>
                            <td>
                                <asp:Label ID="lblModifyPersonName" runat="server"></asp:Label></td>
                            <td class="form-item">
                                审核人：</td>
                            <td>
                                <asp:Label ID="lblCheckPersonName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                保管人：</td>
                            <td>                               
                                <asp:Label ID="KeeperLabel" runat="server"></asp:Label>
                            </td>
                            <td class="form-item">
                                文件性质：</td>
                            <td>                              
                                <asp:Label ID="FileKindLabel" runat="server"></asp:Label>
                            </td>
                            <td class="form-item" style="height: 31px">
                                文件日期：</td>
                            <td style="height: 31px">
                                <asp:Label ID="FileDateLabel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="height: 31px">
                                存放位置：</td>
                            <td style="height: 31px" colspan="3">                               
                                <asp:Label ID="SavePlaceLabel" runat="server" ></asp:Label>
                            </td>
                            <td class="form-item">
                                保存日期：</td>
                            <td>
                                <asp:Label ID="SaveDateLabel" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                创建日期：</td>
                            <td>
                                <asp:Label ID="lblCreateDate" runat="server"></asp:Label></td>
                            <td class="form-item">
                                修改日期：</td>
                            <td>
                                <asp:Label ID="lblModifyDate" runat="server"></asp:Label></td>
                            <td class="form-item">
                                审核日期：</td>
                            <td>
                                <asp:Label ID="lblCheckDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                附件：</td>
                            <td colspan="3">
                                <uc1:AttachMentList ID="myAttachMentList" runat="server"></uc1:AttachMentList>
                            </td>
                            <td class="form-item">
                                页数/份数：</td>
                            <td>
                                <asp:Label ID="Counts" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                备注：</td>
                            <td colspan="5">
                                <asp:Label ID="RemarkLabel" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div id="divMainText" runat="server" style="overflow: auto; width: 100%; height: 80%;
                        position: absolute;">
                    </div>
                    <table style="display: none">
                        <tr>
                            <td class="form-item" width="120">
                                固定文档类型：</td>
                            <td colspan="3">
                                <asp:Label ID="lblFixDocumentTypeName" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="display: none">
                            <td class="form-item" width="120">
                                自定义文档类型：</td>
                            <td colspan="3">
                                <asp:Label ID="lblUnFixDocumentTypeName" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input id="txtProjectCode" type="hidden" name="txtProjectCode" runat="server">
        <input id="txtAct" type="hidden" name="txtAct" runat="server"><input id="txtDocumentCode"
            type="hidden" name="txtDocumentCode" runat="server">
        <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server">
        <input id="txtStatus" type="hidden" name="txtStatus" runat="server">
    </form>
</body>
</html>
