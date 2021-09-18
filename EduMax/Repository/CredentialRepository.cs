using EduMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMax.Repository
{
    public class CredentialRepository : Repository<Credential>
    {
        public int GetLatestId()
        {
            string sqlQuery = "select top 1 * from Credentials order by CredentialId desc";
            Credential credential = context.Credentials.SqlQuery(sqlQuery).SingleOrDefault();

            return credential.CredentialId;
        }

        public Credential CheckForLogin(Credential credential)
        {
            Credential userLogin = context.Credentials.SqlQuery("Select * from Credentials where email = '"+credential.Email+"' and password = '" +credential.Password+"'").FirstOrDefault();
            return userLogin;
        }
    }
}