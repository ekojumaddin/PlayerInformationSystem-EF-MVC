using log4net;
using PlayerInformationSystem.Library;
using PlayerInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlayerInformationSystem.Repository
{
    public class PositionRepository : IOperation<Position, PlayerInformationSystemEntities>
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
            Position position = context.Positions.Find(paramTxtId);
            context.Positions.Remove(position);
            context.SaveChanges();
        }

        public List<Position> GetAllData(PlayerInformationSystemEntities context)
        {
            return context.Positions.ToList();
        }

        public List<Position> GetAllData()
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

        public Position GetDataById(int? paramId, PlayerInformationSystemEntities context)
        {
            return context.Positions.Find(paramId);
        }

        public Position GetDataById(int? paramTxtId)
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

        public string Insert(Position paramData)
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

        public string Insert(Position paramData, PlayerInformationSystemEntities context)
        {
            context.Positions.Add(paramData);
            context.SaveChanges();

            return "Index";
        }

        public string Update(Position paramData)
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

        public string Update(Position paramData, PlayerInformationSystemEntities context)
        {
            context.Entry(paramData).State = EntityState.Modified;
            context.SaveChanges();

            return "Index";
        }
    }
}