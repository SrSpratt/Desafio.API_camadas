using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Enums
{
    public enum SqlQueryType
    {
        //Product
        READNAME,
        CREATE,
        READ,
        READALL,
        UPDATE,
        DELETE,
        //Category
        READCATEGORY,
        CREATECATEGORY,
        READALLCATEGORIES,
        UPDATECATEGORY,
        DELETECATEGORY,
        //User
        READUSER,
        READUSERS,
        CREATEUSER,
        DELETEUSER,
        UPDATEUSER,
        READROLE,
        READUSERNAME

    }
}
