using log4net;
using PlayerInformationSystem.Library;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;

namespace PlayerInformationSystem.Repository
{
    public class UserRoleRepository : IOperation<UserRole, PlayerInformationSystemEntities>
    {
        #region Property 
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserRepository));
        #endregion

        public void Delete(int? paramTxtId)
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    Delete(paramTxtId, context);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public void Delete(int? paramTxtId, PlayerInformationSystemEntities context)
        {
            UserRole userRole = context.UserRoles.Find(paramTxtId);
            context.UserRoles.Remove(userRole);
            context.SaveChanges();
        }

        public List<UserRole> GetAllData(PlayerInformationSystemEntities context)
        {
            var userRoles = context.UserRoles.Include(u => u.Role).Include(u => u.User);

            return userRoles.ToList();
        }

        public List<UserRole> GetAllData()
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    return GetAllData(context);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public UserRole GetDataById(int? paramId, PlayerInformationSystemEntities context)
        {
            return context.UserRoles.Find(paramId);
        }

        public UserRole GetDataById(int? paramTxtId)
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    return GetDataById(paramTxtId, context);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public string Insert(UserRole paramData)
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    return Insert(paramData, context);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public string Insert(UserRole paramData, PlayerInformationSystemEntities context)
        {
            context.UserRoles.Add(paramData);
            context.SaveChanges();

            return "Index";
        }

        public string Update(UserRole paramData)
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    return Update(paramData, context);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public string Update(UserRole paramData, PlayerInformationSystemEntities context)
        {
            context.Entry(paramData).State = EntityState.Modified;
            context.SaveChanges();

            return "Index";
        }

        public List<UserModel> GetAll()
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    List<UserModel> userRoleList = (from ur in context.UserRoles
                                                    join u in context.Users on ur.UserId equals u.UserId
                                                    join r in context.Roles on ur.RoleId equals r.RoleId
                                                    orderby ur.UserRoleId
                                                    select new UserModel
                                                    {
                                                        Username = u.Username,
                                                        RoleName = r.RoleName
                                                    }).ToList();

                    return userRoleList;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
    }
}