﻿// This file has been modified by Microsoft on 6/2017.

using GoogleTestAdapter.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GoogleTestAdapter.Helpers;
// ReSharper disable LocalizableElement

namespace GoogleTestAdapter.VsPackage.OptionsPages
{

    public class TestExecutionOptionsDialogPage : NotifyingDialogPage
    {
        #region Parallelization

        [Category(SettingsWrapper.CategoryParallelizationName)]
        [DisplayName(SettingsWrapper.OptionEnableParallelTestExecution)]
        [Description(SettingsWrapper.OptionEnableParallelTestExecutionDescription)]
        public bool EnableParallelTestExecution
        {
            get => _enableParallelTestExecution;
            set => SetAndNotify(ref _enableParallelTestExecution, value);
        }
        private bool _enableParallelTestExecution = SettingsWrapper.OptionEnableParallelTestExecutionDefaultValue;

        [Category(SettingsWrapper.CategoryParallelizationName)]
        [DisplayName(SettingsWrapper.OptionMaxNrOfThreads)]
        [Description(SettingsWrapper.OptionMaxNrOfThreadsDescription)]
        public int MaxNrOfThreads
        {
            get => _maxNrOfThreads;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(MaxNrOfThreads), value, "Expected a number greater than or equal to 0.");
                SetAndNotify(ref _maxNrOfThreads, value);
            }
        }
        private int _maxNrOfThreads = SettingsWrapper.OptionMaxNrOfThreadsDefaultValue;

        #endregion

        #region Run configuration

        [Category(SettingsWrapper.CategoryRunConfigurationName)]
        [DisplayName(SettingsWrapper.OptionAdditionalPdbs)]
        [Description(SettingsWrapper.OptionAdditionalPdbsDescription)]
        public string AdditionalPdbs
        {
            get => _additionalPdbs;
            set
            {
                var patterns = Utils.SplitAdditionalPdbs(_additionalPdbs);
                var errorMessages = new List<string>();
                foreach (string pattern in patterns)
                {
                    if (!Utils.ValidatePattern(pattern, out string errorMessage))
                    {
                        errorMessages.Add(errorMessage);
                    }
                }
                if (errorMessages.Any())
                {
                    throw new ArgumentException(string.Join(Environment.NewLine, errorMessages), nameof(AdditionalPdbs));
                }

                SetAndNotify(ref _additionalPdbs, value);
            }
        }
        private string _additionalPdbs = SettingsWrapper.OptionAdditionalPdbsDefaultValue;

        [Category(SettingsWrapper.CategoryRunConfigurationName)]
        [DisplayName(SettingsWrapper.OptionWorkingDir)]
        [Description(SettingsWrapper.OptionWorkingDirDescription)]
        public string WorkingDir
        {
            get => _workingDirectory;
            set => SetAndNotify(ref _workingDirectory, value);
        }
        private string _workingDirectory = SettingsWrapper.OptionWorkingDirDefaultValue;

        [Category(SettingsWrapper.CategoryRunConfigurationName)]
        [DisplayName(SettingsWrapper.OptionPathExtension)]
        [Description(SettingsWrapper.OptionPathExtensionDescription)]
        public string PathExtension
        {
            get => _pathExtension;
            set => SetAndNotify(ref _pathExtension, value);
        }
        private string _pathExtension = SettingsWrapper.OptionPathExtensionDefaultValue;

        [Category(SettingsWrapper.CategoryRunConfigurationName)]
        [DisplayName(SettingsWrapper.OptionAdditionalTestExecutionParams)]
        [Description(SettingsWrapper.OptionAdditionalTestExecutionParamsDescription)]
        public string AdditionalTestExecutionParams
        {
            get => _additionalTestExecutionParams;
            set => SetAndNotify(ref _additionalTestExecutionParams, value);
        }
        private string _additionalTestExecutionParams = SettingsWrapper.OptionAdditionalTestExecutionParamsDefaultValue;

        #endregion

        #region Setup and teardown

        [Category(SettingsWrapper.CategorySetupAndTeardownName)]
        [DisplayName(SettingsWrapper.OptionBatchForTestSetup)]
        [Description(SettingsWrapper.OptionBatchForTestSetupDescription)]
        public string BatchForTestSetup
        {
            get => _batchForTestSetup;
            set => SetAndNotify(ref _batchForTestSetup, value);
        }
        private string _batchForTestSetup = SettingsWrapper.OptionBatchForTestSetupDefaultValue;

        [Category(SettingsWrapper.CategorySetupAndTeardownName)]
        [DisplayName(SettingsWrapper.OptionBatchForTestTeardown)]
        [Description(SettingsWrapper.OptionBatchForTestTeardownDescription)]
        public string BatchForTestTeardown
        {
            get => _batchForTestTeardown;
            set => SetAndNotify(ref _batchForTestTeardown, value);
        }
        private string _batchForTestTeardown = SettingsWrapper.OptionBatchForTestTeardownDefaultValue;

        #endregion

        #region Misc

        [Category(SettingsWrapper.CategoryMiscName)]
        [DisplayName(SettingsWrapper.OptionKillProcessesOnCancel)]
        [Description(SettingsWrapper.OptionKillProcessesOnCancelDescription)]
        public bool KillProcessesOnCancel
        {
            get => _killProcessesOnCancel;
            set => SetAndNotify(ref _killProcessesOnCancel, value);
        }
        private bool _killProcessesOnCancel = SettingsWrapper.OptionKillProcessesOnCancelDefaultValue;

        [Category(SettingsWrapper.CategoryMiscName)]
        [DisplayName(SettingsWrapper.OptionUseNewTestExecutionFramework)]
        [Description(SettingsWrapper.OptionUseNewTestExecutionFrameworkDescription)]
        public bool UseNewTestExecutionFramework2
        {
            get => _useNewTestExecutionFramework;
            set => SetAndNotify(ref _useNewTestExecutionFramework, value);
        }
        private bool _useNewTestExecutionFramework = SettingsWrapper.OptionUseNewTestExecutionFrameworkDefaultValue;

        [Category(SettingsWrapper.CategoryMiscName)]
        [DisplayName(SettingsWrapper.OptionExitCodeTestCase)]
        [Description(SettingsWrapper.OptionExitCodeTestCaseDescription)]
        public string ExitCodeTestCase
        {
            get => _exitCodeTestCase;
            set => SetAndNotify(ref _exitCodeTestCase, value);
        }
        private string _exitCodeTestCase = SettingsWrapper.OptionExitCodeTestCaseDefaultValue;

        #endregion

    }

}