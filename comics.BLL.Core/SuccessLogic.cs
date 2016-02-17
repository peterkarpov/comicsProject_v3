namespace comics.BLL.Core
{
    using System;
    using System.Collections.Generic;
    using comics.BLL.Contract;
    using comics.Entities;

    public class SuccessLogic : ISuccessLogic
    {
        public bool CreateSuccess(Success success)
        {
            if (success.Id == default(Guid))
            {
                success.Id = Guid.NewGuid();
            }

            if (success.Status == null)
            {
                success.Status = "created";
            }
            
            if ((success.Status != "created") & (success.Status != "inProgress") & (success.Status != "checked"))
            {
                throw new ArgumentException("No valide status of Success");
            }

            if ((success.SuccessDiscription != "") & (success.CreationTime.ToString() != ""))
            {
                return Daos.SuccessDao.CreateSuccess(success);
            }
            else
            {
                return false;
            }
        }
        
        public bool ExistSuccess(Guid successId)
        {
            return Daos.SuccessDao.ExistSuccess(successId);
        }

        public IEnumerable<Success> GetAllSuccessOnCurator(Guid userId)
        {
            return Daos.SuccessDao.GetAllSuccessOnCurator(userId);
        }

        public IEnumerable<Success> GetAllSuccessOnPerformer(Guid userId)
        {
            return Daos.SuccessDao.GetAllSuccessOnPerformer(userId);
        }

        public Success GetById(Guid successId)
        {
            return Daos.SuccessDao.GetById(successId);
        }

        public IEnumerable<Success> GetAllSuccess()
        {
            return Daos.SuccessDao.GetAllSuccess();
        }

        public bool SetPerformer(Guid successId, Guid PerformerId)
        {
            if (Daos.SuccessDao.GetById(successId).Status == "checked") return false;

            if (Daos.SuccessDao.GetById(successId).PerformerId == default(Guid))
            {
                return Daos.SuccessDao.SetPerformer(successId, PerformerId, "inProgress");
            }
            else
            {
                return Daos.SuccessDao.RemovePerformer(successId, "created");
            }
        }
        
        public bool CheckedSuccess(Guid successId)
        {
            if (Daos.SuccessDao.GetById(successId).Status != "checked")
            {
                return Daos.SuccessDao.CheckedSuccess(successId, "checked");
            }
            else
            {
                return Daos.SuccessDao.CheckedSuccess(successId, "inProgress");
            }
        }

        public bool EditSuccessDiscription(Guid successId, string discription)
        {
            if (discription != "")
            {
                return Daos.SuccessDao.EditSuccessDiscription(successId, discription);
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Guid succesId)
        {
            return Daos.SuccessDao.Delete(succesId);
        }
    }
}