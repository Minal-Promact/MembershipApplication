using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;
using System.Security;
using System.Xml.Linq;

namespace MembershipApplication.QueryMoveMembershipToIdentityCore
{
    public class UpdateQuery
    {
        //1. Insert to aspnetusers

        //INSERT INTO identitycoreapidb.aspnetusers(
        //Id, UserName, NormalizedUserName, Email, NormalizedEmail,
        //EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber,
        //PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount
        //)
        //SELECT apidb.my_aspnet_users.id AS Id,
        //apidb.my_aspnet_users.name AS UserName,
        //apidb.my_aspnet_users.name AS NormalizedUserName,
        //apidb.my_aspnet_users.name AS Email,
        //apidb.my_aspnet_users.name AS NormalizedEmail,
        //1 AS EmailConfirmed, apidb.my_aspnet_membership.Password AS PasswordHash,
        //uuid() AS SecurityStamp,'' AS ConcurrencyStamp,'' AS PhoneNumber,
        //0 AS PhoneNumberConfirmed,0 AS TwoFactorEnabled,null AS LockoutEnd,
        //1 AS LockoutEnabled,0 AS AccessFailedCount
        //FROM apidb.my_aspnet_users
        //LEFT OUTER JOIN apidb.my_aspnet_membership ON apidb.my_aspnet_users.id = apidb.my_aspnet_membership.userId;

        // 2. Insert into roles
        //INSERT INTO identitycoreapidb.aspnetroles
        //(Id, Name, NormalizedName, ConcurrencyStamp)
        //SELECT id,name,upper(name),''
        //FROM identitycoreapidb.my_aspnet_roles;

        //3. insert into usersrole
        //INSERT INTO identitycoreapidb.aspnetuserroles
        //(UserId, RoleId)
        //SELECT userId,roleId
        //FROM identitycoreapidb.my_aspnet_usersinroles
        //WHERE userId IN(1,2,5,6) AND roleId NOT IN(1);



    }
}
