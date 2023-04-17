using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerInformationSystem.Library
{
    public interface IOperation<T, DBContext>
    {
        List<T> GetAllData(DBContext context);
        List<T> GetAllData();
        T GetDataById(int? paramId, DBContext context);
        T GetDataById(int? paramTxtId);
        string Update(T paramData);
        string Update(T paramData, DBContext context);
        void Delete(int? paramTxtId);
        void Delete(int? paramTxtId, DBContext context);
        string Insert(T paramData);
        string Insert(T paramData, DBContext context);
    }
}
