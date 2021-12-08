﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Ultilities.Extentions
{
    public static class EnumExtention
    {
        public static string GetDescription<T>( this T e) where T : IConvertible
        {
            string description = null;
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);
                foreach (int value in values)
                {
                    if(value == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(value));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            // we're only getting the first description we find
                            // others will be ignored
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }
            return description;
        }
    }
}
