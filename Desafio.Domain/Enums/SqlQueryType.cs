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
        REPLACE,
        //ProductAggregated
        READOPERATIONS,
        READLATESTOPERATION,
        READFIRSTOPERATION,
        READDAYOPERATION,
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
        READUSERNAME,
        //UserAggregated
        INSERTNAMEUSER,


        //Trying to disarrange
        P_READALL,
        P_READ,
        P_CREATE,
        P_UPDATE,
        P_DELETE,

        PC_READALL,
        PC_READ,
        PC_CREATE,
        PC_UPDATE,
        PC_DELETE,

        S_READALL,
        S_READ,
        S_CREATE,
        S_UPDATE,
        S_DELETE,

        C_READALL,
        C_READ,
        C_CREATE,
        C_UPDATE,
        C_DELETE,

        U_READALL,
        U_READ,
        U_CREATE,
        U_UPDATE,
        U_DELETE,

        UN_READALL,
        UN_READ,
        UN_CREATE,
        UN_UPDATE,
        UN_DELETE,

        R_READALL,
        R_READ,
        R_CREATE,
        R_UPDATE,
        R_DELETE,

        O_READALL,
        O_READ,
        O_CREATE,
        O_UPDATE,
        O_DELETE,

    }
}
