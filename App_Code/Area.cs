using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace rapidInfoModel
{
    public partial class Area
    {

        public Area()
        {
        }

        public static List<Area> GetData()
        {
            using (rapidInfoEntities context = new rapidInfoEntities())
            {
                return context.Areas.ToList();
            }
        }

        public Area Save()
        {
            try
            {
                using (rapidInfoEntities context = new rapidInfoEntities())
                {
                    var temp = context.Areas.FirstOrDefault(m => m.Id == Id);
                    Boolean IsNew = temp == null ? true : false;

                    if (Id == 0)
                    {
                        Id = 1;
                        try { Id = context.Areas.Max(m => m.Id) + 1; }
                        catch { }
                        IsNew = true;
                    }

                    LUDate = DateTime.Now;
                    if (IsNew)
                        context.AddToAreas(this);
                    else
                    {
                        if (temp != null)
                            context.CreateObjectSet<Area>().Detach(temp);

                        context.CreateObjectSet<Area>().Attach(this);
                        context.ObjectStateManager.ChangeObjectState(this, EntityState.Modified);
                    }
                    context.SaveChanges();
                    //Message = "Successfully Updated";
                }
            }
            catch (Exception ex) { //Message += ex.Message; 
            }
            return this;
        }
    }
}