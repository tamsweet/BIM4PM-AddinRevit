﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ProjectManagement.Commun;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdLogout : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AuthProvider.Instance.Logout();

            return Result.Succeeded;
        }
    }
}