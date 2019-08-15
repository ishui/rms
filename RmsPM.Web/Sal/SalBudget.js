function NodeCollapse(PBSTypeCode, Deep, FieldName, IsAct)
{
	var img = document.all("myimg" + FieldName + "_" + IsAct + "_" + PBSTypeCode);
	if (!img) return;
	if (img.exp == "0") return;
	
	img.exp = 0;
	img.src = "../Images/Plus.gif";
	
	var ChildDeep = parseInt(Deep) + 1;
	
	var table = document.all("tableList");
	var l = table.rows.length;
	for(var i=2;i<l;i++)
	{
		var tr = table.rows[i];
		
		if ((tr.Deep == ChildDeep) && (tr.ParentCode == PBSTypeCode) && (tr.FieldName == FieldName) && (tr.IsAct == IsAct))
		{
			tr.style.display = "none";
		}
	}
}

function NodeExpand(PBSTypeCode, Deep, FieldName, IsAct)
{
	var img = document.all("myimg" + FieldName + "_" + IsAct + "_" + PBSTypeCode);
	if (!img) return;
	if (img.exp == "1") return;
	
	img.exp = 1;
	img.src = "../Images/Minus.gif";
	
	var ChildDeep = parseInt(Deep) + 1;
	
	var table = document.all("tableList");
	var l = table.rows.length;
	for(var i=2;i<l;i++)
	{
		var tr = table.rows[i];
		
		if ((tr.Deep == ChildDeep) && (tr.ParentCode == PBSTypeCode) && (tr.FieldName == FieldName) && (tr.IsAct == IsAct))
		{
			tr.style.display = "";
		}
	}
}

function TreeExpand(sender)
{
	var PBSTypeCode = sender.PBSTypeCode;
	var Deep = sender.Deep;
	var FieldName = sender.FieldName;
	var IsAct = sender.IsAct;
	var exp = sender.exp;
	
	if (exp == "0") //现要展开
	{
		sender.exp = 1;
		sender.src = "../Images/Minus.gif";
	}
	else //现要折叠
	{
		sender.exp = 0;
		sender.src = "../Images/Plus.gif";
	}
	
	var ChildDeep = parseInt(Deep) + 1;
	
	var table = document.all("tableList");
	var l = table.rows.length;
	for(var i=2;i<l;i++)
	{
		var tr = table.rows[i];
		
		if (exp == "0") //现要展开
		{
			if ((tr.Deep == ChildDeep) && (tr.ParentCode == PBSTypeCode) && (tr.FieldName == FieldName) && (tr.IsAct == IsAct))
			{
				tr.style.display = "";
			}
		}
		else //现要折叠
		{
			if ((tr.Deep == ChildDeep) && (tr.ParentCode == PBSTypeCode) && (tr.FieldName == FieldName) && (tr.IsAct == IsAct))
			{
				NodeCollapse(tr.PBSTypeCode, tr.Deep, tr.FieldName, tr.IsAct);
				tr.style.display = "none";
			}
		}

		/*
		if ((tr.Deep == ChildDeep) && (tr.ParentCode == PBSTypeCode))
		{
			if (exp == "0") //现要展开
			{
				tr.style.display = "";
			}
			else //现要折叠
			{
				tr.style.display = "none";
			}
		}
		*/
	}
}

function CollapseAllRoot()
{
	//计划
	CollapseRoot("myimgHouseCount_0_");
	CollapseRoot("myimgHouseArea_0_");
	CollapseRoot("myimgPrice_0_");
	CollapseRoot("myimgMoney_0_");
	CollapseRoot("myimgRcvMoney_0_");

	//实际
	CollapseRoot("myimgHouseCount_1_");
	CollapseRoot("myimgHouseArea_1_");
	CollapseRoot("myimgPrice_1_");
	CollapseRoot("myimgMoney_1_");
	CollapseRoot("myimgRcvMoney_1_");
}

function CollapseRoot(img_name)
{
	var img = document.all(img_name);
	if ((img) && (img.exp == "1"))
	{
		img.click();
	}
}

