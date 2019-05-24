## Microsoft_System_Center_2012_Orchestrator_API
Project to make use of Microsoft System Center 2012 Orchestrator with an API implementation in C#. Notice, that Microsoft has better and more modern ways of doing this with Azure services.

Required to operate:
orchestratorApiAddress: https://servername:port/00000000-0000-0000-0000-000000000000
runbookId: xxxxxxxx-3xxxx-xxxx-xxxx-xxxxxxxxxxxx
RunbookUsername: samaccount
RunbookPassword: samaccount-password

### To reuse service in different Project do the following:
* Create project
* Install dependencies:
	* Microsoft.Data.Edm
	* System.Spatial
	* Microsoft.Data.OData 
	* Microsoft.Data.Services.Client
* Copy the Service References and it's contents to the root of your project
* In "Service References\OrchestratorApiService\Reference.cs update the namespace from "Runbook_template.*" to fit in three places:
```
namespace Runbook_template.OrchestratorApiService
```
```
global::System.Type resolvedType = this.DefaultResolveType(typeName, "Orchestrator.ResourceModel", "Runbook_template.OrchestratorApiService");
```
```
if (clientType.Namespace.Equals("Runbook_template.OrchestratorApiService", global::System.StringComparison.Ordinal))
```

* Update the datamap to fit the right server + orchestratorApi uuid

* In the *.csproj file add:
To Item group containing "Compile"
```
<Compile Include="Service References\OrchestratorApiService\Reference.cs">
    <AutoGen>True</AutoGen>
    <DesignTime>True</DesignTime>
	<DependentUpon>Reference.datasvcmap</DependentUpon>
</Compile>
```

To their own item groups:
```
<ItemGroup>
  <WCFMetadata Include="Service References\" />
</ItemGroup>
<ItemGroup>
  <ServiceReferenceMetadataStorage Include="Service References\OrchestratorApiService\">
    <Type>datasvcmap</Type>
  </ServiceReferenceMetadataStorage>
</ItemGroup>
<ItemGroup>
  <None Include="packages.config" />
  <None Include="Service References\OrchestratorApiService\service.edmx" />
</ItemGroup>
<ItemGroup>
  <None Include="Service References\OrchestratorApiService\Reference.datasvcmap">
    <Generator>DataServicesCoreClientGenerator</Generator>
	<LastGenOutput>Reference.cs</LastGenOutput>
  </None>
</ItemGroup>
```