using System.Collections.Generic;

namespace CommercialRecords.Constants
{
    public class IconConsts
    {
        public enum SegoeIcons
        {
            SAVE, DELETE, DISCARD, REMOVE, ADD, NO, YES, MORE, REDO,
            HOME, BOY, FIRM, PIN, PHONE, CELLPHONE, FOLDER, CURRENCY, NOTE,
            TALK,
            CONTACT,
            CALENDAR,
            COMMENT,
            OPENLOCAL,
            TODAY,
            MAP,
            STOCK,
            LIST,
            VAT,
            PRICE,
            TAG,
            TRIM,
            SHOP,
            SWITCHAPPS,
            FACTORY,
            PERMISSION,
            AUTH,
            WITHDRAWAL
        }

        public static string iconStr(SegoeIcons icon)
        {
            if (null == SegoeIconsDic)
                initIconStr();
            return SegoeIconsDic[icon];
        }

        private static void initIconStr()
        {
            SegoeIconsDic = new Dictionary<SegoeIcons, string>() { 
            {SegoeIcons.ADD, System.Net.WebUtility.HtmlDecode("&#xE109;")},
            {SegoeIcons.DELETE, System.Net.WebUtility.HtmlDecode("&#xE106;")},
            {SegoeIcons.DISCARD, System.Net.WebUtility.HtmlDecode("&#xE107;")},
            {SegoeIcons.REMOVE, System.Net.WebUtility.HtmlDecode("&#xE108;")},
            {SegoeIcons.NO, System.Net.WebUtility.HtmlDecode("&#xE10A;")},
            {SegoeIcons.YES, System.Net.WebUtility.HtmlDecode("&#xE10B;")},
            {SegoeIcons.MORE, System.Net.WebUtility.HtmlDecode("&#xE10C;")},
            {SegoeIcons.REDO, System.Net.WebUtility.HtmlDecode("&#xE10D;")},
            {SegoeIcons.HOME, System.Net.WebUtility.HtmlDecode("&#xE10F;")},
            {SegoeIcons.BOY, System.Net.WebUtility.HtmlDecode("&#x1f466;")},
            {SegoeIcons.FIRM, System.Net.WebUtility.HtmlDecode("&#x1f3e2;")},
            {SegoeIcons.PIN, System.Net.WebUtility.HtmlDecode("&#xE141;")}, 
            {SegoeIcons.PHONE, System.Net.WebUtility.HtmlDecode("&#xE13A;")},
            {SegoeIcons.CELLPHONE, System.Net.WebUtility.HtmlDecode("&#xE1c9;")},
            {SegoeIcons.FOLDER, System.Net.WebUtility.HtmlDecode("&#xE188;")},
            {SegoeIcons.CURRENCY, App.CurrencySymbol},
            {SegoeIcons.NOTE, System.Net.WebUtility.HtmlDecode("&#x1f4dd;")},
            {SegoeIcons.TALK, System.Net.WebUtility.HtmlDecode("&#xe200;")},
            {SegoeIcons.CALENDAR, System.Net.WebUtility.HtmlDecode("&#xE163;")},
            {SegoeIcons.COMMENT, System.Net.WebUtility.HtmlDecode("&#xE134;")},
            {SegoeIcons.OPENLOCAL, System.Net.WebUtility.HtmlDecode("&#xE197;")},
            {SegoeIcons.TODAY, System.Net.WebUtility.HtmlDecode("&#xe184;")},
            {SegoeIcons.CONTACT, System.Net.WebUtility.HtmlDecode("&#xE13D;")},
            {SegoeIcons.MAP, System.Net.WebUtility.HtmlDecode("&#xE1C4;")},
            {SegoeIcons.STOCK, System.Net.WebUtility.HtmlDecode("&#x1f142;")},
            {SegoeIcons.LIST, System.Net.WebUtility.HtmlDecode("&#xE14C;")},
            {SegoeIcons.VAT, System.Net.WebUtility.HtmlDecode("&#xE118;") + App.CurrencySymbol},
            {SegoeIcons.PRICE, System.Net.WebUtility.HtmlDecode("&#xE10B;")+ App.CurrencySymbol},
            {SegoeIcons.TAG, System.Net.WebUtility.HtmlDecode("&#xE1cb;")},
            {SegoeIcons.TRIM, System.Net.WebUtility.HtmlDecode("&#xE12C;")},
            {SegoeIcons.SHOP, System.Net.WebUtility.HtmlDecode("&#xE14D;")},
            {SegoeIcons.SWITCHAPPS, System.Net.WebUtility.HtmlDecode("&#xE1E1;")},
            {SegoeIcons.FACTORY, System.Net.WebUtility.HtmlDecode("&#x1f3ed;")},
            {SegoeIcons.PERMISSION, System.Net.WebUtility.HtmlDecode("&#xE192;")},
            {SegoeIcons.AUTH, System.Net.WebUtility.HtmlDecode("&#xE1A7;")},
            {SegoeIcons.WITHDRAWAL, System.Net.WebUtility.HtmlDecode("&#x1f3e7;")}
        };
        }

        private static Dictionary<SegoeIcons, string> SegoeIconsDic = null;
    }
}
