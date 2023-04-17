using PlayerInformationSystem.Models;
using PlayerInformationSystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PlayerInformationSystem
{
    public class Util
    {
        private static PlayerInformationSystemEntities db = new PlayerInformationSystemEntities();
        public static string GetPlayerNumber(int? id)
        {
            var dateNow = DateTime.Now.ToString("yyyyddMM");

            string playerNumber = "";
            var posNumber = db.Positions.Where(a => a.PositionId == id).FirstOrDefault();
            var lastPlayerNumber = db.Players.Where(a => a.PositionId == id).OrderByDescending(a=>a.PlayerNumber).FirstOrDefault();

            if (lastPlayerNumber == null)
            {
                if (posNumber.Name == "Goalkeeper")
                {
                    playerNumber = "GK_" + dateNow + "_0001";
                }
                else if (posNumber.Name == "Defender")
                {
                    playerNumber = "DF_" + dateNow + "_0001";
                }
                else if (posNumber.Name == "Midfielder")
                {
                    playerNumber = "MF_" + dateNow + "_0001";
                }
                else if (posNumber.Name == "Forward")
                {
                    playerNumber = "FW_" + dateNow + "_0001";
                }
            }
            else
            {
                string getPlayerNumber = lastPlayerNumber.PlayerNumber.Substring(lastPlayerNumber.PlayerNumber.Length - 4);

                int number = Int32.Parse(getPlayerNumber);
                number++;

                string nomorUrut = number.ToString("D4");

                if (posNumber.Name == "Goalkeeper")
                {
                    playerNumber = "GK_" + dateNow + "_" + nomorUrut;
                }
                else if (posNumber.Name == "Defender")
                {
                    playerNumber = "DF_" + dateNow + "_" + nomorUrut;
                }
                else if (posNumber.Name == "Midfielder")
                {
                    playerNumber = "MF_" + dateNow + "_" + nomorUrut;
                }
                else if (posNumber.Name == "Forward")
                {
                    playerNumber = "FW_" + dateNow + "_" + nomorUrut;
                }
            }

            return playerNumber;
        }

        public static bool sendEmail(MailModel paramModel)
        {
            MailAddress to = new MailAddress(paramModel.txtTo);
            MailAddress from = new MailAddress(paramModel.txtFrom);

            MailMessage mail = new MailMessage(from, to);

            mail.Subject = paramModel.txtSubject;
            mail.Body = paramModel.txtBody;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "localhost";
            smtp.Port = 587;
            smtp.Send(mail);

            return true;
        }
    }
}