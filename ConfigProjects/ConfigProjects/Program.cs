//#define TEST_AESELECT
//#define TEST_AEGENERATE
//#define TEST_AEINIT

using CgenMin.MacroProcesses;
using CgenMin.MacroProcesses.QR;
using CodeGenerator.cgenXMLSaves;
using CodeGenerator.ProblemHandler;
using System;
using System.Collections.Generic;


using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace testaertosproj
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;



    class Program
    {

        static void Main(string[] args)
        {


            //=====================================================
            //AEGENERATE
            //=====================================================  
#if !TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT
            if (args[0] == "QR_generate")
            {

            string _envIronDirectory = args.Count() < 2 ? "" : args[1];
            string typeOfTheProject = args.Count() < 3 ? "" : args[2];
            string selectedTargetName = args.Count() < 4 ? "" : args[3];
            CodeGenerator.Program._envIronDirectory = _envIronDirectory;
#endif

#if (TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT)
            // CodeGenerator.Program._envIronDirectory = @"C:\\Users\\SyncthingServiceAcct\\QR_Sync\\world2";
            CodeGenerator.Program._envIronDirectory = @"/home/hadi/QR_Sync/world2";
            //CodeGenerator.Program._envIronDirectory = @"C:\\Users\\SyncthingServiceAcct\\QR_Sync\\sometest";
            //string typeOfTheProject = "c";
            //string selectedTargetName = "defaultTest";
            string selectedTargetName = "defaultTestRos";
            string typeOfTheProject = "r";
#endif

#if (TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT) || (!TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT)
            //string ModuleName = CodeGenerator.Program.GetQRProjectName();
            System.Console.WriteLine("envIronDirectory:" +CodeGenerator.Program.envIronDirectory);
            //get directory of the Launch file directory
            string ModuleName = CodeGenerator.Program.GetProjectNameFromDirectory(CodeGenerator.Program.envIronDirectory);
            string projDir = Path.Combine(CodeGenerator.Program.QRBaseDir, ModuleName);
            Console.WriteLine(ModuleName);
            if (selectedTargetName == null || selectedTargetName == "")
            {
                ProblemHandle problemHandle = new ProblemHandle();
                problemHandle.ThereisAProblem("You did not provide selectedTargetName. QR_generate <typeOfTheProject> <selectedTargetName>");
            }



            ProblemHandle prob = new ProblemHandle();
            if (typeOfTheProject != "c" && typeOfTheProject != "r" && typeOfTheProject != "i")
            {
                prob.ThereisAProblem($"typeOfTheProject was put as {typeOfTheProject} but should be either value c, r, or i  for cpp, rqt, if, respectively. QR_generate <typeOfTheProject> <selectedTargetName>");
            }

            //generate the project.
            
            var moduleSelected = QRInitializing.GetProjectIfNameExists(ModuleName);
            if (moduleSelected == null)
            {
                prob.ThereisAProblem($"No such module of name {ModuleName} exists.");
            }
            moduleSelected.Init();

            //get project exe target currently selected for this project
            //string ProjectTest = CodeGenerator.Program.GetAEProjectTestName();
            QRTargetCmake qRTargetCmake = new QRTargetCmake(moduleSelected.DirectoryOfProject, typeOfTheProject == "c");
            QRTarget TestSelected = typeOfTheProject == "c" ?
    moduleSelected.ListOfTargets_cpEXE.FirstOrDefault(s => s.TargetName == selectedTargetName) : // qRTargetCmake.GetSelectedProjName()) :
    moduleSelected.ListOfTargets_rosEXE.FirstOrDefault(s => s.TargetName == selectedTargetName); // qRTargetCmake.GetSelectedProjName());
            //if TestSelected is null, then return a list of possible targets to choose from
            if (TestSelected == null) {
                List<QRTarget_EXE> targetsAvailable = typeOfTheProject == "c" ?
                    moduleSelected.ListOfTargets_cpEXE.Cast<QRTarget_EXE>().ToList() :
                    moduleSelected.ListOfTargets_rosEXE.Cast<QRTarget_EXE>().ToList();
                string disp = $"No such target of name {selectedTargetName} exists for module named {ModuleName} project {typeOfTheProject}."; disp += "\n";
                disp += $"Here is a list of targets from chosen module {ModuleName} "; disp += "\n";
                disp += targetsAvailable.Select(t => t.TargetName).Aggregate((a, b) => a + "\n" + b);
                Console.WriteLine(disp);
                return;
            }


            QRInitializing aEInitializing = new QRInitializing(ModuleName, TestSelected);

            qRTargetCmake.GenerateFile(aEInitializing, moduleSelected, TestSelected.TargetName);

           
            //CodeGenerator.Program.aeinitOptions aeinitOptions = new CodeGenerator.Program.aeinitOptions() { nameOfTheProject = ProjectName };
            //CodeGenerator.Program._aeinitProjectFileStructure(aeinitOptions, aEInitializing, projectSelected, projectSelected.DirectoryOfLibrary);

            aEInitializing.GenerateProject();
#endif


#if !TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT
            }
#endif



#if !TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT
            else if (args[0] == "QR_select")
            {




                string projectNameSelection = "";
                string projectEXETestSelection = ""; 
                string SettingFileName = "";
                string typeOfTheProject = ""; 
                string _envIronDirectory = "";

                try
                {
                    //projectNameSelection =      args[1] == null ? "" : args[1];
                    //projectEXETestSelection =   args[2] == null ? "" : args[2]; 
                    //SettingFileName =           args[4] == null ? "" : args[3]; 
                  _envIronDirectory = args.Count() < 2 ? "" : args[1];
                 typeOfTheProject = args.Count() < 3 ? "" : args[2];
                 SettingFileName = args.Count() < 4 ? "" : args[3];
                 CodeGenerator.Program._envIronDirectory = _envIronDirectory;

                }
                catch (Exception)
                {

                }
#endif














            //=====================================================
            //AEselect
            //===================================================== 

#if (TEST_AESELECT && !TEST_AEGENERATE && !TEST_AEINIT)

            //string projectNameSelection = "world2";
            //string moduleNameSelection = "sometest";
            //string projectEXETestSelection = "defaultTest";
            //string projectEXETestSelection = "defaultTestRos";
            //string projectEXETestSelection = "test2";
            //string SettingFileName = "defaultsettings";
            string typeOfTheProject = "r";

            string SettingFileName = "MySetting1";
            CodeGenerator.Program._envIronDirectory = @"C:\\Users\\SyncthingServiceAcct\\QR_Sync\\world2";
#endif

#if (TEST_AESELECT && !TEST_AEGENERATE && !TEST_AEINIT) || (!TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT)

            //ignore all this as Select is deprectated. this is now only for selecting the settings file

            //get directory of the Launch file directory
            string moduleNameSelection = CodeGenerator.Program.GetProjectNameFromDirectory(CodeGenerator.Program.envIronDirectory);
            string projDir = Path.Combine(CodeGenerator.Program.QRBaseDir, moduleNameSelection);
            Console.WriteLine(moduleNameSelection);
            if (moduleNameSelection == null)
            {
                ProblemHandle problemHandle = new ProblemHandle();
                problemHandle.ThereisAProblem("You are not in a QR project directory with base directory of QR_Sync");
            }
             
        


            ProblemHandle prob = new ProblemHandle();

            if (typeOfTheProject != "c" && typeOfTheProject != "r" && typeOfTheProject != "i")
            {
                prob.ThereisAProblem($"typeOfTheProject was put as {typeOfTheProject} but should be either value c, r, or i  for cpp, rqt, if, respectively");
            }

            //if projectName is null, return back a list of possible projects you can select
            if (moduleNameSelection == null)
                {
                    string disp = "You did not provide projectNameSelection"; disp += "\n";
                    disp += "Here is a list of projects to choose from"; disp += "\n";
                    disp += GetProjectsDisplay();

                    Console.WriteLine(disp);
                    return;
                }




                //projectNameSelection and is valid project provided but not projectSelected
                var projectSelected = QRInitializing.GetProjectIfNameExists(moduleNameSelection);
                if (moduleNameSelection == null && projectSelected != null)
                {
                    List<QRTarget_EXE> targetsToCheck1 = projectSelected.ListOfTargets_rosEXE.Cast<QRTarget_EXE>().ToList();
                    targetsToCheck1.AddRange(projectSelected.ListOfTargets_cpEXE.Cast<QRTarget_EXE>().ToList());
                    //            List<QRTarget_EXE> targetsToCheck1 = typeOfTheProject == cppTypeSTR ?
                    //projectSelected.ListOfTargets_cpEXE.Cast<QRTarget_EXE>().ToList() :
                    //projectSelected.ListOfTargets_rosEXE.Cast<QRTarget_EXE>().ToList();

                    string disp = $"{moduleNameSelection} is valid but no target selected "; disp += "\n";
                    disp += $"Here is a list of target from chosen project {moduleNameSelection}"; disp += "\n";
                    disp += GetProjectTestsDisplay(targetsToCheck1);
                    Console.WriteLine(disp);
                    return;
                }


                //projectNameSelection provided but is NOT valid
                if (projectSelected == null)
                {
                    string disp = $"No such project of name {moduleNameSelection} exists."; disp += "\n";
                    disp += "Here is a list of projects to choose from"; disp += "\n";
                    disp += GetProjectsDisplay();
                    Console.WriteLine(disp);
                    return;
                }


                //if (typeOfTheProject != cppTypeSTR && typeOfTheProject != rqtTypeSTR && typeOfTheProject != "if")
                //{
                //    prob.ThereisAProblem($"typeOfTheProject was put as {typeOfTheProject} but should be either value {cppTypeSTR}, {rqtTypeSTR}, if ");
                //}

                projectSelected.Init();

                //projectNameSelection provided  and is valid but projectEXETestSelection is NOT valid
                List<QRTarget_EXE> targetsToCheck = projectSelected.ListOfTargets_rosEXE.Cast<QRTarget_EXE>().ToList();
                targetsToCheck.AddRange(projectSelected.ListOfTargets_cpEXE.Cast<QRTarget_EXE>().ToList());

#if ignoreSelectdeprecated
            QRTarget TestSelected = targetsToCheck.FirstOrDefault(s => s.TargetName == projectEXETestSelection) == null ?
                    targetsToCheck.FirstOrDefault(s => s.TargetName == projectEXETestSelection) :
                    targetsToCheck.FirstOrDefault(s => s.TargetName == projectEXETestSelection);

                string dispp = projectEXETestSelection == null ? "You did not provide projectEXETestSelection" : ""; dispp += "\n";
                if (projectSelected != null && (TestSelected == null || string.IsNullOrWhiteSpace(TestSelected.TargetName)))
                {


                    dispp += $"No such target of name {projectEXETestSelection} exists for project named {moduleNameSelection}."; dispp += "\n";
                    dispp += $"Here is a list of targets from chosen project {moduleNameSelection} "; dispp += "\n";
                    dispp += GetProjectTestsDisplay(targetsToCheck);
                    Console.WriteLine(dispp);
                    return;
                }
#endif

            //============================================================================================
            //need to check if SettingFileName is a setting file that exists
            if ((SettingFileName != null) && (SettingFileName != ""))
            {
                string pathToConfig = typeOfTheProject == "c" ? //TestSelected.qRTargetType == QRTargetType.cpp_exe ?
                    Path.Combine(projectSelected.DirectoryOfProject, "config") :
                    Path.Combine(projectSelected.DirectoryOfProject, "rosqt", "config");
                string pathToSettingFile = Path.Combine(pathToConfig, "AllAOSettings", SettingFileName + ".cereal");


                if (!File.Exists(pathToSettingFile))
                {
                    //get a list of all the setting files in the directory
                    string[] allSettingFiles = Directory.GetFiles(Path.Combine(pathToConfig, "AllAOSettings"), "*.cereal");
                    string allSettingFilesStr = allSettingFiles.Aggregate((a, b) => Path.GetFileName(a) + "\n" + Path.GetFileName(b));


                    prob.ThereisAProblem($"No such file of name {SettingFileName} exists. in directory {pathToSettingFile}. settings files are: \n------\n {allSettingFilesStr}");
                }
                else
                {
                    //set the selected settings by writing it in the AOSelection.txt file in the config directory
                    string pathToAOSelection = Path.Combine(pathToConfig, "AOSelection.txt");
                    Console.WriteLine($"writing target {SettingFileName} in file  AOSelection at location {pathToAOSelection}.");
                    File.WriteAllText(pathToAOSelection, SettingFileName);
                }
            }
            else    //if no setting file is provided, then just print out the list of setting files
            { 

                Console.WriteLine($"No setting file name provided. QR_select <typeOfTheProject> <SettingFileName>");
            }


#if ignoreSelectdeprecated
            QRInitializing aEInitializing = new QRInitializing(moduleNameSelection, TestSelected);



            QRTargetCmake qRTargetCmake = new QRTargetCmake(projectSelected.DirectoryOfProject, TestSelected.qRTargetType == QRTargetType.cpp_exe);
            qRTargetCmake.GenerateFile(aEInitializing, projectSelected, TestSelected.TargetName);
#endif




#endif

#if !TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT
            }
#endif









            //=====================================================
            //AEINIT
            //===================================================== 

#if !TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT
            else if (args[0] == "aeinit")
            {

                string nameOfEXE = "";
                string typeOfTheProject = "";
                string envIronDirectory = "";

                try
                {
                    nameOfEXE = args[1] == null ? "" : args[1];
                    typeOfTheProject = args[2] == null ? "" : args[2];
                    envIronDirectory = args[3] == null ? "" : args[3];
                }
                catch (Exception)
                {

                }
#endif

#if (TEST_AEINIT && !TEST_AEGENERATE && !TEST_AESELECT)

            string nameOfEXE = "defaultTest";
            string typeOfTheProject = "cpp";
            string envIronDirectory = @"C:\\Users\\SyncthingServiceAcct\\QR_Sync\\world2";
#endif


                //#if (TEST_AEINIT && !TEST_AEGENERATE && !TEST_AESELECT) || (!TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT)
#if Initdeprecated

                ProblemHandle prob = new ProblemHandle();

            //check if a project already exists here.
            QRModule projAlreadyExists = QRInitializing.GetProjectIfDirExists(envIronDirectory);
            QRModule.CurrentWorkingProject = projAlreadyExists;
            string nameOfTheProject = projAlreadyExists.Name;
            if (nameOfEXE == null)
                {

                    if (projAlreadyExists == null)
                    {
                        prob.ThereisAProblem("You didnt provide a QRProject nameOfEXE and no project exists here yet. do that with \"aeinit <projectName>\"");
                    }

                    //nameOfTheProject = projAlreadyExists.Name;


                }
                else if (nameOfTheProject != null)
                {
                    if (projAlreadyExists != null)
                    {
                        if (projAlreadyExists.Name != nameOfTheProject)
                        {
                            prob.ThereisAProblem($"There already exists a project here of different name {projAlreadyExists.Name}");
                        }

                    }


                }


            if (typeOfTheProject != "cpp" && typeOfTheProject != "rqt" && typeOfTheProject != "if" )
            {
                prob.ThereisAProblem($"typeOfTheProject was put as {typeOfTheProject} but should be either value cpp, rqt, if ");
            }

            if (projAlreadyExists != null)
            {
                if (projAlreadyExists.ListOfTargets_AllEXE.FirstOrDefault(s => s.MethodName == nameOfEXE) == null)
                {
                    prob.ThereisAProblem($"There is no project exe target of name {nameOfEXE} for prject type {typeOfTheProject} ");
                }
            }

            projAlreadyExists.Init();
                QRInitializing aEInitializing = new QRInitializing();

            QRTargetCmake qRTargetCmake = new QRTargetCmake(envIronDirectory, typeOfTheProject == "cpp");
            qRTargetCmake.GenerateFile( aEInitializing, projAlreadyExists,  nameOfEXE);



            //check that the current name is not already in use by another project at a different directory
            //if (QRInitializing.GetProjectIfNameExists(nameOfTheProject) != null)
            //    {
            //        var bla = QRInitializing.GetProjectIfNameExists(nameOfTheProject);
            //        prob.ThereisAProblem($"There already exists a project of name {bla.Name}, at directory, {bla.DirectoryOfLibrary}. choose another name.");
            //    }



            if (projAlreadyExists == null)
                {

                    //just use the relative directory if in base directory
                    string basdir_ = QRModule.BaseAEDir.Replace("\\", "/");
                    string envIronDirectory_ = envIronDirectory.Replace("\\", "/");
                    //Console.WriteLine($"basdir_: {basdir_}");
                    //Console.WriteLine($"envIronDirectory_: {envIronDirectory_}");
                    //Console.WriteLine($"isSubDirOfPath(basdir_, envIronDirectory_): {isSubDirOfPath(basdir_, envIronDirectory_)}");
                    string DirOfProject = CodeGenerator.Program.isSubDirOfPath(basdir_, envIronDirectory_) ?
                        envIronDirectory_.Replace(basdir_ + "/", "") :
                        envIronDirectory_;



                    string PathToconfcs = Path.Combine(envIronDirectory, "conf");

                    Console.WriteLine($"creating {nameOfTheProject}.cs at directory  {DirOfProject}");
                    //create a .cs class file that will start the project type 
                    aEInitializing.WriteFileContents_FromCGENMMFile_ToFullPath(
                        "AERTOS\\AEProjectCS",
                        Path.Combine(PathToconfcs, $"{nameOfTheProject}.cs"),
                        false, false,
                         new MacroVar() { MacroName = "NameOfProject", VariableValue = nameOfTheProject },
                         new MacroVar() { MacroName = "DirOfProject", VariableValue = DirOfProject }
                         );


                }

                Console.WriteLine($"initing {nameOfTheProject}"); 
                //CodeGenerator.Program._aeinitProjectFileStructure(nameOfTheProject, aEInitializing, projAlreadyExists, envIronDirectory);
                Console.WriteLine($"done initing {nameOfTheProject}");
#endif



#if !TEST_AEGENERATE && !TEST_AESELECT && !TEST_AEINIT
            }
#endif


            //Console.WriteLine("Hello World!");
        }





        static string GetProjectsDisplay()
        {
            string disp = "";
            foreach (var proj in QRInitializing.GetAllCurrentAEProjects())
            {
                disp += "=============================================================="; disp += "\n";
                disp += $"ProjectName: {proj.Name}"; disp += "\n";
                disp += $"ProjectDirectory: {proj.DirectoryOfProject}"; disp += "\n";
                disp += $"ProjectTestsToChoose: "; disp += "\n";
                foreach (var test in proj.ListOfTargets_AllEXE)
                {
                    disp += $"  {test}"; disp += "\n";
                }
            }

            return disp;
        }

        static string GetProjectTestsDisplay(List<QRTarget_EXE> targetsToCheck)
        {

            string disp1 = "";
            //get list of all names of targets available for this project TestSelected
            foreach (var item in targetsToCheck)
            {
                string typ = targetsToCheck[0].qRTargetType == QRTargetType.cpp_exe ? "cpp" : "rqt";
                disp1 += $"{item.TargetName}: type: {typ}"; disp1 += "\n";
            }


            List<string> targetNames = targetsToCheck.Select(t => t.TargetName).ToList();
            string targetNamesDisplay = string.Join("\n", targetNames);

            string disp = "";
            disp += "=============================================================="; disp += "\n";
            //disp += $"ProjectName: {proj.Name}"; disp += "\n";
            //disp += $"ProjectDirectory: {proj.DirectoryOfProject}"; disp += "\n";
            //disp += $"ProjectTestsToChoose: "; disp += "\n";
            disp += disp1;// $"{targetNamesDisplay}"; disp += "\n"; 

            return disp;
        }
    }




}
