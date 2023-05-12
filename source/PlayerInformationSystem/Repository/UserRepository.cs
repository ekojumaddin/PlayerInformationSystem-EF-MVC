using log4net;
using log4net.Repository.Hierarchy;
using PlayerInformationSystem.Library;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace PlayerInformationSystem.Repository
{
    public class UserRepository : IOperation<User, PlayerInformationSystemEntities>
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
            var userRole = context.UserRoles.Where(u => u.UserId == paramTxtId).ToList();

            if (userRole != null)
            {
                foreach (var role in userRole)
                {
                    context.UserRoles.Remove(role);
                    context.SaveChanges();
                }
            }

            var user = context.Users.Find(paramTxtId);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public List<User> GetAllData(PlayerInformationSystemEntities context)
        {
            return context.Users.ToList();
        }

        public List<User> GetAllData()
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

        public User GetDataById(int? paramId, PlayerInformationSystemEntities context)
        {
            return context.Users.Find(paramId);
        }

        public User GetDataById(int? paramTxtId)
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

        public string Insert(User paramData)
        {
            throw new NotImplementedException();
        }

        public string Insert(User paramData, PlayerInformationSystemEntities context)
        {
            throw new NotImplementedException();
        }

        public string Update(User paramData)
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

        public string Update(User paramData, PlayerInformationSystemEntities context)
        {
            context.Entry(paramData).State = EntityState.Modified;
            context.SaveChanges();

            return "success";
        }

        public UserModel Insert(UserModel paramData)
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

        public UserModel Insert(UserModel paramData, PlayerInformationSystemEntities context)
        {
            User user = new User();
            var role = context.Roles.Find(paramData.RoleId);

            if (role.RoleName == "Player")
            {
                Player player = new Player();
                player.PlayerNumber = Util.GetPlayerNumber(paramData.PositionId);
                player.Name = paramData.PlayerName;
                player.HireDate = paramData.HireDate;
                player.Age = paramData.Age;
                player.GenderId = paramData.GenderId;
                player.PositionId = paramData.PositionId;
                player.ClubId = paramData.ClubId;
                player.ExpiredDate = paramData.ExpiredDate;
                player.IsActive = false;
                player.IsTermContract = paramData.IsTermContract;
                player.CreatedBy = paramData.Username;
                player.CreatedTime = DateTime.Now;

                context.Players.Add(player);
                context.SaveChanges();

                paramData.PlayerNumber = player.PlayerNumber;
                user.PlayerId = player.PlayerId;

                TaskApproval task = new TaskApproval();
                task.PlayerNumber = player.PlayerNumber;
                task.IsClosed = false;
                task.Status = "OnProgress";
                task.CreatedTime = DateTime.Now;
                task.CreatedBy = paramData.Username;
                task.Owner = "Committee";
                context.TaskApprovals.Add(task);

                context.SaveChanges();
            }

            user.Username = paramData.Username;
            user.Password = paramData.Password;
            user.Email = paramData.Email;

            context.Users.Add(user);

            UserRole userRole = new UserRole();
            userRole.UserId = user.UserId;
            userRole.RoleId = paramData.RoleId;
            context.UserRoles.Add(userRole);
            context.SaveChanges();

            paramData.RoleName = role.RoleName;

            return paramData;
        }

        public List<User> GetDataByFilter(string sortOrder, string searchString)
        {
            try
            {
                var listUser = new List<User>();

                using (var context = new PlayerInformationSystemEntities())
                {
                    var model = from s in context.Users
                                select s;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        model = model.Where(s => s.Username.Contains(searchString)
                                               || s.Email.Contains(searchString));
                    }

                    //Switch action according to sortOrder
                    switch (sortOrder)
                    {
                        case "username_desc":
                            listUser = model.OrderByDescending(s => s.Username).ToList();
                            break;

                        case "email_desc":
                            listUser = model.OrderByDescending(s => s.Email).ToList();
                            break;

                        case "email":
                            listUser = model.OrderBy(s => s.Email).ToList();
                            break;

                        default:
                            listUser = model.OrderBy(s => s.Username).ToList();
                            break;
                    }

                    return listUser.ToList();
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