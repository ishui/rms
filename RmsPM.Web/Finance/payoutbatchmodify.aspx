<%@ Page language="c#" Inherits="RmsPM.Web.Finance.PayoutBatchModify" CodeFile="PayoutBatchModify.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>批量修改供应商</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
		<script language="javascript">
		function doCheck()
		{
			var num = document.all("dgDetailData").rows.length;			
			for(i=2;i<=num;i++)
			{
				var txtName = document.all("dgDetailData__ctl"+i+"_txtName").value;
				var txtUnit  = document.all("dgDetailData__ctl"+i+"_txtUnit").value;
				
				if(txtName.length==0)
				{					
					document.all("dgDetailData__ctl"+i+"_txtName").select();
					document.all("dgDetailData__ctl"+i+"_txtName").focus();
					alert("请输入受款人");
					return false;
				}
				if(txtUnit.length==0)
				{					
					document.all("dgDetailData__ctl"+i+"_txtUnit").select();
					document.all("dgDetailData__ctl"+i+"_txtUnit").focus();
					alert("请输入受款单位");
					return false;
				}
			}
			return true;
		}		
		</script>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr vAlign="top" align="center">
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="topic" align="center" background="../images/topic_bg.gif" height="25">批量修改供应商</td>
							</tr>
						</TABLE>
						<asp:datagrid id="dgDetailData" runat="server" AutoGenerateColumns="False" CssClass="list" Width="100%"
							DataKeyField="PayoutCode">
							<AlternatingItemStyle CssClass="list-i"></AlternatingItemStyle>
							<HeaderStyle CssClass="list-title"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="付款单号">
									<ItemTemplate>
										<%#  DataBinder.Eval(Container.DataItem, "PayoutCode")  %>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="受款单位">
									<ItemTemplate>
										<INPUT class="input" id="txtSupplierName" readOnly type="text" size="32" name="txtSupplierName" value='<%#  RmsPM.BLL.ProjectRule.GetSupplierName(DataBinder.Eval(Container.DataItem, "SupplyCode").ToString())  %>'
										runat="server"><INPUT id="txtSupplierCode" type="hidden" name="txtSupplierCode" value='<%#  DataBinder.Eval(Container.DataItem, "SupplyCode")  %>'
										runat="server"> <A onclick="doSelectSupplier(<%#Container.ItemIndex + 2 %>);return false;" href="##"><IMG src="../images/ToolsItemSearch.gif" border="0"></A>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="受款人">
									<ItemTemplate>
										<asp:TextBox CssClass="input" id="txtName" runat="server" value='<%#  DataBinder.Eval(Container.DataItem, "Payer")  %>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
						<P></P>
						<input class="submit" id="btnSave" type="submit" onclick='if(!doCheck()) return false;'
							value="保存" name="btnSave" runat="server" onserverclick="btnSave_ServerClick"> <!--   -->
					</td>
				</tr>
			</table>
		</form>
		<script language=javascript>
			function doSelectSupplier(flag)
	{
		var supplierCode = document.all["dgDetailData__ctl"+flag+"_txtSupplierCode"].value;
		OpenLargeWindow('../SelectBox/SelectSupplier.aspx?flag='+flag+'&SupplierCode=' + supplierCode ,'选择供应商');
	}

	function DoSelectSupplierReturn ( supplierCode,supplierName,ContractPerson,OfficePhone,WorkAddress,flag )
	{
		document.all["dgDetailData__ctl"+flag+"_txtSupplierName"].value = supplierName;
		document.all["dgDetailData__ctl"+flag+"_txtSupplierCode"].value = supplierCode;
	}
		</script>
	</body>
</HTML>
