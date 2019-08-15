namespace RmsOA.MODEL
{
    using System;
    using System.Data;

    public class OAPersonQueryModel : QueryBaseModel
    {
        public string addressEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" address like @address ");
                    base.InsertParameter("@address", SqlDbType.VarChar, 100, "%" + value + "%");
                }
            }
        }

        public string avoirdupoisEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" avoirdupois=@avoirdupois ");
                    base.InsertParameter("@avoirdupois", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime birthdayEqualDY
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" birthday<=@birthday ");
                    base.InsertParameter("@birthday", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime birthdayEqualXY
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" birthday<=@endbirthday ");
                    base.InsertParameter("@endbirthday", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string bloodgroupEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" bloodgroup=@bloodgroup ");
                    base.InsertParameter("@bloodgroup", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime cjgz_dateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" cjgz_date=@cjgz_date ");
                    base.InsertParameter("@cjgz_date", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string cnameEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" cname=@cname ");
                    base.InsertParameter("@cname", SqlDbType.VarChar, 50, "%" + value + "%");
                }
            }
        }

        public int CodeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" Code=@Code ");
                    base.InsertParameter("@Code", SqlDbType.Int, 4, value);
                }
            }
        }

        public string degreeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" degree=@degree ");
                    base.InsertParameter("@degree", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string dutyEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" duty=@duty ");
                    base.InsertParameter("@duty", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string educationEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" education=@education ");
                    base.InsertParameter("@education", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string emailEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" email=@email ");
                    base.InsertParameter("@email", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string fkdzEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" fkdz=@fkdz ");
                    base.InsertParameter("@fkdz", SqlDbType.VarChar, 100, value);
                }
            }
        }

        public string folkEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" folk=@folk ");
                    base.InsertParameter("@folk", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string homeplaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" homeplace=@homeplace ");
                    base.InsertParameter("@homeplace", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime htc_dateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" htc_date=@htc_date ");
                    base.InsertParameter("@htc_date", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string IDcardEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" IDcard=@IDcard ");
                    base.InsertParameter("@IDcard", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string ismarryEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" ismarry=@ismarry ");
                    base.InsertParameter("@ismarry", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string jobnoEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" jobno=@jobno ");
                    base.InsertParameter("@jobno", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string LeaderEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Leader=@Leader ");
                    base.InsertParameter("@Leader", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string mobileEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" mobile=@mobile ");
                    base.InsertParameter("@mobile", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string nativeplaceEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" nativeplace=@nativeplace ");
                    base.InsertParameter("@nativeplace", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string phoneEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" phone=@phone ");
                    base.InsertParameter("@phone", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public byte photoimagesEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" photoimages=@photoimages ");
                    base.InsertParameter("@photoimages", SqlDbType.Image, 0x10, value);
                }
            }
        }

        public int photosizeEqual
        {
            set
            {
                if (value != 0)
                {
                    base.QueryConditionStrAdd(" photosize=@photosize ");
                    base.InsertParameter("@photosize", SqlDbType.Int, 4, value);
                }
            }
        }

        public string phototypeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" phototype=@phototype ");
                    base.InsertParameter("@phototype", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string polityEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" polity=@polity ");
                    base.InsertParameter("@polity", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string postcodeEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" postcode=@postcode ");
                    base.InsertParameter("@postcode", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public DateTime rdt_dateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" rdt_date=@rdt_date ");
                    base.InsertParameter("@rdt_date", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public DateTime rgs_dateEqual
        {
            set
            {
                if (value != DateTime.MinValue)
                {
                    base.QueryConditionStrAdd(" rgs_date=@rgs_date ");
                    base.InsertParameter("@rgs_date", SqlDbType.DateTime, 8, value);
                }
            }
        }

        public string sexEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" sex=@sex ");
                    base.InsertParameter("@sex", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string statureEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" stature=@stature ");
                    base.InsertParameter("@stature", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string StatusEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" Status=@Status ");
                    base.InsertParameter("@Status", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string workNoEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" workNo=@workNo ");
                    base.InsertParameter("@workNo", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string yardEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" yard=@yard ");
                    base.InsertParameter("@yard", SqlDbType.VarChar, 50, value);
                }
            }
        }

        public string zcEqual
        {
            set
            {
                if (value != null)
                {
                    base.QueryConditionStrAdd(" zc=@zc ");
                    base.InsertParameter("@zc", SqlDbType.VarChar, 50, value);
                }
            }
        }
    }
}

