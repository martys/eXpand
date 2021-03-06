﻿using System;
using System.IO;
using System.Windows.Forms;
using Xpand.ExpressApp.WorldCreator.Core;
using Xpand.Persistent.BaseImpl.PersistentMetaData;
using Machine.Specifications;
using System.Linq;

namespace Xpand.Tests.Xpand.WorldCreator
{
    public class When_validating_script:With_In_Memory_DataStore
    {
        static PersistentAssemblyInfo _info;

        Establish context = () => {
            _info = ObjectSpace.CreateObject<PersistentAssemblyInfo>();
            _info.Name = "TestAssemlby";
        };

         Because of = () => _info.Validate(Path.GetDirectoryName(Application.ExecutablePath));

        
        It should_not_generate_assembly_in_memory =
            () =>
            AppDomain.CurrentDomain.GetAssemblies().Where(
                assembly => (assembly.FullName + "").StartsWith("TestAssemlby")).FirstOrDefault().ShouldBeNull();

        It should_save_errors_at_persistentAssemblyInfo_compile_errors = () => _info.CompileErrors.ShouldBeNull();

    }

}
