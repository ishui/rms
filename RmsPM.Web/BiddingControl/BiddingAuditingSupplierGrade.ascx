<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BiddingAuditingSupplierGrade.ascx.cs" Inherits="BiddingControl_BiddingAuditingSupplierGrade" %>
<table width="100%" class="blackbordertable" border=0 cellspacing="0" cellpadding="0" >
        <div id="OperableDiv" runat="server">
            <tr>
              
                        <td class="blackbordertd">&nbsp;</td>
                        <td align="center" class="blackbordertd">评分指标</td>
                        <td align="center" class="blackbordertd">权重</td>
                        <div runat="server" id="DynamicColumnName"></div>
                      </tr>  
                        
                 <tr>
                <asp:Repeater ID="Repeater1" runat="server">
            
                    <ItemTemplate>                                                
                        <asp:Label ID="LabelCode" runat="server" Visible="False" Text='<%# (string)DataBinder.Eval(Container.DataItem, "BiddingConsiderDiathesisCode")%>'></asp:Label>
                        <asp:Label ID="LabelFlag" runat="server" Visible="False" Text='<%# ((string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "1":"0"%>'></asp:Label>
                        <asp:Label ID="LblColumnCount" runat="server" Visible="False" Text='<%# System.Convert.ToString(DataBinder.Eval(Container.DataItem, "ColumnCount"))%>'></asp:Label>
                        <td class="blackbordertd" ><%# DataBinder.Eval(Container.DataItem, "BiddingConsiderDiathesis") %> &nbsp;</td>
                        <%# "<td class=\"blackbordertd\">" + (string)DataBinder.Eval(Container.DataItem, "GradeGuideline") + "&nbsp;</td>"%>
                      <td  class="blackbordertd"><%# System.Convert.ToString(DataBinder.Eval(Container.DataItem, "Percentage"))%>%&nbsp;</td>
                        
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 1 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td class=\"blackbordertd\" NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=1&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point1" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=1)?(string)DataBinder.Eval(Container.DataItem, "Point1"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=1 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code1" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=1)?(string)DataBinder.Eval(Container.DataItem, "Code1"):""%>'></asp:Label>
                            <asp:Label ID="GradeMessageCode1" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=1)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode1"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 1 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                        
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 2 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td class=\"blackbordertd\" NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=1&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point2" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=2)?(string)DataBinder.Eval(Container.DataItem, "Point2"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=2 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code2" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=2)?(string)DataBinder.Eval(Container.DataItem, "Code2"):""%>'></asp:Label>
                            <asp:Label ID="GradeMessageCode2" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=2)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode2"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 2 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                        
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 3 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=3&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point3" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=3)?(string)DataBinder.Eval(Container.DataItem, "Point3"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=3 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code3" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=3)?(string)DataBinder.Eval(Container.DataItem, "Code3"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode3" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=3)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode3"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 3 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")?"&nbsp;</td>" : ""%>
                         
                          <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 4 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=4&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point4" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=4)?(string)DataBinder.Eval(Container.DataItem, "Point4"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=4 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code4" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=4)?(string)DataBinder.Eval(Container.DataItem, "Code4"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode4" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=4)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode4"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 4 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")?"&nbsp;</td>" : ""%>
                         
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 5 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=5&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point5" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=5)?(string)DataBinder.Eval(Container.DataItem, "Point5"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=5 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code5" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=5)?(string)DataBinder.Eval(Container.DataItem, "Code5"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode5" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=5)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode5"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 5 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")?"&nbsp;</td>" : ""%>
                        
                        
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 6 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=6&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point6" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=6)?(string)DataBinder.Eval(Container.DataItem, "Point6"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=6 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code6" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=6)?(string)DataBinder.Eval(Container.DataItem, "Code6"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode6" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=6)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode6"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 6 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")?"&nbsp;</td>" : ""%>
                         
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 7 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=7&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point7" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=7)?(string)DataBinder.Eval(Container.DataItem, "Point7"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=7 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code7" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=7)?(string)DataBinder.Eval(Container.DataItem, "Code7"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode7" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=7)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode7"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 7 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")?"&nbsp;</td>" : ""%>
                         
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 8 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=8&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point8" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=8)?(string)DataBinder.Eval(Container.DataItem, "Point8"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=8 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code8" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=8)?(string)DataBinder.Eval(Container.DataItem, "Code8"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode8" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=8)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode8"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 8 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")?"&nbsp;</td>" : ""%>
                         
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 9 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=9&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point9" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=9)?(string)DataBinder.Eval(Container.DataItem, "Point9"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=9 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code9" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=9)?(string)DataBinder.Eval(Container.DataItem, "Code9"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode9" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=9)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode9"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 9 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                         
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 10 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "<td  class=\"blackbordertd\"  NOWRAP>" : ""%>
                            <asp:TextBox Enabled='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=10&&(string)DataBinder.Eval(Container.DataItem, "issubtotal") != "1")%>' Width="35" ID="Point10" runat="server" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=10)?(string)DataBinder.Eval(Container.DataItem, "Point10"):""%>'
                                Visible='<%# ( System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=10 &&(string)DataBinder.Eval(Container.DataItem, "freeflag") == "1")%>'></asp:TextBox>
                            <asp:Label ID="Code10" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=10)?(string)DataBinder.Eval(Container.DataItem, "Code10"):""%>'></asp:Label>
                             <asp:Label ID="GradeMessageCode10" runat="server" Visible="False" Text='<%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount"))>=10)?(string)DataBinder.Eval(Container.DataItem, "GradeMessageCode10"):""%>'></asp:Label>
                         <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 10 && (string)DataBinder.Eval(Container.DataItem, "freeflag") == "1") ? "&nbsp;</td>" : ""%>
                        
                        
                        </tr><tr>
                    </ItemTemplate>
                </asp:Repeater>
             </tr>
        </div>
                                            
        <div id="EyeableDiv" runat="server">
            <tr>     
            
                        <td class="blackbordertd">&nbsp;</td>
                        <td align="center" class="blackbordertd">评分指标</td>
                        <td align="center" class="blackbordertd">权重</td>
                         <div runat="server" id="DynamicColumnName1"></div>
                </tr>
                    <tr>                              
                <asp:Repeater ID="Repeater2" runat="server">
      
                    <ItemTemplate>
                        <td  class="blackbordertd" >
                            <%# (string)DataBinder.Eval(Container.DataItem, "BiddingConsiderDiathesis")%>
                            &nbsp;</td>
                        <td class="blackbordertd" ><%#(string)DataBinder.Eval(Container.DataItem, "GradeGuideline") %>&nbsp;</td>
                        <td  class="blackbordertd" ><%# System.Convert.ToString(DataBinder.Eval(Container.DataItem, "Percentage"))%>%&nbsp;</td>
                       <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 1) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point1") + "&nbsp;</td>" : ""%>
                        <%#  (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 2) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point2") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 3) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point3") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 4) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point4") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 5) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point5") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 6) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point6") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 7) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point7") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 8) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point8") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 9) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point9") + "&nbsp;</td>" : ""%>
                        <%# (System.Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ColumnCount")) >= 10) ? "<td  class=\"blackbordertd\" >" + (string)DataBinder.Eval(Container.DataItem, "Point10") + "&nbsp;</td>" : ""%>
                        </tr><tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </div>
</table>
    