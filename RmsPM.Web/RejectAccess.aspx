<%@ Page language="c#" Inherits="RmsPM.Web.RejectAccess" CodeFile="RejectAccess.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>房地产行业OA管理系统</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Images/Style.css" type="text/css" rel="stylesheet">
		<Script language="javascript" src="./Rms.js"></Script>
		<Script language="javascript">
		</Script>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
	</HEAD>
	<body bgcolor="#f5f5f5" leftmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<div id="div0" style="Z-INDEX:999; LEFT:0px; WIDTH:100%; POSITION:absolute; TOP:0px; HEIGHT:100%">
				<table width="100%" height="100%">
					<tr>
						<td>
							<table width="250" height="60" align="center" bgcolor="#f7f7f7" style="BORDER-RIGHT:#a6c4e1 1px solid; BORDER-TOP:#a6c4e1 1px solid; FONT-WEIGHT:normal; FONT-SIZE:12px; BORDER-LEFT:#a6c4e1 1px solid; LINE-HEIGHT:normal; BORDER-BOTTOM:#a6c4e1 1px solid; FONT-STYLE:normal; FONT-VARIANT:normal">
								<tr>
									<td align="center"><font color="red">无此权限<asp:label Runat="server" ID="lblOperationName"></asp:label>，拒绝访问！</font></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
