<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeedBackListl.ascx.cs" Inherits="Remind_FeedBackListl" %>


<LINK href="../Images/index.css" type="text/css" rel="stylesheet">
<TABLE cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD class="intopic">反馈内容</TD>
		<td></td>
	</TR>
</TABLE>
<table  width="100%" border="0" cellpadding="0" cellspacing="0"  height=50 border=0>
	<tr>
		<td>
			<div style="OVERFLOW: auto; ">
				<table  width="100%" border="0" cellpadding="0" cellspacing="0" class="form">	
					<asp:repeater id="rpFeedBack" Runat="server">
						<ItemTemplate>
							<tr>
								<td class="form-item"  nowrap><%# RmsPM.BLL.SystemRule.GetUserName((string)DataBinder.Eval(Container.DataItem, "Author")) %></td>
								<td ><%# DataBinder.Eval(Container.DataItem, "Content") %></td>
								<td width="150" align=right><%# DataBinder.Eval(Container.DataItem, "CreateDate") %></td>
								<td  style="VISIBILITY: hidden">
									<asp:LinkButton ID="lbtDelFeedBack" Runat="server" CommandName="Delete">删除</asp:LinkButton>
								</td>
								<td runat =server id="DelCode" style="VISIBILITY: hidden"><%# DataBinder.Eval(Container.DataItem, "FeedBackCode") %></td>
							</tr>
							
						</ItemTemplate>
					</asp:repeater>
					
				</table>
			</div>
		</td>		
	</tr>
	
</table>

