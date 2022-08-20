# DeathEye - Sample Version
 DeathEye is a Windows-focused Remote Administration tool. It includes a web command shell that takes effect by using [Powershell][powershell] and a simple discord logger to return output see explanation below. 
 
 ### ⚠️This is only a sample version
 Not all features and code is included here.
 


 [powershell]: https://github.com/PowerShell/PowerShell/issues
 
 
 
 ## Command Execution
For the command execution the process goes as follows:
1. we take advantage of the user channel bio inside youtube, this is where our script/command is stored.
2. we will use a headless browser and regex to extract this info.
3. we execute the script/command on a new PowerShell process and return the output to the discord webhook logger.

![logo][]

[logo]: https://i.imagesup.co/images2/e500fe8d2bed4ab4ee670ae6c75511ba1cc536b3.png
