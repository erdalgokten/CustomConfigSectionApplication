# Custom Config Section Application
* Section definition metadata must be the first thing in App.config. Otherwise error occurs.
* In section definition metadata type is important. It must be in the form of type="FullClassName,AssemblyName"
* FullClassName is namespace + classname
* AssemblyName is the name of the dll file (without .dll extension)
* System.Configuration must be referenced in the project.
