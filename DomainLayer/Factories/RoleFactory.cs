using DomainLayer.Enums;
using System;

namespace DomainLayer.Factories
{
    public static class RoleFactory
    {
        public static Role CreateRole(string role)
        {
            switch (role.ToLower())
            {
                case "actor":
                    return Role.Actor;

                case "director":
                    return Role.Director;

                case "writer":
                    return Role.Writer;

                case "producer":
                    return Role.Producer;

                default:
                    throw new ArgumentOutOfRangeException(role);
            }
        }
    }
}