using log4net;
using log4net.Repository.Hierarchy;
using PlayerInformationSystem.Library;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PlayerInformationSystem.Repository
{
    public class PlayerRepository : IOperation<Player, PlayerInformationSystemEntities>
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
            Player player = context.Players.Find(paramTxtId);
            context.Players.Remove(player);
            context.SaveChanges();
        }

        public List<Player> GetAllData(PlayerInformationSystemEntities context)
        {
            var allPlayers = context.Players.Include(p => p.Club).Include(p => p.Gender).Include(p => p.Position);

            return allPlayers.ToList();
        }

        public List<Player> GetAllData()
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

        public Player GetDataById(int? paramId, PlayerInformationSystemEntities context)
        {
            return context.Players.Find(paramId);
        }

        public Player GetDataById(int? paramTxtId)
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

        public string Insert(Player paramData)
        {
            using (var context = new PlayerInformationSystemEntities())
            {
                return Insert(paramData, context);
            }
        }

        public string Insert(Player paramData, PlayerInformationSystemEntities context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Database.CommandTimeout = 60;
                    paramData.PlayerNumber = Util.GetPlayerNumber(paramData.PositionId);
                    paramData.CreatedTime = DateTime.Now;
                    paramData.IsActive = false;
                    context.Players.Add(paramData);

                    TaskApproval task = new TaskApproval();
                    task.PlayerNumber = paramData.PlayerNumber;
                    task.IsClosed = false;
                    task.Status = "OnProgress";
                    task.CreatedTime = DateTime.Now;
                    task.CreatedBy = paramData.CreatedBy;
                    task.Owner = "Committee";
                    context.TaskApprovals.Add(task);

                    context.SaveChanges();
                    transaction.Commit();

                    return "Your registered successfully with Player Number : " + paramData.PlayerNumber;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    transaction.Rollback();
                    throw;
                }                
            }            
        }

        public string Update(Player paramData)
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

        public string Update(Player paramData, PlayerInformationSystemEntities context)
        {
            context.Entry(paramData).State = EntityState.Modified;
            context.SaveChanges();

            return "Index";
        }

        public List<Player> GetDataPlayer(string sortOrder, string searchString, DateTime? hireDate, DateTime? expireDate)
        {
            try
            {
                var listPlayers = new List<Player>();

                using (var context = new PlayerInformationSystemEntities())
                {
                    var model = context.Players.Include(p => p.Club).Include(p => p.Gender).Include(p => p.Position);

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        model = model.Where(s => s.PlayerNumber.Contains(searchString)
                                               || s.Name.Contains(searchString)
                                               || s.Age.ToString().Contains(searchString)
                                               || s.Club.ClubName.Contains(searchString)
                                               || s.Gender.Name.Contains(searchString)
                                               || s.Position.Name.Contains(searchString));
                    }

                    if (hireDate != null)
                    {
                        model = model.Where(s => s.HireDate >= hireDate.Value);
                    }

                    if (expireDate != null)
                    {
                        model = model.Where(s => s.ExpiredDate <= expireDate.Value);
                    }

                    //Switch action according to sortOrder
                    switch (sortOrder)
                    {
                        case "playernumber_desc":
                            listPlayers = model.OrderByDescending(s => s.PlayerNumber).ToList();
                            break;

                        case "playername_desc":
                            listPlayers = model.OrderByDescending(s => s.Name).ToList();
                            break;

                        case "playername":
                            listPlayers = model.OrderBy(s => s.Name).ToList();
                            break;

                        case "age_desc":
                            listPlayers = model.OrderByDescending(s => s.Age).ToList();
                            break;

                        case "age":
                            listPlayers = model.OrderBy(s => s.Age).ToList();
                            break;

                        case "hiredate_desc":
                            listPlayers = model.OrderByDescending(s => s.HireDate).ToList();
                            break;

                        case "hiredate":
                            listPlayers = model.OrderBy(s => s.HireDate).ToList();
                            break;

                        case "expiredate_desc":
                            listPlayers = model.OrderByDescending(s => s.ExpiredDate).ToList();
                            break;

                        case "expiredate":
                            listPlayers = model.OrderBy(s => s.ExpiredDate).ToList();
                            break;

                        case "clubname_desc":
                            listPlayers = model.OrderByDescending(s => s.Club.ClubName).ToList();
                            break;

                        case "clubname":
                            listPlayers = model.OrderBy(s => s.Club.ClubName).ToList();
                            break;

                        case "position_desc":
                            listPlayers = model.OrderByDescending(s => s.Position.Name).ToList();
                            break;

                        case "position":
                            listPlayers = model.OrderBy(s => s.Position.Name).ToList();
                            break;

                        default:
                            listPlayers = model.OrderBy(s => s.PlayerNumber).ToList();
                            break;
                    }

                    return listPlayers.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public string Approve(ApprovalModel paramData)
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    var player = context.Players.Where(m => m.PlayerId == paramData.playerId).FirstOrDefault();
                    if (player != null)
                    {
                        player.IsActive = true;
                        player.UpdatedTime = DateTime.Now;
                        player.UpdatedBy = paramData.CreatedBy;

                        var lastTask = context.TaskApprovals.Where(t => t.PlayerNumber == player.PlayerNumber && t.Owner == "Committee").FirstOrDefault();
                        if (lastTask != null)
                        {
                            lastTask.UpdatedTime = DateTime.Now;
                            lastTask.UpdatedBy = paramData.CreatedBy;
                            lastTask.Status = "Approved";
                            lastTask.Note = paramData.note;
                            lastTask.IsClosed = true;
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }            

            return "Index";
        }
    }
}