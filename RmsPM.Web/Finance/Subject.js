function GetSubjectName(SubjectCode, SubjectSetCode)
{
	if (SubjectCode == "") return "";

	var SubjectName = "";

	var items = GetXMLResult("../Finance/GetSubjectData.aspx?SubjectCode=" + escape(SubjectCode) + "&SubjectSetCode=" + SubjectSetCode);	

	SubjectName = GetXMLTagData(items, "SubjectName");
	
	return SubjectName;
}

function GetSubjectFullName(SubjectCode, SubjectSetCode)
{
	if (SubjectCode == "") return "";

	var SubjectName = "";
	
	var items = GetXMLResult("../Finance/GetSubjectData.aspx?SubjectCode=" + escape(SubjectCode) + "&SubjectSetCode=" + SubjectSetCode);	

	SubjectName = GetXMLTagData(items, "SubjectFullName");
	
	return SubjectName;
}

function GetSubjectNameHint(SubjectCode, SubjectSetCode)
{
	var arr = new Array();
	var SubjectName = "";
	var Hint = "";
	
	if (SubjectCode != "")
	{
		var items = GetXMLResult("../Finance/GetSubjectData.aspx?SubjectCode=" + escape(SubjectCode) + "&SubjectSetCode=" + SubjectSetCode);	

		SubjectName = GetXMLTagData(items, "SubjectName");
		Hint = GetXMLTagData(items, "Hint");
	}

	arr.push(SubjectName);
	arr.push(Hint);
	return arr;
}

function GetSubjectFullNameHint(SubjectCode, SubjectSetCode)
{
	var arr = new Array();
	var SubjectName = "";
	var Hint = "";
	
	if (SubjectCode != "")
	{
		var items = GetXMLResult("../Finance/GetSubjectData.aspx?SubjectCode=" + escape(SubjectCode) + "&SubjectSetCode=" + SubjectSetCode);	

		SubjectName = GetXMLTagData(items, "SubjectFullName");
		Hint = GetXMLTagData(items, "Hint");
	}

	arr.push(SubjectName);
	arr.push(Hint);
	return arr;
}

