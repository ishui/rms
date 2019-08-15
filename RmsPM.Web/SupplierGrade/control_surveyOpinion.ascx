<%@ Control Language="C#" AutoEventWireup="true" CodeFile="control_surveyOpinion.ascx.cs" Inherits="SupplierGrade_control_surveyOpinion" %>
<%@ Register TagPrefix="cc2" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>


<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form">
    <div id="OperableDiv" runat="server">
    <tr>
        
        <td class="form-item"  >
            状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态</td>
         <td align=center colspan=3><asp:Label ID="txtState" ForeColor=red runat="server" ></asp:Label></td>
         </tr>
        <tr>
        
            <td  class="form-item">工种名称</td>
            <td><uc1:inputsystemgroup id="inputSystemGroup" runat="server"></uc1:inputsystemgroup></td>
            <td  class="form-item">专员姓名</td>
            <td><input id="TxtPersonName" type="text" runat="server" class="input" /></td>   
        </tr>    
        
        <tr>
        
            <td  class="form-item">公司名称</td>
            <td><asp:Label ID="TxtCompanyName" runat="server" ></asp:Label></td>
            <td  class="form-item">调查日期</td>
            <td  noWrap>
			    <cc2:Calendar ID="Txtdate" runat="server" ReadOnly=true
                        CalendarMode="All" CalendarResource="../Images/CalendarResource/">
                    </cc2:Calendar>
				
			</td>
        </tr>    
        <tr>
            <td colspan=4>
            
             
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form">
                    <tr>
                        <td>序号
                        </td>
                        <td>重 点 调 查 内 容
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（01）
                        </td>
                        <td>企业类型为外国独资？中外合资？国内企业？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（02）
                        </td>
                        <td>公司（工厂）法定代表人为公司（工厂）主要经营者？还是人头户？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（03）
                        </td>
                        <td>公司（工厂）资本额大小？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（04）
                        </td>
                        <td>公司(工厂)办公室规模大小，办公室门面及内装修是否气派？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（05）
                        </td>
                        <td>办公室，位于办公楼内？还是商住楼内？还是住宅楼内？厂房地理位置是否便利？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（06）
                        </td>
                        <td>办公地点位于高档地区(市中心)？中档地区？低档地区？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（07）
                        </td>
                        <td>办公室内，员工多少？工厂生产线员工多少？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（08）
                        </td>
                        <td>公司（工厂）办公室为自己物业（价值多少）？还是租赁（租金多少）？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（09）
                        </td>
                        <td>工程实例多寡？有无知名工程案例？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（10）
                        </td>
                        <td>工厂是否取得ISO品质认证？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（11）
                        </td>
                        <td>其他
                        </td>
                        
                    </tr>
                </table>
            </td>
              			
        </tr>
        <tr>
            <td class="form-item" align="center">
                <br>调<br>查<br>说<br>明<br>
            </td>
            <td colspan=3>
                 <textarea id="TxtRemark" runat=server style="width:100%;" rows="12"></textarea>
            </td>
        </tr>
        
        <tr>
            <td class="form-item">
                 等级
            </td>
            <td>
              
                
               
                <select id="selectGrade" runat=server>
                    <option></option>
                </select>&nbsp;级
            </td>
            <td class="form-item">
                 建议评级
            </td>
            <td>
             
             <select id="selectAdviceGrade" runat=server>
                <option></option>
            </select>&nbsp;级
            </td>
          
        
        </tr>
        
        <tr>
            <td >
                 A级
            </td>
            <td>
                 公司评比优质厂商（配合度高，积极性强之厂商，可长期合作）
            </td>
            <td >
                C级
            </td>
            <td>
                资本额小，可承接我司小工程或零星工程之厂商。
            </td>
        
        </tr>
     				
			<tr>
            <td >
                B级
            </td>
            <td>
                 资本额大，可承接我司大工程厂商。（需有多个知名工程案例）
            </td>
            <td >
               D级
            </td>
            <td>
                 不录用。
            </td>
        
        </tr>								
											
											
											
											

   </div>
   <div id="EyeableDiv" runat="server">
   <tr>
   
    <td class="form-item" >
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态</td>
            <td colspan=3 align=center><asp:Label ID="lblstate" ForeColor=red runat="server" ></asp:Label></td>
   </tr>
         <tr>
        
            <td  class="form-item">工种名称</td>
            <td><span id="SpanlblTaskname" runat="server"></span></td>
            <td  class="form-item">专员姓名</td>
            <td><asp:Label ID="lblPersonName" runat="server" ></asp:Label></td>   
        </tr>    
        
        <tr>
        
            <td  class="form-item">公司名称</td>
            <td><asp:Label ID="lblCompanyname" runat="server" ></asp:Label></td>
            <td  class="form-item">调查日期</td>
            <td class="blackbordertdpaddingcontent">
			    <cc2:Calendar ID="lblDate" runat="server" ReadOnly=true
                        CalendarMode="All" CalendarResource="../Images/CalendarResource/">
                    </cc2:Calendar>
				
			</td>
        </tr>    
        <tr>
            <td colspan=4>
            
            
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form">
                    <tr>
                        <td>序号
                        </td>
                        <td>重 点 调 查 内 容
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（01）
                        </td>
                        <td>企业类型为外国独资？中外合资？国内企业？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（02）
                        </td>
                        <td>公司（工厂）法定代表人为公司（工厂）主要经营者？还是人头户？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（03）
                        </td>
                        <td>公司（工厂）资本额大小？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（04）
                        </td>
                        <td>公司(工厂)办公室规模大小，办公室门面及内装修是否气派？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（05）
                        </td>
                        <td>办公室，位于办公楼内？还是商住楼内？还是住宅楼内？厂房地理位置是否便利？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（06）
                        </td>
                        <td>办公地点位于高档地区(市中心)？中档地区？低档地区？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（07）
                        </td>
                        <td>办公室内，员工多少？工厂生产线员工多少？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（08）
                        </td>
                        <td>公司（工厂）办公室为自己物业（价值多少）？还是租赁（租金多少）？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（09）
                        </td>
                        <td>工程实例多寡？有无知名工程案例？
                        </td>
                        
                    </tr>
                    <tr>
                        <td>（10）
                        </td>
                        <td>工厂是否取得ISO品质认证？
                        </td>
                       
                    </tr>
                    <tr>
                        <td>（11）
                        </td>
                        <td>其他
                        </td>
                        
                    </tr>
                </table>
            </td>
              			
        </tr>
        <tr>
            <td class="form-item" align="center">
                <br>调<br>查<br>说<br>明<br>
            </td>
            <td colspan=3 runat=server id="lblRemark">
                 
            </td>
        </tr>
        
        <tr>
            <td class="form-item">
                 等级
            </td>
            <td>
               <asp:Label ID="lblGrade" runat="server" ></asp:Label>&nbsp;级
            </td>
            <td class="form-item">
                 建议评级
            </td>
            <td>
                  <asp:Label ID="lblAdviceGrade" runat="server" ></asp:Label>&nbsp;级
            </td>
        
        </tr>
        
        <tr>
            <td >
                 A级
            </td>
            <td>
                 公司评比优质厂商（配合度高，积极性强之厂商，可长期合作）
            </td>
            <td >
                C级
            </td>
            <td>
                资本额小，可承接我司小工程或零星工程之厂商。
            </td>
        
        </tr>
     				
			<tr>
            <td >
                B级
            </td>
            <td>
                 资本额大，可承接我司大工程厂商。（需有多个知名工程案例）
            </td>
            <td >
               D级
            </td>
            <td>
                 不录用。
            </td>
        
        </tr>		
   </div>
</table>

<script>
