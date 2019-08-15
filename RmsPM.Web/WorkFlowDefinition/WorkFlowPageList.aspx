<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowPageList.aspx.cs" Inherits="WorkFlowDefinition_WorkFlowPageList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
		<link href="../Images/index.css" type="text/css" rel="stylesheet" />
		<script language="javascript" src="../Rms.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <table cellspacing="0" cellpadding="0" style="width:100%;" border="0" height="100%">
			<tr>
				<td style="background-color:#e4eff6;height:6px"></td>
			</tr>
			<tr>
				<td style="background-image:url(../images/topic_bg.gif);height:25px">
					<table cellspacing="0" cellpadding="0" style="width:100%;" border="0">
						<tr>
							<td class="topic" style="height:25px;width:35px;" valign="top">
							    <img height="25" src="../images/topic_li.jpg" width="35" alt="" />
							 </td>
							 <td class="topic">流程定义&nbsp;流程列表</td>
							<td style="width:9px;"><img height="25" src="../images/topic_corr.gif" width="9" alt="" /></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="tools-area" valign="top">
				    <table cellpadding="0" cellspacing="0" border="0">
				        <tr>
				            <td><img src="../images/btn_li.gif" alt="" style="vertical-align:top"/></td>
				            <td><input class="button" id="btnAdd" onclick="AddNewPage();return false;" type="button"value="新 增" name="btnAdd" /></td>
				        </tr>
				    </table>
				</td>
			</tr>
			<tr>
				<td class="table" valign="top"  style="height:518px">
					<div style="overflow:auto;width:100%;height:100%">
					    <asp:GridView id="gvPageList" runat="server" AutoGenerateColumns="false" CssClass="list" CellPadding="0" CellSpacing="0">
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<FooterStyle CssClass="list-title"></FooterStyle>
							<Columns>
							    <asp:TemplateField HeaderText="页面名称">
									<ItemTemplate>
									
										<a href="##" onclick='ModifyPage(this.PageCode)' PageCode='<%# DataBinder.Eval(Container.DataItem, "WorkFlowPageCode") %>'>
										    <%# DataBinder.Eval(Container.DataItem, "PageName") %>
										</a>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="PagePath" HeaderText="页面路径" />
								<asp:BoundField DataField="Remark" HeaderText="备注" />
							</Columns>
					    </asp:GridView>
					</div>
				</td>
			</tr>
			<tr>
				<td style="height:12px">
					<table cellspacing="0" cellpadding="0" width="100%" border="0">
						<tr>
							<td style="background-image:url(../images/corl_bg.gif)">
							    <img height="12" src="../images/corl.gif" width="12" alt="" />
							</td>
							<td style="width:12px"><img height="12" src="../images/corr.gif" width="12" alt="" /></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="background-color:#e4eff6;height:6px"></td>
			</tr>
		</table>
    </form>
		<script language="javascript">
	
		function AddNewPage()
		{
			OpenFullWindow( '../WorkFlowDefinition/WorkFlowPageModify.aspx?Act=Add' ,'流程页面新增');
		}
	
		function ModifyPage( PageCode )
		{
			OpenFullWindow( '../WorkFlowDefinition/WorkFlowPageModify.aspx?Act=Edit?PageCode=' + PageCode ,'流程页面修改');
		}


		</script>    
</body>
</html>
