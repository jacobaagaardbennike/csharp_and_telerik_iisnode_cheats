using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.AD.Model
{
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("Person")]
    public class UserPrincipalExtension : UserPrincipal
    {
        /// <summary>
        /// tom constructor
        /// </summary>
        /// <param name="context"></param>
        public UserPrincipalExtension(PrincipalContext context)
            : base(context)
        {

        }

        /// <summary>
        /// constructor med parametre
        /// </summary>
        /// <param name="context"></param>
        /// <param name="samAccountName"></param>
        /// <param name="password"></param>
        /// <param name="enabled"></param>
        public UserPrincipalExtension(PrincipalContext context,
                             string samAccountName,
                             string password,
                             bool enabled)
            : base(context,
                   samAccountName,
                   password,
                   enabled)
        {
        }

        /// <summary>
        /// search filter
        /// </summary>
        InetOrgPersonSearchFilter searchFilter;
        new public InetOrgPersonSearchFilter AdvancedSearchFilter
        {
            get
            {
                if (null == searchFilter)
                    searchFilter = new InetOrgPersonSearchFilter(this);

                return searchFilter;
            }
        }

        /// <summary>
        /// Custom AD  property - manager (leder)
        /// </summary>
        [DirectoryProperty("Manager")]
        public string Manager
        {
            get
            {
                if (ExtensionGet("Manager").Length != 1)
                    return null;

                return (string)ExtensionGet("Manager")[0];
            }

            set
            {
                ExtensionSet("Manager", value);
            }
        }

        /// <summary>
        /// Custom AD  property - title (stilling)
        /// </summary>
        [DirectoryProperty("Title")]
        public string Title
        {
            get
            {
                if (ExtensionGet("Title").Length != 1)
                    return null;

                return (string)ExtensionGet("Title")[0];
            }

            set
            {
                ExtensionSet("Title", value);
            }
        }

        /// <summary>
        /// Custom AD  property - telefon nummer (der findes andre AD properties til flere devices f.eks. mobil)
        /// </summary>
        [DirectoryProperty("telephoneNumber")]
        public string TelephoneNumber
        {
            get
            {
                if (ExtensionGet("telephoneNumber").Length != 1)
                    return null;

                return (string)ExtensionGet("telephoneNumber")[0];
            }
            set
            {
                ExtensionSet("telephoneNumber", value);
            }
        }

        /// <summary>
        /// custom AD property - extensionAttribute7
        /// </summary>
        [DirectoryProperty("extensionAttribute7")]
        public string LicenseId
        {
            get
            {
                if (ExtensionGet("extensionAttribute7").Length != 1)
                    return null;

                return (string)ExtensionGet("extensionAttribute7")[0];
            }
            set
            {
                ExtensionSet("extensionAttribute7", value);
            }
        }

        /// <summary>
        /// Finder en user ud fra standard identityvalue
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityValue"></param>
        /// <returns></returns>
        public static new UserPrincipalExtension FindByIdentity(PrincipalContext context,
                                                       string identityValue)
        {
            return (UserPrincipalExtension)FindByIdentityWithType(context,
                                                         typeof(UserPrincipalExtension),
                                                         identityValue);
        }

        /// <summary>
        /// finder en user ud fra en custom identity value
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityType"></param>
        /// <param name="identityValue"></param>
        /// <returns></returns>
        public static new UserPrincipalExtension FindByIdentity(PrincipalContext context,
                                                       IdentityType identityType,
                                                       string identityValue)
        {
            return (UserPrincipalExtension)FindByIdentityWithType(context,
                                                         typeof(UserPrincipalExtension),
                                                         identityType,
                                                         identityValue);
        }

    }

    /// <summary>
    /// advanced search filter som søger på manager
    /// </summary>
    public class InetOrgPersonSearchFilter : AdvancedFilters
    {
        public InetOrgPersonSearchFilter(Principal p) : base(p) { }

        public void ManagerEquals(string manager)
        {
            this.AdvancedFilterSet("Manager", manager, typeof(string), MatchType.Equals);
        }
    }
}
