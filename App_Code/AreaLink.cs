using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace rapidInfoModel
{
    public partial class AreaLink
    {

        public AreaLink()
        {
        }

        public static List<AreaLink> GetData(int identityId)
        {
            using (rapidInfoEntities context = new rapidInfoEntities())
            {
                return context.AreaLinks.Where(m => m.IdentityId == identityId).ToList();
            }
        }

        public static List<AreaLink> GetDataByArea(int areaId)
        {
            using (rapidInfoEntities context = new rapidInfoEntities())
            {
                return context.AreaLinks.Where(m => m.AreaId == areaId).ToList();
            }
        }

        public AreaLink Save()
        {
            try
            {
                using (rapidInfoEntities context = new rapidInfoEntities())
                {
                    var temp = context.AreaLinks.FirstOrDefault(m => m.AreaId == AreaId && m.IdentityId == IdentityId);
                    context.AddToAreaLinks(this);

                    context.SaveChanges();
                    //Message = "Successfully Updated";
                }
            }
            catch (Exception ex)
            { //Message += ex.Message; 
            }
            return this;
        }

        public AreaLink Delete()
        {
            try
            {
                using (rapidInfoEntities context = new rapidInfoEntities())
                {
                    var temp = context.AreaLinks.FirstOrDefault(m => m.AreaId == AreaId && m.IdentityId == IdentityId);

                    context.AreaLinks.Attach(this);
                    context.AreaLinks.DeleteObject(this);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            return this;
        }
    }
}