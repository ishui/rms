<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectCostImportDlg.aspx.cs" Inherits="ProjectCost_ProjectCostImportDlg" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>项目造价导入</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
	<SCRIPT language="javascript" src="../Rms.js">
	//alert("document.all(btnOK)");
	//if(document.all(txtResult))
	//document.all(txtResult).style.display = "none";
	
	</SCRIPT>
</head>
    <body scroll="no">
        <form id="form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
			    <tr>					
			        <td class="topic" align="center" background="../images/topic_bg.gif" height="25">项目造价导入</td>
			    </tr>
			    <tr align="center">
				    <td style="COLOR: blue">项目造价将导入项目造价库</td>
			    </tr>
			    <tr height="100%">
				    <td vAlign="top" align="left">
					    <table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
						    <tr>
							    <td class="form-item" width="60">文件：</td>
							    <td><input class="textbox" id="txtFile" type="file" style="WIDTH:100%" size="45" name="txtFile" runat="server"></td>
						    </tr>
					    </table>
	    			    <table cellSpacing="0" cellPadding="0" border="0" id="tabResult" name="tabResult" style="display:none" runat="server" width="100%">
					        <tr>
						        <td class="intopic" width="200">导入结果</td>
					        </tr>
				            <tr>		
					            <td colspan="2"><TEXTAREA class="input" id="txtResult" style="WIDTH: 100%; HEIGHT: 120px;" name="txtResult" runat="server" rows="10"></TEXTAREA></td>
				            </tr>
				        </table>
					    <table cellSpacing="0" cellPadding="0" width="90%" border="0">
					    <br>
						    <tr>
							    <td>文件格式说明：<br>
								    1.文件类型必须是csv（逗号分隔）<br>
								    2.文件的第1行为标题行，以后为数据行。<br>
								    3.数据行依次为：<br>
								    &nbsp;&nbsp;<asp:Label Runat="server" ID="lblFieldDesc" ForeColor="blue"></asp:Label><br>
								    4.“类别全名”用“-&gt;”分隔，如：项目造价-&gt;弱电，且必须和系统类别中的项目类别对应

							    </td>
						    </tr>
					    </table>
				    </td>
			    </tr>
			    <tr>
			        <td>
	                    <table align="center">
                            <tr>
	                            <td>	
	                                <input class="submit" type="button" value="清 空" runat="server" id="btnDeleteAll" name="btnDeleteAll" onclick="if (!confirm('确实要删除所有项目造价吗？')) return false;" onserverclick="btnDeleteAll_ServerClick" />
                                    <input class="submit" type="button" value="导 入" runat="server" id="btnOK" name="btnOK"  onserverclick="btnOK_ServerClick" />
                                    <input class="submit" type="button" value="关 闭" runat="server" id="btnCancel" name="btnCancel" onclick="window.close();" />
                                </td>
                            </tr>
                             <tr><td><br></td></tr>
                        </table>
			        </td>
			    </tr>
			   
            </table>
		</form>
    </body>
</html>
