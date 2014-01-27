using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Library.Kernel.DataBaseHelper;
using Library.StringItemDict;
using Library.Common;
using System.Data.Common;
using Library.Models;
namespace Library.Logic.Classes
{
    public class LogicUser 
    {

        #region ˽�к���
        /// <summary>
        /// Model ��ֵ IDataReader
        /// </summary>
        private IList<ModelUser> GetModel(IDataReader dr)
        {
            var modelList = new List<ModelUser>();

            while (dr.Read())
            {
                var model = new ModelUser();
                model.Id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]);
                model.Name = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString();
                model.Account = dr["Account"] == DBNull.Value ? string.Empty : dr["Account"].ToString();
                model.Password = dr["PassWord"] == DBNull.Value ? string.Empty : dr["PassWord"].ToString();
                model.Organization = dr["Organization"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Organization"]);
                model.Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]);
                model.Permissions = dr["Permissions"] == DBNull.Value ? string.Empty : dr["Permissions"].ToString();
                model.Createdate = dr["CreateDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CreateDate"]);
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>
        /// Model ��ֵ DataSet
        /// </summary>
        private IList<ModelUser> GetModel(DataSet ds)
        {
            var modelList = (from DataRow dr in ds.Tables[0].Rows
                             select new ModelUser()
                             {
                                 Id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]),
                                 Name = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString(),
                                 Account = dr["Account"] == DBNull.Value ? string.Empty : dr["Account"].ToString(),
                                 Password = dr["PassWord"] == DBNull.Value ? string.Empty : dr["PassWord"].ToString(),
                                 Organization = dr["Organization"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Organization"]),
                                 Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                                 Permissions = dr["Permissions"] == DBNull.Value ? string.Empty : dr["Permissions"].ToString(),
                                 Createdate = dr["CreateDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CreateDate"]),
                             }).ToList();
            return modelList;
        }

        #endregion

        /// <summary>
        /// ��ҳ��ѯ 
        /// </summary>
        /// <param name="recordCount">������� ��������</param>
        /// <param name="criteria">��ѯ��������</param>
        /// <param name="pageSize">ÿҳ��ʾ����</param>
        /// <param name="pageIndex">��ǰҳ����</param>
        /// <returns>����� ����</returns>
        public IList<ModelUser> QueryUserListPager(out string resultMsg, out decimal recordCount, CriteriaUser.Pager criteria, decimal pageSize = 10, decimal pageIndex = 1)
        {
            recordCount = decimal.Zero;
            resultMsg = string.Empty;
            IList<ModelUser> list = new List<ModelUser>();
            try
            {
                //�洢��������
                string sql = "usp_user_select_pager";

                //��������
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "@Organization", ParameterValue = criteria.Organization, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "@KeyWord", ParameterValue = criteria.KeyWord, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "PagerSize", ParameterValue = pageSize, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "PagerIndex", ParameterValue = pageIndex, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "RowsCount", ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String });

                //��ѯִ��
                using (DataSet ds = DBHelper.ExecuteDataSet(criteria.AuthorizeInfo,sql, true, parm))
                {
                    //DataReader ת���� List
                    list = GetModel(ds);
                    foreach (var item in parm)
                    {
                        //��ȡ�������ֵ
                        if (item.ParameterName == "RowsCount")
                        {
                            decimal.TryParse(item.ParameterValue.ToString(), out recordCount);
                            break;
                        }
                    }
                }
            }
            catch (AuthorizeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, ex.ToString());
            }
            return list;
        }

        /// <summary>
        ///  ��ѯʵ��
        /// </summary>
        /// <param name="id">ModelId ���</param>
        /// <returns>ModelUser</returns>
        public ModelUser UserDetail(out string resultMsg, string authorizeInfo,  Int32 id)
        {
            resultMsg = string.Empty;
            var model = new ModelUser();
            try
            {
                //�洢��������
                string sql = "usp_user_select_detail_by_id";

                //��������
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "ID", ParameterValue = id, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.Int32 });

                //��ѯִ��
                using (IDataReader dr = DBHelper.ExecuteReader(authorizeInfo,sql, true, parm))
                {
                    IList<ModelUser> list = GetModel(dr);
                    model = list.First();
                }
            }
            catch (AuthorizeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, ex.ToString());
            }
            return model;
        }

        /// <summary>
        ///  ��ѯʵ��
        /// </summary>
        /// <param name="id">ModelId ���</param>
        /// <returns>ModelUser</returns>
        public ModelUser UserDetail(out string resultMsg,string authorizeInfo, string account)
        {
            resultMsg = string.Empty;
            ModelUser model = null;
            try
            {
                //�洢��������
                string sql = "usp_user_select_detail_by_account";

                //��������
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "@Account", ParameterValue = account, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });

                //��ѯִ��
                using (IDataReader dr = DBHelper.ExecuteReader(authorizeInfo,sql, true, parm))
                {
                    IList<ModelUser> list = GetModel(dr);
                    model = list.First();
                }
            }
            catch (AuthorizeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, ex.ToString());
            }
            return model;
        }

        /// <summary>
        /// ���� ����/����
        /// </summary>
        /// <param name="user">ʵ��</param>
        /// <returns>ִ�н��</returns>
        public int UserInsertUpdate(out string resultMsg,string authorizeInfo , ModelUser user, DbTransaction tran = null)
        {
            resultMsg = string.Empty;
            int res = 0;
            try
            {
                //�洢��������
                string sql = "USP_USER_INSERT_UPDATE";
                //��������
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "ID", ParameterValue = user.Id, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.Int32 });
                parm.Add(new DBParameter() { ParameterName = "NAME", ParameterValue = user.Name, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "ACCOUNT", ParameterValue = user.Account, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "PASSWORD", ParameterValue = user.Password, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "ORGANIZATION", ParameterValue = user.Organization, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.Int32 });
                parm.Add(new DBParameter() { ParameterName = "STATUS", ParameterValue = user.Status, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.Int32 });
                parm.Add(new DBParameter() { ParameterName = "PERMISSIONS", ParameterValue = user.Permissions, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });
                parm.Add(new DBParameter() { ParameterName = "resultMsg",  ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String }); 

                //����/����ִ��
                res = DBHelper.ExecuteNonQuery(authorizeInfo,sql, true, parm, tran);
                foreach (var item in parm)
                {
                    //��ȡ�������ֵ
                    if (item.ParameterName == "resultMsg")
                    {
                        resultMsg = item.ParameterValue.ToString();
                        if (resultMsg.Contains(BaseDict.ErrorPrefix)) 
                            res = -1; 
                        break;
                    }
                }
            }
            catch (AuthorizeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, ex.ToString());
            }
            return res;
        }

        /// <summary>
        /// ����״̬ ����
        /// </summary>
        /// <param name="id">Id ���</param>
        /// <param name="status">״̬</param>
        /// <returns>ִ�н��</returns>
        public int UserUpdateStatus(out string resultMsg, string authorizeInfo, Int32 id, Int32 status, DbTransaction tran = null)
        {
            resultMsg = string.Empty;
            int res = 0;
            try
            {
                //�洢��������
                string sql = "USP_User_UPDATE_STATUS";
                //��������
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "ID", ParameterValue = id, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.Int32 });
                parm.Add(new DBParameter() { ParameterName = "STATUS", ParameterValue = status, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.Int32 });
                parm.Add(new DBParameter() { ParameterName = "resultMsg", ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String });
                //����ִ��
                res = DBHelper.ExecuteNonQuery(authorizeInfo,sql, true, parm, tran);
                foreach (var item in parm)
                {
                    //��ȡ�������ֵ
                    if (item.ParameterName == "resultMsg")
                    {
                        resultMsg = item.ParameterValue.ToString();
                        break;
                    }
                }
            }
            catch (AuthorizeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, ex.ToString());
            }
            return res;
        }

        /// <summary>
        /// ���� ɾ��
        /// </summary>
        /// <param name="id">Id ���</param>
        /// <returns>ִ�н��</returns>
        public int UserDelete(out string resultMsg, string authorizeInfo, Int32 id, DbTransaction tran = null)
        {
            resultMsg = string.Empty;
            int res = 0;
            try
            {
                //�洢��������
                string sql = "usp_user_delete";

                //��������
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "ID", ParameterValue = id, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.Int32 });
                parm.Add(new DBParameter() { ParameterName = "resultMsg", ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String });
                //����ִ��
                res = DBHelper.ExecuteNonQuery(authorizeInfo,sql, true, parm, tran);
                foreach (var item in parm)
                {
                    //��ȡ�������ֵ
                    if (item.ParameterName == "resultMsg")
                    {
                        resultMsg = item.ParameterValue.ToString();
                        break;
                    }
                }
            }
            catch (AuthorizeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, ex.ToString());
            }
            return res;
        }

    }
}