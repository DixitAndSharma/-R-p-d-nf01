using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
//using eCabModel;
using rapidInfoModel;
using System.Linq;
using System.Timers;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global
{
   

    public static Boolean UseCache = false;
   
    public Global()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<ComparePairs> comparePairs = new List<ComparePairs>();
    public static Dictionary<int, Area> AreaList = new Dictionary<int, Area>();

    public class ComparePairs
    {
        public int id1;
        public int id2;
    }

    public static void LoadGlobalData()
    {
        using (rapidInfoEntities context = new rapidInfoEntities())
        {
            AreaList.Clear();
            foreach (Area A in context.Areas)
            {
                //A.searchName


                AreaList.Add(A.Id, A);



            }
        }
    }


    public static string GetRootPathVirtual
    {
        get
        {
            string host = (HttpContext.Current.Request.Url.IsDefaultPort) ?
            HttpContext.Current.Request.Url.Host :
            HttpContext.Current.Request.Url.Authority;
            host = String.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, host);
            if (HttpContext.Current.Request.ApplicationPath == "/")
                return host;
            else
                return host + HttpContext.Current.Request.ApplicationPath;
        }
    }

    //public static DateTime? ServiceProvideRefreshTime(int ServiceProviderID, DateTime? LastRefreshTime = null)
    //{
    //    if (!ServiceProverderRefreshList.ContainsKey(ServiceProviderID))
    //        ServiceProverderRefreshList.Add(ServiceProviderID, LastRefreshTime);

    //    if (LastRefreshTime != null)
    //        ServiceProverderRefreshList[ServiceProviderID] = LastRefreshTime;

    //    return ServiceProverderRefreshList[ServiceProviderID];

    //}

    public static string ConnectionStringrapidInfoEntity
    {
        get
        {
            return @"metadata=res://*/App_Code.rapidInfo.csdl|res://*/App_Code.rapidInfo.ssdl|res://*/App_Code.rapidInfo.msl;provider=System.Data.SqlServerCe.4.0;provider connection string=""Data Source=|DataDirectory|\rapidInfo.sdf;""";
        }
    }

    public static string ConnectionStringrapidInfo
    {
        get
        {
            return @"Data Source=|DataDirectory|\rapidInfo.sdf";
        }
    }

    public static void ResetCompanySetting()
    {
        HttpContext.Current.Session["CompanySetting"] = null;
    }

    public static Boolean LogInDone
    {
        get
        {
            if (HttpContext.Current.Session["LOGINDONE"] == null)
                return false;
            else
                return HttpContext.Current.Session["LOGINDONE"].ToString() == "1" ? true : false;
        }

        set
        {
            HttpContext.Current.Session["LOGINDONE"] = value ? "1" : "0";
        }
    }

    public static Boolean IsAdmin
    {
        get
        {
            if (HttpContext.Current.Session["IsAdmin"] == null)
                return true;
            else
                return HttpContext.Current.Session["IsAdmin"].ToString() == "1" ? true : false;
        }

        set
        {
            HttpContext.Current.Session["IsAdmin"] = value ? "1" : "0";
        }
    }

    public static Boolean IsAdminLogin
    {
        get
        {
            if (HttpContext.Current.Session["ISADMINLOGIN"] == null)
                return false;
            else
                return HttpContext.Current.Session["ISADMINLOGIN"].ToString() == "1" ? true : false;
        }

        set
        {
            HttpContext.Current.Session["ISADMINLOGIN"] = value ? "1" : "0";
        }
    }

    public static int CompanyID
    {
        get
        {
            try
            {
                if (HttpContext.Current.Session["CompanyID"] == null)
                    return 1;
                else
                    return Convert.ToInt32(HttpContext.Current.Session["CompanyID"].ToString());
            }
            catch { }
            return 0;
        }

        set
        {
            HttpContext.Current.Session["CompanyID"] = value;
        }
    }


    //public static Role _Role
    //{
    //    get
    //    {
    //        try
    //        {
    //            if (HttpContext.Current.Session["_Role"] == null)
    //                return Role.None;
    //            else
    //                return (Role)HttpContext.Current.Session["_Role"];
    //        }
    //        catch { }
    //        return 0;
    //    }

    //    set
    //    {
    //        HttpContext.Current.Session["_Role"] = value;
    //    }
    //}

    public static int ServiceProviderID
    {
        get
        {
            try
            {
                if (HttpContext.Current.Session["ServiceProviderID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session["ServiceProviderID"].ToString());
            }
            catch { }
            return 0;
        }

        set
        {
            HttpContext.Current.Session["ServiceProviderID"] = value;
        }
    }

    private static Hashtable s_UserID_SessionID_Pair_Collection = new Hashtable();

    public static string CurrentUserID
    {
        get
        {
            if (HttpContext.Current.Session == null ||
                HttpContext.Current.Session["CurrentUserID"] == null)
            {
                return "";
            }
            return (string)HttpContext.Current.Session["CurrentUserID"];
        }
        set
        {
            HttpContext.Current.Session["CurrentUserID"] = value;
        }
    }

    public static void Login(string userid)
    {
        CurrentUserID = userid;
        s_UserID_SessionID_Pair_Collection[userid] = HttpContext.Current.Session.SessionID;
    }

    public static bool IsDoubleLogin()
    {
        if (CurrentUserID == "")
            return false;

        bool isDoubleLogin =
           string.Compare((string)
           s_UserID_SessionID_Pair_Collection[CurrentUserID]
        , HttpContext.Current.Session.SessionID
        ) != 0;
        if (isDoubleLogin)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
        return isDoubleLogin;
    }
}


namespace rapidInfoModel
{
    public enum Mode
    {
        None = 0,
        Main = 1,
        Admin = 2,
        Edit = 3
    }

    public enum ModelType
    {
        None = 0,
        newlaunch = 1,
        upcoming = 2
    }

    public enum Product
    {
        car,
        mobile,
        cab
    }

    public enum FeatureUnit
    {
        None, //0
        cc, //1
        kmpl, //2
        Person, //3
        mm, //4
        Gears, //5
        Doors, //6
        litres, //7
        Rows, //8
        Kg, //9
        Inch, //10
        Feet, //11
        Text, //12
        Sqcm, //13
        Number, //14
        Less, //15
        Less_Than, //16
        mmxmm, //17
        Kg_Per_Box, //18
        cm, //19
        Sqmtr_Per_Box, //20
        Psnt, //21
        Binary, //22
        No_Per_box, //23
        kg_Per_cm2, //24
        Scale, //25
        Group, //26
        MHz, //27
        MP, //28
        grams, //29
        mAh, //30
        minutes, //31
        hours, //32
        years, //33
        Rs //34
    }

    public enum FeatureType
    {
        Text, //0
        Integer, //1
        Decimal, //2
        Date, //3
        Multiselect, //4
        Single_Select, //5
        Yes_No, //6
        Binary //7
    }

    public enum AvailabilityType
    {
        NotAvailable = 0,
        Available = 1,
        //Booked = 2
    }

    public enum FilterType
    {
        None = 0,
        Compare = 1,
        Range = 2,
    }

    public enum ResourceType
    {
        None = 0,
        Car = 1,
        Driver = 2
    }

    public enum CarCategory
    {
        None = 0,
        Standard = 1,
        Van = 2
    }

    public enum TravelType
    {
        None = 0,
        Pick = 1,
        Drop = 2
    }

    public enum Role
    {
        None = 0,
        Admin = 1,
        Operator = 2,
        Customer = 3,
        Driver = 4
    }

    public enum LocationType
    {
        None = 0,
        Home = 1,
        Office = 2,
        Airport = 3,
        Society = 4,
        RailwayStation = 5,
    }

    public enum CarFeatures
    {
        None = 0,
        Seating_Capacity = 1,
        Mileage = 2,
        Displacement = 3,
        Fuel = 4,
        Price = 5,
        Air_Conditioner = 6,
        Length = 7,
        Width = 8,
        Height = 9,
        Max_Power = 10,
        Max_Torque = 11,
        Transmission = 12,
        Gears = 13,
        Drivetrain = 14,
        Central_Locking = 15,
        Anti_Lock_Braking_System = 16,
        Airbags = 17,
        Seat_Upholstery = 18,
        Alloy_Wheels = 19,
        Doors = 20,
        Seating_Rows = 21,
        Fuel_Tank_Capacity = 22,
        Cylinders = 23,
        Minimum_Turning_Radius = 24,
        Front_Tyres = 25,
        Rear_Tyres = 26,
        Heater = 27,
        Steering_Adjustment = 28,
        Keyless_Start = 29,
        CD_Changer = 30,
        Make = 31,
        Body_Style = 32,
        Max
    }

}

public enum States
{
    //1
    Andaman_Nicobar_Islands = 1,
    Andhra_Pradesh,
    Arunachal_Pradesh,
    Assam,
    Bihar,
    Chandigarh,
    Chhattisgarh,
    Dadra_Nagar_Haveli,
    Daman_Diu,
    Delhi,
    Goa,
    Gujarat,
    Haryana,
    Himachal_Pradesh,
    Jammu_Kashmir,
    Jharkhand,
    Karnataka,
    Kerala,
    Lakshdweep,
    Madhya_Pradesh,
    Maharashtra,
    Manipur,
    Meghalaya,
    Mizoram,
    Nagaland,
    Orissa,
    Pondicherry,
    Punjab,
    Rajasthan,
    Sikkim,
    Tamil_Nadu,
    Tripura,
    Uttar_Pradesh,
    Uttarakhand,
    West_Bengal
}

public static class StatesName
{
    static Dictionary<States, string> _StatesName = null;

    public static Dictionary<States, string> Name()
    {
        if (_StatesName == null)
        {
            _StatesName = new Dictionary<States, string>();
            _StatesName.Add(States.Andaman_Nicobar_Islands, "Andaman Nicobar Islands");
            _StatesName.Add(States.Andhra_Pradesh, "Andhra Pradesh");
            _StatesName.Add(States.Arunachal_Pradesh, "Arunachal Pradesh ");
            _StatesName.Add(States.Assam, "Assam");
            _StatesName.Add(States.Bihar, "Bihar");
            _StatesName.Add(States.Chandigarh, "Chandigarh");
            _StatesName.Add(States.Chhattisgarh, "Chhattisgarh");
            _StatesName.Add(States.Dadra_Nagar_Haveli, "Dadra Nagar Haveli");
            _StatesName.Add(States.Daman_Diu, "Daman Diu");
            _StatesName.Add(States.Delhi, "Delhi");
            _StatesName.Add(States.Goa, "Goa");
            _StatesName.Add(States.Gujarat, "Gujarat");
            _StatesName.Add(States.Haryana, "Haryana");
            _StatesName.Add(States.Himachal_Pradesh, "Himachal Pradesh");
            _StatesName.Add(States.Jammu_Kashmir, "Jammu Kashmir");
            _StatesName.Add(States.Jharkhand, "Jharkhand");
            _StatesName.Add(States.Karnataka, "Karnataka");
            _StatesName.Add(States.Kerala, "Kerala");
            _StatesName.Add(States.Lakshdweep, "Lakshdweep");
            _StatesName.Add(States.Madhya_Pradesh, "Madhya Pradesh");
            _StatesName.Add(States.Maharashtra, "Maharashtra");
            _StatesName.Add(States.Manipur, "Manipur");
            _StatesName.Add(States.Meghalaya, "Meghalaya");
            _StatesName.Add(States.Mizoram, "Mizoram");
            _StatesName.Add(States.Nagaland, "Nagaland");
            _StatesName.Add(States.Orissa, "Orissa");
            _StatesName.Add(States.Pondicherry, "Pondicherry");
            _StatesName.Add(States.Punjab, "Punjab");
            _StatesName.Add(States.Rajasthan, "Rajasthan");
            _StatesName.Add(States.Sikkim, "Sikkim");
            _StatesName.Add(States.Tamil_Nadu, "Tamil Nadu");
            _StatesName.Add(States.Tripura, "Tripura");
            _StatesName.Add(States.Uttar_Pradesh, "Uttar Pradesh");
            _StatesName.Add(States.Uttarakhand, "Uttarakhand");
            _StatesName.Add(States.West_Bengal, "West Bengal");
        }

        return _StatesName;
    }

    public static string Name(States _States)
    {
        Name();
        string _Name;
        if (_StatesName.TryGetValue(_States, out _Name))
            return _Name;

        return "";
    }
}