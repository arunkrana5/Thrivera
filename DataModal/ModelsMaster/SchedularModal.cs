using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.ModelsMaster
{
    public  class SchedularModal: ISchedularHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
       
       


    }
}
