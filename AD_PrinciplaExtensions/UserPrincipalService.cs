using Lib.AD.Model;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.AD
{
    public class UserPrincipalService
    {

        private string strContextName;
        private string strContextContainer;

        public UserPrincipalService(string contextName, string contextContainer)
        {
            this.strContextName = contextName;
            this.strContextContainer = contextContainer;
        }



        /// <summary>
        /// Returnerer en dictionary med en employees, som har en bestemt manager
        /// </summary>
        /// <param name="managerSamAccountName"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetManagerEmployees(string managerSamAccountName)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, strContextName, strContextContainer))
            using (UserPrincipalExtension userPrincipal = new UserPrincipalExtension(principalContext))
            {
                userPrincipal.AdvancedSearchFilter.ManagerEquals(GetUserDistinguishedName(managerSamAccountName));

                using (PrincipalSearcher principalSearcher = new PrincipalSearcher(userPrincipal))
                {

                    foreach (UserPrincipalExtension user in principalSearcher.FindAll())
                    {
                        if (user.Name != null && user.SamAccountName != null)
                        {
                            result.Add(user.SamAccountName, user.DisplayName);
                        }
                    }
                }
            }
            return result;
        }

        public string GetUserDistinguishedName(string samAccountName)
        {
            string result;

            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, strContextName, strContextContainer))
            using (UserPrincipalExtension userPrincipal = UserPrincipalExtension.FindByIdentity(principalContext, IdentityType.SamAccountName, samAccountName))
            {
                result = userPrincipal.DistinguishedName;
            }

            return result;
        }


        /// <summary>
        /// Tjekker om et SamAccountName findes i active directory
        /// </summary>
        /// <param name="samAccountNamee"></param>
        /// <returns></returns>
        public bool IsInActiveDirectory(string samAccountName)
        {

            if (string.IsNullOrEmpty(samAccountName))
                return false;

            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, strContextName, strContextContainer))
            {
                using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, samAccountName))
                {
                    return userPrincipal != null;
                }
            }
        }


    }
}
