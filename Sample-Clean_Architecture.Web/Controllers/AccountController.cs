using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Application.Services.Account.Commands.DeleteAccountLedger;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccess;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccount;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models;
using Sample_Clean_Architecture.Web.Models.Account;
using Sample_Clean_Architecture.Web.Utilities;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IGetAccessService _getAccessService;
        private readonly IGetAccountService _getAccountService;
        private readonly IGetAccountInfoService _getAccountInfoService;
        private readonly IAddNewAccountLedgerService _addNewAccountLedgerService;
        private readonly IGetListItemService _getListItemService;

        private readonly IDeleteAccountLedgerService _deleteAccountLedgerService;

        public AccountController(IGetAccountService getAccountService, IGetAccessService getAccessService,
            IGetAccountInfoService getAccountInfoService, IAddNewAccountLedgerService addNewAccountLedgerService, IGetListItemService getListItemService, IDeleteAccountLedgerService deleteAccountLedgerService)
        {
            _getAccountService = getAccountService;
            _getAccessService = getAccessService;
            _getAccountInfoService = getAccountInfoService;
            _addNewAccountLedgerService = addNewAccountLedgerService;
            _getListItemService = getListItemService;
            _deleteAccountLedgerService = deleteAccountLedgerService;
        }
        //public IActionResult Index()
        //{
        //    return this.loadAccounts(0);
        //}
        //public IActionResult Customer()
        //{
        //    return this.loadAccounts(26);
        //}
        //public IActionResult Supplier()
        //{
        //    return this.loadAccounts(22);
        //}
        //[HttpGet("Account/Index/{accountGroupId}")]
        public IActionResult Index(int accountGroupId)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<InFormAccess> result = _getAccessService.Execute(activeUser.CompanyUsers_Id);

            if (result.IsSuccess)
            {
                ViewBag.AccountGroupId = accountGroupId;
                return View(result.Data);
            }

            return View();
        }
        //[HttpGet("Account/Index/AddOrEdit/{id}")]
        public IActionResult AddOrEdit(int id/*, int accountGroup_Id*/)
        {
            //if id<0 New Mode
            //If id>0 Edit Mode
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
            ResultDto<AccountLedgerDto> accountList = _getAccountInfoService.Execute(activeUser.Company_Id, id < 0 ? 0 : id);
            ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups, accountList.Data.AcountGroupList).Data); //new SelectList(accountList.Data.AcountGroupList, "AccountGroup_Id", "AccountGroup_Name");
            ViewBag.TermsAndConditions = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.TermsAndConditionList, accountList.Data.TermsAndConditionList).Data);// new SelectList(accountList.Data.TermsAndConditionList, "TermsAndCondition_Id", "TermsAndCondition_Name");
            ViewBag.CurrencyCompanies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList, accountList.Data.CurrencyCompanyList).Data);

            if (accountList.IsSuccess)
            {
                if (id < 0)
                    accountList.Data.AccountGroup_Id = id * -1;
                return View(DtosToModels.AccountToModel(accountList.Data));
            }

            return View();
        }


        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, AccountModel request)
        {
            if (ModelState.IsValid)
            {
                ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");

                request.Company_Id = activeUser.Company_Id;

                ResultDto result = _addNewAccountLedgerService.Execute(Utilities.ModelsToDtos.AccountToDto(request));

                ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups).Data); //new SelectList(accountList.Data.AcountGroupList, "AccountGroup_Id", "AccountGroup_Name");
                ViewBag.TermsAndConditions = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.TermsAndConditionList).Data);// new SelectList(accountList.Data.TermsAndConditionList, "TermsAndCondition_Id", "TermsAndCondition_Name");
                ViewBag.CurrencyCompanies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);

                if (result.IsSuccess)
                {
                    //ResultDto<InFormAccess> dto = this._getAccessService.Execute(activeUser.CompanyUsers_Id);
                    //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", dto.Data) });
                    request.OprMessage = new Models.MessageViewModel() { Message = result.Message, Color = AppMessages.GetMessageColor(MessageType.Success) };
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });


                }
                else
                {
                    //return Json(result);
                    request.OprMessage = new Models.MessageViewModel() { Message = result.Message, Color = AppMessages.GetMessageColor(MessageType.Warning) };
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });
                }
            }
            request.OprMessage = new Models.MessageViewModel() { Message = AppMessages.REQUIRED, Color = AppMessages.GetMessageColor(MessageType.Error) };

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });
        }

        public IActionResult ViewInfo(int id)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<AccountLedgerDto> accountList = _getAccountInfoService.Execute(activeUser.Company_Id, id < 0 ? 0 : id);
            ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups, accountList.Data.AcountGroupList).Data); //new SelectList(accountList.Data.AcountGroupList, "AccountGroup_Id", "AccountGroup_Name");
            ViewBag.TermsAndConditions = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.TermsAndConditionList, accountList.Data.TermsAndConditionList).Data);// new SelectList(accountList.Data.TermsAndConditionList, "TermsAndCondition_Id", "TermsAndCondition_Name");
            ViewBag.CurrencyCompanies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList, accountList.Data.CurrencyCompanyList).Data);

            if (accountList.IsSuccess)
            {
                if (id < 0)
                    accountList.Data.AccountGroup_Id = id * -1;
                return View(DtosToModels.AccountToModel(accountList.Data));
            }

            return View();

        }

        [HttpPost]
        public ActionResult GetTreeJson(string id, string accountGroupId)
        {
            int account_group_id = 0;

            try
            {
                account_group_id = int.Parse(accountGroupId);
            }
            catch (Exception ex)
            {
                account_group_id = 0;
            }

            ActiveUser activeUser = CurrentUser.Get();
            var nodesList = new List<JsTreeNode>();
            ResultDto<AccountListDto> accountList = _getAccountService.Execute(activeUser.Company_Id, account_group_id, activeUser.CompanyUsers_Id);
            AccountListDto accountListData = accountList.Data;
            InFormAccess inFormAccess = new InFormAccess();

            List<AccountList> accounts = new List<AccountList>();

            if (accountListData != null && accountListData.AccountList.Count > 0)
            {
                foreach (AccountList account in accountListData.AccountList)
                {
                    accounts.Add(account);
                }

                inFormAccess.Insert_Row = accountListData.InFormAccess.Insert_Row;
                inFormAccess.Edit_Row = accountListData.InFormAccess.Edit_Row;
                inFormAccess.Delete_Row = accountListData.InFormAccess.Delete_Row;
            }

            if (accounts != null && accounts.Count > 0)
            {
                accounts.Sort((a, b) => a.Account_AccountGroup_Parent.CompareTo(a.Account_AccountGroup_Parent));

                int firstParentLevel = accounts[0].Account_AccountGroup_Parent;

                foreach (AccountList account in accounts)
                {
                    if (firstParentLevel == account.Account_AccountGroup_Parent)
                    {
                        var rootNode = new JsTreeNode()
                        {
                            id = account.Account_Node_Id.ToString(),
                            text = "<span " + (account.Account_Is_Group == 1 ? " style = 'font-size: 0.8rem; font-weight: bold' " : "") + ">" + account.Account_Node_Name + "</span>" + "<div id=div_" + account.Account_Node_Id.ToString() + " class='account-accounts' style='float:right;padding-right:15px'>  " +
                             "<button type='button'  class='btn  btn-info' style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'view')\"><i class='fa fa-info'> </i></button>" +
                         (inFormAccess.Insert_Row > 0 ? "<button type='button'class='btn  btn-success' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'new')\"><i class='fa fa-file'></i></button>" : "") +
                            (account.Account_Is_Group != 1 && inFormAccess.Edit_Row > 0 ? "<button type='button'  class='btn  btn-warning'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'edit')\"><i class='fa fa-pencil'></i></button>" : "") +
                         "</div>",
                            state = new JsTreeNodeState()
                            {
                                selected = true,
                            },
                            li_attr = new JsTreeNodeLiAttributes()
                            {
                                data = account.Account_AccountGroup_Parent.ToString()
                            }
                        };

                        nodesList.Add(rootNode);
                        PopulateTree(accounts, rootNode, inFormAccess);
                    }
                }
            }

            return Json(nodesList);
        }
        public void PopulateTree(List<AccountList> accounts, JsTreeNode parentNode, InFormAccess inFormAccess)
        {
            foreach (AccountList account in accounts)
            {
                if (account.Account_AccountGroup_Parent == parentNode.id.ToInt())
                {
                    //string jstree_actions = "<div class='btn-group account-accounts'>" +
                    //        (inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn btn-success btn-sm add-account-ledger'><i class='ti-plus'></i></button>" : "") +
                    //        "</div>";
                    //string title = account.Account_Node_Name;

                    //if (account.Account_Is_Group == 1)
                    //{
                    //    title = "<span style='font-size: 0.8rem; font-weight: bold'>" + title + "</span>";
                    //}

                    //if (account.Account_Is_Group != 1)
                    //{
                    //    jstree_actions = "<div class='btn-group account-accounts'>" +
                    //        (inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn btn-warning btn-sm edit-account-ledger'><i class='ti-pencil'></i></button>" : "") +
                    //        (inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn btn-danger btn-sm remove-account-ledger'><i class='ti-trash'></i></button>" : "") +
                    //        "</div>";
                    //}


                    var node = new JsTreeNode()
                    {
                        id = account.Account_Node_Id.ToString(),
                        //text = "<div>" + title + jstree_actions + "</div>",
                        text = "<span " + (account.Account_Is_Group == 1 ? " style = 'font-size: 0.8rem; font-weight: bold' " : "") + ">" + account.Account_Node_Name + "</span>" + "<div id=div_" + account.Account_Node_Id.ToString() + " class='account-accounts' style='float:right;padding-right:15px'>  " +
                             "<button type='button' class='btn  btn-info' style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'view')\"><i class='fa fa-info'> </i></button>" +
                         (inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn  btn-success'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'new')\"><i class='fa fa-file'></i> </button>" : "") +
                            (account.Account_Is_Group != 1 && inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn  btn-warning'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'edit')\"><i class='fa fa-pencil'></i> </button>" : "") +
                            (account.Account_Is_Group != 1 && inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn  btn btn-danger'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'delete')\"><i class='fa fa-trash'></i></button>" : "") +

                         "</div>",
                        li_attr = new JsTreeNodeLiAttributes()
                        {
                            data = account.Account_AccountGroup_Parent.ToString()
                        }
                    };

                    parentNode.children.Add(node);
                    PopulateTree(accounts, node, inFormAccess);
                }
            }

        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {


            ResultDto result = _deleteAccountLedgerService.Execute(id);
            if (result.IsSuccess)
            {
                ActiveUser activeUser = CurrentUser.Get();
                ResultDto<InFormAccess> dto = _getAccessService.Execute(activeUser.CompanyUsers_Id);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", dto.Data) });
            }
            else
                return Json(result);
        }

        [HttpPost, ActionName("Close")]
        public IActionResult CloseDialog()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<InFormAccess> dto = _getAccessService.Execute(activeUser.CompanyUsers_Id);
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", dto.Data) });
        }
    }
}
