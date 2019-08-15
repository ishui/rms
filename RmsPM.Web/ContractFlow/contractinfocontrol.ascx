<%@ Control Language="c#" Inherits="RmsPM.Web.ContractFlow.ContractInfoControl" CodeFile="ContractInfoControl.ascx.cs" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<table cellSpacing="7" cellPadding="0" width="100%" border="0">
	<tr>
		<td style="BORDER-RIGHT: #ededed 3px dotted; PADDING-RIGHT: 7px" vAlign="top" width="60%">
			<table class="form" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="form-item" width="20%">合同名称：</TD>
					<TD width="30%"><asp:label id="LabelContractName" runat="server"></asp:label></TD>
					<TD class="form-item" width="20%">合同编号：</TD>
					<TD width="30%"><asp:label id="LabelContractID" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="lblStatus" runat="server" ForeColor="Red"></asp:label></TD>
				</TR>
				<tr>
					<TD class="form-item">总 金 额（元）：</TD>
					<TD align="right"><asp:label id="LabelTotalMoney" runat="server"></asp:label></TD>
					<TD class="form-item" id="tdBefore" runat="server"></TD>
					<TD align="right"><asp:label id="lblBeforeAccountTotalMoney" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="form-item" width="10%">已付：</TD>
					<TD align="right"><asp:label id="lblAPMoney" runat="server"></asp:label></TD>
					<TD class="form-item" width="10%">未付：</TD>
					<TD align="right"><asp:label id="lblUPMoney" runat="server"></asp:label></TD>
				</tr>
				<TR>
					<TD class="form-item">供 应 商：</TD>
					<TD><span id="SupplierSpan" runat="server"></span></TD>
					<TD class="form-item">第 三 方：</TD>
					<TD><asp:label id="lblThirdParty" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="form-item">部门：</TD>
					<TD><asp:label id="lblUnitName" runat="server"></asp:label></TD>
					<TD class="form-item">类型：</TD>
					<TD><asp:label id="LabelType" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="form-item">对应工作：</TD>
					<TD colSpan="3"><A id="hrefTaskName" onmouseover="init(myjoybox, joyboxTable, linktitle, hint);" onmouseout="mouseend();"
							href="javascript:OpenTask();" runat="server"><asp:label id="lblTaskName" runat="server"></asp:label></A></TD>
				</TR>
				<TR>
					<TD class="form-item">合同标的：</TD>
					<TD colSpan="3"><asp:label id="lblContractObject" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="form-item">分摊方式：</TD>
					<TD id="tdAllocateRelation" colSpan="3" runat="server"></TD>
				</TR>
				<TR>
					<TD class="form-item">附属文件：</TD>
					<TD><uc1:attachmentlist id="myAttachMentList" runat="server"></uc1:attachmentlist></TD>
				</TR>
				<TR>
					<TD class="form-item">经办人：</TD>
					<TD><asp:label id="LabelContractPerson" runat="server"></asp:label></TD>
					<TD class="form-item">生效日期：</TD>
					<TD><asp:label id="LabelContractDate" runat="server"></asp:label></TD>
				</TR>
				<tr>
					<td colSpan="3"><FONT face="宋体"></FONT></td>
				</tr>
				<TR>
					<TD class="form-item">备注：</TD>
					<TD colSpan="3"><asp:label id="LabelRemark" runat="server"></asp:label></TD>
				</TR>
				<tr runat="server" id="PurchaseTr">
					<td class="form-item">采 购 单：</td>
					<td colSpan="3" runat="server" id="PurchaseLink"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td vAlign="top">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="intopic" width="200">付款计划</td>
					<td><input class="button-small" id="btnModifyPaymentPlan" onclick="DoModifyPaymentPlan();return false;"
							type="button" value="修改付款计划" name="btnModifyPaymentPlan" runat="server"></td>
				</tr>
			</table>
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:datagrid id="dgPaymentPlanList" runat="server" CssClass="list" CellPadding="2" GridLines="Horizontal"
					AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Width="100%" AllowPaging="False">
					<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
					<HeaderStyle CssClass="list-title"></HeaderStyle>
					<FooterStyle CssClass="list-title"></FooterStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="序号">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="ItemName" HeaderText="款 项" HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
						<asp:BoundColumn DataField="PlanningPayDate" HeaderText="付款时间" DataFormatString="{0:yyyy-MM-dd}"
							HeaderStyle-Wrap="False" ItemStyle-Wrap="False"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="费用项" HeaderStyle-Wrap="False" ItemStyle-Wrap="False">
							<ItemTemplate>
								<%# RmsPM.BLL.CBSRule.GetCostName( DataBinder.Eval(Container.DataItem, "CostCode").ToString() ) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Money" HeaderText="金 额" DataFormatString="{0:N}" HeaderStyle-Wrap="False"
							ItemStyle-Wrap="False">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="已付（元）" HeaderStyle-Wrap="False" ItemStyle-Wrap="False">
							<HeaderStyle HorizontalAlign="Right" Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container, "DataItem.TotalPayoutMoney", "{0:N}") %>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="付款条件" HeaderStyle-Wrap="False" ItemStyle-Wrap="False">
							<ItemStyle></ItemStyle>
							<ItemTemplate>
								<span id="spanPayConditionHtml">
									<%#  DataBinder.Eval(Container.DataItem, "PayConditionText")  %>
									<%#  DataBinder.Eval(Container.DataItem, "PayConditionHtml")  %>
								</span>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle NextPageText="下页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上页" HorizontalAlign="Right"
						CssClass="list-title"></PagerStyle>
				</asp:datagrid></div>
		</td>
	</tr>
</table>
<div id="myjoybox" style="DISPLAY: none; LEFT: 10px; POSITION: absolute; TOP: 336px; HEIGHT: 120px"
	myOffsetTop="0px">
	<table id="joyboxTable" height="120" cellSpacing="0" cellPadding="0" width="220" border="0">
		<tbody>
			<tr>
				<td width="8%" bgColor="#ffffcc">
				<td width="92%" bgColor="#ffffcc"><label id="linktitle"></label></td>
			</tr>
		</tbody>
	</table>
</div>
<span id="ScriptSpan" runat="server"></span>
<script language="javascript">
function doViewSupplier(SupplierCode)
{
    OpenFullWindow("../Supplier/SupplierInfo.aspx?ProjectCode=&SupplierCode=" + SupplierCode,'供应商信息');
}
function OpenTask(WBSCode)
{
	OpenFullWindow("../Project/WBSInfo.aspx?WBSCode="+WBSCode,"");
}

</script>
