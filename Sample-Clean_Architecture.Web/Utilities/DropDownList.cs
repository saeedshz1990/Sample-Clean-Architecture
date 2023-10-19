using Microsoft.AspNetCore.Mvc.Rendering;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Common.Dtos;
using static Sample_Clean_Architecture.Common.Enums;

namespace Sample_Clean_Architecture.Web.Utilities
{
    public static class DropDownList
    {
        //private IGetListItemService _getListItemService;
        //public DropDownList(IGetListItemService getListItemService)
        //{
        //    this._getListItemService = getListItemService;
        //}

        //public static IEnumerable<SelectListItem> GetList(ListType listType)
        //{

        //    ResultDto<List<ListItemDto>> result = _getListItemService.Execute(Common.Enums.ListType.Category);
        //    if (result.IsSuccess)
        //    {
        //        return GetSelectListItems(result.Data);
        //    }
        //    else
        //        return null;
        //}

        public static IEnumerable<SelectListItem> GetSelectListItems(List<ListItemDto> elements, long selectedId = -1)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Description,
                    Selected = element.Id == selectedId

                });
            }
            return selectList;
        }
    }
}
