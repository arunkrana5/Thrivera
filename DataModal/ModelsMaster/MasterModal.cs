using Dapper;
using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataModal.ModelsMaster
{
    public class MasterModal : IMasterHelper
    {
        string ConnectionStrings = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
        public List<Masters.List> GetMastersList(GetResponse Modal)
        {

            List<Masters.List> result = new List<Masters.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@MasterID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@GroupID", dbType: DbType.Int64, value: Modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@TableName", dbType: DbType.String, value: ClsCommon.EnsureString(Modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMasters", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Masters.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMasters", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Masters.Add GetMasters(GetResponse Modal)
        {

            Masters.Add result = new Masters.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@MasterID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@GroupID", dbType: DbType.Int64, value: Modal.AdditionalID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    param.Add("@TableName", dbType: DbType.String, value: ClsCommon.EnsureString(Modal.Doctype), direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMasters", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Masters.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMasters", "GetMaster", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetMasters(Masters.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMasters", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MasterID", SqlDbType.Int).Value = modal.MasterID;
                        command.Parameters.Add("@TableName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.TableName);
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Name);
                        command.Parameters.Add("@Value", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Value);
                        command.Parameters.Add("@GroupID", SqlDbType.Int).Value = modal.GroupID;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public List<Branch.List> GetBranchList(GetResponse Modal)
        {

            List<Branch.List> result = new List<Branch.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBranchList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Branch.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBranchList", "spu_GetBranch", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Branch.Add GetBranch(GetResponse Modal)
        {

            Branch.Add result = new Branch.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@BranchID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBranch", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Branch.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Branch.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {
                            result.CityList = reader.Read<DropDownlist>().ToList();
                            if (result.CityList == null)
                            {
                                result.CityList = new List<DropDownlist>();
                            }

                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBranch", "spu_GetBranch", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetBranch(Branch.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBranch", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = modal.BranchID;
                        command.Parameters.Add("@MaxEMPCount", SqlDbType.Int).Value = modal.MaxEMPCount;
                        command.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchCode);
                        command.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BranchName);
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = modal.StateID;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public List<Department.List> GetDepartmentList(GetResponse Modal)
        {

            List<Department.List> result = new List<Department.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DeptID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDepartment", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Department.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDepartmentList", "spu_GetDepartment", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Department.Add GetDepartment(GetResponse Modal)
        {

            Department.Add result = new Department.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DeptID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDepartment", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Department.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDepartment", "spu_GetDepartment", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetDepartment(Department.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDepartment", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DeptID", SqlDbType.Int).Value = modal.DeptID;
                        command.Parameters.Add("@DeptCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DeptCode);
                        command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DeptName);
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }




        public List<Designation.List> GetDesignationList(GetResponse Modal)
        {

            List<Designation.List> result = new List<Designation.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DesignID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDesignation", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Designation.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDesignationList", "spu_GetDesignation", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Designation.Add GetDesignation(GetResponse Modal)
        {

            Designation.Add result = new Designation.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DesignID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDesignation", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Designation.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDesignation", "spu_GetDesignation", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetDesignation(Designation.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDesignation", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DesignID", SqlDbType.Int).Value = modal.DesignID;
                        command.Parameters.Add("@DesignCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DesignCode);
                        command.Parameters.Add("@DesignName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.DesignName);
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }




        public List<Brand.List> GetBrandList(GetResponse Modal)
        {

            List<Brand.List> result = new List<Brand.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@BrandID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBrand", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Brand.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBrandList", "spu_GetBrand", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Brand.Add GetBrand(GetResponse Modal)
        {

            Brand.Add result = new Brand.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@BrandID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetBrand", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Brand.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetBrand", "spu_GetBrand", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetBrand(Brand.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBrand", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BrandID", SqlDbType.Int).Value = modal.BrandID;
                        command.Parameters.Add("@BrandName", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BrandName);
                        command.Parameters.Add("@BrandCode", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.BrandCode);
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = ClsCommon.EnsureString(modal.Description);
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }




        public List<Employee.List> GetEMPList(Tab.Approval Modal)
        {

            List<Employee.List> result = new List<Employee.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Approved", dbType: DbType.Int64, value: Modal.Approved, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployeeList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Employee.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetEMPList", "spu_GetEmployeeList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public Employee.Add GetEMP(GetResponse Modal)
        {

            Employee.Add result = new Employee.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@EMPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEmployee", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Employee.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Employee.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DepartmentList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DesignationList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RoleList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.UserDetails = reader.Read<Users.Add>().FirstOrDefault();
                            if (result.UserDetails == null)
                            {
                                result.UserDetails = new Users.Add();
                            }
                            if (!string.IsNullOrEmpty(result.UserDetails.Password))
                            {
                                result.UserDetails.Password = ClsCommon.Decrypt(result.UserDetails.Password);
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.EMPList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BankDetails = reader.Read<Bank>().FirstOrDefault();
                            if (result.BankDetails == null)
                            {
                                result.BankDetails = new Bank();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.AddressDetails = reader.Read<Address>().FirstOrDefault();
                            if (result.AddressDetails == null)
                            {
                                result.AddressDetails = new Address();
                            }
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();
                        }
                        result.CityList = new List<DropDownlist>();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetEMP", "spu_GetEmployeeList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetEMP(Employee.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID ?? 0;
                        command.Parameters.Add("@EMPName", SqlDbType.NVarChar).Value = model.EMPName ?? "";
                        command.Parameters.Add("@EMPCode", SqlDbType.NVarChar).Value = model.EMPCode ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar, 50).Value = model.Phone ?? "";
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar, 500).Value = model.EmailID ?? "";
                        command.Parameters.Add("@FatherName", SqlDbType.NVarChar).Value = model.FatherName ?? "";
                        command.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = model.DOB ?? "";
                        command.Parameters.Add("@Gender", SqlDbType.VarChar, 50).Value = model.Gender.ToString() ?? "";
                        command.Parameters.Add("@DesignID", SqlDbType.Int).Value = model.DesignID ?? 0;
                        command.Parameters.Add("@DepartID", SqlDbType.Int).Value = model.DepartID ?? 0;
                        command.Parameters.Add("@DOJ", SqlDbType.VarChar).Value = model.DOJ ?? "";
                        command.Parameters.Add("@PAN", SqlDbType.VarChar, 50).Value = model.PAN ?? "";
                        command.Parameters.Add("@UAN", SqlDbType.VarChar, 50).Value = model.UAN ?? "";
                        command.Parameters.Add("@ESIC", SqlDbType.VarChar, 50).Value = model.ESIC ?? "";
                        command.Parameters.Add("@PaymentMode", SqlDbType.VarChar, 50).Value = model.PaymentMode ?? "";
                        command.Parameters.Add("@HODID", SqlDbType.Int).Value = model.HODID ?? 0;
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachID;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse fnSetEMP_DOL(Employee.UpdateDOL model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_DOL", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPID", SqlDbType.Int).Value = model.EMPID;
                        command.Parameters.Add("@DOL", SqlDbType.NVarChar).Value = model.DOL ?? "";
                        command.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = model.Reason ?? "";
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public List<Dealer.List> GetDealerList(GetResponse Modal)
        {

            List<Dealer.List> result = new List<Dealer.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Dealer.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public List<DealerImport.List> GetDealerImportList(GetResponse Modal)
        {
            List<DealerImport.List> result = new List<DealerImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }

        public Dealer.Add GetDealer(GetResponse Modal)
        {

            Dealer.Add result = new Dealer.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealer", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<Dealer.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new Dealer.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.RegionList = reader.Read<DropDownlist>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {

                            result.StateList = reader.Read<DropDownlist>().ToList();

                        }
                        if (!reader.IsConsumed)
                        {

                            result.CityList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.AreaList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.BranchList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.DealerCategoryList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {

                            result.DealerTypeList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealer", "spu_GetDealer", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetDealer(Dealer.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealer", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@DealerID", SqlDbType.Int).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@DealerCode", SqlDbType.VarChar).Value = model.DealerCode ?? "";
                        command.Parameters.Add("@DealerName", SqlDbType.VarChar).Value = model.DealerName ?? "";
                        command.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = model.EmailID ?? "";
                        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = model.Phone ?? "";
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = model.Address ?? "";
                        command.Parameters.Add("@DealerTypeID", SqlDbType.VarChar).Value = model.DealerTypeID ?? 0;
                        command.Parameters.Add("@DealerCategoryID", SqlDbType.Int).Value = model.DealerCategoryID ?? 0;
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = model.StateID ?? 0;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = model.BranchID ?? 0;
                        command.Parameters.Add("@CityID", SqlDbType.Int).Value = model.CityID ?? 0;
                        command.Parameters.Add("@AreaID", SqlDbType.Int).Value = model.AreaID ?? 0;
                        command.Parameters.Add("@RegionID", SqlDbType.Int).Value = model.RegionID ?? 0;
                        command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = model.PinCode ?? "";
                        command.Parameters.Add("@Latitude", SqlDbType.VarChar).Value = model.Latitude ?? "";
                        command.Parameters.Add("@Longitude", SqlDbType.VarChar).Value = model.Longitude ?? "";
                        command.Parameters.Add("@BillingCode", SqlDbType.VarChar).Value = model.BillingCode ?? "";
                        command.Parameters.Add("@BillingName", SqlDbType.VarChar).Value = model.BillingName ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse UploadDealerImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "DealerImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\DealerImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\DealerImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\DealerImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\DealerImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {
                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveDealerImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.StatusCode = -1;
                Result.SuccessMessage = ex.ToString();
            }
            return Result;

        }

        public PostResponse SaveDealerImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into Dealer import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@MasterDealerImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }

            //try
            //{

            //    if (TempDataset != null && TempDataset.Tables[0].Rows.Count > 0)
            //    {
            //        int count = 0;
            //        using (SqlConnection conn = new SqlConnection(ConnectionStrings))
            //        {
            //            TempDataset.Tables[0].Rows.RemoveAt(0);
            //            SqlCommand cmd = conn.CreateCommand();
            //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //            cmd.CommandText = "dbo.spu_SetDealerImport";
            //            cmd.Parameters.AddWithValue("@LoginID", getResponse.LoginID);
            //            cmd.Parameters.AddWithValue("@MasterDealerImportType", TempDataset.Tables[0]);
            //            conn.Open();
            //            cmd.ExecuteNonQuery();                        
            //        }
            //        //clsDataBaseHelper.executeArrayOfSql(SqlArrayList);
            //        Result.Status = true;
            //        Result.SuccessMessage = "data inserted";
            //    }
            //    else
            //    {
            //        Result = "NoRecordFound";
            //    }


            //}
            //catch (Exception ex)
            //{

            //}
            return Result;

        }
        public PostResponse ClearDealerImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table Master_Dealer_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {
                Result.StatusCode = -1;
                Result.SuccessMessage = ex.ToString();
            }
            return Result;
        }
        public PostResponse UploadDealerImportDetailList(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerFromDealerImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public List<AttendenceStatus.List> GetAttendenceStatusList(GetResponse Modal)
        {

            List<AttendenceStatus.List> result = new List<AttendenceStatus.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAttendenceStatusList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AttendenceStatus.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetAttendenceStatusList", "spu_GetAttendenceStatusList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public AttendenceStatus.Add GetAttendenceStatus(GetResponse Modal)
        {

            AttendenceStatus.Add result = new AttendenceStatus.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetAttendenceStatus", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<AttendenceStatus.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new AttendenceStatus.Add();
                            result.UseFor = "";
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLeaveType", "spu_GetLeaveType", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public PostResponse fnSetAttendenceStatus(AttendenceStatus.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetAttendenceStatus", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ID", SqlDbType.Int).Value = model.ID ?? 0;
                        command.Parameters.Add("@Status_Name", SqlDbType.VarChar).Value = model.Status ?? "";
                        command.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = model.DisplayName ?? "";
                        command.Parameters.Add("@Icon", SqlDbType.VarChar).Value = model.Icon ?? "";
                        command.Parameters.Add("@Color", SqlDbType.VarChar).Value = model.Color ?? "";
                        command.Parameters.Add("@MonthlyAccrued", SqlDbType.VarChar).Value = model.MonthlyAccrued ?? 0;
                        command.Parameters.Add("@UseFor", SqlDbType.VarChar).Value = model.UseFor ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public List<UserHierarchy.List> GetUserHierarchyList(GetResponse Modal)
        {

            List<UserHierarchy.List> result = new List<UserHierarchy.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@UserType", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetUserHierarchyList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<UserHierarchy.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetHierarchyList", "spu_GetHierarchyList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public UserHierarchy.Add GetUserHierarchy(GetResponse Modal)
        {

            UserHierarchy.Add result = new UserHierarchy.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@HierarchyID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Doctype", dbType: DbType.String, value: Modal.Doctype ?? "", direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetUserHierarchy", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<UserHierarchy.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new UserHierarchy.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.UserList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.TableList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerTypeList = reader.Read<DropDownlist>().ToList();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetUserHierarchy", "GetUserHierarchy", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetUserHierarchy(UserHierarchy.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUserHierarchy", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = modal.UserLoginID;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = modal.UserType ?? "";
                        command.Parameters.Add("@DealerType", SqlDbType.VarChar).Value = modal.DealerType ?? "";
                        command.Parameters.Add("@LinkIDs", SqlDbType.VarChar).Value = modal.TableIDs ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public PostResponse fnSetUserHierarchy_Mapping(long HierarchyID, string LinkID, GetResponse Modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUserHierarchy_Mapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@HierarchyID", SqlDbType.Int).Value = HierarchyID;
                        command.Parameters.Add("@LinkID", SqlDbType.Int).Value = LinkID;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public List<EMPImport.List> GetEMPImportList(GetResponse Modal)
        {
            List<EMPImport.List> result = new List<EMPImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMPImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerList", "spu_GetDealerList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }


        public PostResponse UploadEMPImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "EMPImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\EMPImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\EMPImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\EMPImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\EMPImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {
                        
                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveEMPImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveEMPImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMPImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);

                        foreach (DataRow dtRow in TempDataset.Tables[0].Rows)
                        {
                            foreach (DataColumn dc in TempDataset.Tables[0].Columns)
                            {
                                if (dc.ColumnName == "F27")
                                {
                                    dtRow["F27"] = ClsCommon.Encrypt(dtRow["F27"].ToString());
                                }
                            }
                        }

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@EMPImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;

        }
        public PostResponse ClearEMPImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table EMP_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public PostResponse SetEMPFromEMPImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMPFromEMPImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public List<MastersImport.List> GetMastersImportList(GetResponse Modal)
        {
            List<MastersImport.List> result = new List<MastersImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMastersImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MastersImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetMastersImportList", "spu_GetMastersImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }


        public PostResponse UploadMastersImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "MastersImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\MastersImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\MastersImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\MastersImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\MastersImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveMastersImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveMastersImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMastersImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@Masters_ImportType", SqlDbType.Structured).Value = TempDataset.Tables[0];
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;

        }
        public PostResponse ClearMastersImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table Masters_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public PostResponse SetMastersFromMastersImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMastersFromMastersImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        

        public List<EMPTalentPool.List> GetEMPTalentPoolList(Tab.Approval Modal)
        {
            DateTime dt;
            DateTime.TryParse(Modal.Month, out dt);
            List<EMPTalentPool.List> result = new List<EMPTalentPool.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.String, value: dt.ToString("dd-MMM-yyyy"), direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPoolList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPool.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetEMPTalentPoolList", "spu_GetEMP_TalentPoolList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public EMPTalentPool.Add GetEMPTalentPool(GetResponse Modal)
        {

            EMPTalentPool.Add result = new EMPTalentPool.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@TPID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);                   
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPool", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPool.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new EMPTalentPool.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.BranchList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.DealerList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CityList = reader.Read<DropDownlist>().ToList();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.CW_CityList = reader.Read<DropDownlist>().ToList();
                        }
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetMasters", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetEMP_TalentPool(EMPTalentPool.Add model)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    float Latitude = 0, Longitude = 0;
                    float.TryParse(model.Latitude, out Latitude);
                    float.TryParse(model.Longitude, out Longitude);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPool", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TPID", SqlDbType.Int).Value = model.TPID??0;
                        command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = model.Name ?? "";
                        command.Parameters.Add("@Mobile", SqlDbType.VarChar, 50).Value = model.Mobile ?? "";
                        command.Parameters.Add("@DOB", SqlDbType.VarChar, 50).Value = model.DOB ?? "";
                        command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = model.Email ?? "";
                        command.Parameters.Add("@WorkProfile", SqlDbType.VarChar, 50).Value = model.WorkProfile ?? "";
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = model.Address ?? "";
                        command.Parameters.Add("@BranchID", SqlDbType.VarChar).Value = model.BranchID ?? 0;
                        command.Parameters.Add("@DealerID", SqlDbType.VarChar).Value = model.DealerID ?? 0;
                        command.Parameters.Add("@State", SqlDbType.Int).Value = model.State??0;
                        command.Parameters.Add("@City", SqlDbType.Int).Value = model.City??0;
                        command.Parameters.Add("@Pincode", SqlDbType.VarChar, 50).Value = model.Pincode ?? "";
                        command.Parameters.Add("@Experience", SqlDbType.VarChar, 50).Value = model.Experience ?? "";
                        command.Parameters.Add("@AttachID", SqlDbType.Int).Value = model.AttachID??0;
                        command.Parameters.Add("@CW_Company", SqlDbType.VarChar, 50).Value = model.CW_Company ?? "";
                        command.Parameters.Add("@CW_Address", SqlDbType.VarChar).Value = model.CW_Address ?? "";


                        command.Parameters.Add("@ExpectedSalary", SqlDbType.Decimal).Value = model.ExpectedSalary ?? 0;
                        command.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = model.Qualification ?? "";
                        command.Parameters.Add("@Trade_Experience", SqlDbType.VarChar).Value = model.Trade_Experience ?? "";

                        command.Parameters.Add("@CW_State", SqlDbType.Int).Value = model.CW_State??0;
                        command.Parameters.Add("@CW_City", SqlDbType.Int).Value = model.CW_City??0;
                        command.Parameters.Add("@CW_Pincode", SqlDbType.VarChar, 50).Value = model.CW_Pincode ?? "";
                        command.Parameters.Add("@CW_Salary", SqlDbType.Decimal).Value = model.CW_Salary??0;

                        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = Latitude;
                        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = Longitude;
                        command.Parameters.Add("@Error", SqlDbType.VarChar).Value = model.Error ?? "";

                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = model.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = model.Priority ;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = model.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = model.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public PostResponse fnSetBranch_Mapping(long BranchID, string LinkID, GetResponse Modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetBranch_Mapping", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
                        command.Parameters.Add("@LinkID", SqlDbType.Int).Value = LinkID;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<ChallanDocuments.List> GetChallanDocumentsList(Tab.Date Modal)
        {
            List<ChallanDocuments.List> result = new List<ChallanDocuments.List>();
            try
            {
                DateTime dt;
                DateTime.TryParse(Modal.Month, out dt);
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    
                    var param = new DynamicParameters();
                    param.Add("@Date", dbType: DbType.String, value: (dt.Year>1900?dt.ToString("dd-MMM-yyyy"):""), direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetChallanDocumentsList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ChallanDocuments.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetChallanDocumentsList", "spu_GetChallanDocumentsList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }
        public ChallanDocuments.Add GetChallanDocuments(GetResponse Modal)
        {

            ChallanDocuments.Add result = new ChallanDocuments.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@ChallanID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetChallanDocuments", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<ChallanDocuments.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new ChallanDocuments.Add();
                        }
                        if (!reader.IsConsumed)
                        {
                            result.StateList = reader.Read<DropDownlist>().ToList();
                        }
                      
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetChallanDocuments", "spu_GetChallanDocuments", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetChallanDocuments(ChallanDocuments.Add Modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    DateTime dt;
                    DateTime.TryParse(Modal.Date, out dt);
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetChallanDocuments", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ChallanID", SqlDbType.Int).Value = Modal.ChallanID??0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = Modal.AttachmentID;
                        command.Parameters.Add("@MonthYear", SqlDbType.VarChar).Value = dt.ToString("dd-MMM-yyyy");
                        command.Parameters.Add("@StateID", SqlDbType.Int).Value = Modal.StateID ?? 0;
                        command.Parameters.Add("@ChallanType", SqlDbType.VarChar).Value = Modal.ChallanType;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = Modal.Priority??0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<EMPTalentPoolImport.List> GetEMPTalentPoolImportList(GetResponse Modal)
        {
            List<EMPTalentPoolImport.List> result = new List<EMPTalentPoolImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetEMP_TalentPoolImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<EMPTalentPoolImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetEMP_TalentPoolImportList", "GetEMPTalentPoolImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }


        public PostResponse UploadEMP_TalentPoolImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "EMP_TalentPoolImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\EMP_TalentPoolImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\EMP_TalentPoolImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\EMP_TalentPoolImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\EMP_TalentPoolImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveEMP_TalentPoolImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveEMP_TalentPoolImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPool_Import", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@EMP_TalentPoolType", SqlDbType.Structured).Value = TempDataset.Tables[0];
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;

        }

       
        public PostResponse ClearEMP_TalentPoolImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table EMP_TalentPool_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public PostResponse SetEMP_TalentPoolFromImportTable(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetEMP_TalentPoolFromImportTable", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public List<UserHierarchyImport.List> GetUserHierarchyImportList(GetResponse Modal)
        {
            List<UserHierarchyImport.List> result = new List<UserHierarchyImport.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetUserHierarchyImportList", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<UserHierarchyImport.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetUserHierarchyImportList]", "GetUserHierarchyImportList", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }

        public PostResponse UploadUserHierarchyImportDataExcelFile(HttpPostedFileBase file, string SheetName, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            DataSet TempDataset = default(DataSet);
            string ExcelFileName = null;
            string DirectotyPath = null;
            string FileExtension = null;
            ArrayList SqlArrayList = new ArrayList();

            try
            {
                ExcelFileName = "UserHierarchyImportData";
                DirectotyPath = ConfigurationManager.AppSettings["ApplicationPath_Physical"] + "\\Attachments\\ImportExcels";

                if (!System.IO.Directory.Exists(DirectotyPath + "\\UserHierarchyImportData"))
                {
                    System.IO.Directory.CreateDirectory(DirectotyPath + "\\UserHierarchyImportData");
                }
                FileExtension = System.IO.Path.GetExtension(file.FileName);
                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {

                    file.SaveAs(DirectotyPath + "\\UserHierarchyImportData\\" + ExcelFileName + "" + FileExtension);
                    TempDataset = clsDataBaseHelper.GetExcelDataAsDataSet(DirectotyPath + "\\UserHierarchyImportData\\" + ExcelFileName + "" + FileExtension, SheetName.Replace("'", "''").Replace("$", "") + "$");

                    if ((TempDataset != null))
                    {

                        if (TempDataset.Tables[0].Columns.Count > 0)
                        {
                            Result = SaveUserHierarchyImportTempDetails(TempDataset, getResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;

        }

        public PostResponse SaveUserHierarchyImportTempDetails(DataSet TempDataset, GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Error occurred while saving into EMP import";
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUserHierarchyImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        TempDataset.Tables[0].Rows.RemoveAt(0);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@LoginID", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@UserHierarchyType", SqlDbType.Structured).Value = TempDataset.Tables[0];
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;

        }
        public PostResponse ClearUserHierarchyImportTemp(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            try
            {
                string SQL = "Truncate table User_Hierarchy_Import";
                clsDataBaseHelper.ExecuteNonQuery(SQL);
                Result.Status = true;
                Result.SuccessMessage = "data cleared";
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public PostResponse SetUserHierarchyFromUserHierarchyImport(GetResponse getResponse)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetUserHierarchyFromUserHierarchyImport", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = getResponse.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = getResponse.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

        public List<LeaveBalance.List> GetLeaveBalanceList(Tab.Approval Modal)
        {
           
            List<LeaveBalance.List> result = new List<LeaveBalance.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@FinYearID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@Usertype", dbType: DbType.String, value: Modal.Usertype, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeaveBalanceList", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<LeaveBalance.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLeaveBalanceList", "spu_GetLeaveBalanceList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public List<LeaveBalance.TranList> GetLeaveBalanceTran(GetResponse Modal)
        {          
            List<LeaveBalance.TranList> result = new List<LeaveBalance.TranList>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@LBID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetLeaveBalanceTran", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<LeaveBalance.TranList>().ToList();
                    }
                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetLeaveBalanceTran", "spu_GetLeaveBalanceTran", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }


        public List<DealerCategory.List> GetDealerCategoryList(GetResponse Modal)
        {

            List<DealerCategory.List> result = new List<DealerCategory.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerCategoryID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerCategory", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerCategory.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealerCategoryList", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public DealerCategory.Add GetDealerCategory(GetResponse Modal)
        {

            DealerCategory.Add result = new DealerCategory.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerCategoryID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerCategory", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerCategory.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerCategory", "spu_GetDealerCategory", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetDealerCategory(DealerCategory.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerCategory", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DealerCategoryID", SqlDbType.Int).Value = modal.DealerCategoryID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = modal.Name ?? "";
                        command.Parameters.Add("@Code", SqlDbType.VarChar).Value = modal.Code ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }



        public List<DealerType.List> GetDealerTypeList(GetResponse Modal)
        {

            List<DealerType.List> result = new List<DealerType.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerTypeID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerType", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerType.List>().ToList();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "spu_GetDealerTypeList", "GetMastersList", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public DealerType.Add GetDealerType(GetResponse Modal)
        {

            DealerType.Add result = new DealerType.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@DealerTypeID", dbType: DbType.Int64, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginID", dbType: DbType.Int64, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetDealerType", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<DealerType.Add>().FirstOrDefault();
                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "GetDealerType", "spu_GetDealerType", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }

        public PostResponse fnSetDealerType(DealerType.Add modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetDealerType", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DealerTypeID", SqlDbType.Int).Value = modal.DealerTypeID;
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = modal.Name ?? "";
                        command.Parameters.Add("@Code", SqlDbType.VarChar).Value = modal.Code ?? "";
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = modal.IsActive;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = modal.Priority ?? 0;
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = modal.LoginID;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }


        public List<MasterCatalogue.List> GetMasterCatalogueList(Tab.Approval Modal)
        {
            List<MasterCatalogue.List> result = new List<MasterCatalogue.List>();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;

                    var param = new DynamicParameters();
                    param.Add("@CatID", dbType: DbType.Int32, value: 0, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMaster_Catalogue", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MasterCatalogue.List>().ToList();
                    }

                    DBContext.Close();
                }

            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "MasterCatalogue", "spu_GetMaster_Catalogue", "DataModal", Modal.LoginID, Modal.IPAddress);

            }
            return result;
        }
        public MasterCatalogue.Add GetMasterCatalogue(GetResponse Modal)
        {

            MasterCatalogue.Add result = new MasterCatalogue.Add();
            try
            {
                using (IDbConnection DBContext = new SqlConnection(ConnectionStrings))
                {
                    int commandTimeout = 0;
                    var param = new DynamicParameters();
                    param.Add("@CatID", dbType: DbType.Int32, value: Modal.ID, direction: ParameterDirection.Input);
                    param.Add("@LoginId", dbType: DbType.Int32, value: Modal.LoginID, direction: ParameterDirection.Input);
                    DBContext.Open();
                    using (var reader = DBContext.QueryMultiple("spu_GetMaster_Catalogue", param: param, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout))
                    {
                        result = reader.Read<MasterCatalogue.Add>().FirstOrDefault();
                        if (result == null)
                        {
                            result = new MasterCatalogue.Add();
                        }

                    }

                    DBContext.Close();
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "MasterCatalogue", "MasterCatalogue", "DataModal", Modal.LoginID, Modal.IPAddress);
            }
            return result;
        }
        public PostResponse fnSetMasterCatalogue(MasterCatalogue.Add Modal)
        {
            PostResponse Result = new PostResponse();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spu_SetMaster_Catalogue", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CatID", SqlDbType.Int).Value = Modal.CatID ?? 0;
                        command.Parameters.Add("@AttachmentID", SqlDbType.Int).Value = Modal.AttachmentID??0;
                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Modal.ProductName;
                        command.Parameters.Add("@Description", SqlDbType.VarChar).Value = Modal.Description??"";
                        command.Parameters.Add("@URL", SqlDbType.VarChar).Value = Modal.URL??"";
                        command.Parameters.Add("@createdby", SqlDbType.Int).Value = Modal.LoginID;
                        command.Parameters.Add("@Priority", SqlDbType.Int).Value = Modal.Priority ?? 0;
                        command.Parameters.Add("@IsActive", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Modal.IPAddress;
                        command.CommandTimeout = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result.ID = Convert.ToInt64(reader["RET_ID"]);
                                Result.StatusCode = Convert.ToInt32(reader["STATUS"]);
                                Result.SuccessMessage = reader["MESSAGE"].ToString();
                                if (Result.StatusCode > 0)
                                {
                                    Result.Status = true;
                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                    Result.StatusCode = -1;
                    Result.SuccessMessage = ex.Message.ToString();
                }
            }
            return Result;
        }

       

    }
}
