﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using UnityEngine;

public static class DataTableExtension
{

    const string separator = "\t";

    public static void PrintToConsole(this DataTable dt) {
        Debug.Log(dt.AsString());
    }

    

    public static string AsString(this DataTable dt) {

        string headerString =
            String.Join(separator, dt.Columns.OfType<DataColumn>().Select(x => string.Join(separator, x.ColumnName))) + "\n";
        string tableString = string.Join(Environment.NewLine,
                                 dt.Rows.OfType<DataRow>().Select(x => string.Join(separator, x.ItemArray)));
        return headerString + tableString;
    }

    public static string AsString(this DataRow row) {
        string rowString  = string.Join(separator, row.ItemArray);
        return rowString;
    }

}
