namespace RmsPM.BLL
{
    using System;
    using System.Collections;

    public class BiddingGradeMainDefine
    {
        public static ArrayList GetSupplierDeparmentCode(string Typecode)
        {
            ArrayList list = new ArrayList();
            string text2 = Typecode;
            if (text2 != null)
            {
                int num;
                string text;
                if (text2 != "100001")
                {
                    if (text2 != "100002")
                    {
                        return list;
                    }
                }
                else
                {
                    for (num = 1; num <= 7; num++)
                    {
                        text = "10000" + num.ToString();
                        list.Add(text);
                    }
                    return list;
                }
                for (num = 0x186a8; num <= 0x186ae; num++)
                {
                    text = num.ToString();
                    list.Add(text);
                }
            }
            return list;
        }

        public static string GetSupplierGradeTypeName(string Typecode)
        {
            switch (Typecode)
            {
                case "100001":
                    return "承包商评分";

                case "100002":
                    return "供应商评分";
            }
            return "";
        }
    }
}

