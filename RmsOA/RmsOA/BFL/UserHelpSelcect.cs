namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using RmsOA.MODEL;

    public class UserHelpSelcect
    {
        private readonly string rootCode = "0";
        private readonly string rootName = "分组类别";

        public static string GetGroupName(string code)
        {
            UserHelpGroupBFL pbfl = new UserHelpGroupBFL();
            return pbfl.GetUserHelpGroup(int.Parse(code)).GroupName;
        }

        public XmlDocument GetTreeStruct(string userCode)
        {
            XmlDocument document = new XmlDocument();
            XmlElement newChild = document.CreateElement("Root");
            newChild.SetAttribute("Name", this.rootName);
            newChild.SetAttribute("Value", this.rootCode);
            UserHelpGroupBFL pbfl = new UserHelpGroupBFL();
            UserHelpGroupQueryModel queryModel = new UserHelpGroupQueryModel();
            queryModel.UserCodeEqual = userCode;
            List<UserHelpGroupModel> userHelpGroupList = pbfl.GetUserHelpGroupList(queryModel);
            if (userHelpGroupList != null)
            {
                foreach (UserHelpGroupModel model2 in userHelpGroupList)
                {
                    XmlElement element2 = document.CreateElement("Child");
                    element2.SetAttribute("Name", model2.GroupName);
                    element2.SetAttribute("Value", model2.Code.ToString());
                    newChild.AppendChild(element2);
                }
            }
            document.AppendChild(newChild);
            return document;
        }
    }
}

