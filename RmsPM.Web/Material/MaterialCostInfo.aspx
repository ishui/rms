<%@ Page language="c#" Inherits="RmsPM.Web.Material.MaterialCostInfo" CodeFile="MaterialCostInfo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>材料价格信息</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">材料价格信息</td>
				</tr>
				<tr>
					<td class="tools-area" vAlign="top"><input class="button" id="btnModify" onclick="doModify('');return false;" type="button"
							value="修 改" name="btnNew" runat="server"> <input class="button" id="btnDelete" onclick="if(!(confirm('确定删除这条记录 ？'))) return false;"
							type="button" value="删 除" name="btnNew" runat="server" onserverclick="btnDelete_ServerClick"></td>
				</tr>
				<tr height="100%">
					<td vAlign="top">
					    <div style="overflow:auto;width:100%;height:100%;">
						    <table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
							    <TR>
								    <TD class="form-item" noWrap width="80">单位：</TD>
								    <TD><asp:label id="lblUnit" runat="server"></asp:label></TD>
								    <TD class="form-item" noWrap width="80"><asp:label runat="server" ID="lblPriceTitle">单价</asp:label>：</TD>
								    <TD><asp:label id="lblPrice" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>项目：</TD>
								    <TD><asp:label id="lblProject" runat="server"></asp:label></TD>
								    <TD class="form-item" noWrap>日期：</TD>
								    <TD><asp:label id="lblBiddingDate" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>分类：</TD>
								    <TD><asp:label id="lblGroupName" runat="server"></asp:label></TD>
								    <TD class="form-item" noWrap>地区：</TD>
								    <TD><asp:label id="lblAreaCode" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>描述：</TD>
								    <TD colspan="3"><asp:label id="lblDescription" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>category：</TD>
								    <TD colspan="3"><asp:label id="lblCategory" runat="server"></asp:label></TD>
							    </TR>
							    <TR>
								    <TD class="form-item" noWrap>description：</TD>
								    <TD colspan="3"><asp:label id="lblDescriptionEn" runat="server"></asp:label></TD>
							    </TR>
						    </table>
					    </div>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtMaterialCostCode" name="txtMaterialCostCode" runat="server" />
		</form>
		<script language="javascript">
		
	function doModify()
	{
		window.navigate('MaterialCostModify.aspx?MaterialCostCode=' + Form1.txtMaterialCostCode.value , '修改材料价格');
	}
	
		</script>
	</body>
</HTML>
