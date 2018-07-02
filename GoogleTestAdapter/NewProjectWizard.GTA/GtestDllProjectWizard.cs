﻿namespace NewProjectWizard.GTA
{
    public class GtestDllProjectWizard : ProjectWizardBase
    {
        protected override void RunStarted()
        {
            string value = GetPlatformToolset(CppProjects);
            ReplacementsDictionary.Add(ToolsetPlaceholder, value);
            Logger.DebugInfo($"Platform toolset: '{value}'");
        }
    }
}