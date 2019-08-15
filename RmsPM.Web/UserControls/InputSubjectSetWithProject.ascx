<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.InputSubjectSetWithProject" CodeFile="InputSubjectSetWithProject.ascx.cs" %>
<asp:datagrid id="dgList" runat="server" Width="100%" CssClass="list" PageSize="15" AutoGenerateColumns="False"
	AllowSorting="True" GridLines="Horizontal" CellSpacing="0" CellPadding="0">
	<AlternatingItemStyle CssClass="AlterGridTr"></AlternatingItemStyle>
	<HeaderStyle CssClass="list-title"></HeaderStyle>
	<FooterStyle CssClass="list-title"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="SubjectSetCode" HeaderText="" Visible="False"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="帐套">
			<ItemTemplate>
				<%# DataBinder.Eval(Container.DataItem, "SubjectSetName")  %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="项目名称">
			<ItemTemplate>
				<%# DataBinder.Eval(Container.DataItem, "ProjectName")  %>
				<input type="hidden" runat="server" id="txtProjectCode" name="txtProjectCode" value='<%# DataBinder.Eval( Container.DataItem,"ProjectCode" ) %>' />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="财务编码">
			<ItemTemplate>
				<input class=input runat=server id=txtU8Code type=text value='<%# DataBinder.Eval( Container.DataItem,"U8Code" ) %>' NAME="txtU8Code">
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="下一页&gt;&gt;&gt;" PrevPageText="&lt;&lt;&lt;上一页" HorizontalAlign="Right"
		CssClass="ListHeadTr"></PagerStyle>
</asp:datagrid>
<input type="hidden" runat="server" id="txtTableName" name="txtTableName"> <input type="hidden" runat="server" id="txtKeyFieldName" name="txtKeyFieldName">
<input type="hidden" runat="server" id="txtCodeFieldName" name="txtCodeFieldName">
<input type="hidden" runat="server" id="txtProjectCode" name="txtProjectCode"><input type="hidden" runat="server" id="txtSubjectSetCode" name="txtSubjectSetCode">
<input type="hidden" runat="server" id="txtIsGroup" name="txtIsGroup">
