<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBiddingWorkFlowLink.ascx.cs" Inherits="RmsPM.Web.BiddingManage.BiddingManage_ucBiddingWorkFlowLink" %>
<asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="list">
<FooterStyle CssClass="list-title"></FooterStyle>
		<HeaderStyle CssClass="list-title"></HeaderStyle>
    <Columns>
        
        <asp:TemplateField HeaderText="序号">
            <ItemTemplate>
             <%# Container.DataItemIndex+1%>
             
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="流程名称">
            <ItemTemplate>
                投标单位评审
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="编号">
            <ItemTemplate>
           <a href='##'
           onclick='gotoDirect(this.path,this.applicationCode); return false;'          
			path='<%# RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("投标单位评审")%>'
			applicationCode='<%# DataBinder.Eval(Container.DataItem, "biddingPrejudicationCode") %>'>
              <%# DataBinder.Eval(Container.DataItem, "Number")%>
            </a>
            </ItemTemplate>      
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="发件人">
            <ItemTemplate>
             <%# RmsPM.BLL.SystemRule.GetUserName(DataBinder.Eval(Container.DataItem,"UserCode").ToString())%>
             
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CreateDate" HeaderText="发起日期" />
    </Columns>
   

</asp:GridView>
<script>
	function gotoDirect ( path , applicationCode)
	{
		OpenFullWindow(  path + '?frameType=List&action=View&applicationCode=' + applicationCode ,'流程查看');
	}
		</script>
