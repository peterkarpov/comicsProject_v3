namespace comics.DAL.SQL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using comics.DAL.Contract;
    using comics.Entities;

    public class SuccessDao : ISuccessDao
    {
        public bool CreateSuccess(Success success)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "INSERT INTO comics_tSuccess (success_id, success_discription, success_status, success_time, success_curator, success_performer) VALUES (@id, @discription, @status, @time, @curator, @performer)";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@id", success.Id);
                command.Parameters.AddWithValue("@discription", success.SuccessDiscription);
                command.Parameters.AddWithValue("@status", success.Status);
                command.Parameters.AddWithValue("@time", success.CreationTime);
                command.Parameters.AddWithValue("@curator", success.CuratorId);
                command.Parameters.AddWithValue("@performer", success.PerformerId);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool Delete(Guid successId)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "DELETE FROM comics_tSuccess WHERE comics_tSuccess.success_id = @successId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@successId", successId);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }
        
        public bool ExistSuccess(Guid successId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 s.success_id FROM comics_tSuccess as s WHERE s.success_id = @successId";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@successId", successId);

                con.Open();
                var reader = command.ExecuteReader();

                return reader.Read();
            }
        }

        public IEnumerable<Success> GetAllSuccessOnCurator(Guid userId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Success> success = new List<Success>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT s.success_id, s.success_discription, s.success_status, s.success_time, s.success_performer FROM comics_tSuccess as s WHERE s.success_curator = @userId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", userId);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    success.Add(new Success
                    {
                        Id = (Guid)reader["success_id"],
                        SuccessDiscription = (string)reader["success_discription"],
                        Status = (string)reader["success_status"],
                        CreationTime = (DateTime)reader["success_time"],
                        CuratorId = userId,
                        PerformerId = (Guid)reader["success_performer"]
                    });
                }
            }

            return success;
        }

        public IEnumerable<Success> GetAllSuccessOnPerformer(Guid userId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Success> success = new List<Success>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT s.success_id, s.success_discription, s.success_status, s.success_time, s.success_curator FROM comics_tSuccess as s WHERE s.success_performer = @userId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@userId", userId);

                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    success.Add(new Success
                    {
                        Id = (Guid)reader["success_id"],
                        SuccessDiscription = (string)reader["success_discription"],
                        Status = (string)reader["success_status"],
                        CreationTime = (DateTime)reader["success_time"],
                        CuratorId = (Guid)reader["success_curator"],
                        PerformerId = userId,
                    });
                }
            }

            return success;
        }

        public IEnumerable<Success> GetAllSuccess()
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            List<Success> success = new List<Success>().ToList();

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT * FROM comics_tSuccess as s ORDER BY s.success_time";
                var command = new SqlCommand(query, con);
                
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    success.Add(new Success
                    {
                        Id = (Guid)reader["success_id"],
                        SuccessDiscription = (string)reader["success_discription"],
                        Status = (string)reader["success_status"],
                        CreationTime = (DateTime)reader["success_time"],
                        CuratorId = (Guid)reader["success_curator"],
                        PerformerId = (Guid)reader["success_performer"]
                    });
                }
            }

            return success;
        }

        public Success GetById(Guid successId)
        {
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            Success succes = default(Success);

            using (var con = new SqlConnection(conStr))
            {
                var query = "SELECT TOP 1 s.success_discription, s.success_status, s.success_time, s.success_curator, s.success_performer FROM comics_tSuccess as s WHERE s.success_id = @successId";

                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@successId", successId);

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    succes = new Success
                    {
                        Id = successId,
                        SuccessDiscription = (string)reader["success_discription"],
                        Status = (string)reader["success_status"],
                        CreationTime = (DateTime)reader["success_time"],
                        CuratorId = (Guid)reader["success_curator"],
                        PerformerId = (Guid)reader["success_performer"]
                    };
                }
            }

            return succes;
        }

        public bool SetPerformer(Guid successId, Guid performerId, string status)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tSuccess SET success_performer = @performerId, success_status = @status WHERE success_id = @successId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@successId", successId);
                command.Parameters.AddWithValue("@PerformerId", performerId);
                command.Parameters.AddWithValue("@status", status);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool RemovePerformer(Guid successId, string status)
        {
            int result;

            Guid PerformerId = default(Guid);

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tSuccess SET success_performer = @performerId, success_status = @status WHERE success_id = @successId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@successId", successId);
                command.Parameters.AddWithValue("@PerformerId", PerformerId);
                command.Parameters.AddWithValue("@status", status);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }
        
        public bool CheckedSuccess(Guid successId, string status)
        {
            int result;
            
            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tSuccess SET success_status = @status WHERE success_id = @successId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@successId", successId);
                command.Parameters.AddWithValue("@status", status);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }

        public bool EditSuccessDiscription(Guid successId, string discription)
        {
            int result;

            string conStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (var con = new SqlConnection(conStr))
            {
                var query = "UPDATE comics_tSuccess SET success_discription = @discription WHERE success_id = @successId";
                var command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@discription", discription);
                command.Parameters.AddWithValue("@successId", successId);

                con.Open();
                result = command.ExecuteNonQuery(); // result > 0;
            }

            return result > 0;
        }
    }
}
