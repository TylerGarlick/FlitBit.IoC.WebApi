FlitBit.IoC.WebApi
================================
This package configures Microsoft's Web Api framework with FlitBit.IoC.


Instructions for Installation
--------------------------------------------------------------------------------
The Controller Factory will automatically register in the App_Start -> FlitBitConfig.cs

Installation for ContainerPerWebRequest
--------------------------------------------------------------------------------
Register the HttpModule, which enables the ContainerPerWebRequest in FlitBit.IoC.  

In the Web.Config register the following:
<system.web>
	<httpModules>
		<add name="ContainerPerWebRequestHttpModule" type="FlitBit.IoC.WebApi.HttpModules.ContainerPerWebRequestHttpModule, FlitBit.IoC.WebApi" />
	</httpModules>
</system.web>

<system.webServer>
	<modules runAllManagedModulesForAllRequests="true">
		<add name="ContainerPerWebRequestHttpModule" type="FlitBit.IoC.WebApi.HttpModules.ContainerPerWebRequestHttpModule, FlitBit.IoC.WebApi" />
	</modules>
</system.webServer>


--------------------------------------------------------------------------------
Release Notes
--------------------------------------------------------------------------------
See https://github.com/TylerGarlick/FlitBit.IoC.WebApi/blob/master/RELEASE-NOTES.md
