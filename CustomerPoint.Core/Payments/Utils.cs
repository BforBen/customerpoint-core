namespace CustomerPoint.Payments
{
    public static class Utils
    {
        public static string GetMethodChannel(string MopCode, string Channel)
        {
            switch (MopCode)
            {
                case "DCRD":
                    return "Debit card (" + Channel + ")";
                case "CCRD":
                    return "Credit card (" + Channel + ")";
                default:
                    return Civica.GetMopCodeDefinition(MopCode);
            }
        }
    }

    public static class Civica
    {
        public static string GetMopCodeDefinition(string MopCode)
        {
            switch (MopCode)
            {
                case "BANK":
                    return "BACS";
                case "CASH":
                case "05":
                    return "Cash";
                case "08":
                    return "Refer To Drawer";
                case "10":
                case "DCRD":
                    return "Debit card";
                case "11":
                case "CCRD":
                    return "Credit card";
                case "16":
                    return "Debit card (web)";
                case "17":
                    return "Credit card (web)";
                case "31":
                case "UDD":
                case "UNDD":
                    return "Unpaid Direct Debit";
                case "36":
                    return "Debit card (CSC)";
                case "37":
                    return "Credit card (CSC)";
                case "38":
                    return "Debit card (ATP)";
                case "39":
                    return "Credit card (ATP)";
                case "40":
                case "CHEQ":
                    return "Cheque";
                case "TR":
                case "JRNL":
                    return "Transfer";
                case "DD":
                    return "Direct Debit";
                default:
                    return "Unknown (" + MopCode + ")";
            }
        }
    }
}
