<%@ Page language="c#" Inherits="RmsPM.Web.Contract.MoreContractList" CodeFile="MoreContractList.aspx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>������غ�ͬ</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript" src="../Rms.js"></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="white" border="0">
				<tr>
					<td class="topic" align="center" background="../images/topic_bg.gif" height="25">������غ�ͬ</td>
				</tr>
				<tr>
					<td align="center" valign="top" class="table">

						<table class="list" cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
							<tr class="list-title">
								<td noWrap>��ͬ����</td>
								<td noWrap align="right">������ý�Ԫ��</td>
								<td noWrap align="right">��������ѷ�����Ԫ��</td>
							</tr>
							<asp:repeater id="repeatContract" runat="server">
								<ItemTemplate>
									<tr class="list-i">
										<td nowrap>
											<a href="#"  onclick='doViewContract(this.code)' code='<%# DataBinder.Eval(Container.DataItem, "ContractCode") %>' >
												<%# DataBinder.Eval(Container.DataItem, "ContractName") %>
											</a>
										</td>
										<td>
											<div align="right"><%# RmsPM.BLL.StringRule.BuildShowNumberString( DataBinder.Eval(Container.DataItem, "ContractCostMoney") ) %></div>
										</td>
										<td>
											<div align="right"><%# RmsPM.BLL.StringRule.BuildShowNumberString ( DataBinder.Eval(Container.DataItem, "ContractPayed") ) %></div>
										</td>
									</tr>
								</ItemTemplate>
							</asp:repeater></table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
			
		function DoSelectSupplierReturn ( code,name)
		{
			Form1.txtSupplierCode.value = code;
			Form1.txtSupplierName.value = name;
		}

		function openSelectSupplier()
		{
			OpenMiddleWindow( '../SelectBox/SelectSupplier.aspx?ProjectCode=<%=Request["ProjectCode"]%>','ѡ��Ӧ��' );
		}

		function doSelectContractType()
		{
			var typeCode = Form1.txtTypeCode.value;
			OpenMiddleWindow('SelContractType.aspx?ContractTypeCode=' + typeCode ,'ѡ���ͬ����');
		}
		
		function doSelectContractTypeReturn( code,name)
		{
			document.all("txtTypeCode").value=code;
			document.all("txtTypeName").value=name;
		}

		function doNewContract()
		{
			OpenFullWindow('ContractModify.aspx?ProjectCode=<%=Request["ProjectCode"]%>&act=Add','������ͬ');
		}
	
		function doViewContractInfo( code )
		{
			OpenFullWindow('ContractInfo.aspx?ProjectCode=<%=Request["ProjectCode"]%>&ContractCode=' + code,'��ͬ��Ϣ');
		}

		</script>
	</body>
</HTML>
