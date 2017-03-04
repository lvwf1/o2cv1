using System;
using System.Linq;
using O2.DataMart.Models.EFModel;
using Query = O2.Common.Utility.Query;
using System.Collections;
using System.Web;
using System.Collections.Generic;

namespace O2.DataMart.Utility
{
    public static class QueryExtensions
    {
        public static string LoadQuery(this Query query, Guid userId, string tag)
        {
            using (var db = new O2DataMart())
            {
                return db.Queries.Where(q => q.UserId == userId && q.Tag == tag).Select(q => q.Text).FirstOrDefault();
            }
        }

        public static List<string> LoadQueries(Guid userId)
        {
            using (var db = new O2DataMart())
            {
                return db.Queries.Where(q => q.UserId == userId).Select(q => q.Tag).ToList();
            }
        }


        public static void SaveQuery(this Query query, Guid userId, string tag, string text)
        {
            using (var db = new O2DataMart())
            {
                var qry = db.Queries.FirstOrDefault(q => q.Tag == tag && q.UserId == userId);

                if (qry == null)
                {
                    db.Queries.Add(new Models.EFModel.Query { QueryId = Guid.NewGuid(), UserId = userId, Tag = tag, Text = text });
                }
                else
                {
                    qry.Text = text;
                }

                db.SaveChanges();
            }
        }

        public static void DeleteQuery(Guid userId, string tag)
        {
            using (var db = new O2DataMart())
            {
                var qry = db.Queries.FirstOrDefault(q => q.Tag == tag && q.UserId == userId);

                if (qry == null)
                {
                    
                }
                else
                {
                    db.Queries.Remove(qry);
                }

                db.SaveChanges();
            }
        }
    }
}
