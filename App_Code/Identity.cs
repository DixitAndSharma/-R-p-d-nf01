using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace rapidInfoModel
{
    public partial class Identity
    {

        public Identity()
        {
        }

        public static List<Identity> GetData()
        {
            using (rapidInfoEntities context = new rapidInfoEntities())
            {
                return context.Identities.ToList();
            }
        }

        public static Identity GetData(int id)
        {
            using (rapidInfoEntities context = new rapidInfoEntities())
            {
                return context.Identities.FirstOrDefault(m => m.Id == id);
            }
        }

        public Identity Save()
        {
            try
            {
                using (rapidInfoEntities context = new rapidInfoEntities())
                {
                    var temp = context.Identities.FirstOrDefault(m => m.Id == Id);
                    Boolean IsNew = temp == null ? true : false;

                    if (Id == 0)
                    {
                        Id = 1;
                        try { Id = context.Identities.Max(m => m.Id) + 1; }
                        catch { }
                        IsNew = true;
                    }

                    LUDate = DateTime.Now;
                    if (IsNew)
                        context.AddToIdentities(this);
                    else
                    {
                        if (temp != null)
                            context.CreateObjectSet<Identity>().Detach(temp);

                        context.CreateObjectSet<Identity>().Attach(this);
                        context.ObjectStateManager.ChangeObjectState(this, EntityState.Modified);
                    }
                    context.SaveChanges();
                    //Message = "Successfully Updated";
                }
            }
            catch (Exception ex)
            { //Message += ex.Message; 
            }
            return this;
        }
    }
}