<%@ Control Language="c#" Inherits="RmsPM.Web.BiddingManage.UCBiddingSupplierList" CodeFile="UCBiddingSupplierList.ascx.cs" %>
<asp:datagrid id="dgList" DataKeyField="BiddingSupplierCode" AutoGenerateColumns="False" runat="server"
	Width="100%" PageSize="15" OnItemCommand="dgList_ItemCommand">
	<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="BiddingSupplierCode" ReadOnly="True" HeaderText="BiddingSupplierCode"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="序号">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%#Container.ItemIndex+1%>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="通过">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<input type="checkbox" checked='<%# DataBinder.Eval(Container, "DataItem.Flag").ToString()=="1" %>'  id="chkSelect" runat="server" name="chkSelect"  class="list-checkbox"  value='<%#DataBinder.Eval(Container, "DataItem.BiddingSupplierCode")%>'/>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="通过">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Flag").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="公司名称">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<a href="#" onclick='doBiddingSupplierModify("<%# DataBinder.Eval(Container, "DataItem.BiddingSupplierCode") %>","SingleView","<%# DataBinder.Eval(Container, "DataItem.SupplierCode") %>");return false;'>
					<%# DataBinder.Eval(Container, "DataItem.SupplierName") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="资质">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quality") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="地址">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkAddress") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="联系人">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ContractPerson") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="电话">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OfficePhone") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="E-Mail">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EMail") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="网址">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WebAddress") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="提名人">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.NominateUser") %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="提名人&lt;font color='#FF0000'&gt;*&lt;/font&gt;">
			<HeaderStyle CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<INPUT class=input id=txtNominateUser disabled type=text size=12 value='<%# DataBinder.Eval(Container, "DataItem.NominateUser") %>' name=txtNominateUser runat="server">
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="建筑部">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<asp:CheckBox id="Depart_Build" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Depart_Build").ToString()=="1" %>'>
				</asp:CheckBox>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="工程部">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:CheckBox id="Depart_Project" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.Depart_Project").ToString()=="1" %>' EnableViewState="true">
							</asp:CheckBox>
						</td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="合约部">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:CheckBox id="Depart_Agreement" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Depart_Agreement").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="项目总监">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:CheckBox id="Md_Item" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Md_Item").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="工程总监">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:CheckBox id="Md_Project" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Md_Project").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="合约总监">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:CheckBox id="Md_Agreement" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Md_Agreement").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="财务总监">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:CheckBox id="DepartmentRemark1" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.DepartmentRemark1").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="工程执董">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><
							<asp:CheckBox id="Director_Project" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Director_Project").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="合约执董">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><
							<asp:CheckBox id="Director_Agreement" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Director_Agreement").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="账务执董">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><
							<asp:CheckBox id="Director_Finaace" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Director_Finance").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="销售执董">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center">
							<asp:CheckBox id="DepartmentRemark" runat="server" EnableViewState="true" Checked='<%# DataBinder.Eval(Container, "DataItem.DepartmentRemark").ToString()=="1" %>'>
							</asp:CheckBox></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="建筑部">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Depart_Build").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="工程部">
			<HeaderStyle HorizontalAlign="Center" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center" CssClass="blackbordertd"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Depart_Project").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="合约部">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Depart_Agreement").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="项目总监">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Md_Item").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="工程总监">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Md_Project").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="合约总监">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Md_Agreement").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="工程执董">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Director_Project").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="合约执董">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Director_Agreement").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="账务执董">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.Director_Finance").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="销售执董">
			<HeaderStyle CssClass="blackbordertdcontent"></HeaderStyle>
			<ItemStyle CssClass="blackbordertdcontent"></ItemStyle>
			<ItemTemplate>
				<table>
					<tr>
						<td align="center"><%# DataBinder.Eval(Container, "DataItem.DepartmentRemark").ToString()=="1" ? "√":""%></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:ButtonColumn Text="删除" CommandName="Delete">
			<HeaderStyle Wrap="False" CssClass="blackbordertd"></HeaderStyle>
			<ItemStyle Wrap="False" CssClass="blackbordertd"></ItemStyle>
			<FooterStyle Wrap="False"></FooterStyle>
		</asp:ButtonColumn>
	</Columns>
</asp:datagrid>
<script>
	function IsWantToReturn()
	{
		//if()
		if(window.confirm("确定选好了招标单位吗?"))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	function doBiddingSupplierModify(strCellCode,strType,strSupplierCode){

	var strURL = '';
	
	strURL = '../Supplier/SupplierInfo.aspx?SupplierCode=' + strSupplierCode;
		
	var theWin = OpenFullWindow(strURL,'供应商信息');
	theWin.focus();

    }

 
</script>
