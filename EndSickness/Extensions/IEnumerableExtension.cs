﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndSickness.Extensions;

public static class IEnumerableExtension
{
    public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
    {
        return items.Select(m => new SelectListItem
        {
            Text = m.GetPropertyValue("Name"),
            Value = m.GetPropertyValue("Id"),
            Selected = m.GetPropertyValue("Id").Equals(selectedValue.ToString())
        }).ToList();

        //return from item in items
        //       select new SelectListItem
        //       {
        //           Text = item.GetPropertyValue("Name"),
        //           Value = item.GetPropertyValue("Id"),
        //           Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
        //       };
    }

}