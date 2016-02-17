namespace comics.DAL.Contract
{
    using System;
    using System.Collections.Generic;
    using comics.Entities;

    public interface ISuccessDao
    {
        bool CreateSuccess(Success success);

        bool Delete(Guid succesId);

        IEnumerable<Success> GetAllSuccessOnCurator(Guid userId);

        IEnumerable<Success> GetAllSuccessOnPerformer(Guid userId);

        Success GetById(Guid successId);

        bool EditSuccessDiscription(Guid successId, string Discription);

        bool ExistSuccess(Guid successId);

        bool SetPerformer(Guid successId, Guid PerformerId, string status);

        bool RemovePerformer(Guid successId, string status);

        bool CheckedSuccess(Guid success, string status);

        IEnumerable<Success> GetAllSuccess();
    }
}
