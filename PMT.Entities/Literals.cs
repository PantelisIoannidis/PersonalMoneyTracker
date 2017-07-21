using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public static class Literals
    {
        public static class TransactionTypes
        {
            public const int Income = (int)TransactionType.Income;
            public const int Expense = (int)TransactionType.Expense;
            public const int Transfer = (int)TransactionType.Transfer;
            public const int Adjustment = (int)TransactionType.Adjustment;

        }
        public static string TransactionTypeDescription(this int t)
        {
            switch (t)
            {
                case TransactionTypes.Income: return ViewText.Income;
                case TransactionTypes.Expense: return ViewText.Expense;
                case TransactionTypes.Transfer: return ViewText.Transfer;
                case TransactionTypes.Adjustment: return ViewText.Adjustment;
                default: return "";
            }
        }

        public static string TransactionTypeDescription(this TransactionType t)
        {
            switch (t)
            {
                case TransactionType.Income: return ViewText.Income;
                case TransactionType.Expense: return ViewText.Expense;
                case TransactionType.Transfer: return ViewText.Transfer;
                case TransactionType.Adjustment: return ViewText.Adjustment;
                default: return "";
            }
        }

        public static class AccountType
        {
            public const int All = -1;
        }

        public static class TransactionFilterOperation
        {
            public const string MoveToNext = "MoveToNext";
            public const string MoveToPrevious = "MoveToPrevious";
            public const string Reset = "Reset";
        }

        public static class DefaultCategoryValues
        {
            public const string IconId = "icon-stickynotealt";
            public const string Color = "#99994d";
        }

        public static class SpecialAttributes
        {
            public const string TransferIncome = "TransferIncome";
            public const string TransferExpense = "TransferExpense";
            public const string AdjustmentIncome = "AdjustmentIncome";
            public const string AdjustmentExpense = "AdjustmentExpense";
        }

        public static class GlobalCookies
        {
            public const string transactionsPreferencesCookie = "transactionsPreferences";
            public const string themePreferenceCookie = "themePreference";
            public const string DisplayLanguageCookie = "displayLanguage";
            public const string LanguageFormatting = "languageFormatting";
            public const string TimeZoneCookie = "_timeZoneCookie";
        }

        public static class Notifications
        {
            public const string NotificationSuccess = "NotificationSuccess";
            public const string NotificationWarning = "NotificationWarning";
        }

    }
   


}
