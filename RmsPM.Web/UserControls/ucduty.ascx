<%@ Control Language="c#" Inherits="RmsPM.Web.UserControls.Migrated_UCDuty" CodeFile="UCDuty.ascx.cs" %>
<SCRIPT language="javascript" src="<%=ctrlPath%>../images/XmlCom.js"></SCRIPT>
<select id='SelectDuty' runat="server">
</select>
<script>

	<%
		if(!this.IsPostBack)
			Response.Write("document.cookie='';");
	%>
	var selobj = document.getElementById("<%=this.SelectDuty.ClientID%>");
	function clearddl()
	{
		for(j=selobj.length;j>=0;j--)
		{
			selobj.options.remove(j);
		}
	}	
	var tTag = '<%=this.SelectDuty.ClientID%>';
	function check()
	{	
		var openones=''
		for (i=0 ; i < selobj.length ; i++)
		{
			openones+=selobj.options(i).text+' '+selobj.options(i).value+'@val';
		}
		if(selobj.options.length>0) openones=tTag+openones+selobj.options(selobj.selectedIndex).value+'@sel'+tTag;	
		document.cookie=openones;	
		//alert(document.cookie);
	}	
	
	// 当用户变化时
	function <%=hClientID%>UserCodeOnChange(userCode)
	{
		var oOptionText  = "";
		var oOptionValue = "";
		
		clearddl();
		
		//alert(userCode);
		// 根据用户取得部门
		var items = GetXMLResult("<%=ctrlPath%>GetUserUnit.aspx?Value=" + userCode);
		//alert(items);
		var values = GetXMLTagData(items, "values");
		//alert(values);
		var arVal = values.split(';');
		for(i=0;i<arVal.length;i++)
		{		
			if(arVal[i]=="") continue;
			arValue = arVal[i].split(':');
			var oOption = document.createElement("OPTION");
			selobj.options.add(oOption);
			oOption.innerText = arValue[1];
			oOption.value = arValue[0];
		}
		check();		
	}
	// 刚载入页面也check
	//check();

	// 通过cookie存储当前值
	function get_cookie() 
	{
		var returnvalue = "";
		if (document.cookie.length > 0) 
		{
			offset = document.cookie.indexOf(tTag);
			if (offset != -1) 
			{ 
				end = document.cookie.lastIndexOf(tTag);				
				returnvalue=unescape(document.cookie.substring(offset+tTag.length, end))
			}
		}
		//alert("returnvalue--"+returnvalue);
		return returnvalue;		
	}	
	
	if (get_cookie() != '')
	{
		clearddl();
		var reValue = get_cookie(window.location.pathname);//alert(reValue);
		var selValue = reValue.substring(reValue.lastIndexOf('@val')+4,reValue.indexOf('@sel'));//alert(selValue);
		var arVal=reValue.substring(reValue.lastIndexOf('@val')+4,0).split("@val");//alert(reValue.substring(reValue.lastIndexOf('@val')+4,0));
		
		for (i=0 ; i < arVal.length ; i++)
		{
			if(arVal[i]=="") continue;
			arValue = arVal[i].split(' ');//alert(arVal[i]);
			var oOption = document.createElement("OPTION");
			selobj.options.add(oOption);
			oOption.innerText = arValue[0];
			oOption.value = arValue[1];
			if(arValue[1]==selValue)
				oOption.selected = true;
		}
	}

	if (document.all)
		document.body.onunload=check
  
</script>
