<%@ Register TagPrefix="ftb" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register TagPrefix="uc2" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>

<%@ Page ValidateRequest="false" Language="c#" Inherits="RmsPM.Web.Project.DocumentModify"
    CodeFile="DocumentModify.aspx.cs" %>

<%@ Register Src="../UserControls/inputunit.ascx" TagName="inputunit" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc1" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>文档修改</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Images/index.css" type="text/css" rel="stylesheet">

    <script language="javascript">

	function SelectDocumentType() {
		var w = 400;
		var h = 300;
		var code = "";
		var name = "";
		
		for (i=0;i<Form1.lstDocumentType.length;i++)
		{
			code = code + "," + Form1.lstDocumentType.item(i).value;
			name = name + "," + Form1.lstDocumentType.item(i).text;
		}
		//alert("SelectDocumentType.aspx?SelectCode=" + escape(code) + "&SelectName=" + escape(name), "选择文档类型" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
		window.open("SelectDocumentType.aspx?SelectCode=" + escape(code) + "&SelectName=" + escape(name), "选择文档类型" , "top="+(screen.height-h)/2+",left="+(screen.width-w)/2+",width="+w+",height="+h+",scrollbars=0,resizable=1,status:no;");
	}
		
	function SelectDocumentTypeClose(code, name) {
		Form1.txtlstDocumentTypeCode.value = code;
		Form1.txtlstDocumentTypeName.value = name;
		Form1.btnRefreshDocumentType.click();
		}
    </script>


    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />
    <link href="/../Images/index.css" rel="stylesheet" type="text/css" />

</head>
<body scroll="no">
    <form id="Form1" name="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" height="100%"
            bgcolor="#ffffff">
            <tr>
                <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                    文档信息</td>
            </tr>
            <tr>
                <td class="topic" valign="top" align="center">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                        <tr>
                            <td width="120" class="form-item">
                                标题：</td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="input" Width="240px"></asp:TextBox><font
                                    color="red">*</font></td>
                            <td width="100" class="form-item">
                                编号：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="txtDocumentID" runat="server" CssClass="input" Width="250"></asp:TextBox>&nbsp;
                                <input id="btnDocumentID" runat="server" class="submit" name="btnDocumentID"
                                    type="button" value="自动生成" onserverclick="btnDocumentID_ServerClick" />
                                <asp:TextBox ID="txtDocumentOther" runat="server" CssClass="input" Width="97px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                文档类型：</td>
                            <td>
                                <uc2:InputSystemGroup ID="ucGroup" runat="server" ClassCode="1001"></uc2:InputSystemGroup>
                                <font color="red">*</font></td>
                            <td class="form-item">
                                交档人：</td>
                            <td>
                                <asp:TextBox ID="txtAuthor" runat="server" CssClass="input" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="form-item">
                                保管人：</td>
                            <td>
                                <asp:TextBox ID="KeeperTextBox" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                            </td>
                            <td class="form-item">
                                文件性质：</td>
                            <td>                                
                                <SELECT id="FileKind" size="1" name="sltDirection" runat="server">
										<OPTION value="" selected>------请选择------</OPTION>
									</SELECT></td>
                        </tr>
                        <tr>
                            <td class="form-item" style="height: 31px">
                                文件日期：</td>
                            <td style="height: 31px">                              
                                <cc1:Calendar ID="FileDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="Date">
                </cc1:Calendar></td>
                            <td class="form-item" style="height: 31px">
                                存放位置：</td>
                            <td style="height: 31px" colspan="3">
                                <uc3:inputunit ID="Inputunit1" runat="server" />
                                <asp:TextBox ID="SavePlaceTextBox" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td class="form-item" style="height: 31px">
                                保存日期：</td>
                            <td style="height: 31px">                              
                                <cc1:Calendar ID="SaveDate" runat="server" CalendarResource="../Images/CalendarResource/"
                    CalendarMode="Date">
                </cc1:Calendar></td>
                            
                            <td class="form-item" style="height: 31px">
                                备注：</td>
                            <td style="height: 31px">                                
                                <asp:TextBox ID="RemarkTextBox" runat="server" CssClass="input" Width="350px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                        
                        </tr>
                        
                        <tr>
                            <td class="form-item">
                                附件：</td>
                            <td>
                                <uc1:AttachMentAdd ID="AttachMentAdd1" runat="server"></uc1:AttachMentAdd>
                            </td>
                            <td class="form-item" nowrap>
                                页数/份数：</td>
                            <td>
                                <asp:TextBox ID="Counts" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="form-item">
                                固定文档类型：</td>
                            <td colspan="3">
                                <asp:Label ID="lblFixDocumentTypeName" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="display: none">
                            <td class="form-item">
                                自定义文档类型：</td>
                            <td colspan="3">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td rowspan="2">
                                            <select id="lstDocumentType" style="width: 240px; height: 72px" size="4" name="lstDocumentType"
                                                runat="server" datavaluefield="DocumentTypeCode" datatextfield="DocumentTypeName">
                                            </select>
                                        </td>
                                        <td align="center" width="60">
                                            <a id="anAddType" runat="server" onclick="SelectDocumentType('');return false;" href="#">
                                                添加</a></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <a id="anDeleteType" runat="server" href="#" onserverclick="anDeleteType_Click">删除</a></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="intopic" width="200">
                                正文</td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td>
                    <ftb:FreeTextBox ID="FreeTextBox" runat="server" Width="100%" Height="100%" ButtonPath="../images/ftb/office2003/"
                        HelperFilesPath="../HelperScripts/">
                    </ftb:FreeTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellspacing="10">
                        <tr>
                            <td align="center">
                                <input id="btnSave" name="btnSave" type="button" class="submit" value="确 定" runat="server"
                                    onserverclick="btnSave_ServerClick">
                                <input id="btnCancel" name="btnCancel" type="button" class="submit" value="取 消" onclick="javascript:self.close()">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input id="txtAct" type="hidden" name="txtAct" runat="server">
        <input id="txtDocumentCode" type="hidden" name="txtDocumentCode" runat="server">
        <input id="txtRefreshScript" type="hidden" name="txtRefreshScript" runat="server">
        <div style="display: none">
            <input id="txtFromUrl" type="hidden" name="txtFromUrl" runat="server"><input id="txtFixedDocumentTypeCode"
                type="hidden" name="txtFixedDocumentTypeCode" runat="server"><input id="btnRefreshAttach"
                    type="button" name="btnRefreshAttach" runat="server">
            <input id="txtCode" type="hidden" name="txtCode" runat="server"><input id="txtlstDocumentTypeCode"
                type="hidden" name="txtlstDocumentTypeCode" runat="server"><input id="txtlstDocumentTypeName"
                    type="hidden" name="txtlstDocumentTypeName" runat="server"><input id="btnRefreshDocumentType"
                        type="button" name="btnRefreshDocumentType" runat="server" onserverclick="btnRefreshDocumentType_ServerClick"></div>
    </form>
</body>
</html>
