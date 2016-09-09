using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public sealed class DataProvider
    {

        public static DataTable Execute(string proc, List<ModelParam> parameters)
        {

            List<Dictionary<string, object>> expandolist = new List<Dictionary<string, object>>();

           

            DataModelContainer1 context = new DataModelContainer1();
            DataTable table = new DataTable();
            string sqlStr = proc;
            var prms = new List<object>();
            foreach (ModelParam p in parameters)
            {
                sqlStr += " " + p.Name;
                prms.Add(new SqlParameter(p.Name, p.Value));
            }

            using (DbDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.SelectCommand = context.Database.Connection.CreateCommand();
                adapter.SelectCommand.CommandText = sqlStr;
                if (prms.Count()>0)
                    adapter.SelectCommand.Parameters.AddRange(prms.ToArray());

                adapter.Fill(table);
                return table.DefaultView.Table;
            }

            
           


        }


    }
}
