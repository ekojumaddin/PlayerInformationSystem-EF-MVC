using log4net;
using log4net.Repository.Hierarchy;
using PlayerInformationSystem.Library;
using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlayerInformationSystem.Repository
{
    public class ClubRepository : IOperation<Club, PlayerInformationSystemEntities>
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
            Club club = context.Clubs.Find(paramTxtId);
            context.Clubs.Remove(club);
            context.SaveChanges();
        }

        public List<Club> GetAllData(PlayerInformationSystemEntities context)
        {
            return context.Clubs.ToList();
        }

        public List<Club> GetAllData()
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

        public Club GetDataById(int? paramId, PlayerInformationSystemEntities context)
        {
            return context.Clubs.Find(paramId);
        }

        public Club GetDataById(int? paramTxtId)
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

        public string Insert(Club paramData)
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

        public string Insert(Club paramData, PlayerInformationSystemEntities context)
        {
            context.Clubs.Add(paramData);
            context.SaveChanges();

            return "Index";
        }

        public string Update(Club paramData)
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

        public string Update(Club paramData, PlayerInformationSystemEntities context)
        {
            context.Entry(paramData).State = EntityState.Modified;
            context.SaveChanges();

            return "Index";
        }

        public List<AutoCompleteModel> GetClubName(string name)
        {
            try
            {
                using (var context = new PlayerInformationSystemEntities())
                {
                    List<AutoCompleteModel> allsearch = context.Clubs.Where(x => x.ClubName.Contains(name)).Select(x => new AutoCompleteModel
                    {
                        Id = x.ClubId,
                        Name = x.ClubName
                    }).ToList();

                    return allsearch;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public List<Club> GetDataClub(string sortOrder, string searchString)
        {
            try
            {
                var listPlayers = new List<Club>();

                using (var context = new PlayerInformationSystemEntities())
                {
                    var model = from s in context.Clubs
                                select s;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        model = model.Where(s => s.ClubName.Contains(searchString));
                    }

                    //Switch action according to sortOrder
                    switch (sortOrder)
                    {
                        case "clubname_desc":
                            listPlayers = model.OrderByDescending(s => s.ClubName).ToList();
                            break;

                        default:
                            listPlayers = model.OrderBy(s => s.ClubName).ToList();
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
    }
}