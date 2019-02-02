using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;

namespace Repository.Context
{
    public class AppContext:DbContext
    {
        public AppContext():base("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=aspnet-MvcMovie;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\\Movies.mdf")
        {
                
        }
        public virtual IDbSet<Student> Students { get; set; }
    }
}
