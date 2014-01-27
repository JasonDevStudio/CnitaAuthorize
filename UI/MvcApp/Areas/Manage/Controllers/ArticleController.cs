using Library.Common;
using Library.Logic.Classes;
using Library.Logic;
using Library.Models;
using Library.StringItemDict;
using MvcApp.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Logic.DAL;

namespace MvcApp.Areas.Manage.Controllers
{
    public class ArticleController : BaseController
    {
        #region 私有函数

        #endregion

        public ActionResult Index()
        {
            var model = new ModelPagerArticle();
            model.PagerCount = 0;
            model.PagerIndex = 1;

            var customScript = string.Empty;
            IList<SelectListItem> categorys = new List<SelectListItem>();
            IList<SelectListItem> categorysTwo = new List<SelectListItem>();

            try
            {
                categorys = base.QueryCategoryAll();
                categorysTwo = base.QueryCategoryAll();
            }
            catch (AuthorizeException ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告", isSuccess: false, funName: "GotoLogin");
            }
            catch (Exception ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告");
            }

            ViewBag.CustomScript = customScript;
            ViewBag.Categorys = categorys;
            ViewBag.CategorysTwo = categorysTwo;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string Category, string CategoryTwo, string KeyWord, string PagerIndex, string PagerSize)
        {
            var pageIndex = 0;
            var pageSize = 0;
            int.TryParse(PagerIndex, out pageIndex);
            int.TryParse(PagerSize, out pageSize);
            var customScript = string.Empty;
            IList<SelectListItem> categorys = new List<SelectListItem>();
            IList<SelectListItem> categorysTwo = new List<SelectListItem>();

            var rowCount = decimal.Zero;
            var resultMsg = string.Empty;
            var model = new ModelPagerArticle();

            try
            {
                categorys = base.QueryCategoryAll(model.Category);
                categorysTwo = base.QueryCategoryAll(model.CategoryTwo);
                LogicArticle artDal = new LogicArticle();

                CriteriaArticle.Pager criteria = new CriteriaArticle.Pager();
                criteria.CategoryId = string.IsNullOrWhiteSpace(Category) ? null : Category;
                criteria.CategoryTwo = string.IsNullOrWhiteSpace(CategoryTwo) ? null : CategoryTwo;
                criteria.KeyWord = string.IsNullOrWhiteSpace(KeyWord) ? null : KeyWord;
                criteria.AuthorizeInfo = base.AuthorizeInfo;
                var list = artDal.QueryArticleListPager(out resultMsg, out rowCount, criteria, pageSize: pageSize, pageIndex: pageIndex);

                model.ArtcleList = list;
                model.PagerCount = pageSize == 0 ? 0 : Math.Ceiling(rowCount / pageSize);
                model.PagerIndex = pageIndex;
            }
            catch (AuthorizeException ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告", isSuccess: false, funName: "GotoLogin");
            }
            catch (Exception ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告");
            }

            ViewBag.CustomScript = customScript;
            ViewBag.Categorys = categorys;
            ViewBag.CategorysTwo = categorysTwo;
            return View(model);
        }

        /// <summary>
        /// 新增文章
        /// </summary> 
        public ActionResult Create(string Id = null)
        {
            var model = new ModelArticle();
            var customScript = string.Empty;
            IList<SelectListItem> categorys = new List<SelectListItem>();
            IList<SelectListItem> categorysTwo = new List<SelectListItem>();
            try
            {
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    var idx = Convert.ToInt32(Id);
                    LogicArticle artDal = new LogicArticle();
                    var resultMsg = string.Empty;
                    model = artDal.ArticleDetail(out resultMsg, base.AuthorizeInfo, idx);
                    model.IsPermission = model.Status == 1 ? true : false;
                    model.IsRec = model.Isrecommend == 1 ? true : false;
                    categorys = QueryCategoryAll(model.Id.ToString());
                    customScript = string.Empty;
                }
                else
                {
                    model.Createdate = DateTime.Now;
                    categorys = QueryCategoryAll();
                    customScript = string.Empty;
                }
            }
            catch (AuthorizeException ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告", isSuccess: false, funName: "GotoLogin");
            }
            catch (Exception ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告");
            }

            ViewBag.Categorys = categorys;
            ViewBag.CategorysTwo = categorysTwo;
            ViewBag.CustomScript = customScript;
            return View(model);

        }

        /// <summary>
        /// 新增文章
        /// </summary> 
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(ModelArticle model, FormCollection fc)
        { 
            var customScript = string.Empty;
            IList<SelectListItem> categorys = new List<SelectListItem>();
            IList<SelectListItem> categorysTwo = new List<SelectListItem>();

            try
            {
                categorys = QueryCategoryAll(model.Categoryid.ToString());
                categorysTwo = QueryCategoryAll(model.CategoryTwo);
                var result = new ResultBase();
                var resultMsg = string.Empty;
                var fileName = CommonMethod.ImageUpload(out result, this.HttpContext);
                if (result.result == -2)
                {
                    customScript = UtilityScript.ShowMessage(result.resultMsg, isCreate: true);
                    return View(model);
                }

                model.Context = fc["editorValue"];
                model.Thumbnails = string.IsNullOrWhiteSpace(fileName) ? model.Thumbnails : fileName;
                model.Status = Convert.ToInt32(model.IsPermission);
                model.Isrecommend = Convert.ToInt32(model.IsRec);
                model.Createdate = model.Createdate;
                LogicArticle artDal = new LogicArticle();
                var resultInsertUpdate = artDal.ArticleInsertUpdate(out resultMsg,base.AuthorizeInfo, model);

                if (resultInsertUpdate > 0)
                    customScript = UtilityScript.ShowMessage(BaseDict.OperationSuccessfullyMsg, isCreate: true, isSuccess: true, funName: "Goto");
                else
                    customScript = UtilityScript.ShowMessage(resultMsg, isCreate: true, isSuccess: false, funName: "BtnShow");
            }
            catch (AuthorizeException ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告", isSuccess: false, funName: "GotoLogin");
            }
            catch (Exception ex)
            {
                customScript = Library.Common.UtilityScript.ShowMessage("系统出错,错误信息:" + ex.Message, title: "警告");
            }

            ViewBag.Categorys = categorys;
            ViewBag.CategorysTwo = categorysTwo;
            ViewBag.CustomScript = customScript;
            return View(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string Id)
        {
            int idx = 0;
            int.TryParse(Id, out idx);
            var result = new ResultBase();
            var resultMsg = string.Empty;
            LogicArticle artDal = new LogicArticle();
            try
            {
                var res = artDal.ArticleDelete(out resultMsg, base.AuthorizeInfo, idx);
                if (res > 0)
                {
                    result.result = BaseNumber.OperationSuccessfullyNo;
                    result.resultMsg = "删除成功!";
                }
                else
                {
                    result.result = BaseNumber.OperationFailedNo;
                    result.resultMsg = string.IsNullOrWhiteSpace(resultMsg) ? "删除失败!" : resultMsg;
                }
            }
            catch (AuthorizeException ex)
            {
                result.result = BaseNumber.NotAuthorizeNo ;
                result.resultMsg = ex.Message;
            }
            catch (Exception ex)
            {
                result.result = BaseNumber.OperationFailedNo;
                result.resultMsg = ex.Message;
            } 

            return Json(result);
        }
    }

}
